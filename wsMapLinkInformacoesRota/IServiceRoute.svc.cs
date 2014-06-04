using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using wsMapLinkInformacoesRota.Model;
using wsMapLinkInformacoesRota.ServiceAddress;
using wsMapLinkInformacoesRota.ServiceRouteMapLink;

namespace wsMapLinkInformacoesRota
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceRoute : IServiceRoute
    {
        public string strGetInformacoesRota(string token, string enderecoPartida, string cidadeUfPartida,
                                           string enderecoDestino, string cidadeUfDestino,
                                           int capacidadeTanque, int categoriaVeiculo, float consumoMedio, double valorCombustivel, int velocidadeMedia,
                                           int tipoRota = 0)
        {
            var veiculo = new Veiculo
            {
                CapacidadeTanque = capacidadeTanque,
                CategoriaVeiculo = categoriaVeiculo,
                ConsumoMedio = consumoMedio,
                ValorCombustivel = valorCombustivel,
                VelocidadeMedia = velocidadeMedia
            };

            var retorno = GetInformacoesRota(token, enderecoPartida, cidadeUfPartida, enderecoDestino, cidadeUfDestino, veiculo, tipoRota);

            return Util.DataContractSerializeObject<ResponseInformacoesRota>(retorno);
        }



        /// <summary>
        /// Método para retorno de informações e valores sobre a rota
        /// </summary>
        /// <param name="token">Chave para autenticação</param>
        /// <param name="enderecoPartida">Endereco com número de partida, ex: Av Paulista, 1000</param>
        /// <param name="cidadeUfPartida">Cidade e estado de partida, ex: São Paulo,SP</param>
        /// <param name="enderecoDestino">Endereco com número de destino, ex: Av Paulista, 1000</param>
        /// <param name="cidadeUfDestino">Cidade e estado de destino, ex: São Paulo,SP</param>
        /// <param name="veiculo">Infomar: CapacidadeTanque,ConsumoMedio,ValorCombustivel,VelocidadeMedia,CategoriaVeiculo(1-Moto,2-Carro,5-Onibus,7-Caminhao)</param>
        /// <param name="tipoRota">0 = Rota padrão mais rápida, 1 = Rota evitando o trânsito. (Somente utilizando base urbana)</param>
        /// <returns>Tempo total da rota;Distância total;Custo de combustível;Custo total considerando pedágio</returns>
        public ResponseInformacoesRota GetInformacoesRota(string token, string enderecoPartida, string cidadeUfPartida, string enderecoDestino, string cidadeUfDestino, Veiculo veiculo, int tipoRota = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(enderecoPartida) || enderecoPartida.Split(',').Length != 2)
                    return new ResponseInformacoesRota { Exception = "O endereço de partida deve ser preenchido corretamente: ex: Av Paulista, 1000" };
                if (string.IsNullOrEmpty(cidadeUfPartida) || cidadeUfPartida.Split(',').Length != 2)
                    return new ResponseInformacoesRota { Exception = "A cidade e UF de partida deve ser preenchida corretamente, ex: São Paulo, SP" };
                if (string.IsNullOrEmpty(enderecoDestino) || enderecoDestino.Split(',').Length != 2)
                    return new ResponseInformacoesRota { Exception = "O endereço de destino deve ser preenchido corretamente: ex: Av Paulista, 2000" };
                if (string.IsNullOrEmpty(cidadeUfDestino) || cidadeUfDestino.Split(',').Length != 2)
                    return new ResponseInformacoesRota { Exception = "A cidade de destino deve ser preenchida corretamente, ex: São Paulo, SP" };
                if (veiculo == null)
                    return new ResponseInformacoesRota { Exception = "As informações do veículo devem ser preenchidas" };
                if (veiculo.CapacidadeTanque <= 0)
                    return new ResponseInformacoesRota { Exception = "A capacidade do tanque deve ser informada" };
                if (veiculo.CategoriaVeiculo <= 0)
                    return new ResponseInformacoesRota { Exception = "A categoria do veículo deve ser informada" };
                if (veiculo.ConsumoMedio <= 0)
                    return new ResponseInformacoesRota { Exception = "O consumo médio do veículo deve ser informado" };
                if (veiculo.ValorCombustivel <= 0)
                    return new ResponseInformacoesRota { Exception = "O valor do combustível deve ser informado" };
                //ToDo: Verificar se existe método no webservice que calcula velocidade média da viagem
                if (veiculo.VelocidadeMedia <= 0)
                    return new ResponseInformacoesRota { Exception = "A velocidade média a ser empregada na viagem deve ser informada" };

                var addressStart = new Address
                {
                    street = enderecoPartida.Split(',')[0].Trim(),
                    houseNumber = enderecoPartida.Split(',')[1].Trim(),
                    city = new ServiceAddress.City { name = cidadeUfPartida.Split(',')[0].Trim(), state = cidadeUfPartida.Split(',')[1].Trim() }
                };

                var addressEnd = new Address
                {
                    street = enderecoDestino.Split(',')[0].Trim(),
                    houseNumber = enderecoDestino.Split(',')[1].Trim(),
                    city = new ServiceAddress.City { name = cidadeUfDestino.Split(',')[0].Trim(), state = cidadeUfDestino.Split(',')[1].Trim() }
                };

                var addressOptions = new AddressOptions
                {
                    usePhonetic = true,
                    searchType = 2,
                    resultRange = new ResultRange { pageIndex = 1, recordsPerPage = 1 }
                };

                double xPartida = 0;
                double yPartida = 0;
                using (var addressFinderSoapClient = new AddressFinderSoapClient())
                {
                    var findAddressResponse = addressFinderSoapClient.findAddress(addressStart, addressOptions, token);
                    if (findAddressResponse.addressLocation.Count() == 0)
                        return new ResponseInformacoesRota { Exception = "O endereço de partida não foi localizado, verifique" };

                    xPartida = findAddressResponse.addressLocation.First().point.x;
                    yPartida = findAddressResponse.addressLocation.First().point.y;
                }

                double xDestino = 0;
                double yDestino = 0;
                using (var addressFinderSoapClient = new AddressFinderSoapClient())
                {
                    var findAddressResponse = addressFinderSoapClient.findAddress(addressEnd, addressOptions, token);
                    if (findAddressResponse.addressLocation.Count() == 0)
                        return new ResponseInformacoesRota { Exception = "O endereço de chegada não foi localizado, verifique" };

                    xDestino = findAddressResponse.addressLocation.First().point.x;
                    yDestino = findAddressResponse.addressLocation.First().point.y;
                }

                var originRoute = new ServiceRouteMapLink.RouteStop
                {
                    description = addressStart.street,
                    point = new ServiceRouteMapLink.Point { x = xPartida, y = yPartida }
                };

                var destinationRoute = new ServiceRouteMapLink.RouteStop
                {
                    description = addressEnd.street,
                    point = new ServiceRouteMapLink.Point { x = xDestino, y = yDestino }
                };

                var routes = new[] { originRoute, destinationRoute };

                var opcaoRota = DescriptionType.RotaUrbana;
                tipoRota = (int)RouteType.PadraoRapida;
                //Se a cidade for diferente troca o descriptionType para rota rodoviária
                if (!cidadeUfPartida.Trim().Equals(cidadeUfDestino.Trim()))
                    opcaoRota = DescriptionType.RotaRodoviaria;

                var routeOptions = new RouteOptions
                {
                    language = "portugues",
                    routeDetails = new RouteDetails { descriptionType = (int)opcaoRota, routeType = tipoRota, optimizeRoute = true },
                    vehicle = new Vehicle
                    {
                        tankCapacity = veiculo.CapacidadeTanque,
                        averageConsumption = veiculo.ConsumoMedio,
                        fuelPrice = veiculo.ValorCombustivel,
                        averageSpeed = veiculo.VelocidadeMedia,
                        tollFeeCat = veiculo.CategoriaVeiculo
                    }
                };

                using (var routeSoapClient = new RouteSoapClient())
                {
                    var getRouteResponse = routeSoapClient.getRoute(routes, routeOptions, token);

                    var result = new ResponseInformacoesRota
                    {
                        TempoTotal = getRouteResponse.routeTotals.totalTime,
                        DistanciaTotal = getRouteResponse.routeTotals.totalDistance,
                        CustoCombustivel = getRouteResponse.routeTotals.totalfuelCost,
                        CustoPedagioCombustivel = getRouteResponse.routeTotals.totalCost

                    };
                    return result;
                }
            }
            catch (Exception)
            {
                return new ResponseInformacoesRota { Exception = "Não foi possível processar sua solicitação" };
            }
        }
    }
}
