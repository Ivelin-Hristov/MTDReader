namespace MTDReader.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AxisElement
    {
        private string name;
        private string label;
        private string type;

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

        public string Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
            }
        }

        public AxisElement(string name, string label, string type)
        {
            this.Name = name;
            this.Label = label;
            this.Type = type;
        }

        public override string ToString()
        {
            return this.label;
        }
    }
}
