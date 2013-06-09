using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;

namespace SC.Mongo.Entity
{
    public class SecretDocument
    {
        public static string CollectionName = "Secrets";
        public ObjectId Id { get; set; }
        public string TextEncrypted { get; set; }
    }
}