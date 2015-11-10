using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpPDFLabel.Labels.A4Labels.Avery
{
    /// <summary>
    /// Dimensions: 46mm x 11.1mm 
    /// Per Sheet: 84 per sheet 
    /// Inkjet code: J8657
    /// </summary>
    public class L7656 : LabelDefinition
    {        
        public L7656()
        {
            _Width = 46;
            _Height = 11.1;
            _HorizontalGapWidth = 4.7;
            _VerticalGapHeight = 1.6;

            _PageMarginTop = 15.9;
            _PageMarginBottom = 15.9;
            _PageMarginLeft = 6;
            _PageMarginRight = 6;

            PageSize = Enums.PageSize.A4;
            LabelsPerRow = 4;
            LabelRowsPerPage = 21;
        }
    }
}
