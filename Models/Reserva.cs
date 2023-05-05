using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospedagem_hotel.Models
{
    public class Reserva
    {
        private List<Pessoa> Hospedes { get; set; }
        private Suite Suite { get; set; }
        public int DiasReservados { get; set; }

        public Reserva( )
        {
            Hospedes = new List<Pessoa>();
        }

        public void CadastrarHospede( Pessoa hospede )
        {
            Hospedes.Add( hospede );
        }

        public void CadastrarSuite( Suite suite )
        {
            Suite = suite;
        }

        public int ObterQuantidadeHospedes( )
        {
            return Hospedes.Count;
        }

        public decimal CalcularValorDiario( )
        {
            return Suite.ValorDiaria * DiasReservados;
        }
    
    }
}