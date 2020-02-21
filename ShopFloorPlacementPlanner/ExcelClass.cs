using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace ShopFloorPlacementPlanner
{
    class ExcelClass
    {
        public string filePath = @"\\designsvr1\DropBox\TEMPLATE.xlsx";
        

        Excel.Application app;
        Excel.Workbook workBook;
        Excel.Worksheet workSheet;

        public int rowNumber { get; set; }
        public void openExcel(int _rownumber)
        {
            rowNumber = _rownumber;
            app = null;
            app = new Excel.Application(); // create a new instance
            app.DisplayAlerts = false; //turn off annoying alerts that make me want to cryyyy

            //no idea how this works
            workBook = (Excel.Workbook)(app.Workbooks._Open(filePath, System.Reflection.Missing.Value,
           System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
           System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
           System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
           System.Reflection.Missing.Value, System.Reflection.Missing.Value));
            //but it opens the workbook

            workSheet = (Excel.Worksheet)workBook.Worksheets[1];//set the target worksheet
        }

        public void addDAtes(string Monday, string Tuesday, string Wednesday, string Thursday, string Friday)
        {
            workSheet.Cells[1, "A"] = Monday;
            workSheet.Cells[5, "A"] = Tuesday;
            workSheet.Cells[9, "A"] = Wednesday;
            workSheet.Cells[13, "A"] = Thursday;
            workSheet.Cells[17, "A"] = Friday;
        }
        public void addData(Double punchH, Double punchO, Double punchA, Double laserH, Double laserO, Double laserA, Double BendingH, Double BendingO, Double BendingA, Double weldingH, Double weldingO, Double weldingA, Double buffingH, Double buffingO, Double buffingA, Double paintingH, Double paintingO, Double paintingA, Double packingH, Double packingO, Double packingA)
        {
            //punch
            workSheet.Cells[rowNumber, "A"] = punchH;
            workSheet.Cells[rowNumber, "B"] = punchO;
            workSheet.Cells[rowNumber, "C"] = punchA;
           // workSheet.Cells[rowNumber, "D"] = punchTH;
            //laser
            workSheet.Cells[rowNumber, "E"] = laserH;
            workSheet.Cells[rowNumber, "F"] = laserO;
            workSheet.Cells[rowNumber, "G"] = laserA;
            //workSheet.Cells[rowNumber, "H"] = laserTH;
            //bending
            workSheet.Cells[rowNumber, "I"] = BendingH;
            workSheet.Cells[rowNumber, "J"] = BendingO;
            workSheet.Cells[rowNumber, "K"] = BendingA;
            //workSheet.Cells[rowNumber, "L"] = BendingTH;
            //welding
            workSheet.Cells[rowNumber, "M"] = weldingH;
            workSheet.Cells[rowNumber, "N"] = weldingO;
            workSheet.Cells[rowNumber, "O"] = weldingA;
          //  workSheet.Cells[rowNumber, "P"] = weldingTH;
            //buffing
            workSheet.Cells[rowNumber, "Q"] = buffingH;
            workSheet.Cells[rowNumber, "R"] = buffingO;
            workSheet.Cells[rowNumber, "S"] = buffingA;
            //workSheet.Cells[rowNumber, "T"] = buffingTH;
            //painting
            workSheet.Cells[rowNumber, "U"] = paintingH;
            workSheet.Cells[rowNumber, "V"] = paintingO;
            workSheet.Cells[rowNumber, "W"] = paintingA;
            //workSheet.Cells[rowNumber, "X"] = paintingTH;
            //packing
            workSheet.Cells[rowNumber, "Y"] = packingH;
            workSheet.Cells[rowNumber, "Z"] = packingO;
            workSheet.Cells[rowNumber, "AA"] = packingA;
           // workSheet.Cells[rowNumber, "AB"] = packingTH;
        }


        public void print()
        {
            // Print out 1 copy to the default printer:
            workSheet.PrintOut(
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Cleanup:
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }


        public  void closeExcel()
        {
            try
            {
                //no idea how this works ----again
                workBook.SaveAs(filePath, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                System.Reflection.Missing.Value, System.Reflection.Missing.Value); 
                // Save data in excel


                workBook.Close(true, filePath, System.Reflection.Missing.Value); // close the worksheet


            }
            finally
            {
                if (app != null)
                {
                    app.Quit(); // close the excel application
                }
            }
        }

    }
}
