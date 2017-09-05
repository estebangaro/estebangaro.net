using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using centro.recursos.net.Models.Utileria;
using centro.recursos.net.Models.Entity_Framework;
using centro.recursos.net.Models.Repositorios;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;

namespace CodeConverter2HTML
{
    class Program
    {
        static string CadenaConexion
        {
            get
            {
                string amb = ConfigurationManager.AppSettings["ambiente"];
                amb = string.IsNullOrEmpty(amb) ? "dev" : amb;
                return $"GaroNETDbContexto_{amb}";
            }
        }

        static void Main(string[] args)
        {
            string code = PrepareCodeString(GetCode());

            code = ReplaceStrings(code);
            code = ReplaceAssemblyWords(code);
            code = ReplaceKeyReservedWords(code);
            code = CleanStrings(code);
            code = ReplaceComments(code);
            code = ReplaceBrackets(code);
            WriteCode(code);
        }

        static string PrepareCodeString(string code)
        {
            return code.Replace("<", "|").Replace(">", "*");
        }

        static string ReplaceKeyReservedWords(string inputString)
        {
            /* string[] keywords = { "int", "decimal", "float", "public",
                "string", "double", "short", "static", "namespace",
                "using", "false", "var", "try", "if", "else", "object", "null", "true", "catch", "return" }; */

            GaroNetDb contextoBD = new GaroNetDb(CadenaConexion);
            List<string> keywords = null;
            Respuesta<List<PalabraCodigo>> estado = contextoBD.ObtenPalabrasCodigo();
            if (estado.Estado)
            {
                keywords = estado.Resultado
                    .Where(palabra => palabra.CategoriaId == 1)
                    .Select(palabra => palabra.Nombre)
                    .ToList();
                foreach (var primitivo in keywords)
                {
                    inputString = Regex.Replace(inputString, $@"\b{primitivo}\b", $"<span class=\"reservada\">{primitivo}</span>");
                }
            }

            inputString = ReplaceClassWords(inputString, estado.Resultado);
            inputString = ReplaceKeyReservedWordsWithWhiteSkips(inputString, estado.Resultado);

            return inputString;
        }

        static string ReplaceKeyReservedWordsWithWhiteSkips(string inputString,
             List<PalabraCodigo> palabras)
        {
            if (palabras != null)
            {
                List<string> keywords = palabras
                    .Where(palabra => palabra.CategoriaId == 3)
                    .Select(palabra => palabra.Nombre)
                    .ToList();

                foreach (var primitivo in keywords)
                {
                    inputString = Regex.Replace(inputString, $@"( {primitivo} |^{primitivo} )",
                        match =>
                        {
                            return match.Value.
                                Replace(primitivo, $"<span class=\"reservada\">{primitivo}</span>");
                        });
                }
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

        static string ReplaceClassWords(string inputString, 
            List<PalabraCodigo> palabras)
        {
            // string[] classWords = { "Console", "Program", "Operaciones", "List", "Multimedia", "Respuesta"};
            GaroNetDb contextoBD = new GaroNetDb(CadenaConexion);
            List<string> classWords = null;
      
            if (palabras != null)
            {
                classWords = palabras
                    .Where(palabra => palabra.CategoriaId == 2)
                    .Select(palabra => palabra.Nombre)
                    .ToList();
                foreach (var primitivo in classWords)
                {
                    inputString = Regex.Replace(inputString, $@"\b{primitivo}\b",
                        $"<span class=\"class\">{primitivo}</span>");
                }
            }

            return inputString;
        }

        static string CleanStrings(string inputString)
        {
            inputString = Regex.Replace(inputString,
                "[^=]\"([^<>]*(<span class=\"\\w+\">\\w+</span>)+[^<>]*)+\"",
                match =>
                {
                    string matchWithOutSpan = Regex.Replace(match.Value,
                            "<span class=\"\\w+\">(?<value>.+)</span>",
                            match2 => match2.Value.Replace(match2.Value, match2.Groups["value"].Value)
                        );

                    return matchWithOutSpan;
                });

            inputString = Regex.Replace(inputString,
                  "(?<prevalue>[^=])(?<value>\".+\")",
                    match =>
                    {
                        string matchWithOutInter = Regex.Replace(match.Groups["value"].Value, "{(?<value>.+)}",
                                 match2 => match2.Value.Replace(match2.Value,
                                    $"<span class=\"normal\">{match2.Value}</span>")
                            );
                        return match.Groups["prevalue"].Value + matchWithOutInter;
                    });

            return inputString;
        }

        static string ReplaceStrings(string inputString)
        {
            inputString = Regex.Replace(inputString,
                  "(?<prevalue>[^=])(?<value>\".+\")",
                    match =>
                    {
                        return match.Groups["prevalue"].Value + match.Groups["value"].Value.
                            Replace(match.Groups["value"].Value,
                            $"<span class=\"strings\">{match.Groups["value"].Value}</span>");
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

        static string ReplaceBrackets(string inputString)
        {
            inputString = Regex
                .Replace(inputString, "\\*", "&gt;");
            inputString = Regex
                .Replace(inputString, "\\|", "&lt;");

            return inputString;
        }

        static string GetCode()
        {
            string code;
            try
            {
                using (FileStream streamCodeFile = new FileStream("Program 2.cs", FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader codeReader = new StreamReader(streamCodeFile))
                    {
                        code = codeReader.ReadToEnd();
                    }
                }
            }
            catch
            {
                code = null;
            }

            return code;
        }

        static bool WriteCode(string code)
        {
            string htmlTemplate =
                "<!DOCTYPE html>"+
 "<html>\r\n"+
 "<hea>\r\n" +
     "<title> Test visualizador </title>\r\n" +
        "<meta charset = \"UTF-8\" />\r\n" +
         "<style>\r\n" +
             ".reservada{\r\n" +
                "color: blue;\r\n" +
            "}\r\n" +
        ".assembly{\r\n" +
                "color: gray;\r\n" +
            "}.normal{color: black;}\r\n" +
        ".class{\r\n" +
            "color: darkcyan;\r\n" +
        "}\r\n" +
        ".coments{\r\n" +
            "color: forestgreen;\r\n" +
        "}\r\n" +
        ".strings {\r\n" +
            "color: orangered;\r\n" +
        "}\r\n" +
    "</style>\r\n" +
"</head>\r\n" +
"<body>\r\n" +
"<div>\r\n" +
"<pre>\r\n" + code + "</pre></div></body></html>";
                
            bool stateWriteProcess;
            try
            {
                if (File.Exists("Program 2.html")) File.Delete("Program 2.html");
                using (FileStream streamCodeFile = new FileStream("Program 2.html",
                FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter codeWritter = new StreamWriter(streamCodeFile))
                    {
                        codeWritter.WriteLine(htmlTemplate);
                        stateWriteProcess = true;
                    }
                }
            }
            catch
            {
                stateWriteProcess = false;
            }

            return stateWriteProcess;
        }
    }
}

