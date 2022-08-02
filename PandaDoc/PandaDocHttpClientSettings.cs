using System;
using System.Configuration;

namespace PandaDoc
{
    public class PandaDocHttpClientSettings
    {
        private string clientId;
        private string clientSecret;
        private Uri apiUri;
        private Uri authUri;

        public PandaDocHttpClientSettings(string clientId = null, string clientSecret = null, Uri apiUrl = null, Uri authUri = null)
        {
            AuthUri = authUri ?? new Uri("https://app.pandadoc.com");
            ApiUri = apiUrl ?? new Uri("https://api.pandadoc.com");

            ClientId = clientId ?? ConfigurationManager.AppSettings["pandadoc:clientid"];
            ClientSecret = clientSecret ?? ConfigurationManager.AppSettings["pandadoc:clientsecret"];
        }

        public string ClientId
        {
            get { return clientId; }
            private set
            {
                clientId = value ?? string.Empty;
            }
        }

        public string ClientSecret
        {
            get { return clientSecret; }
            private set
            {
                clientSecret = value ?? string.Empty;
            }
        }

        public Uri ApiUri
        {
            get { return apiUri; }
            private set
            {
                apiUri = value ?? new Uri("https://api.pandadoc.com");
            }
        }

        public Uri AuthUri
        {
            get { return authUri; }
            private set
            {
                authUri = value ?? new Uri("https://api.pandadoc.com");
            }
        }
    }
}