public class ValidaInputConsole {
    //Validação do número escolihido pelo cliente
    public static bool ValidaInputEscolhido(Int32 numeroMinimo, Int32 numeroMaximo, string strNumeroEscolhido)
    {
        Int32 numeroEscolhido = 0;

        //Validando se o INPUT é nulo
        if (strNumeroEscolhido is null)
        {
            return false;
        }

        //Validando se o INPUT é um número mesmo.
        if (!int.TryParse(strNumeroEscolhido, out numeroEscolhido))
        {
            return false;
        }

        //Validando se o número escolhido se encaixa entre numeroMinimo e numeroMaximo
        if (numeroEscolhido < numeroMinimo || numeroEscolhido > numeroMaximo)
        {
            return false;
        }

        return true;
    }
}