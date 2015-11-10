using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpPDFLabel
{
    /// <summary>
    /// The base class for a Label, all the actual label definition classes derive from this
    /// Dimensions and margins are defined in mm but converted to Points when read back (for use in iTextSharp)
    /// </summary>
    public abstract class LabelDefinition
    {
        /// <summary>
        /// The width of 1 label
        /// </summary>
        protected double _Width;
        public float Width
        {
            get { return mmToPoint(_Width); }
        }

        /// <summary>
        /// The height of 1 label
        /// </summary>
        protected double _Height;
        public float Height
        {
            get { return mmToPoint(_Height); }
        }

        /// <summary>
        /// The width of the horizontal gap between labels
        /// </summary>
        protected double _HorizontalGapWidth;
        public float HorizontalGapWidth
        {
            get { return mmToPoint(_HorizontalGapWidth); }
        }

        /// <summary>
        /// The height of the vertical gap between labels
        /// </summary>
        protected double _VerticalGapHeight;
        public float VerticalGapHeight
        {
            get { return mmToPoint(_VerticalGapHeight); }
        }

        /// <summary>
        /// The left page margin
        /// </summary>
        protected double _PageMarginLeft;
        public float PageMarginLeft
        {
            get { return mmToPoint(_PageMarginLeft); }
        }

        /// <summary>
        /// The right page margin
        /// </summary>
        protected double _PageMarginRight;
        public float PageMarginRight
        {
            get { return mmToPoint(_PageMarginRight); }
        }

        /// <summary>
        /// The top page margin
        /// </summary>
        protected double _PageMarginTop;
        public float PageMarginTop
        {
            get { return mmToPoint(_PageMarginTop); }
        }

        /// <summary>
        /// The bottom page margin
        /// </summary>
        protected double _PageMarginBottom;
        public float PageMarginBottom
        {
            get { return mmToPoint(_PageMarginBottom); }
        }

        /* page definitions */
        
        /// <summary>
        /// The paper size
        /// </summary>
        public Enums.PageSize PageSize { get; set; }
        
        
        /// <summary>
        /// The number of labels running across the page
        /// </summary>
        public int LabelsPerRow { get; set; }
        
        
        /// <summary>
        /// The number of labels running down the page
        /// </summary>
        public int LabelRowsPerPage { get; set; }


        /// <summary>
        /// iTextSharp uses points as its units
        /// </summary>
        /// <param name="mm">Millimetres to convert</param>
        /// <returns>millimetres converted to points represented by a float</returns>
        private float mmToPoint(double mm)
        {
            return (float)((mm / 25.4) * 72);
        }

    }
}
