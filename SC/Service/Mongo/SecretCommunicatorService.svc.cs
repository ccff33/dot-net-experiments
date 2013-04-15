using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using System.Configuration;

using MongoDB.Driver;

using SC.Mongo.Entity;

namespace SC.Mongo
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SecretCommunicatorService : ISecretCommunicatorService
    {
        string connectionString = ConfigurationManager.AppSettings.Get("MONGOHQ_URL");
        MongoCollection<ChannelDocument> Channels { get; set; }

        public SecretCommunicatorService()
        {
            this.Channels = this.GetDb().GetCollection<ChannelDocument>(ChannelDocument.CollectionName);
        }

        public void RegisterChannel(string channelName, string password)
        {
            ChannelDocument channel = new ChannelDocument();
            channel.Name = channelName;
            channel.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            channel.Password = BCrypt.Net.BCrypt.HashPassword(password, channel.Salt);

            OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;

            try
            {
                this.Channels.Insert(channel);
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (WriteConcernException e)
            {
                response.StatusCode = System.Net.HttpStatusCode.Forbidden;
            }
        }

        protected MongoDatabase GetDb()
        {
            MongoUrl url = new MongoUrl(this.connectionString);
            MongoClient client = new MongoClient(url);
            MongoServer server = client.GetServer();
            return server.GetDatabase(url.DatabaseName);
        }
    }
}
