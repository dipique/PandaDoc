using System;
using System.Configuration;

namespace PandaDoc
{
    public class PandaDocApiSettings
    {
        public PandaDocApiSettings(string clientId = null, string clientSecret = null, Uri apiUrl = null, Uri authUri = null)
        {
            AuthUri = authUri ?? new Uri("https://app.pandadoc.com");
            ApiUri = apiUrl ?? new Uri("https://api.pandadoc.com");

            ClientId = clientId ?? ConfigurationManager.AppSettings["pandadoc:clientid"];
            ClientSecret = clientSecret ?? ConfigurationManager.AppSettings["pandadoc:clientsecret"];
        }

        public string ClientSecret { get; set; }
        public string ClientId { get; set; }
        public Uri ApiUri { get; set; }
        public Uri AuthUri { get; set; }
    }
}