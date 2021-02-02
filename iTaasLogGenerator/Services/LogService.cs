using CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGenerator.Domain;
using CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGenerator.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGenerator.Services
{
    public class LogService : ILogService
    {
        public void ConvertLog(string sourceUrl, string fileName)
        {
            if (string.IsNullOrEmpty(sourceUrl))
                throw new ArgumentException("Parameter sourceUrl is invalid");
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("Parameter fileName is invalid");

            var listaLog = this.GetInformacoesLog(sourceUrl);
            var arquivoAgora = new StringBuilder();

            Console.WriteLine("Starting file conversion...");
            StreamWriter arquivo;

            if (File.Exists(fileName))
                File.Delete(fileName);

            arquivo = File.AppendText(fileName);

            arquivoAgora.AppendLine($"#Version: 1.0");
            arquivoAgora.AppendLine($"#Date: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:mmm")}");
            arquivoAgora.AppendLine($"#Fields: provider http-method status-code uri-path time-taken response-size cache-status");
            listaLog.ForEach(l => arquivoAgora.AppendLine($"\"{l.provider}\" {l.httpMethod} {l.statusCode} {l.uriPath} {l.timeTaken} {l.responseSize} {l.cacheStatus}"));

            try
            {
                arquivo.WriteLine(arquivoAgora.ToString());
                arquivo.Close();
                arquivo.Dispose();

                Console.WriteLine($"File successfully generated on the way: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while converting the log file. The error was: '{ex.Message}'");
            }
        }

        private List<InformacaoLog> GetInformacoesLog(string sourceUrl)
        {
            var listaLog = new List<InformacaoLog>();
            var destino = "Temp/Temp1.txt"; 

            try
            {
                if (!Directory.Exists("Temp"))
                    Directory.CreateDirectory("Temp");
                Console.WriteLine("Downloading log file...");
                using (var client = new WebClient())
                {
                    client.DownloadFile(sourceUrl, destino);
                }

                Console.WriteLine("Downloading complete...");

                string[] linhasLog = File.ReadAllLines(destino);

                foreach (var linha in linhasLog)
                {
                    var informacaoLinha = linha.Split("|");
                    var uri = informacaoLinha[3].Replace("\"", "").Split("/");

                    listaLog.Add(new InformacaoLog
                    {
                        httpMethod = uri[0].Trim(),
                        statusCode = informacaoLinha[1].Trim(),
                        uriPath = $"/{uri[1].Split(" ")[0].Trim()}",
                        timeTaken = Math.Round(Convert.ToDecimal(informacaoLinha[4].Replace('.',','))).ToString().Trim(),
                        responseSize = informacaoLinha[0].Trim(),
                        cacheStatus = informacaoLinha[2].Trim()
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while converting the log file. The error was: '{ex.Message}'");
            }
            finally
            {
                System.IO.Directory.Delete("Temp", true);
            }

            return listaLog;
        }
    }
}
