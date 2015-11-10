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
        private LabelDefinition _label;
        private List<byte[]> _images;
        private List<TextChunk> _textChunks;

        /// <summary>
        /// Useful for debugging the formatting if needed
        /// </summary>
        public bool IncludeLabelBorders { get; set; }

        public SingleSheetLabelCreator(LabelDefinition label)
        {
			FontFactory.RegisterDirectories(); //Register all local fonts
			
            _label = label;
            _images = new List<byte[]>();
            _textChunks = new List<TextChunk>();
            IncludeLabelBorders = false;
        }

        /// <summary>
        /// Add an image to the labels
        /// Currently adds images and then text in that specific order
        /// </summary>
        /// <param name="img"></param>
        public void AddImage(Stream img)
        {
            var mem = new MemoryStream();
            CopyStream(img, mem);
            _images.Add(mem.GetBuffer());
        }
        private void CopyStream(Stream input, Stream output)
        {
            byte[] b = new byte[32768];
            int r;
            while ((r = input.Read(b, 0, b.Length)) > 0)
                output.Write(b, 0, r);
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
        
        
        /// <summary>
        /// Create the PDF using the defined page size, label type and content provided
        /// Ensure you have added something first using either AddImage() or AddText()
        /// </summary>
        /// <returns></returns>
        public Stream CreatePDF()
        {

            //Get the itext page size
            Rectangle pageSize;
            switch (_label.PageSize)
            {
                case Enums.PageSize.A4:
                    pageSize = iTextSharp.text.PageSize.A4;
                    break;
                default:
                    pageSize = iTextSharp.text.PageSize.A4;
                    break;
            }

            //Create a new iText document object, define the paper size and the margins required
            var doc = new Document(pageSize, 
                                   _label.PageMarginLeft, 
                                   _label.PageMarginRight, 
                                   _label.PageMarginTop, 
                                   _label.PageMarginBottom);

            //Create a stream to write the PDF to
            var output = new MemoryStream();

            //Creates the document tells the PdfWriter to use the output stream when Document.Close() is called
            var writer = PdfWriter.GetInstance(doc, output);

            //Ensure stream isn't closed when done - we need to return it
            writer.CloseStream = false;

            //Open the document to begin adding elements
            doc.Open();

            //Create a new table with label and gap columns
            var numOfCols = _label.LabelsPerRow + (_label.LabelsPerRow - 1);
            var tbl = new PdfPTable(numOfCols);

            //Build the column width array, even numbered index columns will be gap columns
            var colWidths = new List<float>();
            for (int i = 1; i <= numOfCols; i++)
            {
                if (i % 2 > 0)
                {
                    colWidths.Add(_label.Width);
                }
                else
                {
                    //Even numbered columns are gap columns
                    colWidths.Add(_label.HorizontalGapWidth);
                }
            }

            /* The next 3 lines are the key to making SetWidthPercentage work */
            /* "size" specifies the size of the page that equates to 100% - even though the values passed are absolute not relative?! */
            /* (I will never get those 3 hours back) */
            var w = iTextSharp.text.PageSize.A4.Width - (doc.LeftMargin + doc.RightMargin);
            var h = iTextSharp.text.PageSize.A4.Height - (doc.TopMargin + doc.BottomMargin);
            var size = new iTextSharp.text.Rectangle(w, h);

            //Set the column widths (in points) - take note of the size parameter mentioned above
            tbl.SetWidthPercentage(colWidths.ToArray(), size);

            //Create the rows for the table
            for (int iRow = 0; iRow < _label.LabelRowsPerPage; iRow++)
            {

                var rowCells = new List<PdfPCell>();

                //Build the row - even numbered index columns are gaps
                for (int iCol = 1; iCol <= numOfCols; iCol++)
                {
                    if (iCol % 2 > 0)
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

                        //Ensure our label height is adhered to
                        cell.FixedHeight = _label.Height;

                        //Centre align the content
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;

                        cell.Border = IncludeLabelBorders ? Rectangle.BOX : Rectangle.NO_BORDER;

                        //Add to the row
                        rowCells.Add(cell);
                    }
                    else
                    {
                        //Create a empty cell to use as a gap
                        var gapCell = new PdfPCell();
                        gapCell.FixedHeight = _label.Height;
                        gapCell.Border = Rectangle.NO_BORDER;
                        //Add to the row
                        rowCells.Add(gapCell);
                    }
                }

                //Add the new row to the table
                tbl.Rows.Add(new PdfPRow(rowCells.ToArray()));

                //On all but the last row, add a gap row if needed
                if ((iRow + 1) < _label.LabelRowsPerPage && _label.VerticalGapHeight > 0)
                {
                    tbl.Rows.Add(CreateGapRow(numOfCols));
                }

            }

            //Add the table to the document
            doc.Add(tbl);

            //Close the document, writing to the stream we specified earlier
            doc.Close();

            //Set the stream back to position 0 so we can use it when it's returned
            output.Position = 0;

            return output;

        }

        private PdfPRow CreateGapRow(int numOfCols)
        {
            var cells = new List<PdfPCell>();

            for (int i = 0; i < numOfCols; i++)
			{
                var cell = new PdfPCell();
                cell.FixedHeight = _label.VerticalGapHeight;
                cell.Border = Rectangle.NO_BORDER;
                cells.Add(cell);
			}
            return new PdfPRow(cells.ToArray());
        }

    }

}
