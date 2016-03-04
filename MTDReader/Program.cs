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
    using TableBuilder;

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
                var reader =  new XMLParser();
                var builder = new CsvBuilder(mtdPath + ".csv");

                reader.LoadMTD(mtdPath);

                reader.MoveToNextTable();                             
                while (reader.IsInTables)
                {
                    builder.BuildTable(reader.Table);
                    reader.MoveToNextTable();
                }
             
                reader.Close();
            }
        }
    }
}
