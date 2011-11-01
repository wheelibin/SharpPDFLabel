About
=====

### This library allows you to create sheets of identical labels (such as Avery labels) using .NET which get created as a PDF document.

The code uses iTextSharp to create the PDFs (http://sourceforge.net/projects/itextsharp/).

Usage
=====

	// Create the required label
	var label = new SharpPDFLabel.Labels.A4Labels.Avery.L7654();

	// Create a LabelCreator, passing the required label
	var labelCreator = new SharpPDFLabel.LabelCreator(label);

	//Add content to the labels

	//images
	labelCreator.AddImage(myImageAsAStream);

	//text
	labelCreator.AddText("Some Text", "Verdana", 12, embedFont: true);
	labelCreator.AddText("Some more text with bold and underlined text", "Verdana", 12, true, SharpPDFLabel.Enums.FontStyle.BOLD, SharpPDFLabel.Enums.FontStyle.UNDERLINE);


	//Create the PDF as a stream
	var pdfStream = labelCreator.CreatePDF();


	//Do something with it!
	Response.AddHeader("Content-Disposition", "attachment; filename=sheet_of_labels.pdf");
	return new FileStreamResult(pdfStream, "application/pdf");


Adding more labels
==================

Extra labels are simply added by deriving from the `Label` class like so:

(all dimensions and margins are specified in mm)

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


