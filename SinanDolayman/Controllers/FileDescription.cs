using System.IO;

namespace SinanDolayman.Controllers
{
    internal class FileDescription
    {
        private string fileName;
        private Stream ınputStream;

        public FileDescription(string fileName, Stream ınputStream)
        {
            this.fileName = fileName;
            this.ınputStream = ınputStream;
        }
    }
}