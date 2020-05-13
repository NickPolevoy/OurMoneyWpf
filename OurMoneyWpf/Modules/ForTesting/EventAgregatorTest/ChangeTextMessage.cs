using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMoneyWpf.Modules.ForTesting.EventAgregatorTest
{
    public class ChangeTextMessage
    {
        public string Text
        {
            get;
            private set;
        }

        public ChangeTextMessage(string text)
        {
            Text = text;
        }
    }
}
