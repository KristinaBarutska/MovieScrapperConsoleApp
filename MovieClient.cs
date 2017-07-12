using ConsoleAppMovieScrapper.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleAppMovieScrapper
{
    internal class MovieClient
    {
        const string baseUrl = "https://api.themoviedb.org";
        const string DefaltApiVersion = "3";
        const string moviePath = "movie";
        const string searchPath = "search";
        const string key = "d8693200a2452f1cd60ee9d7760a4895";

        internal MovieClient()
        {
            client.BaseAddress = new Uri("https://api.themoviedb.org/3");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        static HttpClient client = new HttpClient();

        internal void ShowMovie(Movie movie)
        {
            Console.WriteLine($"Name: {movie.Title}");
            Console.WriteLine($"Release date: {movie.Release_date}");
        }

        internal void ShowMoviesCollection(MoviesCollection movies)
        {
            foreach (var movie in movies.Results)
            {
                Console.WriteLine($"{movie.Title} ({movie.Release_date})");
            }
        }

        internal async Task<Movie> GetMovieAsync(int movieID)
        {
            string path = $"{baseUrl}/{DefaltApiVersion}/{moviePath}/{movieID}?api_key={key}";
            var response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Movie movie = await response.Content.ReadAsAsync<Movie>();
                return movie;
            }
            return null;
        }

        internal async Task<MoviesCollection> SearchMovieAsync(string searchString)
        {
            string path = String.Format("{0}/{1}/{2}/{3}?api_key={4}&query={5}", baseUrl, DefaltApiVersion, searchPath, moviePath, key, searchString);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var movies = await response.Content.ReadAsAsync<MoviesCollection>();
                return movies;
            }
            return null;
        }
        
    }
}
