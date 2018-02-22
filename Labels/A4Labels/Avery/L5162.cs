using System;
using System.Collections.Generic;
using System.Text;

namespace SharpPDFLabel.Labels.A4Labels.Avery
{
    /// <summary>
    /// Dimensions: 66.675mm x 25.4
    /// Per Sheet: 30
    /// </summary>
    public class L5162 : Label
    {
        /// <summary>
        /// Dimensions: 101.6mm x 33.782mm 
        /// Per Sheet: 14
        /// </summary>
        public L5162()
        {
            _Width = 101.6;
            _Height = 33.782;
            _HorizontalGapWidth = 3.96875;
            _VerticalGapHeight = 0;

            _PageMarginTop = 22.352;
            _PageMarginBottom = 22.352;
            _PageMarginLeft = 3.8735;
            _PageMarginRight = 3.8735;

            PageSize = Enums.PageSize.A4;
            LabelsPerRow = 2;
            LabelRowsPerPage = 7;
        }
    }

    public class L5262 : L5162 { }
    public class L5522 : L5162 { }
    public class L5962 : L5162 { }
    public class L8162 : L5162 { }
    public class L8252 : L5162 { }
    public class L8462 : L5162 { }
    public class L8662 : L5162 { }
}
