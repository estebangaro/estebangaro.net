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
            //List<string> codeLines = code.Split('\n').ToList();

            //foreach(string line in codeLines)
            //{
            //    var coincidencias = line
            //        .Split(' ')
            //        .Where(EvalComentsStrings);
            //    Console.WriteLine(line);
            //}
            Console.WriteLine("Antes de reemplazo");
            Console.WriteLine(code);
            code = ReplaceAssemblyWords(code);
            code = ReplaceKeyReservedWords(code);
            code = ReplaceClassWords(code);
            code = ReplaceKeyReservedWordsWithWhiteSkips(code);
            code = ReplaceStrings(code);
            code = ReplaceComments(code);
            Console.WriteLine("Después de reemplazo");
            Console.WriteLine(code);
        }

        static string ReplaceKeyReservedWords(string inputString)
        {
            string[] keywords = { "int", "decimal", "float", "string", "double", "short", "static", "namespace", "using" };
            foreach (var primitivo in keywords)
            {
                inputString = Regex.Replace(inputString, $@"\b{primitivo}\b", $"<span class=\"reservada\">{primitivo}</span>");
            }

            return inputString;
        }

        static string ReplaceKeyReservedWordsWithWhiteSkips(string inputString)
        {
            string[] keywords = { "class" };
            foreach (var primitivo in keywords)
            {
                inputString = Regex.Replace(inputString, $@"( {primitivo} |^{primitivo} )",
                    match =>
                    {
                        return match.Value.
                            Replace(primitivo, $"<span class=\"reservada\">{primitivo}</span>");
                    });
            }

            return inputString;
        }

        static string ReplaceAssemblyWords(string inputString)
        {
            int count = 0;
            var matches = Regex.Matches(inputString, @"using (?<ensamblado>\w+[\.\w]*);");
            foreach(Match match in matches)
            {
                StringBuilder stringBuilder = new StringBuilder(inputString);
                string assemblyMatchString = match.Groups["ensamblado"].Value;
                
                inputString = stringBuilder.Replace(assemblyMatchString,
                    assemblyMatchString.Replace(assemblyMatchString,
                        $"<span class=\"assembly\">{assemblyMatchString}</span>"),
                        match.Groups["ensamblado"].Index + (count * 30),
                        match.Groups["ensamblado"].Length
                    ).ToString();

                count++;
            }

            return inputString;
        }

        static string ReplaceClassWords(string inputString)
        {
            string[] classWords = { "Console", "Program", "Operaciones"};
            foreach (var primitivo in classWords)
            {
                inputString = Regex.Replace(inputString, $@"\b{primitivo}\b", 
                    $"<span class=\"class\">{primitivo}</span>");
            }

            return inputString;
        }

        static string ReplaceStrings(string inputString)
        {
            inputString = Regex.Replace(inputString, "[^=]\"[\\w \\.ÁÉÍÓÚáéíóú:]+\"",
                    match =>
                    {
                        string matchWithOutSpan = Regex.Replace(match.Value, "<span class=\"\\w+\">(?<value>.+)</span>",
                                match2 =>
                                {
                                    return match2.Value.
                                        Replace(match2.Value, match2.Groups["value"].Value);
                                }
                            );
                        return matchWithOutSpan.Replace(matchWithOutSpan,
                            $"<span class=\"strings\">{matchWithOutSpan}</span>");
                    });

            return inputString;
        }

        static string ReplaceComments(string inputString)
        {
            inputString = Regex.Replace(inputString, "//.+[^\r\n]",
                    match =>
                    {
                         string matchWithOutSpan = Regex.Replace(match.Value, "<span class=\"\\w+\">(?<value>.+)</span>",
                                match2 =>
                                {
                                    return match2.Value.
                                        Replace(match2.Value, match2.Groups["value"].Value);
                                }
                            );
                        return matchWithOutSpan.Replace(matchWithOutSpan,
                            $"<span class=\"coments\">{matchWithOutSpan}</span>");
                    });

            return inputString;
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
            "            // Se asigna método con nombre a la referencia delegado Action<string>\r\n" +
            "            operacion.MostrarMsj = Console.WriteLine;\r\n" +
            "            // Se obtiene el resultado de la operación.\r\n" +
            "            float division = operacion.Divide(A, B);\r\n" +
            "        }\r\n" +
            "    }\r\n" +
            "}\r\n";
        }
    }
}

