using CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGenerator.Interfaces.Services;
using Moq;
using NUnit.Framework;
using System;

namespace CandidateTesting.GustavoHenriqueSilvaSeabra.iTaasLogGeneratorTeste
{
    public class LogService
    {
        private Mock<ILogService> _mock;

        [Test]
        public void ParametroUrlInvalido()
        {
            _mock = new Mock<ILogService>();

            try
            {
                _mock.Setup(m => m.ConvertLog("", "teste.txt"));
            }
            catch(ArgumentException ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void ParametroArquivoInvalido()
        {
            _mock = new Mock<ILogService>();

            try
            {
                _mock.Setup(m => m.ConvertLog("http://google.com", ""));
            }
            catch (ArgumentException ex)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void ParametrosValidos()
        {
            _mock = new Mock<ILogService>();

            try
            {
                _mock.Setup(m => m.ConvertLog("http://google.com", "teste.txt"));
            }
            catch (ArgumentException ex)
            {
                Assert.Fail();
            }
            finally
            {
                Assert.Pass();
            }
        }
    }
}