

namespace MTDReader.TableBuilder
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CsvBuilder
    {
        /// <summary>
        /// This class does work
        /// </summary>
        
        private string outputPath;
        private StringBuilder outputTable = new StringBuilder();

        public CsvBuilder(string outputFilePath)
        {
            this.OutputPath = outputFilePath;

            if (File.Exists(this.OutputPath))
            {
                File.Delete(this.OutputPath);
            }
        }

        public void BuildTable(Table inputTable)
        {
            this.outputTable.AppendFormat("{0}{1}{0}\n","\"", inputTable.Name);
            this.outputTable.AppendFormat("{0}{1}{0}\n", "\"", inputTable.Label);

            for (var i = 0; i<inputTable.Annotations.Count/2;i++)
            {
                this.outputTable.AppendFormat("{0}{1}{0}\n", "\"", inputTable.Annotations[i]);
            }

            for (var i = 0; i<inputTable.TopAxis.Count; i++)
            {
                this.outputTable.AppendFormat("\t{0}{1}{0}", "\"", inputTable.TopAxis[i]);
            }
            this.outputTable.Append("\n");

            for (var i = 0; i < inputTable.SideAxis.Count; i++)
            {
                this.outputTable.AppendFormat("{0}{1}{0}\t", "\"", inputTable.SideAxis[i]);

                for (var j=0; j < inputTable.CellMatrixes[0].Rows[i].Cells.Count; j++)
                {
                    this.outputTable.AppendFormat("{0}{1}{0}\t", "\"", inputTable.CellMatrixes[0].Rows[i].Cells[j]);
                }

                this.outputTable.AppendLine();
            }

            for (var i = (inputTable.Annotations.Count/2+1); i<inputTable.Annotations.Count;i++)
            {
                this.outputTable.AppendFormat("{0}{1}{0}\n", "\"", inputTable.Annotations[i]);
            }

            using (TextWriter textWriter = new StreamWriter(this.OutputPath, true, Encoding.UTF8))
            {
                
                textWriter.Write(this.outputTable);
            }
            this.outputTable.Clear();
        }

        public string OutputPath
        {
            get
            {
                return this.outputPath;
            }

            set
            {
                this.outputPath = value;
            }
        }


    }
}
