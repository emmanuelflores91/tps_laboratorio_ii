using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Entidades
{
    public class Operando
    {
        private double numero;

        public Operando()
        {
            this.numero = 0;
        }

        public Operando(string strNumero) : this()
        {
            this.Numero = strNumero;
        }

        public Operando(double numero) : this()
        {
            this.numero = numero;
        }

        public static double operator - (Operando n1, Operando n2)
        {
            return n1.numero - n2.numero;
        }
        public static double operator + (Operando n1, Operando n2)
        {
            return n1.numero + n2.numero;
        }
        public static double operator / (Operando n1, Operando n2)
        {
            if (n2.numero != 0)
            {
                return n1.numero / n2.numero;
            }
            return double.MinValue;
        }
        public static double operator * (Operando n1, Operando n2)
        {
            return n1.numero * n2.numero;
        }

        private double ValidarOperando(string strNumero)
        {
            double numero;

            if (double.TryParse(strNumero, out numero))
            {
                return numero;
            }
            return 0;
        }

        private string Numero
        {
            set
            {
                this.numero = ValidarOperando(value);
            }
        }

        private static bool EsBinario(string binario)
        {
            for(int i = 0; i < binario.Length; i++)
            {
                if (binario[i] != '0' && binario[i] != '1')
                {
                    return false;
                }
            }
            return true;
        }

        public static string BinarioDecimal(string binario)
        {
            double numeroDouble= 0;

            if (EsBinario(binario))
            {
                int posicion = 0;
                int longitud = binario.Length;
                int digitoEntero;

                for (int i = longitud - 1; i >= 0; i--)
                {
                    digitoEntero = int.Parse(binario[i].ToString());
                    numeroDouble += digitoEntero * (Math.Pow(2, posicion));
                    posicion++;
                }
                int numeroEntero = (int) numeroDouble;
                return numeroEntero.ToString();
            }

            return "Valor inválido";
        }

        public static string DecimalBinario(string numero)
        {
            if (double.TryParse(numero, out double numeroDouble))
            {
                return DecimalBinario(numeroDouble);              

            }
            
            return "Valor inválido";
        }
        public static string DecimalBinario(double numero)
        {
            double resto;
            int cociente;
            cociente = (int)Math.Abs(numero);
            StringBuilder numeroBinario = new StringBuilder();
            StringBuilder restosBinarios = new StringBuilder();
            int longitud;
            string auxiliarRestos;            

            if ( cociente == 0 || cociente == 1)
            {
                return cociente.ToString();
            }
            else
            { 
                while (cociente >= 1)
                {
                    resto = cociente % 2;
                    cociente -= (int)resto;
                    cociente /= 2;
                    restosBinarios.Append(resto.ToString());
                }
                longitud = restosBinarios.Length;
                auxiliarRestos = restosBinarios.ToString();

                for(int i = longitud-1; i >= 0; i--)
                {
                   numeroBinario.Append(auxiliarRestos[i]);
                }

                return numeroBinario.ToString();
            }
        }
    }
}
