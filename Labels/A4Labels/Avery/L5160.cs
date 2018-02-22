using System;
using System.Collections.Generic;
using System.Text;

namespace SharpPDFLabel.Labels.A4Labels.Avery
{
    /// <summary>
    /// Dimensions: 66.675mm x 25.4
    /// Per Sheet: 30
    /// </summary>
    public class L5160 : Label
    {
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

    public class L5260 : L5160 { }
    public class L5520 : L5160 { }
    public class L5660 : L5160 { }
    public class L5810 : L5160 { }
    public class L5960 : L5160 { }
    public class L5970 : L5160 { }
    public class L5971 : L5160 { }
    public class L5972 : L5160 { }
    public class L5979 : L5160 { }
    public class L5980 : L5160 { }
    public class L8160 : L5160 { }
    public class L8460 : L5160 { }
    public class L8660 : L5160 { }
    public class L8810 : L5160 { }
}
