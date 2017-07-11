using System.Threading.Tasks;

namespace ConsoleAppMovieScrapper
{
        
    public class Program
    {
                        
        public static void Main()
        {
            Task t = MovieClient.RunAsync();
            t.Wait();
        }       

    }
}
