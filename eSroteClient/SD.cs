using System;
namespace eSroteClient
{
	public static class SD
	{
        public static string eStoreAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}

