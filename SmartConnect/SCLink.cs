using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartConnect
{
    public class SCLink
    {
        private String text, link;
        public String Text
        {
            get { return text; }
        }
        public String Link
        {
            get { return link; }
        }

        public SCLink(String text, String link)
        {
            this.text = text;
            this.link = link;
        }
    }
}
