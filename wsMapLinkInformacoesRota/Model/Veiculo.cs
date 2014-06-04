using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace wsMapLinkInformacoesRota.Model
{
    [DataContract]
    public class Veiculo
    {
        private int _capacidadeTanque;
        [DataMember]
        public int CapacidadeTanque
        {
            get { return _capacidadeTanque; }
            set { _capacidadeTanque = value; }
        }

        private float _consumoMedio;
        [DataMember]
        public float ConsumoMedio
        {
            get { return _consumoMedio; }
            set { _consumoMedio = value; }
        }

        private double _valorCombustivel;
        [DataMember]
        public double ValorCombustivel
        {
            get { return _valorCombustivel; }
            set { _valorCombustivel = value; }
        }

        private int _velocidadeMedia;
        [DataMember]
        public int VelocidadeMedia
        {
            get { return _velocidadeMedia; }
            set { _velocidadeMedia = value; }
        }

        private int _categoriaVeiculo;
        [DataMember]
        public int CategoriaVeiculo
        {
            get { return _categoriaVeiculo; }
            set { _categoriaVeiculo = value; }
        }
    }
}