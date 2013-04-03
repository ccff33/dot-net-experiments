using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.ServiceModel.Web;

using MongoDB.Driver;

using NUnit.Framework;

using SC.Mongo;
using SC.Mongo.Entity;

namespace SC.Test
{
    [TestFixture]
    class SecretCommunicatorServiceMongoImplTest
    {
        MongoCollection<ChannelDocument> channels;

        [SetUp]
        public void SetUp()
        {
            //this.channels = new MongoClient(this.connectionString)
            //                        .GetServer().GetDatabase(this.connectionString.Split('/').Last())
            //                        .GetCollection<ChannelDocument>(ChannelDocument.CollectionName);
            //this.channels.RemoveAll();
        }

        [TestCase]
        public void RegisterChannelSuccessfullyTest()
        {
        }
    }
}
