using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SC
{
    [ServiceContract]
    public interface ISecretCommunicatorService
    {
        [WebInvoke(Method="POST", UriTemplate="register?name={channelName}&pwd={password}")]
        void RegisterChannel(string channelName, string password);
    }
}
