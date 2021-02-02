using CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGenerator.Interfaces.Services
{
    public interface ILogService
    {
        void ConvertLog(string sourceUrl, string fileName);
    }
}
