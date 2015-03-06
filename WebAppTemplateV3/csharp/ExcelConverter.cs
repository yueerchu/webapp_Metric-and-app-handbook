using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using OfficeOpenXml;
using System.Web.UI.WebControls;



namespace DevTemplateV3.WebApp.Common
{
    public class ExcelConverter
    {
        private MemoryStream spreadSheetStream;
        private ExcelPackage spreadSheet;
        private ExcelWorksheet workSheet;
        private Int32 rowIndex;
        private Int32 colIndex;

        public ExcelConverter(String sheetName)
        {
            rowIndex = 1;
            colIndex = 1;

            spreadSheetStream = new MemoryStream();
            spreadSheet = new ExcelPackage(spreadSheetStream);
            
            workSheet = spreadSheet.Workbook.Worksheets.Add(sheetName);
        }


        public void AddRow(List<String> cellValues, List<Int32> cellColumnSpans, Boolean isBold) 
        {
            Int32 endColIndex = 0;

            for (Int32 i = 0; i < cellValues.Count; i++)
            {
                endColIndex = colIndex + cellColumnSpans[i] - 1;

                workSheet.Cells[rowIndex, colIndex].Value = cellValues[i];
                workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Merge = true;

                workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Style.Font.Bold = isBold;
                
                colIndex = endColIndex + 1;
            }

            AddEmptyRow();
        }


        public void AddEmptyRow()
        {
            rowIndex++;
            colIndex = 1;
        }


        public void ConvertTable(Table table)
        {
            TableRow row = null;
            TableCell cell = null;

            Int32 endColIndex = 0;

            LinkButton lnkbut = null;
            Image image = null;

            //Go through every row in the table
            for (Int32 i = 0; i < table.Rows.Count; i++)
            {
                row = table.Rows[i];

                //Go through every cell in the row
                for (Int32 j = 0; j < row.Cells.Count; j++)
                {
                    cell = row.Cells[j];

                    //handle column span
                    if (cell.ColumnSpan > 1)
                    {
                        endColIndex = colIndex + cell.ColumnSpan - 1;

                        workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Merge = true;
                    }
                    else
                    {
                        endColIndex = colIndex;
                    }

                    //apply formatting
                    if ((cell.Controls.Count > 0) && (cell.Controls[0].GetType() == typeof(Image)))
                    {
                        //If there's a red or green box image, color the background (needed for Dashboard)
                        image = (Image)cell.Controls[0];

                        if (image.ImageUrl.Equals("../img/greenbox.png"))
                        {
                            workSheet.Cells[rowIndex, colIndex].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            workSheet.Cells[rowIndex, colIndex].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(0, 200, 0));
                        }
                        else if (image.ImageUrl.Equals("../img/redbox.png"))
                        {
                            workSheet.Cells[rowIndex, colIndex].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            workSheet.Cells[rowIndex, colIndex].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(200, 0, 0));
                        }

                    }
                    else if (cell.CssClass.Equals("DashboardTopicLine"))
                    {
                        workSheet.Cells[rowIndex, colIndex].Style.Font.Bold = true;
                        workSheet.Cells[rowIndex, colIndex].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[rowIndex, colIndex].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(180, 180, 180)); //grey
                        workSheet.Cells[rowIndex, colIndex].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(255, 0, 0));
                    }
                    else if (cell.CssClass.Equals("DashboardHeader") || (cell.CssClass.Equals("DashboardHeaderLeftBorder")) || (cell.CssClass.Equals("DashboardHeaderBold")) || (cell.CssClass.Equals("DashboardHeaderBoldLeftBorder")))
                    {
                        workSheet.Cells[rowIndex, colIndex].Style.Font.Bold = true;
                        workSheet.Cells[rowIndex, colIndex].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[rowIndex, colIndex].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(100, 100, 100)); //grey
                        workSheet.Cells[rowIndex, colIndex].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(255, 255, 255));
                    }
                    else if (cell.CssClass.Equals("DashboardSummaryHeader"))
                    {
                        workSheet.Cells[rowIndex, colIndex].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[rowIndex, colIndex].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 201, 14)); //orange
                        workSheet.Cells[rowIndex, colIndex].Style.Font.Bold = true;
                    }
                    else if (cell.CssClass.Equals("PlantMatrixTableHeaderCell"))
                    {
                        workSheet.Cells[rowIndex, colIndex].Style.Font.Bold = true;
                        workSheet.Cells[rowIndex, colIndex].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[rowIndex, colIndex].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(180, 180, 180)); //grey
                        workSheet.Cells[rowIndex, colIndex].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(0, 0, 0));
                        //workSheet.Cells[rowIndex, colIndex].Style.WrapText = true;
                    }
                    else if (cell.BackColor.IsEmpty == false)
                    {
                        workSheet.Cells[rowIndex, colIndex].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[rowIndex, colIndex].Style.Fill.BackgroundColor.SetColor(cell.BackColor);
                    }
                    

                    //apply value
                    if (cell.Controls.Count == 0)
                    {
                        //workSheet.Cells[rowIndex, colIndex].Value = cell.Text;
                        SetCellText(workSheet.Cells[rowIndex, colIndex], cell.Text);
                    }
                    else
                    {
                        //Because cell text is inside Link Button, use the link button's text
                        if (cell.Controls[0].GetType() == typeof(LinkButton))
                        {
                            lnkbut = (LinkButton)cell.Controls[0];
                            workSheet.Cells[rowIndex, colIndex].Value = lnkbut.Text;
                        }
                    }

                    workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Style.Border.Top.Color.SetColor(System.Drawing.Color.FromArgb(0, 0, 0));

                    workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Style.Border.Left.Color.SetColor(System.Drawing.Color.FromArgb(0, 0, 0));

                    workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Style.Border.Right.Color.SetColor(System.Drawing.Color.FromArgb(0, 0, 0));

                    workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    workSheet.Cells[rowIndex, colIndex, rowIndex, endColIndex].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.FromArgb(0, 0, 0));
                    

                    colIndex = endColIndex + 1;
                }

                rowIndex++;
                colIndex = 1;
            }
        }


        public Byte[] GetBytes()
        {
            Byte[] bytes = null;

            workSheet.Cells.AutoFitColumns();

            bytes = spreadSheet.GetAsByteArray();

            return bytes;
        }


        private void SetCellText(ExcelRange range, String text)
        {
            String format = "";
            Boolean isNumeric = false;

            String rawNumberText = "";
            Boolean isDecimal = false;
            Boolean isPercent = false;
            Int32 precision = 0;
            Double dblNumber = 0;
            Int32 intNumber = 0;

            
            isNumeric = IsStringNumeric(text);

            if (isNumeric)
            {
                //Get Raw Number
                rawNumberText = text;
                rawNumberText = rawNumberText.Replace("$", "");
                rawNumberText = rawNumberText.Replace("%", "");
                rawNumberText = rawNumberText.Replace(",", "");

                //Detect if percent
                if (text.Contains("%"))
                {
                    isPercent = true;
                }

                //Detect if decimal or integer
                if (rawNumberText.Contains("."))
                {
                    isDecimal = true;

                    precision = rawNumberText.Length - rawNumberText.IndexOf(".") - 1;
                }

                format = GetCellFormat(text, isDecimal, precision);

                //Output as appropriate object
                if (isDecimal)
                {
                    dblNumber = Double.Parse(rawNumberText);

                    if (isPercent)
                    {
                        dblNumber = dblNumber / 100.00;
                    }

                    range.Value = dblNumber;
                }
                else
                {
                    intNumber = Int32.Parse(rawNumberText);
                    range.Value = intNumber;
                }

                //Set Cell Formatting
                range.Style.Numberformat.Format = format;
            }
            else
            {
                range.Value = text;
            }
        }


        private Boolean IsStringNumeric(String text)
        {
            Boolean result = false;
            String manipText = "";
            Double number = 0;

            manipText = text;
            manipText = manipText.Replace("$", "");
            manipText = manipText.Replace("%", "");
            manipText = manipText.Replace(",", "");

            if(Double.TryParse(manipText, out number)) 
            {
                result = true;
            }

            return result;
        }


        private String GetCellFormat(String text, Boolean isDecimal, Int32 precision)
        {
            String format = "";

            //default start
            if (text.Contains(","))
            {
                format = "#,##0";
            }
            else
            {
                format = "0";
            }

            //precision
            if (isDecimal)
            {
                format = format + ".";

                for (Int32 i = 0; i < precision; i++)
                {
                    format = format + "0";
                }
            }

            //type
            if (text.Contains("%"))
            {
                format = format + "%";
            }
            else if (text.Contains("$"))
            {
                format = "$" + format;
            }


            return format;
        }


    }
}