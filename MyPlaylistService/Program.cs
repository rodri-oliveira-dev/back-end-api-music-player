using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DojoMyPlaylist
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(op=>op.AddServerHeader=false)
                .UseStartup<Startup>();
    }
}
