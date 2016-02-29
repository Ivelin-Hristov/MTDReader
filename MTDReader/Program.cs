
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


    class Program
    {
        static void Main()
        {
            string mtdPath = @"C:\Users\BOX\Desktop\Global_Yearly_1415\CANADA_P&G_MALE GROOMING_ToplineData_Additional_Dec_2014_RE_CS.mtd";
            //string mtdPath = @"C:\Users\BOX\Desktop\Global_Yearly_1415\Global_Yearly_1415.mtd";

            
            var parser = new XMLParser();
                parser.LoadMTD(mtdPath);

            var table = parser.MoveToNextTable();
            var table2 = parser.MoveToNextTable();

            parser.Close(); 
        }
    }
}
