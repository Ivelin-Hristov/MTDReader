namespace MTDReader.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Table
    {
        public Table()
        {

        }

        public Table(Table tab)
        {
            this.Name = tab.Name;
            this.Label = tab.Label;
            this.SideAxis = tab.SideAxis;
            this.TopAxis = tab.TopAxis;
            this.CellItemsInfo = tab.CellItemsInfo;
            this.CellMatrixes = tab.CellMatrixes;
            this.Annotations = tab.Annotations;
        }
        private string name;
        private string label;

        private List<AxisElement> sideAxis = new List<AxisElement>();
        private List<AxisElement> topAxis = new List<AxisElement>();

        private List<string> cellItemsInfo = new List<string>();
        private List<CellMatrix> cellMatrixes = new List<CellMatrix>();

        private List<string> annotations = new List<string>();

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }
        public string Label
        {
            get
            {
                return this.label;
            }

            set
            {
                this.label = value;
            }
        }

        public List<AxisElement> SideAxis
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
        public List<AxisElement> TopAxis
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

        public List<CellMatrix> CellMatrixes
        {
            get
            {
                return this.cellMatrixes;
            }

            set
            {
                this.cellMatrixes = value;
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

        public void Clear()
        {
            this.Name = "";
            this.Label = "";
            this.SideAxis.Clear();
            this.TopAxis.Clear();
            this.CellItemsInfo.Clear();
            this.CellMatrixes.Clear();
            this.Annotations.Clear();
        }
    }
}
