using System.Web;

namespace CountriesImporter.Example
{
    public class ExampleImport
    {
        /// <summary>
        /// Serves as an example of how to import Countries list
        /// </summary>
        public static void ImportFromCountriesCsv()
        {
            var settings = new ImporterSettings()
            {
                Context = HttpContext.Current,
                CountriesJsonPath = "/countries/countries.json",            // path relative to your web project where JSON extract lives
                CountriesParentItemPath = "/sitecore/content/Repository/countries", // parent item to create Country child items
                CountryTemplatePath = "User Defined/Country",               // country template to use (assumes "/sitecore/templates" as start path)
                DatabaseName = "master"                         // database context
            };

            var importer = new Importer(settings);
            importer.Import();
        }
    }
}
