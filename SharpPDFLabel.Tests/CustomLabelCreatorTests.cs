using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpPDFLabel.Labels.A4Labels.Avery;
using System.IO;

namespace SharpPDFLabel.Tests
{
    [TestClass]
    public class CustomLabelCreatorTests
    {
        [TestMethod]
        public void TestCreateAPdf()
        {

            var labelDefinition = new L5160();
            var labelCreator = new SharpPDFLabel.CustomLabelCreator(labelDefinition);
            labelCreator.IncludeLabelBorders = true;

            for (var i = 1; i <= 30; i++)
            {
                var label = new Label(Enums.Alignment.LEFT);
                label.AddText("Person Name " + i.ToString(), "Verdana", 12, embedFont: true);
                label.AddText("Address one for " + i.ToString(), "Verdana", 12, embedFont: true);
                label.AddText("Address two for " + i.ToString(), "Verdana", 12, embedFont: true);
                label.AddText("City, State ZIPCODE " + i.ToString(), "Verdana", 12, embedFont: true);
                labelCreator.AddLabel(label);
            }


            var pdfStream = labelCreator.CreatePDF();

            var fileStream = File.Create(@".\pdf5160.pdf");
            pdfStream.CopyTo(fileStream);
            fileStream.Close();
            pdfStream.Close();

            // yeah, lame test
            Assert.IsTrue(File.Exists(@".\pdf5160.pdf"));


            // I comment this out to look at the pdf..
            // how would you test this?
            File.Delete(@".\pdf5160.pdf");

        }
    }
}
