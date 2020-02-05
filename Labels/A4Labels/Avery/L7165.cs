using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpPDFLabel.Labels.A4Labels.Avery
{
    public class L7165 : Label
    {
        public L7165()
        {
            _Width = 100;
            _Height = 67;
            _HorizontalGapWidth = 2.5;
            _VerticalGapHeight = 0;

            _PageMarginTop = 13.1;
            _PageMarginBottom = 13.1;
            _PageMarginLeft = 4.65;
            _PageMarginRight = 4.65;

            PageSize = Enums.PageSize.A4;
            LabelsPerRow = 2;
            LabelRowsPerPage = 4;
        }
    }
}
