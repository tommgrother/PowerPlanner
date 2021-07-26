using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace ShopFloorPlacementPlanner
{
    internal class ExcelClass
    {
        public string filePath = @"\\designsvr1\public\Kevin Power Planner\TEMPLATE.xlsx";

        public int rowNumber { get; set; }
        public uint process_ID { get; set; }

        public string fileName { get; set; }

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary> Tries to find and kill process by hWnd to the main window of the process.</summary>
        /// <param name="hWnd">Handle to the main window of the process.</param>
        /// <returns>True if process was found and killed. False if process was not found by hWnd or if it could not be killed.</returns>
        public static bool TryKillProcessByMainWindowHwnd(int hWnd)
        {
            uint processID;
            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            if (processID == 0) return false;
            try
            {
                Process.GetProcessById((int)processID).Kill();
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Win32Exception)
            {
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return true;
        }

        public static void KillProcessByMainWindowHwnd(int hWnd)
        {
            uint processID;
            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            if (processID == 0)
                throw new ArgumentException("Process has not been found by the given main window handle.", "hWnd");
            Process.GetProcessById((int)processID).Kill();
        }

        public void openExcel(int print, int _rownumber, string _fileName, string Monday, string Tuesday, string Wednesday, string Thursday, string Friday, Double punchH, Double punchO, Double punchA, Double laserH, Double laserO, Double laserA, Double BendingH, Double BendingO, Double BendingA, Double weldingH, Double weldingO, Double weldingA, Double buffingH, Double buffingO, Double buffingA, Double paintingH, Double paintingO, Double paintingA, Double packingH, Double packingO, Double packingA)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbooks workbooks;
            Excel.Workbook excelBook;
            fileName = _fileName;
            //app = null;
            //app = new Excel.Application(); // create a new instance
            excelApp.DisplayAlerts = false; //turn off annoying alerts that make me want to cryyyy
            uint processID = 0;
            rowNumber = _rownumber;

            workbooks = excelApp.Workbooks;
            excelBook = workbooks.Add(filePath);
            Excel.Sheets sheets = excelBook.Worksheets;
            Excel.Worksheet excelSheet = (Excel.Worksheet)(sheets[1]);
            excelSheet.DisplayRightToLeft = false;

            //no idea how this works
            // workBook = (Excel.Workbook)(app.Workbooks._Open(filePath, System.Reflection.Missing.Value,
            //System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            //System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            //System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            //System.Reflection.Missing.Value, System.Reflection.Missing.Value));
            //but it opens the workbook

            //workSheet = (Excel.Worksheet)workBook.Worksheets[1];//set the target worksheet

            process_ID = processID;
            //add dates
            excelSheet.Cells[1, "A"] = Monday;
            excelSheet.Cells[5, "A"] = Tuesday;
            excelSheet.Cells[9, "A"] = Wednesday;
            excelSheet.Cells[13, "A"] = Thursday;
            excelSheet.Cells[17, "A"] = Friday;
            //add data
            //punch
            excelSheet.Cells[rowNumber, "A"] = punchH;
            excelSheet.Cells[rowNumber, "B"] = punchO;
            excelSheet.Cells[rowNumber, "C"] = punchA;
            // workSheet.Cells[rowNumber, "D"] = punchTH;
            //laser
            excelSheet.Cells[rowNumber, "E"] = laserH;
            excelSheet.Cells[rowNumber, "F"] = laserO;
            excelSheet.Cells[rowNumber, "G"] = laserA;
            //workSheet.Cells[rowNumber, "H"] = laserTH;
            //bending
            excelSheet.Cells[rowNumber, "I"] = BendingH;
            excelSheet.Cells[rowNumber, "J"] = BendingO;
            excelSheet.Cells[rowNumber, "K"] = BendingA;
            //workSheet.Cells[rowNumber, "L"] = BendingTH;
            //welding
            excelSheet.Cells[rowNumber, "M"] = weldingH;
            excelSheet.Cells[rowNumber, "N"] = weldingO;
            excelSheet.Cells[rowNumber, "O"] = weldingA;
            //  workSheet.Cells[rowNumber, "P"] = weldingTH;
            //buffing
            excelSheet.Cells[rowNumber, "Q"] = buffingH;
            excelSheet.Cells[rowNumber, "R"] = buffingO;
            excelSheet.Cells[rowNumber, "S"] = buffingA;
            //workSheet.Cells[rowNumber, "T"] = buffingTH;
            //painting
            excelSheet.Cells[rowNumber, "U"] = paintingH;
            excelSheet.Cells[rowNumber, "V"] = paintingO;
            excelSheet.Cells[rowNumber, "W"] = paintingA;
            //workSheet.Cells[rowNumber, "X"] = paintingTH;
            //packing
            excelSheet.Cells[rowNumber, "Y"] = packingH;
            excelSheet.Cells[rowNumber, "Z"] = packingO;
            excelSheet.Cells[rowNumber, "AA"] = packingA;
            // workSheet.Cells[rowNumber, "AB"] = packingTH;

            //print possibly
            if (print == 1)
            {
                excelSheet.PrintOut(
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }

            // now we release the objects
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(rng);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
            //excelBook.Save();
            excelBook.SaveAs(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing);
            excelBook.Close(true);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            excelApp = null;
            workbooks = null;
            try
            {
                excelApp = new Excel.Application();
                workbooks = excelApp.Workbooks;
            }
            finally
            {
                if (workbooks != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }

        //public void addDAtes(string Monday, string Tuesday, string Wednesday, string Thursday, string Friday)
        //{
        //    worksheet.Cells[1, "A"] = Monday;
        //    workSheet.Cells[5, "A"] = Tuesday;
        //    workSheet.Cells[9, "A"] = Wednesday;
        //    workSheet.Cells[13, "A"] = Thursday;
        //    workSheet.Cells[17, "A"] = Friday;
        //}
        //public void addData(Double punchH, Double punchO, Double punchA, Double laserH, Double laserO, Double laserA, Double BendingH, Double BendingO, Double BendingA, Double weldingH, Double weldingO, Double weldingA, Double buffingH, Double buffingO, Double buffingA, Double paintingH, Double paintingO, Double paintingA, Double packingH, Double packingO, Double packingA)
        //{
        //    //punch
        //    workSheet.Cells[rowNumber, "A"] = punchH;
        //    workSheet.Cells[rowNumber, "B"] = punchO;
        //    workSheet.Cells[rowNumber, "C"] = punchA;
        //    // workSheet.Cells[rowNumber, "D"] = punchTH;
        //    //laser
        //    workSheet.Cells[rowNumber, "E"] = laserH;
        //    workSheet.Cells[rowNumber, "F"] = laserO;
        //    workSheet.Cells[rowNumber, "G"] = laserA;
        //    //workSheet.Cells[rowNumber, "H"] = laserTH;
        //    //bending
        //    workSheet.Cells[rowNumber, "I"] = BendingH;
        //    workSheet.Cells[rowNumber, "J"] = BendingO;
        //    workSheet.Cells[rowNumber, "K"] = BendingA;
        //    //workSheet.Cells[rowNumber, "L"] = BendingTH;
        //    //welding
        //    workSheet.Cells[rowNumber, "M"] = weldingH;
        //    workSheet.Cells[rowNumber, "N"] = weldingO;
        //    workSheet.Cells[rowNumber, "O"] = weldingA;
        //    //  workSheet.Cells[rowNumber, "P"] = weldingTH;
        //    //buffing
        //    workSheet.Cells[rowNumber, "Q"] = buffingH;
        //    workSheet.Cells[rowNumber, "R"] = buffingO;
        //    workSheet.Cells[rowNumber, "S"] = buffingA;
        //    //workSheet.Cells[rowNumber, "T"] = buffingTH;
        //    //painting
        //    workSheet.Cells[rowNumber, "U"] = paintingH;
        //    workSheet.Cells[rowNumber, "V"] = paintingO;
        //    workSheet.Cells[rowNumber, "W"] = paintingA;
        //    //workSheet.Cells[rowNumber, "X"] = paintingTH;
        //    //packing
        //    workSheet.Cells[rowNumber, "Y"] = packingH;
        //    workSheet.Cells[rowNumber, "Z"] = packingO;
        //    workSheet.Cells[rowNumber, "AA"] = packingA;
        //    // workSheet.Cells[rowNumber, "AB"] = packingTH;
        //}

        public void print()
        {
            // Print out 1 copy to the default printer:
            //workSheet.PrintOut(
            //    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            //    Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Cleanup:
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

        //public void closeExcel()
        //{
        //    int hWnd = app.Application.Hwnd;
        //    app.Quit();
        //    Marshal.FinalReleaseComObject(app);
        //    TryKillProcessByMainWindowHwnd(hWnd);

        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();

        //    System.Diagnostics.Process.Start(@"\\designsvr1\DropBox\TEMPLATE-" + fileName + ".xlsx");
        //}
    }
}