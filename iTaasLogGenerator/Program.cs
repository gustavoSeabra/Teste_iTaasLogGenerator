using CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGenerator.Interfaces.Services;
using CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGenerator.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var logService = serviceProvider.GetService<ILogService>();

            string url;
            string arquivo;

            // Validando se foi informado os argumentos para a execução do projeto.
            // Caso não seja, usa as variáveis do config.
            if(args.Length == 2)
            {
                url = args[0];
                arquivo = args[1];
            }
            else
            {
                url = System.Configuration.ConfigurationManager.AppSettings["CaminhoLog"].ToString();
                arquivo = System.Configuration.ConfigurationManager.AppSettings["ArquivoLogTransformado"].ToString();
            }

            Console.WriteLine("Starting the transformation ...");

            try
            {
                // Chamo o método do meu servico
                logService.ConvertLog(url, arquivo);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while converting the log file. The error was: '{ex.Message}'");
            }
        }

        /// <summary>
        /// Adicionando a Injeção de dependência do meu serviço
        /// </summary>
        /// <param name="services">Coleção de serviços</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILogService, LogService>();
        }
    }
}
