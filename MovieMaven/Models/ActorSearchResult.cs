using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMaven.Models
{
    public class ActorSearchResult
    {
        public double popularity { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string profile_path { get; set; }
        public bool adult { get; set; }
        public List<object> known_for { get; set; }
        public int gender { get; set; }
    }
}
