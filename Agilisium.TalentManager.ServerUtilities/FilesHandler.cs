using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.ServerUtilities
{
    public static class FilesHandler
    {
        public static string GetFileContent(string filePath)
        {
            FileStream fs = null;
            StreamReader sr = null;

            try
            {
                fs = File.OpenRead(filePath);
                sr = new StreamReader(fs);
                string fileCotent = sr.ReadToEnd();
                return fileCotent;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        public static string GetApplicationTempDirectory()
        {
            return CreateDirectory(Environment.CurrentDirectory, "Temp");
        }

        public static void RemoveAllFilesFromTempDirectory()
        {
            DirectoryInfo tempDir = new DirectoryInfo(GetApplicationTempDirectory());
            FileInfo[] tempFiles = tempDir.GetFiles();
            foreach (var file in tempFiles) file.Delete();
        }

        public static void RemoveAllFilesFromDirectory(string directoryToDelete)
        {
            DirectoryInfo tempDir = new DirectoryInfo(directoryToDelete);
            FileInfo[] tempFiles = tempDir.GetFiles();
            foreach (var file in tempFiles) file.Delete();
        }

        public static string CreateFile(string targetDirectory, string fileName, string fileContent)
        {
            if (Directory.Exists(targetDirectory) == false) throw new InvalidOperationException("Target directory (" + targetDirectory + ") does not exist to create a file");

            FileStream fs = null;
            StreamWriter sw = null;
            string filePath = string.Format(@"{0}\{1}", targetDirectory, fileName);
            try
            {
                fs = File.Create(filePath);
                sw = new StreamWriter(fs);
                sw.WriteLine(fileContent);
                sw.Flush();
            }
            catch (Exception exp)
            {
                throw new InvalidOperationException(exp.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            return filePath;
        }

        public static string CreateFile(string targetDirectory, string fileName, byte[] fileContent)
        {
            if (Directory.Exists(targetDirectory) == false) throw new InvalidOperationException("Target directory (" + targetDirectory + ") does not exist to create a file");

            string filePath = string.Format(@"{0}\{1}", targetDirectory, fileName);
            try
            {
                File.WriteAllBytes(filePath, fileContent); ;
            }
            catch (Exception exp)
            {
                throw new InvalidOperationException(exp.Message);
            }
            return filePath;
        }

        public static string CreateDirectory(string targetDirectory, string newDirectoryName)
        {
            if (Directory.Exists(targetDirectory) == false) throw new InvalidOperationException("Target directory (" + targetDirectory + ") does not exist to create a directory");
            string newDirPath = string.Format(@"{0}\{1}", targetDirectory, newDirectoryName);
            if (Directory.Exists(newDirPath)) return newDirPath;
            DirectoryInfo dirInfo = Directory.CreateDirectory(newDirPath);
            return dirInfo.FullName;
        }
    }
}
