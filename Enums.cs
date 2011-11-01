using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpPDFLabel
{
    public class Enums
    {
        /// <summary>
        /// The page size of the document
        /// See the Switch statement in LabelCreator.CreatePDF() if you add a new one
        /// </summary>
        public enum PageSize
        {
            A4
        }

        /// <summary>
        /// The possible styles for a font
        /// Taken straight from iTextSharp
        /// </summary>
        [Flags]
        public enum FontStyle
        {
            BOLD = 1,
            BOLDITALIC = 3,
            DEFAULTSIZE = 12,
            ITALIC = 2,
            NORMAL = 0,
            STRIKETHRU = 8,
            UNDEFINED = -1,
            UNDERLINE = 4,
        }

        

    }
}
