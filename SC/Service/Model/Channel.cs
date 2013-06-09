using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace SC.Model
{
    [DataContract]
    public class Channel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}