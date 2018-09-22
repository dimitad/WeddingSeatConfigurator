using System;
using System.IO;

namespace WeddingSeating
{
    /// <summary>
    /// Utility class to read a file given a physical path.
    /// </summary>
    public class FileLoader : IDataLoader
    {
        public string SourceFile { get; private set; }

        /// <summary>
        /// Instantiates a new instance of the FileLoader class.
        /// </summary>
        /// <param name="sourceFile">The physical path to the file being loaded.</param>
        public FileLoader(string sourceFile)
        {
            if (string.IsNullOrEmpty(sourceFile))
            {
                throw new ArgumentNullException("sourceFile");
            }

            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException();
            }

            SourceFile = sourceFile;
        }

        public string Read()
        {
            return File.ReadAllText(SourceFile);
        }
    }
}
