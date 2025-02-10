namespace Huaju.Ai
{
    public class Program
    {
        public static void Main(string[] args) => CreateBuilder(args).Build().Run();

        public static IHostBuilder CreateBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
        }
    }
}
