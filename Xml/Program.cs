using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Linq;
using Antlr4.Runtime;
using System.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data.OleDb;
using System.Globalization;
using System.Net;
using System.Web;

namespace Xml
{

    class Program
    {
        static void Main(string[] args)
        {

            //OldTestPrgrm();

            //GetIpAddress();
            //getclientIP();

            Fetch_UserIP();
            Console.ReadLine();
        }

        private static void GetIpAddress()
        {           
            // Get the IP  
            string myIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            Console.WriteLine("My IP Address is :" + myIP);            
        }


        static string IPAddress = GetIPAddress();

        public static string GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }


        private static string Fetch_UserIP()
        {
            string VisitorsIPAddress = string.Empty;
            try
            {
                
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    VisitorsIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
                {
                    VisitorsIPAddress = HttpContext.Current.Request.UserHostAddress;
                }
            }
            catch (Exception ex)
            {

                //Handle Exceptions  
            }
            return VisitorsIPAddress;
        }

        public static string getclientIP()
        {
            string result = string.Empty;
            string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                string[] ipRange = ip.Split(',');
                int le = ipRange.Length - 1;
                result = ipRange[0];
            }
            else
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }                    

            return result;
        }

        private static void OldTestPrgrm()
        {
            //Read XLSX file with empty column value

            //ImportExcel(@"D:\DummySheet1.xlsx");  //Using OleDbConnection
            DataSet dataSet = ReadExcelFile(@"D:\DummySheet1.xlsx"); //SpreadsheetDocument (Open XML)

            DataTable dt1 = dataSet.Tables["outputsheet$"];
            DataTable dt2 = dataSet.Tables["appexport$"];

            List<DataRow> rows = dt1.Rows.Cast<DataRow>().ToList();
            List<DataRow> rows2 = dt2.Rows.Cast<DataRow>().ToList();

            DataSet sampleDataSet = new DataSet();
            sampleDataSet.Locale = CultureInfo.InvariantCulture;
            DataTable sampleDataTable = sampleDataSet.Tables.Add("OutPutData");

            foreach (DataColumn column in dt1.Columns)
            {
                sampleDataTable.Columns.Add(column.ColumnName, typeof(string));
            }

            DataRow sampleDataRow;
            int row = -1;

            foreach (var item in rows)
            {
                row++;
                var row2Result = rows2[row];
                sampleDataRow = sampleDataTable.NewRow();
                for (int i = 0; i < item.ItemArray.Count(); i++)
                {
                    bool str = false;
                    Int32 result2 = 0;

                    if (!string.IsNullOrEmpty(item.ItemArray[i].ToString()))
                    {
                        str = row2Result.ItemArray[i].ToString().Equals(item.ItemArray[i].ToString());
                        string data = item.ItemArray[i].ToString();
                        string data2 = row2Result.ItemArray[i].ToString();
                        var dd = item.ItemArray[i].GetType();

                        if (dd.Name != "String")
                            result2 = Convert.ToInt32(item.ItemArray[i]) - Convert.ToInt32(row2Result.ItemArray[i].ToString());

                        sampleDataRow[i] = dd.Name == "String" ? str.ToString() : result2.ToString();
                    }
                }
                sampleDataTable.Rows.Add(sampleDataRow);
            }


            //--Recusrsive Parectice

            //Console.WriteLine("Enetr folder path");
            //string path = @"D:\test"; //Console.ReadLine();
            //var data = ChildrenOf1(path);

            // List<Folder> objrootobj = FlatToHierarchy(lstfolder);
            //objroot.Folders = objrootobj;
            //string json = JsonConvert.SerializeObject(objroot);

            ////XNode node = JsonConvert.DeserializeXNode(json);

            //XDocument doc = (XDocument)JsonConvert.DeserializeXNode("{\"Roots\":" + json + "}");

            //XmlDocument xdoc = new XmlDocument();
            //xdoc.LoadXml(doc.ToString());
            //xdoc.Save("D://Sample123.xml");
            //Console.Write(doc.ToString());

            //ReadXmlFile();

            //try
            //{
            //    var serializer = new XmlSerializer(typeof(Folders));
            //    using (var stream = new StringWriter())
            //    {
            //        serializer.Serialize(stream, data);
            //        XmlDocument doc = new XmlDocument();
            //        doc.LoadXml(stream.ToString());
            //        doc.Save("D:\\test.xml");
            //        ReadXmlFile();
            //        //Console.Write(stream.ToString());
            //    }
            //}
            //catch (Exception ee)
            //{

            //    throw ee;
            //}
        }

        private static DataSet ReadExcelFile(string filepath)
        {
            DataSet ds = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + "; Extended Properties=Excel 12.0;"))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                // Get all Sheets in Excel File
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                // Loop through all Sheets to get data
                foreach (DataRow dr in dtSheet.Rows)
                {
                    string sheetName = dr["TABLE_NAME"].ToString();
                    if (!sheetName.EndsWith("$"))
                        continue;
                    // Get all rows from the Sheet
                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                    DataTable dt = new DataTable();
                    dt.TableName = sheetName;
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    ds.Tables.Add(dt);
                }
                cmd = null;
                conn.Close();
            }

            return ds;
        }
        
        public static void ReadXmlFile()
        {
            string folderPath = @"D:\Original.xml";
            AscendantOne datalst = new AscendantOne();
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.IsNullable = true;
            XmlSerializer serializer = new XmlSerializer(typeof(AscendantOne), xRoot);
            using (TextReader reader = new StreamReader(folderPath))
            {
                datalst = (AscendantOne)serializer.Deserialize(reader);
                reader.Close();
            }


            var xmlData = biuldFactorGroup(datalst.Tables.Table);


            //string str = "GeneralLiabilityAddlInsdCondoUnitOwners";

            //var data = datalst.Tables.Table;
            //var chagedData = UpDateData(str, data);
            //AscendantOne objdata = new AscendantOne();
            //objdata.Tables = new Tables();
            //objdata.Tables.Table = chagedData;
            //try
            //{
            //    var serializer1 = new XmlSerializer(typeof(AscendantOne));
            //    using (var stream = new StringWriter())
            //    {
            //        serializer1.Serialize(stream, objdata);
            //        XmlDocument doc = new XmlDocument();
            //        doc.LoadXml(stream.ToString());
            //        doc.Save("D:\\Changetest11.xml");
            //        //ReadXmlFile();
            //        //Console.Write(stream.ToString());
            //    }
            //}
            //catch (Exception ee)
            //{

            //    throw;
            //}
        }

        private static FactorGroup biuldFactorGroup(Table objTable)
        {
            FactorGroup objFactorGroup = new FactorGroup();

            objFactorGroup.Id = objTable.Id;
            objFactorGroup.Name = objTable.Name;

            objFactorGroup.FactorGroupList = new List<FactorGroup>();
            objFactorGroup.FactorEntity = new List<FactorEntity>();
            foreach (var Columnsitem in objTable.lstColumns)
            {
                FactorEntity objfactEntity = new FactorEntity();
                objfactEntity.Id = Columnsitem.Id;
                objfactEntity.Name = Columnsitem.Name;
                objFactorGroup.FactorEntity.Add(objfactEntity);
            }
            foreach (var item in objTable.Tables)
            {
                if (item.Table.Tables.Count() > 0)
                {
                    foreach (var childitem in item.Table.Tables)
                    {
                        objFactorGroup.FactorGroupList.Add(biuldFactorGroup(childitem.Table));
                    }
                }
            }

            return objFactorGroup;
        }

        public static Table UpDateData(string strValue, Table objTable)
        {
            foreach (var item in objTable.Tables)
            {
                var name = item.Table.Name;

                if (name == strValue)
                {
                    item.Table.Name = "12354";
                    break;
                }
                else
                {
                    UpDateData(strValue, item.Table);
                }
            }
            return objTable;
        }

        public static Folders ChildrenOf1(string path)
        {
            Folders result1 = new Folders();
            result1.FolderName = path;
            List<string> abc = Directory.GetFiles(path).ToList();
            result1.Files = new List<File>();
            result1.Folder = new List<Folders>();
            if (abc != null && abc.Any())
            {
                foreach (string fileName in abc)//Gets all files in the current path
                {
                    File objfile = new File();
                    objfile.FileName = fileName;
                    result1.Files.Add(objfile);
                }
            }

            List<string> abb = Directory.GetDirectories(path).ToList();

            if (abb != null && abb.Any())
            {
                foreach (string folderName in abb)
                {
                    result1.Folder.Add(ChildrenOf1(folderName));
                }
            }
            return result1;
        }

        public static List<Folders> ChildrenOf(string path)
        {
            List<Folders> result1 = new List<Folders>();
            List<File> fileresult = new List<File>();
            foreach (string folderName in Directory.GetDirectories(path))
            {
                Folders child = new Folders();
                child.FolderName = folderName;
                foreach (string fileName in Directory.GetFiles(folderName))//Gets all files in the current path
                {
                    File objfile = new File();
                    objfile.FileName = fileName;
                    fileresult.Add(objfile);
                }
                child.Files = fileresult;
                child.Folder.Add(child);
                result1.AddRange(ChildrenOf(folderName));
                //result1.AddRange(ChildrenOf(folderName));
            }

            return result1;
        }


        #region ImportExcel
        public static DataTable ImportExcel(string filepath)
        {
            OleDbConnection cnn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + "; Extended Properties=Excel 12.0;");
            OleDbCommand oconn = new OleDbCommand("select * from [Sheet1$]", cnn);
            cnn.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter(oconn);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }

        public static DataTable ReadAsDataTable1(string fileName)
        {
            DataTable dt = new DataTable();
            using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, false))
            {
                WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                string relationshipId = sheets.First().Id.Value;
                WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                Worksheet workSheet = worksheetPart.Worksheet;
                SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Descendants<Row>();

                foreach (Cell cell in rows.ElementAt(0))
                {
                    dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                }

                foreach (Row row in rows) //this will also include your header row...
                {
                    DataRow tempRow = dt.NewRow();

                    for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                    {
                        Cell cell = row.Descendants<Cell>().ElementAt(i);
                        int index = 0;
                        string reference = cell.CellReference.ToString().ToUpper();
                        foreach (char ch in reference)
                        {
                            if (Char.IsLetter(ch))
                            {
                                int value = (int)ch - (int)'A';
                                index = (index == 0) ? value : ((index + 1) * 26) + value;
                            }
                        }
                        tempRow[index] = GetCellValue(spreadSheetDocument, cell);
                    }
                    dt.Rows.Add(tempRow);
                }
            }
            dt.Rows.RemoveAt(0);
            return dt;
        }


        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }


        #endregion ImportExcel



        //public static void ReadFolder(string path)
        //{
        //    List<File> lstfiles = new List<File>();
        //    DirectoryInfo dir = new DirectoryInfo(path);

        //    //Get Sub Folders list from Dir
        //    DirectoryInfo[] dirlist = dir.GetDirectories().Where(a => (a.Attributes & FileAttributes.System) == 0).ToArray();

        //    foreach (var item in dirlist)
        //    {
        //        List<File> sublstfiles = new List<File>();
        //        Folder folder = new Folder();
        //        folder.FolderName = item.Name.ToString();
        //        folder.ParentName = Path.GetDirectoryName(item.FullName);
        //        folder.ParentName = folder.ParentName.Split('\\').Last();
        //        //Get Files from Folder
        //        FileInfo[] SubFiles = item.GetFiles();

        //        foreach (var subitem in SubFiles)
        //        {
        //            File file = new Xml.File();
        //            file.FileName = subitem.Name;
        //            sublstfiles.Add(file);
        //            //Console.WriteLine(subitem.Name);
        //        }
        //        folder.Files = sublstfiles;

        //        lstfolder.Add(folder);
        //        string name = path + "\\" + item.Name.ToString();
        //        //Console.WriteLine(name);
        //        ReadFolder(name);
        //    }
        //}

        //public static List<Folder> FlatToHierarchy(List<Folder> list)
        //{
        //    // hashtable lookup that allows us to grab references to containers based on ParentName
        //    var lookup = new Dictionary<string, Folder>();
        //    // actual nested collection to return
        //    var nested = new List<Folder>();

        //    foreach (Folder item in list)
        //    {
        //        if (lookup.ContainsKey(item.ParentName))
        //        {
        //            // add to the parent's child list 
        //            lookup[item.ParentName].lstFolder.Add(item);
        //        }
        //        else
        //        {
        //            // no parent added yet (or this is the first time)
        //            nested.Add(item);
        //        }
        //        lookup.Add(item.FolderName, item);
        //    }
        //    return nested;
        //}        
    }

    public class Column
    {
        public string Comment { get; set; }
        public string Id { get; set; }
        public string DataType { get; set; }
        public string DataSize { get; set; }
        public string Name { get; set; }
        public string Encrypted { get; set; }
        public string DataScale { get; set; }
        public string Distortion { get; set; }
    }

    [XmlRoot(ElementName = "Files")]
    public class File
    {
        [XmlElement(ElementName = "FileName")]
        public string FileName { get; set; }
    }
    [XmlRoot(ElementName = "Folders")]
    public class Folders
    {
        [XmlElement(ElementName = "Files")]
        public List<File> Files { get; set; }

        [XmlElement(ElementName = "FolderName")]
        public string FolderName { get; set; }

        [XmlElement(ElementName = "Folder")]
        public List<Folders> Folder { get; set; } = new List<Folders>();
    }

    [XmlRoot(ElementName = "Tables")]
    public class Tables
    {
        [XmlElement(ElementName = "Table")]
        public Table Table { get; set; }
    }

    [XmlRoot(ElementName = "Table")]
    public class Table
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "Comment")]
        public string Comment { get; set; }
        [XmlElement(ElementName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "OverridePremium")]
        public int OverridePremium { get; set; }
        [XmlElement(ElementName = "Column")]
        public List<Column> lstColumns { set; get; }
        [XmlElement(ElementName = "Tables")]
        public List<Tables> Tables { get; set; }
    }

    public class AscendantOne
    {
        [XmlElement(ElementName = "Tables")]
        public Tables Tables { get; set; }
    }

    public class FactorGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<FactorEntity> FactorEntity { get; set; }
        public List<FactorGroup> FactorGroupList { get; set; }
    }

    public class FactorEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

}



