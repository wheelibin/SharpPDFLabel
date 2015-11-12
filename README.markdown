About
=====

### This (C#) library allows you to create sheets of labels (such as Avery Labels) as a PDF using .NET

There are two ways to use this library. The first is to create sheets of identical labels. The second
lets you specify each individual label that will be injected into the sheet(s)

The code uses iTextSharp to create the PDFs. You can reference iTextSharp using NuGet - I advise you to avoid sourceforge


### Table of Contents
* <a href="#sameLabels">Single Sheet of Repeated Identical Labels</a>
* <a href="#uniqueLabels">Sheet(s) of Individual Labels</a>
* <a href="#definingLabelDefs">Create New Label Definitions</a>


 

#### Usage Case 1 - Single Sheet of Repeated Identical Labels  <a name="sameLabels"></a>
```cs

	// Create the required labelDefinition
	var label = new SharpPDFLabel.Labels.A4Labels.Avery.L7654();

	// Create a SingleSheetLabelCreator, passing the required label
	var singleSheetLabelCreator = new SharpPDFLabel.SingleSheetLabelCreator(label);

	//add images to all the labels on the sheet
	singleSheetLabelCreator.AddImage(myImageAsAStream);

	//add text too all labels on the sheet
	singleSheetLabelCreator.AddText("Some Text", "Verdana", 12, embedFont: true);
	singleSheetLabelCreator.AddText("Some more text with bold and underlined text", "Verdana", 12, true, SharpPDFLabel.Enums.FontStyle.BOLD, SharpPDFLabel.Enums.FontStyle.UNDERLINE);

	//Create the PDF as a stream
	var pdfStream = singleSheetLabelCreator.CreatePDF();

	//Do something with it!
	Response.AddHeader("Content-Disposition", "attachment; filename=sheet_of_labels.pdf");
	return new FileStreamResult(pdfStream, "application/pdf");
```

**NOTE**: I rewrote the way the original use case (1) works behind the scense but the API for it remains unchanged to stay backward compatible with anyone using the source project.

I did make one optional change though - by default the labels generated this way center everything. You can optionally specify the horizontal alignment like so:

```cs

	// Create a SingleSheetLabelCreator, passing the required label
	var singleSheetLabelCreator = new SharpPDFLabel.SingleSheetLabelCreator(label, Enums.Alignment.LEFT);
	
```




#### Usage Case 2 - Sheet(s) of Individual Labels  <a name="uniqueLabels"></a>
```cs

	// Create the required label
	var labelDefinition = new SharpPDFLabel.Labels.A4Labels.Avery.L5160();

	// Create a CustomLabelCreator, passing the required label
	var customLabelCreator = new SharpPDFLabel.CustomLabelCreator(label);

	//Add content to the labels
	... 
		some IEnumerable collection was created...
	...


    foreach (var person in personCollection)
    {
		// you can specify LEFT, RIGHT, or CENTER for the horizontal alignment during Label construction
        var label = new Label(Enums.Alignment.LEFT);
        label.AddText(person.fullName, "Verdana", 12, embedFont: true);
        label.AddText(person.address.address1, "Verdana", 12, embedFont: true);
        label.AddText(person.address.address2, "Verdana", 12, embedFont: true);
        label.AddText(person.address.city + ", " + person.address.stateCode + " " + person.address.zipCode, "Verdana", 12, embedFont: true);
        customLabelCreator.AddLabel(label);
    }

	//Create the PDF as a stream
	var pdfStream = customLabelCreator.CreatePDF();

	//Do something with it!
	Response.AddHeader("Content-Disposition", "attachment; filename=address_labels.pdf");
	return new FileStreamResult(pdfStream, "application/pdf");
```

#### Create New Label Definitions<a name="definingLabelDefs"></a>
A label definition is a class that provides the layout specifications for a label. Extra label definitions are simply added by deriving from the `LabelDefinition` class like so:

(all dimensions and margins are specified in mm)
```cs
	namespace SharpPDFLabel.Labels.A4Labels.Avery
	{
		/// <summary>
		/// Dimensions: 63.5mm x 38.1mm 
		/// Per Sheet: 21 per sheet 
		/// Inkjet code: J8160
		/// </summary>
		public class L7160 : LabelDefinition
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
```

