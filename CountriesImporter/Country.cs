using System;
using System.Runtime.Serialization;

namespace CountriesImporter
{
	[Serializable,
	 DataContract]
	public class Country
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "tld")]
		public string Tld { get; set; }

		[DataMember(Name = "cca2")]
		public string Cca2 { get; set; }

		[DataMember(Name = "ccn3")]
		public string Ccn3 { get; set; }

		[DataMember(Name = "cca3")]
		public string Cca3 { get; set; }

		[DataMember(Name = "currency")]
		public string Currency { get; set; }

		[DataMember(Name = "calling-code")]
		public string CallingCode { get; set; }

		[DataMember(Name = "alt-spellings")]
		public string AltSpellings { get; set; }

		[DataMember(Name = "relevance")]
		public string Relevance { get; set; }

		[DataMember(Name = "region")]
		public string Region { get; set; }

		[DataMember(Name = "subregion")]
		public string Subregion { get; set; }
	}
}