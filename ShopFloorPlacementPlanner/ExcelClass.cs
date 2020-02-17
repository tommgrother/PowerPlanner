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
        public int rowNumber = 3;

        Excel.Application app;
        Excel.Workbook workBook;
        Excel.Worksheet workSheet;


        public void openExcel()
        {
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


        public void addData(Double punchH, Double punchO, Double punchA, Double punchTH, Double laserH, Double laserO, Double laserA, Double laserTH, Double BendingH, Double BendingO, Double BendingA, Double BendingTH, Double weldingH, Double weldingO, Double weldingA, Double weldingTH, Double buffingH, Double buffingO, Double buffingA, Double buffingTH, Double paintingH, Double paintingO, Double paintingA, Double paintingTH, Double packingH, Double packingO, Double packingA, Double packingTH)
        {
            //punch
            workSheet.Cells[rowNumber, "A"] = punchH;
            workSheet.Cells[rowNumber, "B"] = punchO;
            workSheet.Cells[rowNumber, "C"] = punchA;
            workSheet.Cells[rowNumber, "D"] = punchTH;
            //laser
            workSheet.Cells[rowNumber, "E"] = laserH;
            workSheet.Cells[rowNumber, "F"] = laserO;
            workSheet.Cells[rowNumber, "G"] = laserA;
            workSheet.Cells[rowNumber, "H"] = laserTH;
            //bending
            workSheet.Cells[rowNumber, "I"] = BendingH;
            workSheet.Cells[rowNumber, "J"] = BendingO;
            workSheet.Cells[rowNumber, "K"] = BendingA;
            workSheet.Cells[rowNumber, "L"] = BendingTH;
            //welding
            workSheet.Cells[rowNumber, "M"] = weldingH;
            workSheet.Cells[rowNumber, "N"] = weldingO;
            workSheet.Cells[rowNumber, "O"] = weldingA;
            workSheet.Cells[rowNumber, "P"] = weldingTH;
            //buffing
            workSheet.Cells[rowNumber, "Q"] = buffingH;
            workSheet.Cells[rowNumber, "R"] = buffingO;
            workSheet.Cells[rowNumber, "S"] = buffingA;
            workSheet.Cells[rowNumber, "T"] = buffingTH;
            //painting
            workSheet.Cells[rowNumber, "U"] = paintingH;
            workSheet.Cells[rowNumber, "V"] = paintingO;
            workSheet.Cells[rowNumber, "W"] = paintingA;
            workSheet.Cells[rowNumber, "X"] = paintingTH;
            //packing
            workSheet.Cells[rowNumber, "Y"] = packingH;
            workSheet.Cells[rowNumber, "Z"] = packingO;
            workSheet.Cells[rowNumber, "AA"] = packingA;
            workSheet.Cells[rowNumber, "AB"] = packingTH;
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
