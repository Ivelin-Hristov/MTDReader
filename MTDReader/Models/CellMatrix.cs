
namespace MTDReader.Models
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CellMatrix
    {
        private string name;
        private List<Row> rows = new List<Row>();
        public CellMatrix()
        {
        }
        public CellMatrix(string name)
        {
            this.Name = name;
        }
        public CellMatrix(string name, List<Row> rows)
        {
            this.Name = name;
            this.Rows = rows;
        }
      

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                name = value;
            }
        }
        public List<Row> Rows
        {
            get
            {
                return this.rows;
            }

            set
            {
                rows = value;
            }
        }
    }
}
