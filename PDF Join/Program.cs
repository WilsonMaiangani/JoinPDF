using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace PDF_Join
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] file1 = File.ReadAllBytes(@"C:\Users\alifa\Downloads\PDF teste\CARTA.pdf");
            byte[] file2 = File.ReadAllBytes(@"C:\Users\alifa\Downloads\PDF teste\A realização do Sonho.pdf");

            Guid guid = Guid.NewGuid();            
            var result = GerarDocumento("", file1, file2, guid);
            salvarArquivoPDF(result);
        }

        public static byte[] GerarDocumento(string html, byte[] capa, byte[] contraCapa, Guid codigo)
        {
            List<byte[]> pdfs = new List<byte[]>();

            if (capa != null)
                pdfs.Add(capa);
        
            if (contraCapa != null)
                pdfs.Add(contraCapa);

            return ConcatenarArquivos(pdfs, codigo);
        }

        public static byte[] ConcatenarArquivos(List<byte[]> pdfs, Guid codigo)
        {
            using (var ms = new MemoryStream())
            {
                var outputDocument = new Document();
                var writer = new PdfCopy(outputDocument, ms);
                outputDocument.Open();

                outputDocument.AddAuthor(codigo.ToString());

                foreach (var doc in pdfs)
                {
                    var reader = new PdfReader(doc);
                    for (var i = 1; i <= reader.NumberOfPages; i++)
                    {
                        writer.AddPage(writer.GetImportedPage(reader, i));
                    }
                    writer.FreeReader(reader);
                    reader.Close();
                }

                writer.Close();
                outputDocument.Close();
                //var allPagesContent = ms.GetBuffer();
                var allPagesContent = ms.ToArray();
                ms.Flush();

                return allPagesContent;
            }
        }

        private static void salvarArquivoPDF(byte[] list)
        {

            var rdoGerado = list;
            Guid guid = Guid.NewGuid();
            //var pasta = string.Format("{0}\\{1}\\{2}\\RDO_{3}\\Revisao\\",
            //            Configuracao.DiretorioArquivosRDO,
            //            rdoPdf.DataCriacao.Year,
            //            rdoPdf.DataCriacao.Month.ToString().PadLeft(2, '0'),
            //            rdoPdf.Id.ToString().PadLeft(2, '0')
            //            );
            var pasta = @"C:\Users\alifa\Downloads\PDF teste\";
            var nomeArquivo = guid.ToString().PadLeft(2, '0') + ".pdf";

            bool exists = System.IO.Directory.Exists(pasta);

            if (!exists)
                System.IO.Directory.CreateDirectory(pasta);

            File.WriteAllBytes(pasta + nomeArquivo, rdoGerado);
        }

    }
}
