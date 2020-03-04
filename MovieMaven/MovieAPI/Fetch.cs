using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MovieMaven.Models;

namespace MovieMaven.MovieAPI
{
    public static class Fetch
    {
        public static HttpClient client = new HttpClient();
        public static string api_key = "5f2b0620639a798e7745ed2b7fc3fb85";

        public static string InTheater1 { get; set; }
        public static string InTheater2 { get; set; }
        public static string InTheater3 { get; set; }
        public static string Data { get; set; }
        public static string Search { get; set; }
        public static string Videos { get; set; }
        public static string Trailer { get; set; }
        public static string Details { get; set; }
        public static string Credits { get; set; }
        public static string Actors { get; set; }


        public static async Task GrabInTheatres1(string date, string finalDate)
        {
            ClearYourHead();

            HttpResponseMessage inTheatreData1 =
                await client.GetAsync("https://api.themoviedb.org/3/discover/movie?primary_release_date.gte=" + date + "&primary_release_date.lte="+ finalDate + 
                "&api_key=" + api_key + "&page=1");



            if (inTheatreData1.IsSuccessStatusCode)
            {
                InTheater1 = await inTheatreData1.Content.ReadAsStringAsync();

                Program.inTheatres = JsonConvert.DeserializeObject<InTheatres>(InTheater1);
            }
            else
            {
                Data = null;
            }
        } // GrabInTheatres1()
        public static async Task GrabInTheatres2(string date2, string finalDate)
        {
            ClearYourHead();

            HttpResponseMessage inTheatreData2 =
                await client.GetAsync("https://api.themoviedb.org/3/discover/movie?primary_release_date.gte=" + date2 + "&primary_release_date.lte=" + finalDate +
                "&api_key=" + api_key + "&page=2");



            if (inTheatreData2.IsSuccessStatusCode)
            {

                InTheater2 = await inTheatreData2.Content.ReadAsStringAsync();

                Program.inTheatres = JsonConvert.DeserializeObject<InTheatres>(InTheater2);

            }
            else
            {
                Data = null;
            }
        } // GrabInTheatres2()

        public static async Task GrabInTheatres3(string date3, string finalDate)
        {
            ClearYourHead();

            HttpResponseMessage inTheatreData3 =
                await client.GetAsync("https://api.themoviedb.org/3/discover/movie?primary_release_date.gte=" + date3 + "&primary_release_date.lte=" + finalDate +
                "&api_key=" + api_key + "&page=3");



            if (inTheatreData3.IsSuccessStatusCode)
            {
                InTheater3 = await inTheatreData3.Content.ReadAsStringAsync();

                Program.inTheatres = JsonConvert.DeserializeObject<InTheatres>(InTheater3);
            }
            else
            {
                Data = null;
            }
        } // GrabInTheatres()

        public static async Task GrabPosterAsync(string search)
        {
            ClearYourHead();

            //===========================>>
            // Grabs 20 posters
            HttpResponseMessage posterData =
                await client.GetAsync(
                    "https://api.themoviedb.org/3/search/movie?api_key=" +
                    api_key + "&query=" + search);

            if (posterData.IsSuccessStatusCode)
            {
                Data = await posterData.Content.ReadAsStringAsync();
                Program.posterSet = JsonConvert.DeserializeObject<PosterSet>(Data);
            }
            else
            {
                Data = null;
            }
        } // GrabPosterAsync()

        public static async Task GrabActorAsync(string search)
        {
            ClearYourHead();

            //===========================>>
            // Grabs 20 posters
            HttpResponseMessage actorSearchData =
                await client.GetAsync(
                    "https://api.themoviedb.org/3/search/person?api_key=" + api_key + "&query=" + search);

            if (actorSearchData.IsSuccessStatusCode)
            {
                Data = await actorSearchData.Content.ReadAsStringAsync();
                Program.actorSearchSet = JsonConvert.DeserializeObject<ActorSearchSet>(Data);
            }
            else
            {
                Data = null;
            }
        } // GrabPosterAsync()

        public static async Task GetMovieDetails(string movieID)
        {
            ClearYourHead();

            //===========================>>
            // Grabs Video details
            HttpResponseMessage videoDetails =
                await client.GetAsync(
                    "https://api.themoviedb.org/3/movie/" + movieID
                                      + "/videos?api_key=" + api_key + "&language=en-US");

            HttpResponseMessage inTheatreTrailer =
            await client.GetAsync(
        "https://api.themoviedb.org/3/movie/" + movieID
                          + "/videos?api_key=" + api_key + "&language=en-US");


            HttpResponseMessage movieDetails =
            await client.GetAsync(
                "https://api.themoviedb.org/3/movie/" + movieID
                    + "?api_key=" + api_key + "&language=en-US");

            HttpResponseMessage castDetails =
            await client.GetAsync(
                "https://api.themoviedb.org/3/movie/" + movieID +
                    "/credits?api_key=" + api_key);



            if (videoDetails.IsSuccessStatusCode ||
                movieDetails.IsSuccessStatusCode || 
                castDetails.IsSuccessStatusCode || 
                inTheatreTrailer.IsSuccessStatusCode)
            {
                Videos = await videoDetails.Content.ReadAsStringAsync(); // 1
                Details = await movieDetails.Content.ReadAsStringAsync();
                Credits = await castDetails.Content.ReadAsStringAsync();
                Trailer = await inTheatreTrailer.Content.ReadAsStringAsync();

                
                Program.videoSet = JsonConvert.DeserializeObject<VideoSet>(Videos); // 1
                Program.movie = JsonConvert.DeserializeObject<Movie>(Details);
                Program.credits = JsonConvert.DeserializeObject<Credits>(Credits);
                Program.trailerSet = JsonConvert.DeserializeObject<TrailerSet>(Trailer);
                
                
            }
            else
            {
                Data = null;
            }
        } // GetMovieDetails()

        public static async Task GetActorDetails(string actorID)
        {
            ClearYourHead();

            //===========================>>
            // Grab Actor Details
            HttpResponseMessage actorData =
                await client.GetAsync("https://api.themoviedb.org/3/person/" + actorID +
                    "?api_key=" + api_key + "&language=en-US");

            if (actorData.IsSuccessStatusCode)
            {
                Data = await actorData.Content.ReadAsStringAsync();
                Program.actor = JsonConvert.DeserializeObject<Actor>(Data);
            }
            else
            {
                Data = null;
            }
        } // GetActorDetails()

        public static async Task GetActorImages(string actorID)
        {
            ClearYourHead();

            HttpResponseMessage actorPic =
                await client.GetAsync("https://api.themoviedb.org/3/person/" + actorID + "/images?api_key=" + api_key + "&language=en-US");

            if (actorPic.IsSuccessStatusCode)
            {
                Data = await actorPic.Content.ReadAsStringAsync();
                Program.profileSet = JsonConvert.DeserializeObject<ProfileSet>(Data);
            }
            else
            {
                Data = null;
            }
        }

        public static async Task GetRelatedMovies(string actorID)
        {
            ClearYourHead();

            //===========================>>
            // Grab Related Movies
            HttpResponseMessage starringMovie =
                await client.GetAsync("https://api.themoviedb.org/3/discover/movie?with_cast=" + actorID +
                "&sort_by=revenue.desc&api_key=" + api_key + "&language=en-US");

            if (starringMovie.IsSuccessStatusCode)
            {
                Data = await starringMovie.Content.ReadAsStringAsync();
                Program.starringInSet = JsonConvert.DeserializeObject<StarringInSet>(Data);
            }
            else
            {
                Data = null;
            }
        }

        private static void ClearYourHead()
        {
            // clear so we don't have to do it again
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("applicationException/json"));

        } // ClearYourHead()


    } // class
} // namespace