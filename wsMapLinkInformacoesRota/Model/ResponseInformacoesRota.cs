using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace wsMapLinkInformacoesRota.Model
{
    [DataContract]
    public class ResponseInformacoesRota
    {
        //ToDo: Formatar os valores de saída no campo

        private string tempoTotal;
        [DataMember]
        public string TempoTotal
        {
            get { return tempoTotal; }
            set { tempoTotal = value; }
        }

        private double distanciaTotal;
        [DataMember]
        public double DistanciaTotal
        {
            get { return distanciaTotal; }
            set { distanciaTotal = value; }
        }

        private double custoCombustivel;
        [DataMember]
        public double CustoCombustivel
        {
            get { return custoCombustivel; }
            set { custoCombustivel = value; }
        }

        private double custoPedagioCombustivel;
        [DataMember]
        public double CustoPedagioCombustivel
        {
            get { return custoPedagioCombustivel; }
            set { custoPedagioCombustivel = value; }
        }

        private string _exception;
        [DataMember]
        public string Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }
    }
}