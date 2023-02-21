using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DocuSign
{
    internal class DSAppHelper
    {
        internal static string PrepareFullPrivateKeyFilePath(string fileName)
        {
            const string DefaultRSAPrivateKeyFileName = "private.key";

            var fileNameOnly = Path.GetFileName(fileName);
            if (string.IsNullOrEmpty(fileNameOnly))
            {
                fileNameOnly = DefaultRSAPrivateKeyFileName;
            }

            var filePath = Path.GetDirectoryName(fileName);
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = Directory.GetCurrentDirectory();
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && Directory.GetCurrentDirectory().Contains("bin"))
            {
                fileNameOnly = DefaultRSAPrivateKeyFileName;
                filePath = Path.GetFullPath(Path.Combine(filePath, @"../../.."));
            }
            var path = Path.Combine(filePath, fileNameOnly);
            return path;
        }

        internal static byte[] ReadFileContent(string path)
        {
            var content = System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));

            //var content = File.ReadAllBytes(path);

            return content;
        }
    }
}