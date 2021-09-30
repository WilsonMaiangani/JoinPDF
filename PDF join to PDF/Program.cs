using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace PDF_join_to_PDF
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (args.Length == 0 || args[0] == "-h" || args[0] == "/h")
            //{
            //    Console.WriteLine("Welcome to MergeSpeedPASS. Created by Mladen Prajdic. Uses iTextSharp 5.4.5.0.");
            //    Console.WriteLine("Tool to create a single SpeedPASS PDF from all downloaded generated PDFs.");
            //    Console.WriteLine("");
            //    Console.WriteLine("Example: MergeSpeedPASS.exe targetFileName sourceDir");
            //    Console.WriteLine("         targetFileName = name of the new merged PDF file. Must include .pdf extension.");
            //    Console.WriteLine("         sourceDir      = path to the dir containing downloaded attendee SpeedPASS PDFs");
            //    Console.WriteLine("");
            //    Console.WriteLine(@"Example: MergeSpeedPASS.exe MergedSpeedPASS.pdf d:\Downloads\SQLSaturdaySpeedPASSFiles");
            //}
            //else if (args.Length == 2)
            //    CreateMergedPDF("join.pdf", @"C:\Users\alifa\Downloads\PDF teste\A realização do Sonho.pdf");

            CreateMergedPDF("join.pdf", @"C:\Users\alifa\Downloads\PDF teste\");

            Console.WriteLine("");
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        static void CreateMergedPDF(string targetPDF, string sourceDir)
        {
            using (FileStream stream = new FileStream(targetPDF, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4);
                PdfCopy pdf = new PdfCopy(pdfDoc, stream);
                pdfDoc.Open();
                var files = Directory.GetFiles(sourceDir);
                Console.WriteLine("Merging files count: " + files.Length);
                int i = 1;
                foreach (string file in files)
                {
                    Console.WriteLine(i + ". Adding: " + file);
                    pdf.AddDocument(new PdfReader(file));
                    i++;
                }

                if (pdfDoc != null)
                    pdfDoc.Close();

                Console.WriteLine("SpeedPASS PDF merge complete.");
            }
        }
    }
}
