﻿using System;
using System.Linq;
using Newtonsoft.Json;

namespace PandaDoc.Models.GetDocument
{
    public enum DocumentStatus
    {
        Uploaded,
        Draft,
        Sent,
        Completed,
        Viewed,
    }

    public static class DocumentStatusExtensions
    {
        public static DocumentStatus AsDocumentStatus(this string Status) => Enum.TryParse<DocumentStatus>(
            Status.Split('.')
                  .Select(s => $"{s[0]}".ToUpper() + s.Substring(1))
                  .Last()
            , out var status
        ) ? status : throw new ArgumentOutOfRangeException($"Status '{Status}' not recognized as a valid DocumentStatus");
    }

    public class GetDocumentResponse
    {
        public GetDocumentResponse()
        {
            Recipients = new Recipient[] { };
        }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("recipients")]
        public Recipient[] Recipients { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public DateTime DateModified { get; set; }

        public DocumentStatus DocumentStatus => Status.AsDocumentStatus();
    }

    public class Recipient
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("recipient_type")]
        public string RecipientType { get; set; }

        [JsonProperty("has_completed")]
        public bool HasCompleted { get; set; }
    }
}