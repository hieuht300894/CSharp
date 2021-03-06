﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace ExcelReadWrite
{
    public class ExcelFunction
    {
        public static object[][] ReadFile(string fileName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            object[][] result;
            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                range = xlWorkSheet.UsedRange;
                int rw = range.Rows.Count;
                int cl = range.Columns.Count;

                result = new object[rw][];
                for (int i = 1; i <= rw; i++)
                {
                    result[i - 1] = new object[cl];
                    for (int j = 1; j <= cl; j++)
                    {
                        result[i - 1][j - 1] = (range.Cells[i, j] as Excel.Range).Value2;
                    }
                }

                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                return result;
            }
            catch { return null; }
        }

        public static void WriteFile()
        {
        }
    }
}
