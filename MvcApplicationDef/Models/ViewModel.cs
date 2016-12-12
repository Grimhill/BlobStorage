using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MvcApplicationDef.Models
{
    public class ViewModel
    {
        public List<DAL.Data> Files { get; set; }
        public List<string> Extensions { get; set; }        
    }
}