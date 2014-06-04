using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace wsUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEnderecoOK()
        {
            var servico = new ServiceCalculoRota.ServiceRouteClient();
            
            var veiculo = new ServiceCalculoRota.Veiculo { 
                VelocidadeMedia = 60, 
                ValorCombustivel = 2.69, 
                ConsumoMedio = 10, 
                CategoriaVeiculo = 2, 
                CapacidadeTanque = 52 
            };
            
            var retorno = servico.GetInformacoesRota("c13iyCvmcC9mzwkLd0LCbmYC5mUF5m2jNGNtNGt6NmK6NJK=", 
                                                     "Av Paulista,1000", "São Paulo, SP", 
                                                     "R Agostinho Correia, 141", 
                                                     "São Paulo,SP", 
                                                     veiculo, 
                                                     1);

            Assert.IsTrue(retorno.CustoCombustivel > 0 &&
                          retorno.CustoPedagioCombustivel > 0 &&
                          retorno.DistanciaTotal > 0 &&
                          !string.IsNullOrEmpty(retorno.TempoTotal) &&
                          string.IsNullOrEmpty(retorno.Exception));
        }

        [TestMethod]
        public void TesteEnderecoImcompleto()
        {
            var servico = new ServiceCalculoRota.ServiceRouteClient();

            var veiculo = new ServiceCalculoRota.Veiculo
            {
                VelocidadeMedia = 60,
                ValorCombustivel = 2.69,
                ConsumoMedio = 10,
                CategoriaVeiculo = 2,
                CapacidadeTanque = 52
            };

            var retorno = servico.GetInformacoesRota("c13iyCvmcC9mzwkLd0LCbmYC5mUF5m2jNGNtNGt6NmK6NJK=",
                                                     "Av Paulista", "São Paulo, SP",
                                                     "Rua Agostrinho Correia, 141",
                                                     "São Paulo,SP",
                                                     veiculo,
                                                     1);

            Assert.IsTrue(!string.IsNullOrEmpty(retorno.Exception));
        }

        [TestMethod]
        public void TesteRetornoXml()
        {
            var servico = new ServiceCalculoRota.ServiceRouteClient();

            var veiculo = new ServiceCalculoRota.Veiculo
            {
                VelocidadeMedia = 60,
                ValorCombustivel = 2.69,
                ConsumoMedio = 10,
                CategoriaVeiculo = 2,
                CapacidadeTanque = 52
            };

            var retorno = servico.strGetInformacoesRota("c13iyCvmcC9mzwkLd0LCbmYC5mUF5m2jNGNtNGt6NmK6NJK=", 
                                                        "Av Paulista,1000", 
                                                        "São Paulo, SP", 
                                                        "Rua Agostinho Correia, 141", 
                                                        "São Paulo,SP", 
                                                        veiculo.CapacidadeTanque, 
                                                        veiculo.CategoriaVeiculo, 
                                                        veiculo.ConsumoMedio, 
                                                        veiculo.ValorCombustivel, 
                                                        veiculo.VelocidadeMedia, 
                                                        1);

            var xml = new XmlDocument();
            xml.LoadXml(retorno);

            Assert.IsTrue(xml != null);
        }

    }
}
