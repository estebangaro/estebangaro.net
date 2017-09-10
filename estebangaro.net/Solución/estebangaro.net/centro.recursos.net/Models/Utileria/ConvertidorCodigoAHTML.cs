using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using centro.recursos.net.Models.Entity_Framework;
using System.Text.RegularExpressions;
using System.Text;

namespace centro.recursos.net.Models.Utileria
{
    public class PalabrasCodigoExepcion: Exception
    {
        public PalabrasCodigoExepcion(string nombreMetodo): 
            base("No se ha proporcionado una colección de palabras código")
        {
            Source = nombreMetodo;
        }
    }

    public class ConvertidorCodigoAHTML
    {
         #region Restricciones

        // ** Restricciones de caracteres dentro del código fuente a convertir **
        // No incluir dentro de cadenas literales caracteres de ", <, >, + 
        // No utilizar caracteres de | y * en cualquier parte del código.
        // No se admiten comentarios "multilinea".

        #endregion

        private List<PalabraCodigo> palabrasCodigo;
        public ConvertidorCodigoAHTML(List<PalabraCodigo> palabrasCodigo)
        {
            if(palabrasCodigo != null)
                this.palabrasCodigo = palabrasCodigo;
            else
                throw new PalabrasCodigoExepcion("Utileria.ConvertidorCodigoAHTML(List<PalabraCodigo>)");
        }

        public ConvertidorCodigoAHTML(List<PalabraCodigo> palabrasCodigo, 
            List<string> clasesPersonalizadas): this(palabrasCodigo)
        {
            if (clasesPersonalizadas != null)
                palabrasCodigo.AddRange(clasesPersonalizadas.
                    Select(cadena => new PalabraCodigo
                    {
                        CategoriaId = 2,
                        Nombre = cadena
                    }));
        }

        private string PrepareCodeString(string code)
        {
            return code.Replace("<", "|").Replace(">", "*");
        }

        private string ReplaceKeyReservedWords(string inputString)
        {
            List<string> keywords = null;
                keywords = palabrasCodigo
                    .Where(palabra => palabra.CategoriaId == 1)
                    .Select(palabra => palabra.Nombre)
                    .ToList();
                foreach (var primitivo in keywords)
                {
                    inputString = Regex.Replace(inputString, $@"\b{primitivo}\b",
                        $"<span class=\"reservada\">{primitivo}</span>");
                }

            inputString = ReplaceClassWords(inputString);
            inputString = ReplaceKeyReservedWordsWithWhiteSkips(inputString);

            return inputString;
        }

        private string ReplaceKeyReservedWordsWithWhiteSkips(string inputString)
        {
                List<string> keywords = palabrasCodigo
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

            return inputString;
        }

        private string ReplaceAssemblyWords(string inputString)
        {
            int count = 0;
            var matches = Regex.Matches(inputString, @"using (?<ensamblado>\w+[\.\w]*);");
            foreach (Match match in matches)
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

        private string ReplaceClassWords(string inputString)
        {
            List<string> classWords = null;

                classWords = palabrasCodigo
                    .Where(palabra => palabra.CategoriaId == 2)
                    .Select(palabra => palabra.Nombre)
                    .ToList();
                foreach (var primitivo in classWords)
                {
                    inputString = Regex.Replace(inputString, $@"\b{primitivo}\b",
                        $"<span class=\"class\">{primitivo}</span>");
                }

            return inputString;
        }

        private string CleanStrings(string inputString)
        {
            /* inputString = Regex.Replace(inputString,
                "[^=]\"([^<>]*(<span class=\"\\w+\">\\w+</span>)+[^<>]*)+\"",
                match =>
                {
                    string matchWithOutSpan = Regex.Replace(match.Value,
                            "<span class=\"\\w+\">(?<value>.+)</span>",
                            match2 => match2.Value.Replace(match2.Value, match2.Groups["value"].Value)
                        );

                    return matchWithOutSpan;
                }); */

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

        private string ReplaceStrings(string inputString)
        {
            inputString = Regex.Replace(inputString,
                  "(?<prevalue>[^=])(?<value>\"[^\\+\"]+\")",
                    match =>
                    {
                        return match.Groups["prevalue"].Value + match.Groups["value"].Value.
                            Replace(match.Groups["value"].Value,
                            $"<span class=\"strings\">{match.Groups["value"].Value}</span>");
                    });

            return inputString;
        }

        private string ReplaceComments(string inputString)
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

        private string ReplaceBrackets(string inputString)
        {
            inputString = Regex
                .Replace(inputString, "\\*", "&gt;");
            inputString = Regex
                .Replace(inputString, "\\|", "&lt;");

            return inputString;
        }

        private string ConvertCode2HTML(string code)
        {
            code = ReplaceStrings(code);
            code = ReplaceAssemblyWords(code);
            code = ReplaceKeyReservedWords(code);
            code = CleanStrings(code);
            code = ReplaceComments(code);
            code = ReplaceBrackets(code);

            return code;
        }

        public string ObtenHTML(string codigoFuente,
            LENGUAJE_CODIGO codigoSintaxis = LENGUAJE_CODIGO.CSHARP)
        {
            if (codigoSintaxis == LENGUAJE_CODIGO.CSHARP)
            {
                codigoFuente = PrepareCodeString(codigoFuente);

                return ConvertCode2HTML(codigoFuente);
            }
            throw new NotImplementedException($"La sintaxis de código {codigoSintaxis.ToString()} " +
                $"no esta implementada");
        }
    }
}