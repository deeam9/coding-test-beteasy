using DataProvider;
using DataProvider.Model;
using DataProvider.Service;
using DataProvider.ServiceAbstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace dotnet_code_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            GetHorsesInPriceAscOrder();
        }

        public static IConfigurationRoot Configuration { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        private static void GetHorsesInPriceAscOrder()
        {
            // set up configration

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            Configuration = builder.Build();

            // set up services
            var services = new ServiceCollection();

            services
                .AddScoped<IXmlService, XmlService>()
                .AddScoped<IJsonService, JsonService>();
            services.AddOptions();

            // set up configuration
            services.Configure<AppSettings>(Configuration.GetSection("BetEasy"));

            // Build ServiceProvider
            var serviceProvider = services.BuildServiceProvider();

            // get jsonService from serviceProvider
            var jsonService = serviceProvider.GetService<IJsonService>();

            // fetch all the horses from jsonService
            List<Horse> horses = jsonService.GetHorsesFromJsonService();
        }
    }
}
