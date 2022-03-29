using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGridAPI.Models
{
    public class MultiEmailTemplateModel
    {
        public List<string> EmailTo { get; set; }
        public string templateid { get; set; }
    }
}
