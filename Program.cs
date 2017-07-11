using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ConsoleAppMovieScrapper
{
    public class Movie
    {
        public string Title { get; set; }

    }
    public class MoviesCollection
    {
        public Movie [] Results  { get; set; }

    }

    class Program
    {
        const string url = "https://api.themoviedb.org/3/movie/";
        const string key = "d8693200a2452f1cd60ee9d7760a4895";
        static HttpClient client = new HttpClient();

        static void ShowMovie(Movie movie)
        {
            Console.WriteLine($"Name: {movie.Title}");
        }

        static void ShowMoviesCollection(MoviesCollection movies)
        {
            foreach (var movie in movies.Results)
            {
                Console.WriteLine($"Name: {movie.Title}");
            }
        }

        static async Task<Movie> GetMovieAsync(int movieID)
        {            
            string path = url + movieID + "?api_key=" + key;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Movie movie = await response.Content.ReadAsAsync<Movie>();
                return movie;
            }
            return null;     
        }

        static async Task<MoviesCollection> SearchMovieAsync(string searchString)
        {
            string path = "https://api.themoviedb.org/3/search/movie?api_key=" + key + "&query=" +searchString;
            //string path = "https://api.themoviedb.org/3/search/movie?api_key=d8693200a2452f1cd60ee9d7760a4895&query=Fight%20Club";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var movies = await response.Content.ReadAsAsync<MoviesCollection>();
                return movies;
            }
            return null;
        }


        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://api.themoviedb.org/3");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {

                //var movie = await GetMovieAsync(550);
                //ShowMovie(movie);
                Console.WriteLine("Search a movie:");
                var searchedMovie = Console.ReadLine();

                var movie = await SearchMovieAsync(searchedMovie);
                ShowMoviesCollection(movie);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

    }
}
