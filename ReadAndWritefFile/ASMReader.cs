using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndWritefFile
{
    public static class ASMReader
    {
        public static  void ReadFile(string path)
        {
            string[] allfiles = Directory.GetFiles(path, "*.txt");
            foreach (string file in allfiles)
            {
                Console.WriteLine($"Reading content from: {file}");
                Console.WriteLine("------------------------------");

                try
                {
                    var content = string.Empty;
                    var stringbuild = new StringBuilder();
                    var lines=   File.ReadAllLines(file);
                    var filename = lines[3].Split(" ")[0];
                    try
                    {
                        foreach (var line in lines)
                        {
                            if (line.Contains("Created by Movement Manager"))
                            {
                                var date = line.Split("/");
                                stringbuild.Append(date[0]);
                                stringbuild.AppendLine(); // This will add the platform-specific newline sequence

                            }
                            else if (line.Contains("OPER"))
                            {
                                var opr = line.Split(" ");
                                stringbuild.Append(opr[0]);
                                stringbuild.AppendLine(); // This will add the platform-specific newline sequence

                                //WriteToFile(opr[0], filename);
                            }
                            else if (line.Contains("ET") && line.Contains('/'))
                            {
                                var opr2 = line.Split("/");
                                if (opr2[1].Length==2)
                                {
                                    stringbuild.Append($"{opr2[0]}/{DateTime.Now.ToString("MMM").ToUpper()}{DateTime.Now.ToString("yyyy")}");
                                    stringbuild.AppendLine(); // This will add the platform-specific newline sequence


                                }
                            }

                            else
                            {
                                stringbuild.Append(line);
                                stringbuild.AppendLine(); // This will add the platform-specific newline sequence


                            }

                        }
                      var appendedcontent=  stringbuild.ToString();
                      
                        WriteToFile(appendedcontent, filename);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
      
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while reading file: {ex.Message}");
                }

                Console.WriteLine("------------------------------\n");
            }

        }

        public static void WriteToFile(string content,string filename)
        {
            //C:/Users/Moon/Desktop/ASM
            string folderPath = "C:/Users/Moon/Desktop/ASM_OUT/";
            string fileName = $"{filename}_{DateTime.Now:ddd_MMM_yyyy_hh_mm_ss}.txt";
            var Logfilename = $"LOG_{DateTime.Now:ddd_MMM_yyyy}.txt";
            //DateTime.Now.ToString("mm")
            //string content = "This is the content that will be written to the file.";

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                string filePath = Path.Combine(folderPath, fileName);
                string logfilepath = Path.Combine(folderPath, Logfilename);
                File.AppendAllText(filePath, content + Environment.NewLine);
                File.AppendAllText(logfilepath, content+ Environment.NewLine);

                Console.WriteLine("Content written to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

    }
}
