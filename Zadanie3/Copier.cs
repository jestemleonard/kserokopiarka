using ver1;

namespace Zadanie3
{
    public class Copier : IDevice
    {
        public int Counter { get; private set; }
        public int PrintCounter => _printer.PrintCounter;
        public int ScanCounter => _scanner.ScanCounter;
        private bool IsOn { get; set; }

        private readonly Printer _printer;

        private readonly Scanner _scanner;

        public Copier()
        {
            Counter = 0;
            IsOn = false;
            _printer = new Printer();
            _scanner = new Scanner();
        }

        public void Scan(out IDocument document)
        {
            if (!IsOn)
            {
                document = null;
                return;
            }
            _scanner.PowerOn();
            _scanner.Scan(out document);
            _scanner.PowerOff();
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (!IsOn)
            {
                document = null;
                return;
            }
            _scanner.PowerOn();
            _scanner.Scan(out document, formatType);
            _scanner.PowerOff();
        }

        public void Print(in IDocument document)
        {
            if(!IsOn)
                return;
            _printer.PowerOn();
            _printer.Print(document);
            _printer.PowerOff();
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
    }
}