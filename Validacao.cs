using System;

namespace Validacao
{
	/// <summary>
	/// Realiza a validação do CPF
	/// </summary>
	public static class ValidaCPF
	{
	     public static bool IsCpf(string cpf)
	    {
		int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
		int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
		string tempCpf;
		string digito;
		int soma;
		int resto;
		cpf = cpf.Trim();
		cpf = cpf.Replace(".", "").Replace("-", "");
		if (cpf.Length != 11)
		   return false;
		tempCpf = cpf.Substring(0, 9);
		soma = 0;

		for(int i=0; i<9; i++)
		    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
		resto = soma % 11;
		if ( resto < 2 )
		    resto = 0;
		else
		   resto = 11 - resto;
		digito = resto.ToString();
		tempCpf = tempCpf + digito;
		soma = 0;
		for(int i=0; i<10; i++)
		    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
		resto = soma % 11;
		if (resto < 2)
		   resto = 0;
		else
		   resto = 11 - resto;
		digito = digito + resto.ToString();
		return cpf.EndsWith(digito);
	      }
	}

	/// <summary>
	/// Realiza a do INPUT do Console
	/// </summary>
	public static class ValidaInputConsole {
        //Validação do número escolihido pelo cliente
        public static bool InputEscolhido(Int32 numeroMinimo, Int32 numeroMaximo, string strNumeroEscolhido)
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
}
