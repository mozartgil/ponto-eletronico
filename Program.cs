using Validacao;
using System.Text.Json;

class PontoEletronico {

    public static void Main() {
        Console.Clear();

        var keyOption = "";

        //MONTANDO MENU E SUAS OPÇÕES
        keyOption = OpcaoMenu();
        Console.WriteLine("");
        Console.WriteLine($"Opção {keyOption} do menu foi escolhida!");

        if (keyOption == "1" || keyOption == "2") {
            NovoRegistroPonto(keyOption);
        }
            
    }

    public static string OpcaoMenu() {
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Bem-vindo ao ponto Eletronico");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("");

        Console.WriteLine("-----------------------------");
        Console.WriteLine("             MENU          ");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("1- Informar Entrada");
        Console.WriteLine("2- Informar Saída");
        Console.WriteLine("3- Total de Horas");
        Console.WriteLine("4- Relatório");
        Console.WriteLine("5- Sair");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("");
        Console.Write("Escolha uma Opção: ");

        var keyOption = "";
        keyOption = Console.ReadLine();

        //NÃO ESQUEÇA DE ATUALIZAR ESSES PARAMETROS CASO AS OPÇÕES DE MENU FOREM ALTERADAS
        while (!ValidaInputConsole.InputEscolhido(1, 5, keyOption)) {
            Console.Clear();
            Console.WriteLine("POR FAVOR, INFORME UMA OPÇÃO VALIDA!");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("1- Informar Entrada");
            Console.WriteLine("2- Informar Saída");
            Console.WriteLine("3- Total de Horas");
            Console.WriteLine("4- Relatório");
            Console.WriteLine("5- Sair");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("");
            Console.Write("Escolha uma Opção: ");
            keyOption = Console.ReadLine();
        }

        return keyOption;        
    }

    public class RegistroPontoEletronico {
        public string idFuncionario { get; set; }
        public DateTime data { get; set; }
        public string chave { get; set; }
        public DateTime horario { get; set; }
        private DateTime totalHoras { get; set; }
    }

    public static void NovoRegistroPonto(string keyOption) {
        var nomeJSONFuncionario = "";
        var listaRegistros = new List<RegistroPontoEletronico> ();
        var funcionario = new RegistroPontoEletronico ();
        var idFuncionario = "05574057930";
        var chave = "Entrada";

        //VALIDANDO SE É ENTRADA OU SAÍDA DO PONTO
        if (keyOption == "2")
            chave = "Saida";

        //VADLIDANDO ID FUNCIONARIO, NO CASO O CPF
        Console.WriteLine("");
        Console.WriteLine("-----------------------------");
        Console.Write("Informe o CPF do funcionário: ");
        idFuncionario = "";
        idFuncionario = Console.ReadLine();

        while (!ValidaCPF.IsCpf(idFuncionario)) {
            Console.Write("CPF inválido. Informe outro: ");
            idFuncionario = Console.ReadLine();
        }

        Console.WriteLine("");
        Console.WriteLine("-----------------------------");

        funcionario.idFuncionario = idFuncionario;
        funcionario.data = DateTime.Now;
        funcionario.chave = chave;
        funcionario.horario = DateTime.Now;

        //VERIFICANDO A EXISTENCIA DO ARQUIVO JSON DO FUNCIONARIO E CRIANDO
        nomeJSONFuncionario = funcionario.idFuncionario.ToString() + ".json";
        // Console.WriteLine(nomeJSONFuncionario);

        if (File.Exists(nomeJSONFuncionario)) {
            // Console.WriteLine($"Arqvuivo {nomeJSONFuncionario} já existe!");

            //RECUPERANDO LISTA DOS REGISTROS DO FUNCIONÁRIOS
            var stringListaRegistros = File.ReadAllText(nomeJSONFuncionario);
            listaRegistros = JsonSerializer.Deserialize<List<RegistroPontoEletronico>>(stringListaRegistros);
        }
        
        //SEMPRE VAI ROLAR UM FILE.CREATE PRA LIMPAR O AQUIVO JSON
        File.Create(nomeJSONFuncionario).Close();

        listaRegistros.Add(funcionario);

        //ESCREVENDO NOVO REGISTRO NO PONTO
        var jsonFuncionario = JsonSerializer.Serialize(listaRegistros);
        // Console.WriteLine("");
        // Console.WriteLine("JSON do Funcionario");
        // Console.WriteLine(jsonFuncionario);
        // Console.WriteLine("-----------------------------");
        // Console.WriteLine("");
        
        File.AppendAllText(nomeJSONFuncionario, jsonFuncionario);

        //TICKET
        Console.WriteLine("");
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"ID Funcionario: {funcionario.idFuncionario}");
        Console.WriteLine($"Data: {funcionario.data}");
        Console.WriteLine($"Horário da {funcionario.chave}");
        Console.WriteLine($"Horário: {funcionario.horario}");
        Console.WriteLine("-----------------------------");
    }
}