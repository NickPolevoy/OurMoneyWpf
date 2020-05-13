using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMoneyWpf.Common
{
    public class MessageArg
    {
        public MessageSenders Sender;
        public object Message { get; set; }
    }
}
