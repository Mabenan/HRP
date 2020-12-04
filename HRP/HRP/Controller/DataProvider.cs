using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HRP.Controller
{
    static class DataProvider
    {
        private static ODataClient _client;

        public static ODataClient Client
        {
            get
            {
                return _client;
            }
        }

        static DataProvider()
        {
        }

        internal static void Init(string server, ICredentials credentials)
        {
            _client = new ODataClient(new ODataClientSettings()
            {
                BaseUri = new Uri(server),
                Credentials = credentials
            });
        }
    }
}
