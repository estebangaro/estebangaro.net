using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeConverter2HTML
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = PrepareCodeString(GetCode());

            code = ReplaceAssemblyWords(code);
            code = ReplaceKeyReservedWords(code);
            code = ReplaceClassWords(code);
            code = ReplaceKeyReservedWordsWithWhiteSkips(code);
            code = ReplaceStrings(code);
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
            string[] keywords = { "int", "decimal", "float", "public",
                "string", "double", "short", "static", "namespace",
                "using", "false", "var", "try", "if", "else", "object", "null", "true", "catch", "return" };
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
            string[] classWords = { "Console", "Program", "Operaciones", "List", "Multimedia", "Respuesta"};
            foreach (var primitivo in classWords)
            {
                inputString = Regex.Replace(inputString, $@"\b{primitivo}\b", 
                    $"<span class=\"class\">{primitivo}</span>");
            }

            return inputString;
        }

        static string ReplaceStrings(string inputString)
        {
            inputString = Regex.Replace(inputString, "[^=]\"[\\w \\.ÁÉÍÓÚáéíóú:{}@\\(\\)\\*\\|]+\"",
                    match =>
                    {
                        string matchWithOutSpan = Regex.Replace(match.Value, "<span class=\"\\w+\">(?<value>.+)</span>",
                                match2 => match2.Value.Replace(match2.Value, match2.Groups["value"].Value)
                            );
                        string matchWithOutInter = Regex.Replace(matchWithOutSpan, "{(?<value>.+)}",
                                 match2 => match2.Value.Replace(match2.Value, 
                                    $"<span class=\"normal\">{match2.Value}</span>")
                            );
                        return matchWithOutInter.Replace(matchWithOutInter,
                            $"<span class=\"strings\">{matchWithOutInter}</span>");
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

