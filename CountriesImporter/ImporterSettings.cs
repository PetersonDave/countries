using System.Web;

namespace CountriesImporter
{
	public class ImporterSettings
	{
		public HttpContext Context { get; set; }
		public string CountriesJsonPath { get; set; }
		public string DatabaseName { get; set; }
		public string CountriesParentItemPath { get; set; }
		public string CountryTemplatePath { get; set; }
	}
}