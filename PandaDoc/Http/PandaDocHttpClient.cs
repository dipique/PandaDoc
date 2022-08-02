using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using PandaDoc.Models.CreateDocument;
using PandaDoc.Models.GetDocument;
using PandaDoc.Models.GetDocuments;
using PandaDoc.Models.SendDocument;

namespace PandaDoc
{
    public class PandaDocHttpClient : IDisposable
    {
        private PandaDocHttpClientSettings settings;
        private HttpClient httpClient;
        private JsonMediaTypeFormatter jsonFormatter;
        private PandaDocBearerToken bearerToken;
        private string apiKey;

        public PandaDocHttpClient()
            : this(new PandaDocHttpClientSettings())
        {
        }

        public PandaDocHttpClient(PandaDocHttpClientSettings settings = null)
        {
            Settings = settings;
            HttpClient = new HttpClient();
            JsonFormatter = new JsonMediaTypeFormatter();
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public PandaDocHttpClientSettings Settings
        {
            get => settings;
            set => settings = value ?? new PandaDocHttpClientSettings();
        }

        public HttpClient HttpClient
        {
            get { return httpClient; }
            set
            {
                httpClient = value ?? throw new ArgumentNullException("value");
            }
        }

        public JsonMediaTypeFormatter JsonFormatter
        {
            get { return jsonFormatter; }
            set
            {
                jsonFormatter = value ?? throw new ArgumentNullException("value");
            }
        }

        public string ApiKey
        {
            get { return apiKey; }
            set
            {
                apiKey = value ?? throw new ArgumentNullException("value");

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "API-Key " + value);
            }
        }

        public PandaDocBearerToken BearerToken
        {
            get { return bearerToken; }
            set
            {
                bearerToken = value ?? throw new ArgumentNullException("value");

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken.AccessToken);
            }
        }

        public void SetBearerToken(PandaDocBearerToken value) => BearerToken = value;
        public void SetApiKey(string apiKey) => ApiKey = apiKey;

        public async Task<PandaDocHttpResponse<PandaDocBearerToken>> Login(string username, string password)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (password == null) throw new ArgumentNullException("password");

            var values = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password},
                {"client_id", settings.ClientId},
                {"client_secret", settings.ClientSecret},
                {"scope", "read write read+write"}
            };

            var content = new FormUrlEncodedContent(values);
            var httpResponse = await httpClient.PostAsync(settings.AuthUri + "oauth2/access_token", content);
            var response = await httpResponse.ToPandaDocResponseAsync<PandaDocBearerToken>();
            return response;
        }

        public async Task<PandaDocHttpResponse<GetDocumentsResponse>> GetDocuments()
        {
            var httpResponse = await httpClient.GetAsync(settings.ApiUri + "public/v1/documents");
            var response = await httpResponse.ToPandaDocResponseAsync<GetDocumentsResponse>();
            return response;
        }

        public async Task<PandaDocHttpResponse<CreateDocumentResponse>> CreateDocument(CreateDocumentRequest request)
        {
            var httpContent = new ObjectContent<CreateDocumentRequest>(request, JsonFormatter);
            var httpResponse = await httpClient.PostAsync(settings.ApiUri + "public/v1/documents", httpContent);
            var response = await httpResponse.ToPandaDocResponseAsync<CreateDocumentResponse>();
            return response;
        }

        public async Task<PandaDocHttpResponse<GetDocumentResponse>> GetDocument(string uuid)
        {
            var httpResponse = await httpClient.GetAsync(settings.ApiUri + "public/v1/documents/" + uuid);

            var response = await httpResponse.ToPandaDocResponseAsync<GetDocumentResponse>();

            return response;
        }

        public async Task<PandaDocHttpResponse<SendDocumentResponse>> SendDocument(string uuid, SendDocumentRequest request)
        {
            var httpContent = new ObjectContent<SendDocumentRequest>(request, JsonFormatter);
            var httpResponse = await httpClient.PostAsync(settings.ApiUri + "public/v1/documents/" + uuid + "/send", httpContent);
            var response = await httpResponse.ToPandaDocResponseAsync<SendDocumentResponse>();
            return response;
        }

        public async Task<PandaDocHttpResponse> DeleteDocument(string uuid)
        {
            var httpResponse = await httpClient.DeleteAsync(settings.ApiUri + "public/v1/documents/" + uuid);
            var response = await httpResponse.ToPandaDocResponseAsync();
            return response;
        }
    }
}