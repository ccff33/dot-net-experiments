using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Configuration;

using MongoDB.Driver;

using SC.Mongo.Entity;

using SC.Model;

namespace SC.Mongo
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SecretCommunicatorService : ISecretCommunicatorService
    {
        string connectionString = WebConfigurationManager.AppSettings.Get("MONGOHQ_URL");
        MongoCollection<ChannelDocument> Channels { get; set; }
        MongoCollection<SecretDocument> Secrets { get; set; }

        public SecretCommunicatorService()
        {
            var db = this.GetDb();
            this.Channels = db.GetCollection<ChannelDocument>(ChannelDocument.CollectionName);
            this.Secrets = db.GetCollection<SecretDocument>(SecretDocument.CollectionName);
        }

        public void RegisterChannel(Channel c)
        {
            ChannelDocument channel = new ChannelDocument();
            channel.Name = c.Name;
            channel.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            channel.Password = BCrypt.Net.BCrypt.HashPassword(c.Password, channel.Salt);

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

        public void CreateMessage(Channel channel, string text)
        {
            ChannelDocument c = this.Channels.FindOne((IMongoQuery)QueryDocument.Create(new {Name = channel.Name}));

            OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
            if (c != null)
            {
                if (c.Password == BCrypt.Net.BCrypt.HashPassword(channel.Password, c.Salt))
                {
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                }
            }

            response.StatusCode = System.Net.HttpStatusCode.Forbidden;
        }

        public IEnumerable<Secret> GetLastSecrets(string channelName, string password, int count)
        {
            return new List<Secret>();
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
