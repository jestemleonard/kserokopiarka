using ver1;
using System;
using System.Linq;

namespace Zadanie2
{
    public class MultifunctionalDevice :IPrinter, IScanner, IFax
    {
        private bool IsOn { get; set; }
        public int Counter { get; private set; }
        public int PrintCounter { get; set; }
        public int ScanCounter { get; set; }

        public MultifunctionalDevice()
        {
            ScanCounter = 0;
            Counter = 0;
            IsOn = false;
        }


        public void PowerOn()
        {
            if (!IsOn)
            {
                IsOn = true;
                Counter++;
            }
        }

        public void PowerOff()
        {
            IsOn = false;
        }

        public IDevice.State GetState()
        {
            if (IsOn)
                return IDevice.State.on;
            return IDevice.State.off;
        }

        public void Send(IDocument document)
        {
            if(!IsOn)
                return;
            string fileType;
            switch (document.GetFormatType())
            {
                case IDocument.FormatType.PDF:
                    fileType = ".pdf";
                    break;
                case IDocument.FormatType.JPG:
                    fileType = ".jpg";
                    break;
                case IDocument.FormatType.TXT:
                    fileType = ".txt";
                    break;
                default:
                    throw new FormatException();
            }
            Console.WriteLine($"Sent {document.GetFileName()}{fileType}.");
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (!IsOn)
            {
                document = null;
                return;
            }
            
            string filename;
            switch (formatType)
            {
                case IDocument.FormatType.PDF:
                    filename = "PDFScan" + (ScanCounter + 1) + ".pdf";
                    document = new PDFDocument(filename);
                    break;
                case IDocument.FormatType.JPG:
                    filename = "ImageScan" + (ScanCounter + 1) + ".jpg";
                    document = new ImageDocument(filename);
                    break;
                case IDocument.FormatType.TXT:
                    filename = "TextScan" + (ScanCounter + 1) + ".txt";
                    document = new TextDocument(filename);
                    break;
                default:
                    throw new FormatException();
            }
                
            string currentTime = GetTime();
            ScanCounter++;
            Console.WriteLine($"{currentTime} Scan: {filename}");
        }
        
        public void Scan(out IDocument document)
        {
            if (!IsOn)
            {
                document = null;
                return;
            }
            string currentTime = GetTime();
            Console.WriteLine($"{currentTime} Scan: Scan{ScanCounter+1}");
            document = null;
            ScanCounter++;
        }

        private string GetTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public void Print(in IDocument file)
        {
            if (!IsOn)
                return;
            string currentTime = GetTime();
            PrintCounter++;
            Console.WriteLine($"{currentTime} Print: {file.GetFileName()}");
        }
    }
}