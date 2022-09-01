using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pw1.Backup.Routine.DATA
{
   public class Criptografia
    {
        public void Criptografar(string texto)
        {
            char[] hex = texto.ToCharArray();
            //Array.Reverse(hex);
            string novoTexto = string.Empty;
            int novoValor = 0;
            foreach (char hexValue in hex)
            {
                novoValor = Convert.ToInt32(hexValue);
                novoTexto += $"{ novoValor:X}-";

            }
           
            Console.WriteLine(novoTexto);
            Console.WriteLine(Descriptografar(novoTexto));

        }
        public string Descriptografar(string texto)
        {
            string[] hex = texto.Split('-');
            string novoTexto = string.Empty;


            foreach (var hexValue in hex)
            {

                novoTexto += string.Format("{0:x2} ", hexValue);

            }
            return novoTexto;
        }

    }
}
