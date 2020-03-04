using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieMaven.Models;

namespace MovieMaven
{
    public class Program
    {
        public static PosterSet posterSet;
        public static Poster poster;
        public static VideoSet videoSet;
        public static Movie movie;
        public static Credits credits;
        public static Actor actor;
        public static InTheatres inTheatres;
        public static Showing showing;
        public static TrailerSet trailerSet;
        public static StarringInSet starringInSet;
        public static StarringIn starringIn;
        public static ProfileSet profileSet;
        public static ActorSearchSet actorSearchSet;


        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    } // class
} // namespace
