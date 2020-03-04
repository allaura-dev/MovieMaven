using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMaven.Models
{
    public class TrailerSet
    {
        public int id { get; set; }
        public List<Trailer> results { get; set; }

    }
}
