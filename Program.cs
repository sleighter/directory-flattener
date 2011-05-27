using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Threading.Tasks;namespace directory_flattener
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 0 || args.Count() > 3 || args[0].StartsWith("-h"))
            {
                Console.WriteLine(  "Directory Flattener - Usage: \n" + 
                                    "\t\tdirectory-flattener [source-directory] [target-directory]\n" +
                                    "\t\tdirectory-flattener [source-directory] [target-directory] [search-pattern]\n" +
                                    "\t\t\t target-directory will be created if it does not exist");
                return;
            }

            var sourceDir = args[0];
            var targetDir = args[1];
            var searchPattern = args[2] ?? "*";

            if (!Directory.Exists(sourceDir))
            {
                Console.WriteLine("source-directory does not exist -- {0}", sourceDir);
                Console.WriteLine("Exiting...");
                return;
            }
            try
            {
                Directory.CreateDirectory(targetDir);
                Console.WriteLine("Target directory created -- {0}", targetDir);
            }
            catch
            {
                Console.WriteLine("Could not create target-directory -- {0}", targetDir);
                Console.WriteLine("Exiting...");
                return;
            }

            var files = Directory.GetFiles(sourceDir, searchPattern, SearchOption.AllDirectories);
            var copied = new List<string>();

            try
            {
                Parallel.ForEach(files,new Action<string>(file =>
                {
                    var targetFile = Path.Combine(targetDir, Path.GetFileName(file));
                    Console.WriteLine("Copying {0} to {1}", file, targetFile);
                    File.Copy(file, targetFile);
                    copied.Add(targetFile);
                }));
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occurred, rolling back.\n\tException details: \n {0}",ex.Message);
                foreach (var file in copied)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex1) 
                    { 
                        Console.WriteLine("Could not delete copied file -- {0}\n\tException details: \n{1}",file,ex1.Message); 
                    }
                }
            }

            Console.WriteLine("Done");

        }
    }
}
