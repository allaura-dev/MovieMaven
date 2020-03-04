using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMaven.Models
{
    public class VideoSet
    {
        public int id { get; set; }
        public List<Video> results { get; set; }
    } // class
} // namespace
