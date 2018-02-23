using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpPDFLabel
{
    public static class Helpers
    {
        public static Label GetLabel(Enums.LabelStyle LabelType)
        {
            switch (LabelType)
            {
                case Enums.LabelStyle.L7656:
                    return new SharpPDFLabel.Labels.A4Labels.Avery.L7656();
                case Enums.LabelStyle.L7654:
                    return new SharpPDFLabel.Labels.A4Labels.Avery.L7654();
                case Enums.LabelStyle.L7160:
                    return new SharpPDFLabel.Labels.A4Labels.Avery.L7160();
                case Enums.LabelStyle.L5162:
                case Enums.LabelStyle.L5262:
                case Enums.LabelStyle.L5522:
                case Enums.LabelStyle.L5962:
                case Enums.LabelStyle.L8162:
                case Enums.LabelStyle.L8252:
                case Enums.LabelStyle.L8462:
                case Enums.LabelStyle.L8662:
                    return new SharpPDFLabel.Labels.A4Labels.Avery.L5162();
                case Enums.LabelStyle.L5160:
                case Enums.LabelStyle.L5260:
                case Enums.LabelStyle.L5520:
                case Enums.LabelStyle.L5660:
                case Enums.LabelStyle.L5810:
                case Enums.LabelStyle.L5960:
                case Enums.LabelStyle.L5970:
                case Enums.LabelStyle.L5971:
                case Enums.LabelStyle.L5972:
                case Enums.LabelStyle.L5979:
                case Enums.LabelStyle.L5980:
                case Enums.LabelStyle.L8160:
                case Enums.LabelStyle.L8460:
                case Enums.LabelStyle.L8660:
                case Enums.LabelStyle.L8810:
                    return new SharpPDFLabel.Labels.A4Labels.Avery.L5160();
                default: return null;
            }
        }
    }
}
