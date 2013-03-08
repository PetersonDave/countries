using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SecurityModel;

namespace CountriesImporter
{
	public class Importer
    {
		private ImporterSettings _importerSettings;
		private Database _db;

		public Importer(ImporterSettings importerSettings)
		{
			Assert.IsNotNull(importerSettings, "importerSettings cannot be null");
			Assert.IsNotNull(importerSettings.Context, "context cannot be null");
			Assert.IsNotNullOrEmpty(importerSettings.CountriesJsonPath, "countriesJsonPath cannot be null or empty");
			Assert.IsNotNullOrEmpty(importerSettings.DatabaseName, "databaseName cannot be null or empty");
			Assert.IsNotNullOrEmpty(importerSettings.CountriesParentItemPath, "countriesParentItem cannot be null or empty");
			Assert.IsNotNullOrEmpty(importerSettings.CountryTemplatePath, "countryTemplatePath cannot be null or empty");

			_importerSettings = importerSettings;
		}

		public void Import()
		{
			var countries = GetCountriesFromJson();

			bool isCountriesValid = countries != null && countries.Count > 0;
			if (isCountriesValid)
			{
				using (new SecurityDisabler())
				{
					_db = Sitecore.Data.Database.GetDatabase(_importerSettings.DatabaseName);
					if (_db != null)
					{
						ProcessCountries(countries);
					}
				}
			}
		}

		private void ProcessCountries(List<Country> countries)
		{
			var parent = _db.Items[_importerSettings.CountriesParentItemPath];
			var template = _db.Templates[_importerSettings.CountryTemplatePath];

			bool isItemAndTemplateValid = parent != null && template != null;
			if (isItemAndTemplateValid)
			{
				foreach (var country in countries)
				{
					TransferCountryToSitecoreItem(country, parent, template);
				}
			}
		}

		private void TransferCountryToSitecoreItem(Country country, Item parent, TemplateItem template)
		{
			var countryName = RemoveSpecialCharacters(country.Name);
			var countryItem = _db.Items[string.Concat(parent.Paths.ContentPath, "/", countryName)] ?? parent.Add(countryName, template);
			if (countryItem != null)
			{
				using (new EditContext(countryItem))
				{
					countryItem.Appearance.DisplayName = countryName.Replace("-", " ");

					Type TCountry = country.GetType();
					foreach (PropertyInfo pi in TCountry.GetProperties())
					{
						bool isFieldValid = countryItem.Fields[pi.Name] != null && pi.GetValue(country, null) != null;
						if (isFieldValid)
						{
							countryItem.Fields[pi.Name].Value = pi.GetValue(country, null).ToString();
						}
					}
				}
			}
		}

		private string RemoveSpecialCharacters(string str)
		{
			str = str.Replace(" ", "-");
			return Regex.Replace(str, "[^a-zA-Z0-9_-]+", "", RegexOptions.Compiled);
		}

		private List<Country> GetCountriesFromJson()
		{
			var countries = new List<Country>();
			var isoPath = _importerSettings.Context.Server.MapPath(_importerSettings.CountriesJsonPath);
			using (var stream = new StreamReader(isoPath))
			{
				var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
				var json = stream.ReadToEnd();

				bool isJsonValid = !string.IsNullOrEmpty(json);
				if (isJsonValid)
				{
					countries = serializer.Deserialize<List<Country>>(json);
				}
			}

			return countries;
		}
    }
}
