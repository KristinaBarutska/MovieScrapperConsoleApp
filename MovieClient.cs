using ConsoleAppMovieScrapper.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleAppMovieScrapper
{
    public class MovieClient
    {
        private const string baseUrl = "https://api.themoviedb.org";
        private const string DefaltApiVersion = "3";
        private const string moviePath = "movie";
        private const string searchPath = "search";
        private string key;        
        public MovieClient(string passedKey)
        
        {
            key = passedKey;                                            
            client.BaseAddress = new Uri("https://api.themoviedb.org/3");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        private static HttpClient client = new HttpClient();       

        public async Task<Movie> GetMovieAsync(int movieID)
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

        public async Task<MoviesCollection> SearchMovieAsync(string searchString)
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
