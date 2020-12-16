using ver1;
using System;
using Zadanie2;

namespace Zadanie3
{
    public class Fax :IFax
    {
        private bool IsOn;
        public int Counter { get; set; }

        public Fax()
        {
            Counter = 0;
            IsOn = false;
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
        
    }
}