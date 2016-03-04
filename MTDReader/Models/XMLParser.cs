namespace MTDReader.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.IO;
     public class XMLParser
    {
        private bool writeToSideAxis = true;
        private bool isInTables = false;
        private XmlReader xmlDoc;
        private Table table = new Table();
   
        public XmlReader XmlDoc
        {
            get { return this.xmlDoc; }
            set { this.xmlDoc = value; }
        }
        public Table Table
        {
            get
            {
                return this.table;
            }

            set
            {
                this.table = value;
            }
        }
        public bool IsInTables
        {
            get
            {
                return this.isInTables;
            }

            set
            {
                this.isInTables = value;
            }
        }

        private bool evalNode(string nodeName, XmlNodeType nodeType, bool additionalExprssion = true)
        {
            if (this.XmlDoc.Name == nodeName && this.XmlDoc.NodeType == nodeType && additionalExprssion)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void LoadMTD(string fileName)
        {
            this.xmlDoc = XmlReader.Create(fileName);
         }  
        public void Close()
        {
            this.xmlDoc.Dispose();

        }
        public void MoveToNextTable()
        {
            Console.Write('.');

            this.Table.Clear();
            this.writeToSideAxis = true;
            while (this.XmlDoc.Read())
            {
                //MOVE TO FIRST TABLE - CONTINUE IF BEFORE THAT;
                if (this.evalNode("Tables",XmlNodeType.Element))
                {
                    this.isInTables = true;
                }

                //BREAK IF NOT YET FIRST
                if (!this.isInTables)
                {
                    continue;
                }

                //BREAK AFTER LAST TABLE
                if (this.evalNode("Tables",XmlNodeType.EndElement))
                {
                    this.isInTables = false;
                    break;
                }

                //TABLE
                if (this.evalNode("Table", XmlNodeType.Element))
                {
                    Table.Name = this.XmlDoc.GetAttribute("Name");
                    Table.Label = this.XmlDoc.GetAttribute("Description");
                }

                if (this.evalNode("Axis", XmlNodeType.Element, this.XmlDoc.GetAttribute("Name") == "Top"))
                {
                    this.writeToSideAxis = false;
                }

                //GOTO SIDE AXIS
                if (this.evalNode("Element", XmlNodeType.Element, this.writeToSideAxis))
                {
                        Table.SideAxis.Add(
                        new AxisElement(
                               this.XmlDoc.GetAttribute("Name"),
                               this.XmlDoc.GetAttribute("Label"),
                               this.XmlDoc.GetAttribute("Type")
                           )

                       );              
                }

                //GOTO TOP AXIS
                if (this.evalNode("Element", XmlNodeType.Element, !this.writeToSideAxis))
                {
                        Table.TopAxis.Add(
                        new AxisElement(
                                this.XmlDoc.GetAttribute("Name"),
                                this.XmlDoc.GetAttribute("Label"),
                                this.XmlDoc.GetAttribute("Type")
                            )

                        );
                 }

                //GOTO CELLITEMS
                if (this.evalNode("CellItem",XmlNodeType.Element))
                {
                    Table.CellItemsInfo.Add(this.XmlDoc.GetAttribute("Type"));

                    this.Table.CellMatrixes.Add(new CellMatrix(this.XmlDoc.GetAttribute("Type")));
                }


                //GOTO ROWS
                if (this.evalNode("row", XmlNodeType.Element))
                {

                    List<string> row = new List<string>();

                    //CELL MATRIX [ROW,COL]
                    for (var col = 0; col < this.XmlDoc.AttributeCount; col++)
                    {
                        row.Add(this.XmlDoc.GetAttribute(col));
                    }

                    for(var matrixIndex = 0; matrixIndex < this.Table.CellMatrixes.Count; matrixIndex++)
                    {
                        List<string> rowPart = row.Skip(matrixIndex+1).Where((x, i) => i % (this.Table.CellMatrixes.Count) == 0).ToList<string>();
                        this.Table.CellMatrixes[matrixIndex].Rows.Add(new Row(row[0],rowPart));
                    }

                    
                }

                //GOTO ANNOTATIONS
                if (this.evalNode("Annotation", XmlNodeType.Element))
                {
                    Table.Annotations.Add(this.XmlDoc.GetAttribute("Text"));
                }

                //RETURN TABLE (LEVEL IS THE LAST TAG IN TABLE)
                if (this.evalNode("Table", XmlNodeType.EndElement))
                {
                    break;
                }     
            }
        }
    }

}
