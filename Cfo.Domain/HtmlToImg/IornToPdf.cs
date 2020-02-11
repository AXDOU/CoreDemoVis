
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using IronPdf;
namespace Cfo.Domain.HtmlToImg
{
    public class IornToPdf
    {

        public void ToPdf()
        {
            var Renderer = new IronPdf.HtmlToPdf();
            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.PaperOrientation = IronPdf.PdfPrintOptions.PdfPaperOrientation.Landscape;
            string url=$"localhost:57076/CRUD/Index";//ocalhost:44339/Home/Index
            var PDF = Renderer.RenderUrlAsPdf(url);
            string filepath = Directory.GetCurrentDirectory() + @"\PDF";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            string filepathname = $"{filepath}\\{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            PDF.SaveAs(filepathname);
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(filepathname);
            process.StartInfo = startInfo;
            try
            {
                process.StartInfo.UseShellExecute = true;//若不设置，会提示The specified executable is not a valid application for this OS platform
                process.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            // This neat trick opens our PDF file so we can see the result
            return;
        }
    }
}