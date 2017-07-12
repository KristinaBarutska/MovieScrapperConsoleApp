using ConsoleAppMovieScrapper.Models;
using System;
using System.Threading.Tasks;

namespace ConsoleAppMovieScrapper
{
        
    public class Program
    {
        private static void ShowMovie(Movie movie)
        {
            Console.WriteLine($"Name: {movie.Title}");
            Console.WriteLine($"Release date: {movie.ReleaseDate}");
        }

        private static void ShowMoviesCollection(MoviesCollection movies)
        {
            foreach (var movie in movies.Results)
            {
                Console.WriteLine($"{movie.Title} ({movie.ReleaseDate})");
            }
        }
        public static async Task RunAsync()
        {
            var environmentKey = Environment.GetEnvironmentVariable("TMDB_API_KEY");
            var movieClient = new MovieClient(environmentKey);
            
            try
            {
                // This one gets movie by Id               
                //var movie1 = await movieClient.GetMovieAsync(550);
                //ShowMovie(movie1);
                
                bool wantToContinue = true;
                do
                {
                    Console.WriteLine("Enter a title:");
                    var searchedMovie = Console.ReadLine();
                    var movie = await movieClient.SearchMovieAsync(searchedMovie);
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

        public static void Main()
        {
            Task t = RunAsync();
            t.Wait();
        }       

    }
}
