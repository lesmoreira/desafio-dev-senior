using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsMapLinkInformacoesRota.Model
{
    public enum CategoriaVeiculo : int
    {
        /// <summary>
        /// 0	Não será calculado o valor do pedágio
        /// </summary>
        SemPedagio,
        /// <summary>
        /// 1	Motocicletas, motonetas e bicicletas a motor
        /// </summary>
        Moto,
        /// <summary>
        /// 2	Automóvel, caminhoneta e furgão (dois eixos simples)
        /// </summary>
        AutomovelDoisEixos,
        /// <summary>
        /// 3	Automóvel, caminhoneta com semi-reboque (três eixos simples)
        /// </summary>
        AutomovelTresEixos,
        /// <summary>
        /// 4	Automóvel, caminhoneta com reboque (quatro eixos simples)
        /// </summary>
        AutomovelQuatroEixos,
        /// <summary>
        /// 5	Ônibus (dois eixos duplos)
        /// </summary>
        OnibusDoisEixos,
        /// <summary>
        /// 6	Ônibus com reboque (três eixos duplos)
        /// </summary>
        OnibusTresEixos,
        /// <summary>
        /// 7	Caminhão leve, furgão e cavalo mecânico (dois eixos duplos)
        /// </summary>
        CaminhaoDoisEixos,
        /// <summary>
        /// 8	Caminhão, caminhão trator e cavalo mecânico com semi-reboque (três eixos duplos)
        /// </summary>
        CaminhaoTresEixos,
        /// <summary>
        /// 9	Caminhão com reboque e cavalo mecânico com semi-reboque (quatro eixos duplos)
        /// </summary>
        CaminhaoQuatroEixos,
        /// <summary>
        /// 10	Caminhão com reboque e cavalo mecânico com semi-reboque (cinco eixos duplos)
        /// </summary>
        CaminhaoCincoEixos,
        /// <summary>
        /// 11	Caminhão com reboque e cavalo mecânico com semi-reboque (seis eixos duplos)
        /// </summary>
        CaminhaoSeisEixos,
        /// <summary>
        /// 12	Caminhão com reboque e cavalo mecânico com semi-reboque (sete eixos duplos)
        /// </summary>
        CaminhaoSeteEixos,
        /// <summary>
        /// 13	Caminhão com reboque e cavalo mecânico com semi-reboque (oito eixos duplos)
        /// </summary>
        CaminhaoOitoEixos,
        /// <summary>
        /// 14	Caminhão com reboque e cavalo mecânico com semi-reboque (nove eixos duplos)
        /// </summary>
        CaminhaoNoveEixos
    }
}