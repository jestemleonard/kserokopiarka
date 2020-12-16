using System;
using ver1;

namespace Zadanie3
{
    public class Printer: IPrinter
    {
        private bool IsOn;
        public int PrintCounter;
        public int Counter { get; private set; }

        public Printer()
        {
            PrintCounter = 0;
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

        private string GetTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}