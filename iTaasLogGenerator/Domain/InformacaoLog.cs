using System;
using System.Collections.Generic;
using System.Text;

namespace CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGenerator.Domain
{
    public class InformacaoLog
    {
        public string provider  => "MINHA CDN";
        public string httpMethod { get; set; }
        public string statusCode { get; set; }
        public string uriPath { get; set; }
        public string timeTaken { get; set; }
        public string responseSize { get; set; }
        public string cacheStatus { get; set; }
    }
}
