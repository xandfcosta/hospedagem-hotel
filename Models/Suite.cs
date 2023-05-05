using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospedagem_hotel.Models
{
    public class Suite
    {
        public string TipoSuite { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorDiaria { get; set; }

        public Suite( string TipoSuite, int Capacidade, decimal ValorDiaria )
        {
            this.TipoSuite = TipoSuite;
            this.Capacidade = Capacidade;
            this.ValorDiaria = ValorDiaria;
        }
    }
}