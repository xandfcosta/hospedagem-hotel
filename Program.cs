using System.Text;
using hospedagem_hotel.Models;

Console.OutputEncoding = Encoding.UTF8;

bool rodando = true;
int tela_atual = 0;
List<Reserva> reservas = new( );
List<Suite> suites = new( );
Tela tela = new( );

while ( rodando )
{
    string input = "";
    if ( tela_atual == 0 ) // Tela inicial
    {   
        tela.TituloPrincipal = "HOSPEDAGEM DE HOTEL";
        tela.TituloSecundario = "";
        tela.Opcoes = new();
        tela.Opcoes.Add( "Cadastrar suíte" );
        tela.Opcoes.Add( "Realizar reserva" );
        tela.Opcoes.Add( "Mostrar reservas" );
        tela.OpcaoVoltar = "Sair do sistema";

        input = tela.MostrarTela( );

        switch ( input )
        {
            case "0":
                rodando = false;
                break;

            case "1":
                tela_atual = 1;
                break;

            case "2":
                tela_atual = 2;
                break;

            case "3":
                tela.TituloPrincipal = "RESERVAS";
                tela.TituloSecundario = "";
                tela.Opcoes = new();
                foreach( var reserva in reservas )
                {
                    tela.Opcoes.Add( $"{ reserva.ObterQuantidadeHospedes( ) } hóspedes - { reserva.CalcularValorDiario( ):C}" );
                }
                tela.OpcaoVoltar = "";

                input = tela.MostrarTela( );

                break;

            default:
                Console.WriteLine( "[!]Opção inválida" );
                break;
        }

    }
    else if ( tela_atual == 1 ) // Cadastro de suite
    {
        tela.TituloPrincipal = "CADASTRAR SUITE";
        tela.Opcoes = new();
        tela.OpcaoVoltar = "Cancelar cadastro";

        tela.TituloSecundario = "Tipo da suite";
        input = tela.MostrarTela( );

        if ( input == "" )
        {   
            tela_atual = 0;
            continue;
        }
        string tipo_suite = input;

        tela.TituloSecundario = "Capacidade";

        input = tela.MostrarTela( );

        if ( input == "" )
        {
            tela_atual = 0;
            continue;
        }
        int capacidade = Convert.ToInt32( input );

        tela.TituloSecundario = "Valor da diaria";

        input = tela.MostrarTela( );
     
        if ( input == "" )
        {
            tela_atual = 0;
            continue;
        }
        decimal valor_diaria = Convert.ToDecimal( input );

        suites.Add( new Suite( tipo_suite, capacidade, valor_diaria ) );

        tela_atual = 0;
    }

    else if ( tela_atual == 2 ) // Realização de reserva
    {   
        Reserva reserva = new();

        tela.TituloPrincipal = "RESERVA";
        tela.Opcoes = new();
        tela.OpcaoVoltar = "Cancelar reserva";

        tela.TituloSecundario = "Quantidade de pessoas";

        bool cancelou = false;
        int quantidade_pessoas = 0;
        while( true )
        {
            input = tela.MostrarTela( );

            if ( input == "" )
            {
                cancelou = true;
                break;
            }
            quantidade_pessoas = Convert.ToInt32( input );

            if ( quantidade_pessoas > 0 )
            {
                break;
            }

            Console.WriteLine( "[!] Quantidade de pessoas inválida" );
        }

        if ( cancelou )
        {
            tela_atual = 0;
            continue;
        }

        tela.TituloPrincipal = "SUITES";
        tela.TituloSecundario = $"Suites disponíveis para { quantidade_pessoas } pessoas";
        tela.Opcoes = new();
        Dictionary<int, int> dict_suite = new();
        int i = 0;
        foreach( var suite in suites )
        {
            if( suite.Capacidade >= quantidade_pessoas )
            {
                tela.Opcoes.Add( $"{ suite.TipoSuite } - {suite.Capacidade} - {suite.ValorDiaria:C}" );
                dict_suite.Add( i + 1, suites.IndexOf( suite ) );
                i++;
            }
        }

        tela.OpcaoVoltar = "Cancelar reserva";
        int id_suite_selecionada = -1;
        cancelou = false;
        while( id_suite_selecionada > i || id_suite_selecionada < 0  )
        {
            input = tela.MostrarTela( );

            if( input == "" )
            {
                cancelou = true;
                break;
            }

            id_suite_selecionada = Convert.ToInt32( input );
        }

        if ( cancelou )
        {
            tela_atual = 0;
            continue;
        }

        id_suite_selecionada = dict_suite[ id_suite_selecionada ];
        reserva.CadastrarSuite( suites[ id_suite_selecionada ] );

        List<Pessoa> pessoas = new();
        tela.TituloPrincipal = "PESSOAS";
        tela.Opcoes = new();
        for( i = 0; i < quantidade_pessoas; i++ )
        {
            tela.TituloSecundario = "Nome";
            input = tela.MostrarTela( );

            if ( input == "" )
            {
                cancelou = true;
                break;
            }
            string nome = input;

            tela.TituloSecundario = "Sobrenome";
            input = tela.MostrarTela( );

            if ( input == "" )
            {
                cancelou = true;
                break;
            }
            string sobrenome = input;

            Pessoa pessoa = new Pessoa( nome, sobrenome );
            reserva.CadastrarHospede( pessoa );
        }

        tela.TituloSecundario = "Quantidade de dias";

        int quantidade_dias = -1;
        while( quantidade_dias < 0 )
        {
            input = tela.MostrarTela( );

            if ( input == "" )
            {
                cancelou = true;
                break;
            }

            quantidade_dias = Convert.ToInt32( input );
        }

        if( !cancelou )
        {
            reserva.DiasReservados = quantidade_dias;
            reservas.Add( reserva );
        }
        tela_atual = 0;
    }
}