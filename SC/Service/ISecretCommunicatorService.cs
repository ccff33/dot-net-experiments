using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using SC.Model;

namespace SC
{
    [ServiceContract]
    public interface ISecretCommunicatorService
    {
        [WebInvoke(Method="POST",
                   UriTemplate="register",
                   RequestFormat=WebMessageFormat.Json)]
        void RegisterChannel(Channel channel);

        [WebInvoke(Method="POST",
                   UriTemplate="message/create",
                   RequestFormat=WebMessageFormat.Json,
                   BodyStyle=WebMessageBodyStyle.Wrapped)]
        void CreateMessage(Channel channel, string text);

        [WebInvoke(Method="GET",
                   UriTemplate="secrets/{channelName}/{password}?c={count}",
                   ResponseFormat=WebMessageFormat.Json)]
        IEnumerable<Secret> GetLastSecrets(string channelName, string password, int count);
    }
}
