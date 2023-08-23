using System;
using System.Runtime.CompilerServices;

namespace DesignPatternStudy.Patterns
{
    public interface Image {
        public void Display();
    }

    public class RealImage : Image
    {
        private string fileName;
        public RealImage(string fileName) {
            this.fileName = fileName;
            loadFromDisk(fileName);
        }

        private void loadFromDisk(string fileName)
        {
            Console.WriteLine("Displaying "+ fileName);
        }

        public void Display()
        {
            Console.WriteLine("Loading "+ fileName);
        }
    }

    public class ProxyImage : Image
    {
        private RealImage realImage;
        private string fileName;
        public ProxyImage(string fileName) {
            this.fileName = fileName;
        }
        public void Display()
        {
            if (realImage == null) {
                // loading from disk
                realImage = new RealImage(fileName);
            }
            realImage.Display();           
        }
    }
}