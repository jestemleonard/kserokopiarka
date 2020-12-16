using System;
using ver1;

namespace Zadanie1
{
    public class Copier : IPrinter, IScanner
    {
        private bool IsOn;
        public int PrintCounter;
        public int ScanCounter;
        public int Counter { get; private set; }

        public Copier()
        {
            PrintCounter = 0;
            ScanCounter = 0;
            Counter = 0;
            IsOn = false;
        }

        public void Print(in IDocument file)
        {
            if (!IsOn)
                return;
            string currentTime = GetTime();
            PrintCounter++;
            Console.WriteLine($"{currentTime} Print: {file.GetFileName()}");
        }

        public void ScanAndPrint()
        {
            if(!IsOn)
                return;
            Scan(out var document, IDocument.FormatType.JPG);
            Print(document);
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
    }
}