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

        //ENTRADA OU SAÍDA DO PONTO
        if (keyOption == "1" || keyOption == "2") {
            NovoRegistroPonto(keyOption);
        } else if (keyOption == "3") {
            TotalDeHorasMensais();
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
        Console.WriteLine("3- Total de Horas Mensais");
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
            Console.WriteLine("3- Total de Horas Mensais");
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
        
        //CHAMANDO METODO PARA PEGAR E VALIDAR O ID DO FUNCIONARIO
        idFuncionario = PegaIDFuncionario();
        
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
        Console.WriteLine($"Data: {funcionario.data.ToString("dd/MM/yyyy")}");
        Console.WriteLine($"Horário da {funcionario.chave}");
        Console.WriteLine($"Horário: {funcionario.horario.ToString("hh:mm:ss tt")}");
        Console.WriteLine("-----------------------------");
    }

    public static string PegaIDFuncionario() {
        var idFuncionario = "";

        //VADLIDANDO ID FUNCIONARIO, NO CASO O CPF
        Console.WriteLine("");
        Console.WriteLine("-----------------------------");
        Console.Write("Informe o CPF do funcionário: ");
        
        idFuncionario = Console.ReadLine();

        while (!ValidaCPF.IsCpf(idFuncionario)) {
            Console.Write("CPF inválido. Informe outro: ");
            idFuncionario = Console.ReadLine();
        }

        Console.WriteLine("");
        Console.WriteLine("-----------------------------");

        return idFuncionario;
    }

    public static void TotalDeHorasMensais() {
        //RECUPERAR IDFUNCIONARIO
        var idFuncionario = "05574057930";
        var nomeJSONFuncionario = "";
        var listaRegistros = new List<RegistroPontoEletronico>();
        var mesVerificar = "";
        var mesEscolhido = 0;

        idFuncionario = PegaIDFuncionario();
        // Console.WriteLine($"ID Recuperado: {idFuncionario}");

        //PEGANDO MÊS PARA O TOTAL DE HORAS
        Console.Write("Informe o NÚMERO do mês que deseja verificar o total de horas trabalhadas: ");
        mesVerificar = Console.ReadLine();
        
        while (!ValidaInputConsole.InputEscolhido(1, 12, mesVerificar)) {
            Console.Write("Informe um NÚMERO válido do mês que deseja verificar o total de horas trabalhadas: ");
            mesVerificar = Console.ReadLine();
        }

        //CONVERTENDO STRING DO MES PARA INTEIRO
        int.TryParse(mesVerificar, out mesEscolhido);
        // Console.WriteLine($"Mês {mesEscolhido} escolhido pelo usuário.");

        //VERIFICANDO A EXISTENCIA DO ARQUIVO JSON DO FUNCIONARIO
        nomeJSONFuncionario = idFuncionario + ".json";

        if (File.Exists(nomeJSONFuncionario)) {
            //RECUPERANDO LISTA DOS REGISTROS DO FUNCIONÁRIOS
            var stringListaRegistros = File.ReadAllText(nomeJSONFuncionario);
            listaRegistros = JsonSerializer.Deserialize<List<RegistroPontoEletronico>>(stringListaRegistros);

            //LER ARQUIVO JSON ORDENADO PELA DATA E HORA
            var linqRegistros = listaRegistros.Where(registro => registro.data.Month == mesEscolhido)
                                                .OrderBy(registro => registro.data)
                                                .OrderBy(registro => registro.horario)
                                                .ToList();

            var ultimaData = new DateTime();
            var ultimaChave = "";
            var ultimoHorario = new DateTime();
            var timeSpan = new TimeSpan();

            foreach (var item in linqRegistros)
            {
                // Console.WriteLine("---------------------------------");
                // Console.WriteLine(item.data.ToString("dd/MM/yyyy"));
                // Console.WriteLine(item.chave);
                // Console.WriteLine(item.horario.ToString("hh:mm:ss tt"));
                // Console.WriteLine("---------------------------------");

                if (ultimaChave == "") {
                    ultimaData = item.data;
                    ultimaChave = item.chave;
                    ultimoHorario = item.horario;
                
                //CASO 1 - TEM ENTRADA E SAÍDA
                } else if (ultimaChave == "Entrada" & item.chave == "Saida") {
                    // Console.WriteLine("");
                    // Console.WriteLine($"ULTIMO TimeSpan: {timeSpan}");
                    timeSpan += item.horario.Subtract(ultimoHorario);
                    // Console.WriteLine($"TimeSpan atualizado: {timeSpan}");
                    // Console.WriteLine("---------------------------------");

                    ultimaChave = "";
                    ultimoHorario = new DateTime();

                //CASO 2 - TEM ENTRADA MAS NÃO TEM SAÍDA
                } else if (item.chave == "Entrada") {
                    Console.WriteLine("O REGISTRO ABAIXO ESTÁ SEM REGISTRO DE SAÍDA");
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine(item.data.ToString("dd/MM/yyyy"));
                    Console.WriteLine(item.chave);
                    Console.WriteLine(item.horario.ToString("hh:mm:ss tt"));
                    Console.WriteLine("---------------------------------");

                //CASO 3 - NÃO TEM ENTRADA E TEM SAÍDA
                } else if (ultimaChave == "Saida") {
                    Console.WriteLine("O REGISTRO ABAIXO ESTÁ SEM REGISTRO DE ENTRADA");
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine(item.data.ToString("dd/MM/yyyy"));
                    Console.WriteLine(item.chave);
                    Console.WriteLine(item.horario.ToString("hh:mm:ss tt"));
                    Console.WriteLine("---------------------------------");
                }
            }
        
        //RETORNAR O TOTAL DE HORAS MENSAL DO FUNCIONARIO
        var strTimeSpan = $"{timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
        var strMonth = PegaNomeMes(mesEscolhido);

        Console.WriteLine("");
        Console.WriteLine($"Total de Horas trabalhadas nesse mês de {strMonth} foram de {strTimeSpan}");
        Console.WriteLine("");

        } else {
            Console.WriteLine($"Arquivo do funcionario {idFuncionario} não foi encontrado.");
        }
    }

    public static string PegaNomeMes(int mesEscolhido) {
        var nomeMesEscolhido = new DateTime(DateTime.Now.Year, mesEscolhido, 1);
        return nomeMesEscolhido.ToString("MMMM");
    }
}