using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SC.Mongo.Entity
{
    //has unique index on Name in db
    public class ChannelDocument
    {
        public static string CollectionName = "Channels";
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}