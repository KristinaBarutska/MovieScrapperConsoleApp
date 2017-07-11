using ConsoleAppMovieScrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMovieScrapper
{
  public  class MovieClient
    {
        const string baseUrl = "https://api.themoviedb.org";
        const string apiVersion = "3";
        const string moviePath = "movie";
        const string searchPath = "search";
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
            string path = $"{baseUrl}/{apiVersion}/{moviePath}/{movieID}?api_key={key}";
            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Movie movie = await response.Content.ReadAsAsync<Movie>();
                return movie;
            }
            return null;
        }

        static async Task<MoviesCollection> SearchMovieAsync(string searchString)
        {
            string path = String.Format("{0}/{1}/{2}/{3}?api_key={4}&query={5}", baseUrl, apiVersion, searchPath, moviePath, key, searchString);
            //string path = "https://api.themoviedb.org/3/search/movie?api_key=" + key + "&query=" +searchString;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var movies = await response.Content.ReadAsAsync<MoviesCollection>();
                return movies;
            }
            return null;
        }

        public static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://api.themoviedb.org/3");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                //This one gets movie by Id
                var movie1 = await GetMovieAsync(550);
                ShowMovie(movie1);

                bool wantToContinue = true;
                do
                {
                    Console.WriteLine("Enter a title:");
                    var searchedMovie = Console.ReadLine();
                    var movie = await SearchMovieAsync(searchedMovie);
                    ShowMoviesCollection(movie);
                    Console.WriteLine("Do you want to search for another movie? y/n");
                    string answer = Console.ReadLine();

                    wantToContinue = (answer == "y" || answer == "Y");

                }
                while (wantToContinue == true);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
