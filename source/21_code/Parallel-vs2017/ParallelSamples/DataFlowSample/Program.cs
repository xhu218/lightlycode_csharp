﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks.Dataflow;
using static System.Console;

namespace DataFlowSample
{
    class Program
    {
        static void Main()
        {
            var target = SetupPipeline();
            target.Post(".");
            ReadLine();

        }

        public static IEnumerable<string> GetFileNames(string path)
        {
            foreach (var fileName in Directory.EnumerateFiles(path, "*.cs"))
            {
                yield return fileName;
            }
        }

        public static IEnumerable<string> LoadLines(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    var reader = new StreamReader(stream);
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //WriteLine($"LoadLines {line}");
                        yield return line;
                    }
                }
            }
        }

        public static IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                string[] words = line.Split(' ', ';', '(', ')', '{', '}', '.', ',');
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                        yield return word;
                }
            }
        }

        public static ITargetBlock<string> SetupPipeline()
        {
            var fileNamesForPath = new TransformBlock<string, IEnumerable<string>>(
              path =>
              {
                  return GetFileNames(path);
              });

            var lines = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(
              fileNames =>
              {
                  return LoadLines(fileNames);
              });

            var words = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(
              lines2 =>
              {
                  return GetWords(lines2);
              });

            var display = new ActionBlock<IEnumerable<string>>(
              coll =>
              {
                  foreach (var s in coll)
                  {
                      WriteLine(s);
                  }
              });

       
            fileNamesForPath.LinkTo(lines);
            lines.LinkTo(words);
            words.LinkTo(display);
            return fileNamesForPath;
        }

    }
}
