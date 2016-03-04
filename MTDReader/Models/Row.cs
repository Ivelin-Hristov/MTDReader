namespace MTDReader.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Row
    {
        private string name;
        private List<string> cells = new List<string>();

        public Row(string name, List<string> cells)
        {
            this.Name = name;
            this.Cells = cells;
        }

        public Row()
        {
        }

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
        public List<string> Cells
        {
            get
            {
                return this.cells;
            }

            set
            {
                this.cells = value;
            }
        }


    }
}
