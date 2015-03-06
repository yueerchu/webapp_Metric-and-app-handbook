using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;

namespace DevTemplateV3.WebApp.Common
{
    public class HtmlConverter
    {
        private StringBuilder builder;


        public HtmlConverter()
        {
            builder = new StringBuilder("");
        }


        public void StartDocument()
        {
            builder.AppendLine("<table border=\"1\">");
        }


        public void ClearDocument()
        {
            builder = new StringBuilder("");
        }


        public void EndDocument()
        {
            builder.AppendLine("</table>");
        }


        public void ConvertTable(Table table)
        {
            TableRow row = null;
            TableCell cell = null;
            Boolean isBold = false;

            LinkButton lnkbut = null;
            Image image = null;

            //Go through every row in the table
            for (Int32 i = 0; i < table.Rows.Count; i++)
            {
                row = table.Rows[i];

                builder.AppendLine("<tr>");

                //Go through every cell in the row
                for (Int32 j = 0; j < row.Cells.Count; j++)
                {
                    cell = row.Cells[j];

                    //---Apply Formatting---
                    builder.Append("<td");

                    //column span
                    if (cell.ColumnSpan != 0)
                    {
                        builder.AppendLine(" colspan=" + cell.ColumnSpan);
                    }

                    //Background Color, Font Color and Bold
                    if ((cell.Controls.Count > 0) && (cell.Controls[0].GetType() == typeof(Image)))
                    {
                        //If there's a red or green box image, color the background (needed for Dashboard)
                        image = (Image)cell.Controls[0];

                        if (image.ImageUrl.Equals("img/greenbox.png"))
                        {
                            builder.Append(" bgcolor=\"Green\"");
                            isBold = false;
                        }
                        else if (image.ImageUrl.Equals("img/redbox.png"))
                        {
                            builder.Append(" bgcolor=\"Red\"");
                            isBold = false;
                        }

                    }
                    else if (cell.CssClass.Equals("DashboardTopicLine"))
                    {
                        builder.Append(" bgcolor=\"DarkGray\"> <b> <font color=\"Red\"");
                        isBold = true;
                    }
                    else if (cell.CssClass.Equals("DashboardHeader"))
                    {
                        builder.Append(" bgcolor=\"Gray\"> <b> <font color=\"White\"");
                        isBold = true;
                    }
                    else if (cell.CssClass.Equals("DashboardSummaryHeader"))
                    {
                        builder.Append(" bgcolor=\"Orange\"> <b> <font color=\"Black\"");
                        isBold = true;
                    }
                    else
                    {
                        //Default Formatting
                        builder.Append(" bgcolor=\"White\"> <font color=\"Black\"");
                        isBold = false;
                    }

                    builder.AppendLine(">");


                    //---Apply Contents---
                    if (cell.Controls.Count == 0)
                    {
                        builder.Append(cell.Text);
                    }
                    else
                    {
                        //Because cell text is inside Link Button, use the link button's text
                        if (cell.Controls[0].GetType() == typeof(LinkButton))
                        {
                            lnkbut = (LinkButton)cell.Controls[0];

                            builder.Append(lnkbut.Text);
                        }
                    }

                    builder.Append("</font>");

                    if (isBold)
                    {
                        builder.Append("</b>");
                    }

                    builder.AppendLine("</td>");
                }

                builder.AppendLine("</tr>");
            }
        }


        public void AddLine(String line)
        {
            //Add Custom Row into document (line variable must have proper html tags)
            builder.AppendLine("<tr>" + line + "</tr>");
        }


        public String GetText()
        {
            //Get Final Result
            return builder.ToString();
        }

        
    }
}
