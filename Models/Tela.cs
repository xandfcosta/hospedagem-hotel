using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospedagem_hotel.Models
{
    public class Tela
    {
        public string TituloPrincipal { get; set; }
        public string TituloSecundario { get; set; }
        public List<string> Opcoes { get; set; }
        public string OpcaoVoltar { get; set; }

        public string MostrarTela()
        {
            // Console.WriteLine( TituloPrincipal );
            // Console.WriteLine( TituloSecundario );
            // Console.WriteLine( Opcoes[0] );
            // Console.WriteLine( OpcaoVoltar );

            string tela = $"\n{TituloPrincipal}\n";
            tela += OpcaoVoltar != "" ? $"[0]{OpcaoVoltar}\n\n" : "";
            tela += TituloSecundario != "" ? $"{TituloSecundario}\n" : "";
            for (int i = 0; i < Opcoes.Count; i++)
            {
                tela += $"[{i + 1}] {Opcoes[i]}\n";
            }

            Console.WriteLine(tela);

            string input = Console.ReadLine();
            Console.Clear();

            if (input == "0")
            {
                return "";
            }

            return input;
        }
    }
}