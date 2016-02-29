
namespace MTDReader
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.IO;
    using Models;
    using System.Windows.Forms;


    class Program
    {
        [STAThread]
        static void Main()
        {
            //string mtdPath = @"C:\Users\BOX\Desktop\Global_Yearly_1415\CANADA_P&G_MALE GROOMING_ToplineData_Additional_Dec_2014_RE_CS.mtd";
            // @"C:\Users\BOX\Desktop\Global_Yearly_1415\Global_Yearly_1415.mtd";

            OpenFileDialog fileSelectPopUp = new OpenFileDialog();
            fileSelectPopUp.Title = "MTD Select";
            //fileSelectPopUp.InitialDirectory = @"c:\";
            fileSelectPopUp.Filter = "All MTD FILES (*.mtd*)|*.mtd";
            fileSelectPopUp.FilterIndex = 2;
            fileSelectPopUp.RestoreDirectory = true;
            if (fileSelectPopUp.ShowDialog() == DialogResult.OK)
            {
                string mtdPath = fileSelectPopUp.FileName;
                var parser = new XMLParser();
                    parser.LoadMTD(mtdPath);
                    parser.MoveToNextTable();
            
                while (parser.IsInTables)
                {
                    Console.WriteLine(parser.Table.TableName);
                    Console.WriteLine(parser.Table.TableLabel);
                    Console.WriteLine(string.Join("\t", parser.Table.CellItemsInfo));
                    Console.WriteLine(string.Join("\n", parser.Table.Annotations));
                    Console.WriteLine("##########################################################");

                    parser.MoveToNextTable();
                }  
                parser.Close();
                Console.ReadLine();
            }
        }
    }
}
