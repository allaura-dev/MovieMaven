using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMaven.Models
{
    public class Video
    {
        public string id { get; set; }
        public string iso_639_1 { get; set; } // probably used for mapping in the background
        public string iso_3166_1 { get; set; } // probably used for mapping in the background
        public string key { get; set; }
        public string name { get; set; }
        public string site { get; set; }
        public string size { get; set; }
        public string type { get; set; }

    } // class 
} // namespace
