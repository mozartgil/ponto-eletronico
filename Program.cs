
class PontoEletronico {
    public static void Main() {
        Console.Clear();

        var keyOption = "";

        keyOption = OpcaoMenu();
        Console.WriteLine("");
        Console.WriteLine($"Opção {keyOption} do menu foi escolhida!");
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

        while (!ValidaInputConsole.ValidaInputEscolhido(1, 5, keyOption)) {
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
}