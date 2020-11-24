using Simple.OData.Client;
using System;
using System.Collections.Generic;
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
            _client = new ODataClient("http://localhost:5000/");
        }
    }
}
