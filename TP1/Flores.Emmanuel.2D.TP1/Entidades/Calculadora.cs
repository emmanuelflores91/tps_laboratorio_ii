using System;

namespace Entidades
{
    public class Calculadora
    {
        private static char ValidarOperador(char operador)
        {
            if (operador == '+' || operador == '-' || operador == '/' || operador == '*')
            {
                return operador;
            }
            else
            {
                return '+';
            }
        }

        public static double Operar(Operando num1, Operando num2, char operador)
        {
            char operadorValidado = ValidarOperador(operador);

            switch (operadorValidado)
            {
                case '+': return num1 + num2;
                case '-': return num1 - num2;
                case '/': return num1 / num2;
                case '*': return num1 * num2;
            }
            return 0;
        }
    }
}
