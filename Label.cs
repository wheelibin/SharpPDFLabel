using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpPDFLabel
{
    public class Label
    {
        private List<byte[]> _images;
        private List<TextChunk> _textChunks;
        private Enums.Alignment _hAlign;

        /// sets align to CENTER
        public Label()
            : this(Enums.Alignment.CENTER)
        {
        }

        /// <param name="hAlign">horizontal alignment: LEFT, CENTER, RIGHT</param>
        public Label(Enums.Alignment hAlign)
        {
            _images = new List<byte[]>();
            _textChunks = new List<TextChunk>();
            _hAlign = hAlign;
        }


        public PdfPCell GetLabelCell()
        {
            // Create a new Phrase and add the image to it
            var cellContent = new Phrase();

            foreach (var img in _images)
            {
                var pdfImg = iTextSharp.text.Image.GetInstance(img);
                cellContent.Add(new Chunk(pdfImg, 0, 0));
            }

            foreach (var txt in _textChunks)
            {
                var font = FontFactory.GetFont(txt.FontName, BaseFont.CP1250, txt.EmbedFont, txt.FontSize, txt.FontStyle);
                cellContent.Add(new Chunk("\n" + txt.Text, font));
            }

            //Create a new cell specifying the content
            var cell = new PdfPCell(cellContent);
            cell.HorizontalAlignment = (int)_hAlign;
            cell.VerticalAlignment = Element.ALIGN_TOP;

            return cell;
        }


        private void CopyStream(Stream input, Stream output)
        {
            byte[] b = new byte[32768];
            int r;
            while ((r = input.Read(b, 0, b.Length)) > 0)
                output.Write(b, 0, r);
        }

        /// <summary>
        /// Add an image to the labels
        /// Currently adds images and then text in that specific order
        /// </summary>
        /// <param name="img"></param>
        public void AddImage(Stream img)
        {
        }
        /// <summary>
        /// Add a chunk of text to the labels
        /// </summary>
        /// <param name="text">The text to add e.g "I am on a label"</param>
        /// <param name="fontName">The name of the font e.g. "Verdana"</param>
        /// <param name="fontSize">The font size in points e.g. 12</param>
        /// <param name="embedFont">If the font you are using may not be on the target machine, set this to true</param>
        /// <param name="fontStyles">An array of required font styles</param>
        public void AddText(string text, string fontName, int fontSize, bool embedFont = false, params Enums.FontStyle[] fontStyles)
        {
            int fontStyle = 0;
            if (fontStyles != null)
            {
                foreach (var item in fontStyles)
                {
                    fontStyle += (int)item;
                }
            }

            _textChunks.Add(new TextChunk() { Text = text, FontName = fontName, FontSize = fontSize, FontStyle = fontStyle, EmbedFont = embedFont });
        }

    }
}
