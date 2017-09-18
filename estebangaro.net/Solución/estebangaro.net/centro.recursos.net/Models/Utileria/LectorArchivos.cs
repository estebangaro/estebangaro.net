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

        public static byte[] ObtenContenidoBinario(string rutaArchivo)
        {
            byte[] code;
            try
            {
                using (FileStream streamCodeFile =
                    new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read))
                {
                    code = new byte[streamCodeFile.Length];
                    streamCodeFile.Read(code, 0, code.Length);
                }
            }
            catch
            {
                code = null;
            }

            return code;
        }

        public static string ObtenNombreCodigo(string rutaArchivo)
        {
            string nombre;
            try
            {
                FileInfo inforArchivo = new FileInfo(rutaArchivo);
                string[] nombreInfo =
                    rutaArchivo.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                if (nombreInfo.Length == 3)
                {
                    nombre = $"{nombreInfo[1]}.{nombreInfo[2].Split('.')[0]}";
                }
                else
                {
                    nombre = "formato.desconocido";
                }
            }
            catch (Exception)
            {
                nombre = "nombre.desconocido";
            }

            return nombre;
        }
    }
}