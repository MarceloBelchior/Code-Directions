using System;
using System.IO;
using Autofac;
using Microsoft.Extensions.Configuration;
using OpenSquare.CodeHB.Domain;
using OpenSquare.CodeHB.Service;

namespace OpenSquare.CodeHB.DI
{
    public class CodeHB : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var ambiente = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(string.Format("appsettings.{0}.json", ambiente), optional: true);

            var configuration = config?.Build();
            builder.RegisterInstance(configuration);
            var _appSettings = configuration.GetSection("configuracao");

            builder.RegisterInstance(_appSettings).As<IConfigurationSection>();

            builder.RegisterType<Helper.HttpHelper>().As<Domain.Helper.IHttpHelper>().SingleInstance();

            builder.RegisterType<HttpClient.Escolas.Repository.EscolaRepository>().As<Domain.IEscolaRepository>().SingleInstance();
            builder.RegisterType<HttpClient.Bing.Repository.BingRepository>().As<IBingRepository>().SingleInstance();


            builder.RegisterType<EscolasService>().As<IEscolaService>().SingleInstance();
        }
    }
}
