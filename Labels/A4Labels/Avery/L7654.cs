using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpPDFLabel.Labels.A4Labels.Avery
{
    /// <summary>
    /// Dimensions: 45.7mm x 25.4mm 
    /// Per Sheet: 40 per sheet 
    /// Inkjet code: J8654
    /// </summary>
    public class L7654 : LabelDefinition
    {
        public L7654()
        {
            _Width = 45.7;
            _Height = 25.4;
            _HorizontalGapWidth = 2.6;
            _VerticalGapHeight = 0;
            
            _PageMarginTop = 21.4;
            _PageMarginBottom = 21.4;
            _PageMarginLeft = 9.7;
            _PageMarginRight = 9.7;

            PageSize = Enums.PageSize.A4;
            LabelsPerRow = 4;
            LabelRowsPerPage = 10;
        }
    }
}
