
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
            //string mtdPath = @"C:\Users\BOX\Desktop\Global_Yearly_1415\CANADA_P&G_MALE GROOMING_ToplineData_Additional_Dec_2014_RE_CS.mtd";
            string mtdPath = @"C:\Users\BOX\Desktop\Global_Yearly_1415\Global_Yearly_1415.mtd";

            var t = new TablesExport(mtdPath);

            while (!t.XmlDoc.EOF)
            {
                //GOTO NEXT TABLE
                t.MoveToElement(TablesExport.tableNameString, XmlNodeType.Element);
                if (!t.XmlDoc.EOF)
                {
                    t.TableName = t.XmlDoc.GetAttribute("Name");
                    t.TableLabel = t.XmlDoc.GetAttribute("Description");
                }

                //GOTO SIDE AXIS
                while (t.XmlDoc.Read())
                {
                    if (t.XmlDoc.Name == "Axis" && t.XmlDoc.GetAttribute("Name") == "Top")
                    {
                        break;
                    }

                    if (t.XmlDoc.Name == "Element" && t.XmlDoc.NodeType == XmlNodeType.Element)
                    {
                        t.SideAxis.Add(t.XmlDoc.GetAttribute("Label"));
                    }
                    
                }

                //GOTO TOP AXIS 
                while (t.XmlDoc.Read())
                {
                    if (t.XmlDoc.Name == "CellItems" && t.XmlDoc.NodeType == XmlNodeType.Element)
                    {
                        break;
                    }

                    if (t.XmlDoc.Name == "Element" && t.XmlDoc.NodeType == XmlNodeType.Element)
                    {
                        t.TopAxis.Add(t.XmlDoc.GetAttribute("Label"));
                    }
                }

                //GOTO CELL ITEMS
                while (t.XmlDoc.Read())
                {
                    if (t.XmlDoc.Name == "Layer" && t.XmlDoc.NodeType == XmlNodeType.Element)
                    {
                        break;
                    }

                    if (t.XmlDoc.Name == "CellItem" && t.XmlDoc.NodeType == XmlNodeType.Element)
                    {
                        t.CellItemsInfo.Add(t.XmlDoc.GetAttribute("Type"));
                    }
                }

                //GOTO ROWS
                while (t.XmlDoc.Read())
                {
                    if (t.XmlDoc.Name == "Layer" && t.XmlDoc.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }

                    if (t.XmlDoc.Name == "row" && t.XmlDoc.NodeType == XmlNodeType.Element)
                    {
                        List<string> row = new List<string>();
                        //CELL MATRIX [ROW,COL]
                        for (var col = 1; col < t.XmlDoc.AttributeCount; col++)
                        {
                            row.Add(t.XmlDoc.GetAttribute(col));
                        }
                        t.CellMatrix.Add(row);

                    }
                }

                //GOTO ANNOTATIONS
                while (t.XmlDoc.Read())
                {
                    if (t.XmlDoc.Name == "Annotations" && t.XmlDoc.NodeType == XmlNodeType.EndElement)
                    {
                        break;
                    }

                    if (t.XmlDoc.Name == "Annotation" && t.XmlDoc.NodeType == XmlNodeType.Element)
                    {
                        t.Annotations.Add(t.XmlDoc.GetAttribute("Text"));
                    }
                }

                Console.WriteLine(t.TableName);

                t.TableName = "";
                t.TableLabel = "";
                t.SideAxis.Clear();
                t.CellItemsInfo.Clear();
                t.TopAxis.Clear();
                t.CellMatrix.Clear();
                t.Annotations.Clear();

                //DEBUG BREAK - RUNS OLNY THE FIRST TABLE
                //break;
            }

            t.Close();
           
        }
    }
}
