using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using wsMapLinkInformacoesRota.Model;

namespace wsMapLinkInformacoesRota
{
    [ServiceContract]
    public interface IServiceRoute
    {
        [OperationContract]
        ResponseInformacoesRota GetInformacoesRota(string token, string enderecoPartida, string cidadeUfPartida, string enderecoDestino, string cidadeUfDestino, Veiculo veiculo, int tipoRota = 0);

        [OperationContract]
        string strGetInformacoesRota(string token, string enderecoPartida, string cidadeUfPartida,
                                           string enderecoDestino, string cidadeUfDestino,
                                           int capacidadeTanque, int categoriaVeiculo, float consumoMedio, double valorCombustivel, int velocidadeMedia,
                                           int tipoRota = 0);
    }
}
