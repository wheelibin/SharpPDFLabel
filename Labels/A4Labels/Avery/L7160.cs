using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpPDFLabel.Labels.A4Labels.Avery
{
    /// <summary>
    /// Dimensions: 63.5mm x 38.1mm 
    /// Per Sheet: 21 per sheet 
    /// Inkjet code: J8160
    /// </summary>
    public class L7160 : Label
    {
        public L7160()
        {
            _Width = 63.5;
            _Height = 38.1;
            _HorizontalGapWidth = 2.5;
            _VerticalGapHeight = 0;
            
            _PageMarginTop = 15.1;
            _PageMarginBottom = 15.1;
            _PageMarginLeft = 7.2;
            _PageMarginRight = 7.2;

            PageSize = Enums.PageSize.A4;
            LabelsPerRow = 3;
            LabelRowsPerPage = 7;
        }
    }
}
