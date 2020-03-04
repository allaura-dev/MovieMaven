using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMaven.Models
{
    public class Profile
    {
        public object iso_639_1 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int vote_count { get; set; }
        public double vote_average { get; set; }
        public string file_path { get; set; }
        public double aspect_ratio { get; set; }
    }
}
