using System.IO;
using System.Security.Cryptography;

namespace MnM_UI.classes
{ 
    public class MergeJournals
    {
	    public MergeJournals()
	    {
	    }

        public void CopyJournal(string sourcePath, string destPath)
        {
            if (!File.Exists(destPath))
            {
                File.Copy(sourcePath, destPath, true);
            }
            else
            {
                if (!CompareFiles(sourcePath, destPath))
                {
                    string mergedJournal = MergeJournal(sourcePath, destPath);

                    File.WriteAllText(sourcePath, mergedJournal);
                    File.WriteAllText(destPath, mergedJournal);
                }
            }
        }

        public bool CompareFiles(string sourcePath, string destPath)
	    {
            if (new FileInfo(sourcePath).Length != new FileInfo(destPath).Length)
            {
                return false;
            }

            byte[] sourceHash = MD5.HashData(File.ReadAllBytes(sourcePath));
            byte[] destHash = MD5.HashData(File.ReadAllBytes(destPath));

            return sourceHash.SequenceEqual(destHash);
        }

        public string MergeJournal(string sourcePath, string destPath)
        {
            string[] sourceLines = File.ReadAllLines(sourcePath);
            string[] destLines = File.ReadAllLines(destPath);

            IEnumerable<string> mergedLines = sourceLines.Union(destLines).Where(s => !string.IsNullOrWhiteSpace(s));
            string mergedJournal = string.Join(Environment.NewLine + Environment.NewLine, mergedLines);

            return mergedJournal;
        }
    }
}
