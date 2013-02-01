using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Main.Common.AmazonS3
{
    public class S3Hierarchy
    {
        public S3File[] Files { get; private set; }

        public S3Path[] Paths { get; private set; }

        public S3Hierarchy(S3File[] files, string[] paths)
        {
            //List<S3File> f = new List<S3File>();
            //foreach(var file in files)
            //{
            //    string fileName = Path.GetFileName(file);
            //    string filePath = file.Substring(0,file.Length-fileName.Length);
            //    f.Add(new S3File(filePath, fileName));
            //}
            Files = files;

            List<S3Path> p = new List<S3Path>();
            foreach (var path in paths)
            {
                int level = path.Split(new char[]{'/'},  StringSplitOptions.RemoveEmptyEntries).Length;
                p.Add(new S3Path(path, level));
            }
            Paths = p.ToArray();

        }

        public void AddFile(S3File file)
        {
            var files = Files;
            Array.Resize(ref files, Files.Length+1);
            files[files.Length - 1] = file;

            if (Paths.Where(p => p.Path.Equals(file.Path)).Count() == 0)
            {
                int level = file.Path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Length;
                var path = new S3Path(file.Path, level);
                var paths = Paths;
                Array.Resize(ref paths, Paths.Length + 1);
                paths[paths.Length - 1] = path;
            }
        }
        
    }

    public class S3File
    {
        public string Path { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        public double Duration { get; set; }

        public S3File() { }

        public S3File(string path, string name, long size)
        {
            Path = path;
            Name = name;
            Size = size;
        }
    }

    public class S3Path
    {
        public string Path { get; private set; }

        public string Name { get; private set; }

        public int Level { get; private set; }


        public S3Path(string path, int level)
        {
            Path = path;
            Level = level;
            Name = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault() ?? "Root";

        }
    }
}