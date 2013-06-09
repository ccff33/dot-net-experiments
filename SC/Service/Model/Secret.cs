using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace SC.Model
{
    [DataContract]
    public class Secret
    {
        [DataMember]
        public string Text { get; set; }
    }
}