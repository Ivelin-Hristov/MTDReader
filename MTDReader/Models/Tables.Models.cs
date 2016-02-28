namespace MTDReader.Models
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.IO;
    public class TablesExport
    {

        public const string tableNameString = "Table";
        public const string tableElementString = "Element";

        private string tableName;
        private string tableLabel;

        private List<string> sideAxis = new List<string>();
        private List<string> topAxis = new List<string>();
        private List<string> cellItemsInfo = new List<string>();
        private List<List<string>> cellMatrix = new List<List<string>>();

        private List<string> annotations = new List<string>();
        private bool colOrientation = true;

        private XmlReader xmlDoc;
        public XmlReader XmlDoc
        {
            get { return this.xmlDoc; }
            set { this.xmlDoc = value; }
        }

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

        public TablesExport(string fileName)
        {
            this.xmlDoc = XmlReader.Create(fileName);

        }
        public void MoveToElement(string elementName, XmlNodeType elementType)
        {

            while (this.XmlDoc.Read())
            {
                if (this.XmlDoc.NodeType == elementType && this.XmlDoc.Name == elementName)
                {
                    break;
                }
                if (this.XmlDoc.EOF)
                {
                    break;                    
                };
            }

        }
        public void Close()
        {
            Console.WriteLine("XML Reader Diposed...");
            this.xmlDoc.Dispose();

        }

        public void BuildTable()
        {

        }
    }
}
