
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using IronPdf;
namespace Cfo.Domain.HtmlToImg
{
    public class IornToPdf
    {
        private const string toolFilename = "wkhtmltoimage.exe";
        private static readonly string directory;
        private static readonly string toolFilepath;

        static IornToPdf()
        {
            directory = AppContext.BaseDirectory;
            toolFilepath = Path.Combine(directory, toolFilename);

            if (!File.Exists(toolFilepath))
            {
                var assembly = typeof(IornToPdf).GetTypeInfo().Assembly;
                var type = typeof(IornToPdf);
                var ns = type.Namespace;

                using (var resourceStream = assembly.GetManifestResourceStream($"{ns}.{toolFilename}"))
                using (var fileStream = File.OpenWrite(toolFilepath))
                {
                    resourceStream.CopyTo(fileStream);
                }
            }
        }
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


        public byte[] FromUrl(string url, int width = 1024, ImageFormat format = ImageFormat.Jpg, int quality = 100)
        {
            var imageFormat = format.ToString().ToLower();
            var filename = Path.Combine(directory, $"{Guid.NewGuid().ToString()}.{imageFormat}");

            Process process = Process.Start(new ProcessStartInfo(toolFilepath, $"--quality {quality} --width {width} -f {imageFormat} {url} {filename}")
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = directory,
                RedirectStandardError = true
            });

            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.WaitForExit();

            if (File.Exists(filename))
            {
                var bytes = File.ReadAllBytes(filename);
                File.Delete(filename);
                return bytes;
            }

            throw new Exception("Something went wrong. Please check input parameters");
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            throw new Exception(e.Data);
        }

        public enum ImageFormat
        {
            Jpg,
            Png
        }
    }
}