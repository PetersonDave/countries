# Sitecore Countries Importer

Generates country Sitecore items using country-specific data such as ISO, currency, calling code and region.

Install items and template directly, or install the template and generate the items youreslf. See below for details:

#1. Installing Template & Items via package

The package **packages/Countries-Template-and-Repository-Items-1** contains country items and their related template generated by this tool using a snapshot of the countries from the cloned repository. While this is the simplest method to install, keep in mind this is a snapshot in time and may be out of date.

#2. Install Template Only

Install the package **packages/Countries-Template-Only-1** to bring over the template only, allowing to generate the items yourself. To generate the country items, execute the code below against the JSON countries list:


    var settings = new CountriesImporter.ImporterSettings()
	{
		Context = HttpContext.Current,
		CountriesJsonPath = "/countries/countries.json",			// path relative to your web project where JSON extract lives
		CountriesParentItemPath = "/sitecore/content/Repository/countries",	// parent item to create Country child items
		CountryTemplatePath = "User Defined/Country",				// country template to use (assumes "/sitecore/templates" as start path)
		DatabaseName = "master"							// database context
	};

    var importer = new CountriesImporter.Importer(settings);
    importer.Import();


# Countries data
This repository contains lists of world countries in JSON, CSV and XML. Each line contains the country:

 - name
 - top-level domain (tld)
 - code ISO 3166-1 alpha-2 (cca2)
 - code ISO 3166-1 numeric (ccn3)
 - code ISO 3166-1 alpha-3 (cca3)
 - currency code(s) (comma separated if several)
 - calling code(s) (comma separated if several)
 - alternative spellings (comma separated if several)
 - relevance
 - region
 - subregion (comma separated if several)

I use http://www.shancarter.com/data_converter/index.html to generate the JSON and XML; the CSV was done by hand.

I will add the following data:
 - the country capital city
 - the country official language(s)

# Sources
 - Wikipedia for country name, TLD, ISO codes and alternative spellings
 - http://www.currency-iso.org/ for currency codes
 - Alternative spellings and relevance are inspired by https://github.com/JamieAppleseed/selectToAutocomplete
 - Region and subregion are taken from https://github.com/hexorx/countries.

# Credits
Thanks to @Glazz for his help with country calling codes.

Thanks to @hexorx for his work.
