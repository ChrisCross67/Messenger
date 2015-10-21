using System.IO;

namespace Messenger.Utils
{
    public class DirectoryWalker
    {
        public string SearchPattern = "*";

        public delegate void OnVisitDirectory(DirectoryInfo dirInfo);

        public delegate void OnVisitFile(FileInfo fileInfo);

        public OnVisitDirectory BeforeVisitDirectory;

        public OnVisitDirectory AfterVisitDirectory;

        public OnVisitFile VisitFile;

        public void Traverse(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            VisitDirectoryInfo(dirInfo);
        }

        protected void VisitDirectoryInfo(DirectoryInfo dirInfo)
        {
            if (BeforeVisitDirectory != null)
            {
                BeforeVisitDirectory(dirInfo);
            }

            // Visit Files
            foreach (FileInfo fileInfo in dirInfo.GetFiles(SearchPattern, SearchOption.TopDirectoryOnly))
            {
                VisitFileInfo(fileInfo);
            }

            // Visit Sub Directories
            foreach (DirectoryInfo subDirInfo in dirInfo.GetDirectories())
            {
                VisitDirectoryInfo(subDirInfo);
            }

            if (AfterVisitDirectory != null)
            {
                AfterVisitDirectory(dirInfo);
            }
        }

        protected void VisitFileInfo(FileInfo fileInfo)
        {
            if (VisitFile != null)
            {
                VisitFile(fileInfo);
            }
        }
    }
}
