namespace MTDReader.Models
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.IO;
    public class Table
    {

        private string tableName;
        private string tableLabel;

        private List<string> sideAxis = new List<string>();
        private List<string> topAxis = new List<string>();

        private List<string> cellItemsInfo = new List<string>();
        private List<List<string>> cellMatrix = new List<List<string>>();

        private List<string> annotations = new List<string>();
        private bool colOrientation = true;

        

        public string TableName
        {
            get
            {
                return this.tableName;
            }

            set
            {
                this.tableName = value;
            }
        }
        public string TableLabel
        {
            get
            {
                return this.tableLabel;
            }

            set
            {
                this.tableLabel = value;
            }
        }

        public List<string> SideAxis
        {
            get
            {
                return this.sideAxis;
            }

            set
            {
                this.sideAxis = value;
            }
        }
        public List<string> TopAxis
        {
            get
            {
                return this.topAxis;
            }

            set
            {
                this.topAxis = value;
            }
        }

        public List<string> CellItemsInfo
        {
            get
            {
                return this.cellItemsInfo;
            }

            set
            {
                this.cellItemsInfo = value;
            }
        }
        public List<List<string>> CellMatrix
        {
            get
            {
                return this.cellMatrix;
            }

            set
            {
                this.cellMatrix = value;
            }
        }

        public List<string> Annotations
        {
            get
            {
                return this.annotations;
            }

            set
            {
                this.annotations = value;
            }
        }
        public bool ColOrientation
        {
            get
            {
                return this.colOrientation;
            }

            set
            {
                this.colOrientation = value;
            }
        }

        public void Clear()
        {
            this.TableName = "";
            this.TableLabel = "";
            this.SideAxis.Clear();
            this.TopAxis.Clear();
            this.CellItemsInfo.Clear();
            this.CellMatrix.Clear();
       }

    }

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

        public void LoadMTD(string fileName)
        {
            this.xmlDoc = XmlReader.Create(fileName);
         }

        public void Close()
        {
            this.xmlDoc.Dispose();

        }

        private bool nodeEval(string nodeName, XmlNodeType nodeType, bool additionalExprssion = true)
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

        public Table MoveToNextTable()
        {
            this.table.Clear();
            this.writeToSideAxis = true;
            while (this.XmlDoc.Read())
            {
                //MOVE TO FIRST TABLE
                if (this.nodeEval("Table",XmlNodeType.Element))
                {
                    this.isInTables = true;
                }

                //REMOVE AFTER LAST TABLE
                if (this.nodeEval("Table",XmlNodeType.EndElement))
                {
                    this.isInTables = false;
                }

                //BREAK IF NOT TO FIRST
                if (!this.isInTables)
                {
                    continue;
                }

                //TABLE
                if (this.nodeEval("Table", XmlNodeType.Element))
                {
                    table.TableName = this.XmlDoc.GetAttribute("Name");
                    table.TableLabel = this.XmlDoc.GetAttribute("Description");
                }

                if (this.nodeEval("Axis", XmlNodeType.Element, this.XmlDoc.GetAttribute("Name") == "Top"))
                {
                    this.writeToSideAxis = false;
                }

                //GOTO SIDE AXIS
                if (this.nodeEval("Element", XmlNodeType.Element, this.writeToSideAxis))
                {
                    table.SideAxis.Add(this.XmlDoc.GetAttribute("Label"));
                }

                //GOTO TOP AXIS
                if (this.nodeEval("Element", XmlNodeType.Element, !this.writeToSideAxis))
                {
                    table.TopAxis.Add(this.XmlDoc.GetAttribute("Label"));
                }

                //GOTO CELLITEMS
                if (this.nodeEval("CellItem",XmlNodeType.Element))
                {
                    table.CellItemsInfo.Add(this.XmlDoc.GetAttribute("Type"));
                }

                //GOTO ROWS
                if (this.nodeEval("row", XmlNodeType.Element))
                {
                    List<string> row = new List<string>();
                    //CELL MATRIX [ROW,COL]
                    for (var col = 1; col < this.XmlDoc.AttributeCount; col++)
                    {
                        row.Add(this.XmlDoc.GetAttribute(col));
                    }
                    table.CellMatrix.Add(row);
                }

                //GOTO ANNOTATIONS
                if (this.nodeEval("Annotation", XmlNodeType.Element))
                {
                    table.Annotations.Add(this.XmlDoc.GetAttribute("Text"));
                }

                //RETURN TABLE (LEVEL IS THE LAST TAG IN TABLE)
                if (this.nodeEval("Level", XmlNodeType.Element))
                {
                    break;
                }     
            }
            return table;
        }
    }

}
