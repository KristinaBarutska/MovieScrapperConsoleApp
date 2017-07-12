using System;
using System.Threading.Tasks;

namespace ConsoleAppMovieScrapper
{
        
    public class Program
    {
        public static async Task RunAsync()
        {
            var movieClient = new MovieClient();

            try
            {
                // This one gets movie by Id               
                //var movie1 = await movieClient.GetMovieAsync(550);
                //movieClient.ShowMovie(movie1);

                bool wantToContinue = true;
                do
                {
                    Console.WriteLine("Enter a title:");
                    var searchedMovie = Console.ReadLine();
                    var movie = await movieClient.SearchMovieAsync(searchedMovie);
                    movieClient.ShowMoviesCollection(movie);
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
            Task t = Program.RunAsync();
            t.Wait();
        }       

    }
}
