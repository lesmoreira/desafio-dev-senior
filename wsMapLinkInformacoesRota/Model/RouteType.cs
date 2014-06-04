using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsMapLinkInformacoesRota.Model
{
    /// <summary>
    /// Enum de tipo de rota 
    /// </summary>
    public enum RouteType : int
    {
        PadraoRapida,
        EvitandoTransito = 23
    }
}