using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YoutubeTV.Misc
{
    public class ZipManager
    {
        // 解壓縮檔案，傳入參數： 來源壓縮檔, 解壓縮後的目的路徑
        public static void Uncompress(string zipFileName, string targetPath)
        {
            using ZipInputStream s = new ZipInputStream(File.OpenRead(zipFileName));
            // 若目的路徑不存在，則先建立路徑
            var di = new DirectoryInfo(targetPath);

            if (!di.Exists)
                di.Create();

            ZipEntry theEntry;

            // 逐一取出壓縮檔內的檔案(解壓縮)
            while ((theEntry = s.GetNextEntry()) != null)
            {
                int size = 2048;
                byte[] data = new byte[2048];

                Console.WriteLine("正在解壓縮: " + GetBasename(theEntry.Name));

                // 寫入檔案
                using FileStream fs = new FileStream(di.FullName + "\\" + GetBasename(theEntry.Name), FileMode.Create);
                while (true)
                {
                    size = s.Read(data, 0, data.Length);

                    if (size > 0)
                        fs.Write(data, 0, size);
                    else
                        break;
                }
            }
        }

        // 取得檔名(去除路徑)
        public static string GetBasename(string fullName)
        {
            string result;
            int lastBackSlash = fullName.LastIndexOf("\\");
            result = fullName.Substring(lastBackSlash + 1);

            return result;
        }
    }
}