using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace centro.recursos.net.Models.Utileria
{
    public class LectorArchivos
    {
        public static string ObtenContenidoCadena(string rutaArchivo)
        {
            string code;
            try
            {
                using (FileStream streamCodeFile = 
                    new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read))
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
    }
}