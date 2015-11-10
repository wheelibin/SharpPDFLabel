using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpPDFLabel.Labels.A4Labels.Avery;
using System.IO;

namespace SharpPDFLabel.Tests
{
    [TestClass]
    public class SingleSheetLabelCreatorTests
    {
        [TestMethod]
        public void TestCreateSingleSheetPDF()
        {

            var labelDefinition = new L7654();
            var labelCreator = new SingleSheetLabelCreator(labelDefinition);
            labelCreator.IncludeLabelBorders = true;
            labelCreator.AddText("WEBBERFUL!", "Verdana", 12, embedFont: true);
            labelCreator.AddText("Wonderful Web Works", "Verdana", 12, embedFont: true);


            var pdfStream = labelCreator.CreatePDF();
            var pdfName = "pdf7654.pdf";

            var fileStream = File.Create(@".\"+pdfName);
            pdfStream.CopyTo(fileStream);
            fileStream.Close();
            pdfStream.Close();

            // yeah, lame test
            Assert.IsTrue(File.Exists(@".\" + pdfName));


            // I comment this out to look at the pdf..
            // how would you test this?
            File.Delete(@".\" + pdfName);

        }
    }
}
