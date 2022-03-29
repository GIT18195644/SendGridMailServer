using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridAPI.Models
{
    public class EmailModel
    {
        public string EmailTo { get; set; }
        public string EmailSubject { get; set; }
        public string EmailPlainText { get; set; }
        public string EmailHtmlText { get; set; }

    }
}
