using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;

namespace DevTemplateV3.WebApp.Common
{
    public class TextConverter
    {
        private StringBuilder builder;


        public TextConverter()
        {
            builder = new StringBuilder("");
        }


        public void StartDocument()
        {
            //do nothing
        }


        public void ClearDocument()
        {
            builder = new StringBuilder("");
        }


        public void EndDocument()
        {
            //do nothing
        }

        public void AddLine(String line)
        {
            builder.AppendLine(line);
        }


        public String GetText()
        {
            return builder.ToString();
        }


        public void ConvertTable(Table table)
        {
            TableRow row = null;
            TableCell cell = null;

            //Go through every row in the table
            for (Int32 i = 0; i < table.Rows.Count; i++)
            {
                row = table.Rows[i];

                //Go through every cell in the row
                for (Int32 j = 0; j < row.Cells.Count; j++)
                {
                    cell = row.Cells[j];

                    builder.Append(cell.Text.Replace(",", ""));

                    if (j != (row.Cells.Count - 1))
                    {
                        builder.Append(",");
                    }
                }

                builder.AppendLine("");
            }
        }
    }
}