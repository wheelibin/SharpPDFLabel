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
    /// Contains the labels/PDF creation logic
    /// </summary>
    public class LabelCreator
    {
        private LabelDefinition _labelDefinition;
        private List<Label> _labels;

        /// <summary>
        /// Useful for debugging the formatting if needed
        /// </summary>
        public bool IncludeLabelBorders { get; set; }

        public LabelCreator(LabelDefinition label)
        {
			FontFactory.RegisterDirectories(); //Register all local fonts
			
            _labelDefinition = label;
            _labels = new List<Label>();
            IncludeLabelBorders = false;
        }

        /// <summary>
        /// Add a label to the collection
        /// </summary>
        /// <param name="label"></param>
        public void AddLabel(Label label)
        {
            _labels.Add(label);
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
            switch (_labelDefinition.PageSize)
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
                                   _labelDefinition.PageMarginLeft, 
                                   _labelDefinition.PageMarginRight, 
                                   _labelDefinition.PageMarginTop, 
                                   _labelDefinition.PageMarginBottom);

            //Create a stream to write the PDF to
            var output = new MemoryStream();

            //Creates the document tells the PdfWriter to use the output stream when Document.Close() is called
            var writer = PdfWriter.GetInstance(doc, output);

            //Ensure stream isn't closed when done - we need to return it
            writer.CloseStream = false;

            //Open the document to begin adding elements
            doc.Open();

            //Create a new table with label and gap columns
            var numOfCols = _labelDefinition.LabelsPerRow + (_labelDefinition.LabelsPerRow - 1);
           // var tbl = new PdfPTable(numOfCols);

            //Build the column width array, even numbered index columns will be gap columns
            var colWidths = new List<float>();
            for (int i = 1; i <= numOfCols; i++)
            {
                if (i % 2 > 0)
                {
                    colWidths.Add(_labelDefinition.Width);
                }
                else
                {
                    //Even numbered columns are gap columns
                    colWidths.Add(_labelDefinition.HorizontalGapWidth);
                }
            }

            /* The next 3 lines are the key to making SetWidthPercentage work */
            /* "size" specifies the size of the page that equates to 100% - even though the values passed are absolute not relative?! */
            /* (I will never get those 3 hours back) */
            var w = iTextSharp.text.PageSize.A4.Width - (doc.LeftMargin + doc.RightMargin);
            var h = iTextSharp.text.PageSize.A4.Height - (doc.TopMargin + doc.BottomMargin);
            var size = new iTextSharp.text.Rectangle(w, h);


            var cellsPerPage = _labelDefinition.LabelRowsPerPage * _labelDefinition.LabelsPerRow;
            // loop over the labels

            var rowNumber = 0;
            var colNumber = 0;


            PdfPTable tbl = null;
            List<PdfPCell> rowCells = null;
            foreach (var label in _labels)
            {
                if (rowNumber == 0)
                {
                    tbl = new PdfPTable(numOfCols);
                    tbl.SetWidthPercentage(colWidths.ToArray(), size);
                    rowCells = new List<PdfPCell>();
                    rowNumber = 1; // so we start with row 1
                }
                colNumber++; // so we start with col 1

                // add the label cell.
                var cell = label.GetLabelCell();

                //Ensure our label height is adhered to
                cell.FixedHeight = _labelDefinition.Height;

                //Centre align the content
                cell.HorizontalAlignment = Element.ALIGN_CENTER;

                cell.Border = IncludeLabelBorders ? Rectangle.BOX : Rectangle.NO_BORDER;

                //Add to the row
                rowCells.Add(cell);

                //Create a empty cell to use as a gap
                var gapCell = new PdfPCell();
                gapCell.FixedHeight = _labelDefinition.Height;
                gapCell.Border = Rectangle.NO_BORDER;
                //Add to the row
                rowCells.Add(gapCell);

                //On all but the last row, add a gap row if needed
                if ((rowNumber) < _labelDefinition.LabelRowsPerPage && _labelDefinition.VerticalGapHeight > 0)
                {
                    tbl.Rows.Add(CreateGapRow(numOfCols));
                }

                
                if (colNumber == _labelDefinition.LabelsPerRow)
                {
                    // add the row to the table and re-initialize
                    tbl.Rows.Add(new PdfPRow(rowCells.ToArray()));
                    rowCells = new List<PdfPCell>();

                    rowNumber++;
                    colNumber = 0;
                }

                
                if (rowNumber > _labelDefinition.LabelRowsPerPage)
                {
                    //Add the table to the document
                    doc.Add(tbl);
                    rowNumber = 0;
                }

            }

            // make sure the last table gets added to the document
            if (rowNumber > 0)
            {
                //Add the table to the document
                doc.Add(tbl);
            }

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
                cell.FixedHeight = _labelDefinition.VerticalGapHeight;
                cell.Border = Rectangle.NO_BORDER;
                cells.Add(cell);
			}
            return new PdfPRow(cells.ToArray());
        }

    }

}
