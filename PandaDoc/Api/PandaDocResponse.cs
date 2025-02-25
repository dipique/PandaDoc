﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PandaDoc
{
    public class PandaDocResponse
    {
        public bool IsSuccessStatusCode { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public HttpResponseMessage HttpResponse { get; set; }
        public HttpResponseHeaders Headers { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public string Content { get; set; }
    }

    public class PandaDocHttpResponse<T> : PandaDocResponse
    {
        public T Value { get; set; }
    }
}