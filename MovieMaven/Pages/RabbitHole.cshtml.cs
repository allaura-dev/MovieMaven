using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieMaven.MovieAPI;
using MovieMaven.Models;
using System.Threading;

namespace MovieMaven.Pages
{
    public class RabbitHoleModel : PageModel
    {
        //search
        public string searchTerm = Temp.searchTerm;

        // actor data
        public List<string> picsOfActors = new List<string>();
        public List<int> actorIDs = new List<int>();
        public List<string> names = new List<string>();


        // poster data
        public List<string> posterURLs = new List<string>();
        public List<string> overviews = new List<string>();
        public List<string> movieIDs = new List<string>();
        public List<string> titles = new List<string>();

        // video data
        public List<string> videoNames = new List<string>();
        public List<string> videoKeys = new List<string>();
        public int MAX_VIDS = 2; // limits the vids that show

        // movie details
        public string movieOverview;
        public int movieRuntime;
        public string movieTagline;
        public int movieRevenue;
        public string movieReleaseDate;

        // cast details
        public List<string> castPics = new List<string>();
        public List<string> castNames = new List<string>();
        public List<string> characterNames = new List<string>();

        // actor details
        public string name;
        public string actorPic;
        public string biography;
        public string birthday;
        public object deathday;
        public int gender;
        public int age;
        public string placeOfBirth;
        public string knownFor;
        public List<string> pictureURLs = new List<string>();
        public List<string> starringInURLs = new List<string>();
        public string imdbID;
        public string nobody = "No information available at this time.";


        public async Task OnPostGetPosters(string search)
        {
            if (Temp.searchTerm != null && search == null)
                search = Temp.searchTerm;
            await Fetch.GrabPosterAsync(search); // this is in the Fetch.cs and is a method called from there
            foreach (Poster poster in Program.posterSet.results)
            {
                posterURLs.Add(poster.poster_path); // the second part of the Add method is in the models 
                overviews.Add(poster.overview);
                movieIDs.Add(poster.id.ToString());
                titles.Add(poster.title);
            }
            Temp.searchTerm = search;

            await Fetch.GrabActorAsync(search); // this is in the Fetch.cs and is a method called from there

            foreach (ActorSearchResult actorSearchResult in Program.actorSearchSet.results)
            {
                picsOfActors.Add(actorSearchResult.profile_path); // the second part of the Add method is in the models 
                actorIDs.Add(actorSearchResult.id);
                names.Add(actorSearchResult.name);

            }
            Temp.searchTerm = search;
        } // OnPostGetPosters()

        public async Task OnPostDetails(string movieID)
        {
            await Fetch.GetMovieDetails(movieID);


            if (movieIDs.Count > 0 && posterURLs.Count >= 0)
            {
                foreach (Video video in Program.videoSet.results)
                {
                    videoNames.Add(video.name);
                    videoKeys.Add(video.key);
                }
            }

            movieOverview = Program.movie.overview;
            movieRuntime = Program.movie.runtime;
            movieTagline = Program.movie.tagline;
            movieReleaseDate = Program.movie.release_date;

            for (int i = 0; i < Program.credits.cast.Count; i++)
            {
                castPics.Add(Program.credits.cast[i].profile_path);
                actorIDs.Add(Program.credits.cast[i].id);
                castNames.Add(Program.credits.cast[i].name);
                characterNames.Add(Program.credits.cast[i].character);
            }
        } // OnPostDetails()

        public void OnPostRabbitHole(string movieID)
        {
            Response.Redirect("./RabbitHole?id=" + movieID);
        } // RabbitHole(); goes down the rabit hold of continuosly being able to click on posters from actor

        public async Task OnPostActorInfo(string actorID)
        {

            await Fetch.GetActorDetails(actorID);

            await Fetch.GetActorImages(actorID);

            for (int i = 0; i < Program.profileSet.profiles.Count; i++)
            {
                pictureURLs.Add(Program.profileSet.profiles[i].file_path);
            }

            await Fetch.GetRelatedMovies(actorID);

            for (int i = 0; i < Program.starringInSet.results.Count; i++)
            {
                starringInURLs.Add(Program.starringInSet.results[i].poster_path);
                movieIDs.Add(Program.starringInSet.results[i].poster_path);
                titles.Add(Program.starringInSet.results[i].title);
            }


            actorPic = Program.actor.profile_path;
            name = Program.actor.name;
            if (Program.actor.biography != null)
            {
                biography = Program.actor.biography;
            }
            else
            {
                biography = nobody;
            }

            deathday = Program.actor.deathday;
            gender = Program.actor.gender; // female = 1 male = 2
            placeOfBirth = Program.actor.place_of_birth;
            knownFor = Program.actor.known_for_department;
            if (Program.actor.birthday != null)
            {
                age = DateTime.Now.Year - Convert.ToInt32(Program.actor.birthday.Substring(0, 4)); // calculate age
                birthday = Program.actor.birthday;
            }
            else
            {
                age = 000;
                birthday = "Unknown";
            }

            imdbID = Program.actor.imdb_id;
        } // Get ActorInfo
    } // class
}
