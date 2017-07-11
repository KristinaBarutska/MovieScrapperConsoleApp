namespace ConsoleAppMovieScrapper
{
    public class Program
    {      
        public static void Main()
        {           
            var t = MovieClient.RunAsync();
           t.Wait();
        }
        
    }
}
