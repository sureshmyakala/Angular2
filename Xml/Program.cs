using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Xml
{
    class Program
    {
        static List<Folder> lstfolder = new List<Folder>();
        static void Main(string[] args)
        {
            Console.WriteLine("Enetr folder path");
            string path = Console.ReadLine();

            List<Files> lstfiles = new List<Files>();
            DirectoryInfo dir = new DirectoryInfo(path);
            //Get Files from Folder
            FileInfo[] Files = dir.GetFiles();

            if (Files.Count() > 0)
            {
                Folder folder = new Folder();
                foreach (var item in Files)
                {
                    Files file = new Xml.Files();
                    file.FileName = item.Name;
                    lstfiles.Add(file);
                    //Console.WriteLine(item.Name);
                }
                folder.Files = lstfiles;
                lstfolder.Add(folder);
            }
            
            ReadFolder(path);
            var serializer = new XmlSerializer(typeof(List<Folder>),
                                   new XmlRootAttribute("Folders"));
            using (var stream = new StringWriter())
            {
                serializer.Serialize(stream, lstfolder);
                Console.Write(stream.ToString());
            }
            Console.ReadLine();
        }


        public static void ReadFolder(string path)
        {
            List<Files> lstfiles = new List<Files>();
            DirectoryInfo dir = new DirectoryInfo(path);

            //Get Sub Folders list from Dir
            DirectoryInfo[] dirlist = dir.GetDirectories();

            foreach (var item in dirlist)
            {
                List<Files> sublstfiles = new List<Files>();
                Folder folder = new Folder();
                folder.FolderName = item.Name.ToString();
                //Get Files from Folder
                FileInfo[] SubFiles = item.GetFiles();

                foreach (var subitem in SubFiles)
                {
                    Files file = new Xml.Files();
                    file.FileName = subitem.Name;
                    sublstfiles.Add(file);
                    //Console.WriteLine(subitem.Name);
                }
                folder.Files = sublstfiles;

                lstfolder.Add(folder);
                string name = path + "\\" + item.Name.ToString();
                //Console.WriteLine(name);

                ReadFolder(name);
            }
        }
    }


    public class Folder
    {
        public string FolderName { get; set; }
        public List<Folder> lstFolder { get; set; }
        public List<Files> Files { get; set; }
    }

    public class Files
    {
        public string FileName { get; set; }
    }
}



