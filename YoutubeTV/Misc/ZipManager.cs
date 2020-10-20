using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YoutubeTV.Misc
{
    public class ZipManager
    {
        public static void Uncompress(string zipFileName, string targetPath)
        {
            using ZipInputStream s = new ZipInputStream(File.OpenRead(zipFileName));

            // check path
            var di = new DirectoryInfo(targetPath);
            if (!di.Exists)
            {
                di.Create();
            }

            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                var size = 2048;
                var data = new byte[2048];

                Console.WriteLine("Unzip: " + GetBasename(theEntry.Name));

                using FileStream fs = new FileStream(di.FullName + "\\" + GetBasename(theEntry.Name), FileMode.Create);
                while (true)
                {
                    size = s.Read(data, 0, data.Length);

                    if (size > 0)
                    {
                        fs.Write(data, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public static string GetBasename(string fullName)
        {
            string result;
            int lastBackSlash = fullName.LastIndexOf("\\");
            result = fullName.Substring(lastBackSlash + 1);

            return result;
        }
    }
}