using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeConverter2HTML
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = GetCode();
            List<string> codeLines = code.Split('\n').ToList();
            
            foreach(string line in codeLines)
            {
                var coincidencias = line
                    .Split(' ')
                    .Where(EvalComentsStrings);
                Console.WriteLine(line);
            }
        }

        static bool EvalComentsStrings(string text)
        {
            Regex comentsStrings = new Regex("(^//.{1,}$|^\".{1,}\"$)");

            return comentsStrings.Match(text).Success;
        }

        static string GetCode()
        {
            return "using System;\r\n" +
            "using System.Collections.Generic;\r\n" +
            "using System.Linq;\r\n" +
            "using System.Text;\r\n" +
            "using System.Threading.Tasks;\r\n" +
            "\r\n" +
            "namespace delegados_t2_2\r\n" +
            "{\r\n" +
            "    class Program\r\n" +
            "    {\r\n" +
            "        static void Main(string[] args)\r\n" +
            "        {\r\n" +
            "            // Se solicita el número entero A\r\n" +
            "            Console.WriteLine( \"Introducir número A: \" );\r\n" +
            "            int A = int.Parse(Console.ReadLine());\r\n" +
            "            // Se solicita el número entero B\r\n" +
            "            Console.WriteLine( \"Introducir número B: \" );\r\n" +
            "            int B = int.Parse(Console.ReadLine());\r\n" +
            "\r\n" +
            "            // Se crea una instancia de Operaciones (expuesta por el proyecto de\r\n" +
            "            // bibliotecas \"delegados_t2_1\")\r\n" +
            "            delegados_t2_1.Operaciones operacion =\r\n" +
            "                new delegados_t2_1.Operaciones();\r\n" +
            "            // Se asigna método con nombre a la referencia delegado Action string\r\n" +
            "            operacion.MostrarMsj = Console.WriteLine;\r\n" +
            "            // Se obtiene el resultado de la operación.\r\n" +
            "            float division = operacion.Divide(A, B);\r\n" +
            "        }\r\n" +
            "    }\r\n" +
            "}\r\n";
        }
    }
}
