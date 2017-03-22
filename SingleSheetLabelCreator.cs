using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SharpPDFLabel
{
    /// <summary>
    /// Contains the labels/PDF creation logic.  Will create a single sheet of the same label over and over again.
    /// </summary>
    public class SingleSheetLabelCreator
    {
        private LabelDefinition _labelDefinition;
        private CustomLabelCreator _creator;
        private Label _label;

        /// <summary>
        /// Useful for debugging the formatting if needed
        /// </summary>
        public bool IncludeLabelBorders { get; set; }

        public SingleSheetLabelCreator(LabelDefinition labelDefinition, Enums.Alignment hAlign)
        {
			FontFactory.RegisterDirectories(); //Register all local fonts
            _labelDefinition = labelDefinition;
            _creator = new CustomLabelCreator(labelDefinition);
            _label = new Label(hAlign);
            IncludeLabelBorders = false;
        }

        public SingleSheetLabelCreator(LabelDefinition labelDefinition) : this(labelDefinition, Enums.Alignment.CENTER)
        {
        }

        /// <summary>
        /// Add an image to the labels
        /// Currently adds images and then text in that specific order
        /// </summary>
        /// <param name="img"></param>
        public void AddImage(Stream img)
        {
            _label.AddImage(img);
        }
        

        /// <summary>
        /// Add a chunk of text to the labels
        /// </summary>
        /// <param name="text">The text to add e.g "I am on a label"</param>
        /// <param name="fontName">The name of the font e.g. "Verdana"</param>
        /// <param name="fontSize">The font size in points e.g. 12</param>
        /// <param name="embedFont">If the font you are using may not be on the target machine, set this to true</param>
        /// <param name="fontStyles">An array of required font styles</param>
        public void AddText(string text, string fontName, int fontSize, bool embedFont = false, params Enums.FontStyle[] fontStyles )
        {
            _label.AddText(text, fontName, fontSize, embedFont, fontStyles);
        }
        
        
        /// <summary>
        /// Create the PDF using the defined page size, label type and content provided
        /// Ensure you have added something first using either AddImage() or AddText()
        /// </summary>
        /// <returns></returns>
        public Stream CreatePDF()
        {

            var cellCount = _labelDefinition.LabelRowsPerPage * _labelDefinition.LabelsPerRow;
            for (var i = 1; i <= cellCount; i++)
            {
                _creator.AddLabel(_label);
            }
            return _creator.CreatePDF();

        }


    }

}
