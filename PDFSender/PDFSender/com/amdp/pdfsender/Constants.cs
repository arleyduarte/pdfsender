using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;

namespace PDFSender.com.amdp.pdfsender
{
    public class Constants
    {

        public static String FONDO = "FONDO";
        public static String DESTINO = "DESTINO";
        public static String CCMail = "CC";
        public static String REMITE = "REMITE";
        public static String ASUNTO = "ASUNTO";
        public static String TEXTO = "TEXTO";
        public static String TAMANOLET = "TAMANO-LET";
        public static String ORIENT = "ORIENT";
        public static String NOM_ADJ = "NOM-ADJ";

        public static String HEADER_INDICATOR = ".";

        public static Font font = new Font(iTextSharp.text.Font.FontFamily.COURIER, 7);
        public static int DEFAULT_SIZE = 7;
        public static int DEFAULT_TOP_MARGIN = 72;
    }
}
