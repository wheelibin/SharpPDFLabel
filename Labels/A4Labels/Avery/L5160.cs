using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpPDFLabel.Labels.A4Labels.Avery
{
    public class L5160 : LabelDefinition
    {
        // all sizes are in mm
        public L5160()
        {
            _Width = 66.675;
            _Height = 25.4;
            _HorizontalGapWidth = 3.556;
            _VerticalGapHeight = 0;

            _PageMarginTop = 12.7;
            _PageMarginBottom = 12.7;
            _PageMarginLeft = 5.58165;
            _PageMarginRight = 5.58165;

            PageSize = Enums.PageSize.A4;
            LabelsPerRow = 3;
            LabelRowsPerPage = 10;
        }
    }
}
