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

        public enum FontFamily
        {
            Arial
        }

        public enum LabelStyle
        {
            L5160,
            L5162,
            L5260,
            L5262,
            L5520,
            L5522,
            L5660,
            L5810,
            L5960,
            L5962,
            L5970,
            L5971,
            L5972,
            L5979,
            L5980,
            L7160,
            L7654,
            L7656,
            L8160,
            L8162,
            L8252,
            L8460,
            L8462,
            L8660,
            L8662,
            L8810
        }
    }
}
