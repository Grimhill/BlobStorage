using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    //Клас интересующих параметров объекта в контейнере
    public class Data 
    {
        //public Data()
       // { }
        public string UserName { get; set; }
        public long Size { get; set; } 
        public DateTime Time {get; set;}        
        public string Url { get; set; }
        public string FileName { get; set; }
    }
}
