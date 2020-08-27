using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopFloorPlacementPlanner
{
    class department_changed
    {
        //the whole point of this class is to track what departments have been updated etc so we can fire fillgrid() only for those
        //this will speed up everything hopefully :)

        //variable for each department
        public static int slimlineSelected { get; set; }
        public static int punchSelected { get; set; }
        public static int laserSelected { get; set; }
        public static int bendSelected { get; set; }
        public static int weldSelected { get; set; }
        public static int buffSelected { get; set; }
        public static int paintSelected { get; set; }
        public static int packSelected { get; set; }
        public static int storesSelected { get; set; }
        public static int dispatchSelected { get; set; }
        public static int toolSelected { get; set; }
        public static int cleaningSelected { get; set; }
        public static int managementSelected { get; set; }
        public static int hsSelected { get; set; }


        public void resetData()
        {
            slimlineSelected = 0;
            punchSelected = 0;
            laserSelected = 0;
            bendSelected = 0;
            weldSelected = 0;
            buffSelected = 0;
            paintSelected = 0;
            packSelected = 0;
            storesSelected = 0;
            dispatchSelected = 0;
            toolSelected = 0;
            cleaningSelected = 0;
            managementSelected = 0;
            hsSelected = 0;
        }

        public void setDepartment(string dept)
        {
            switch(dept)
            {
                case "Slimline":
                    slimlineSelected = -1;
                    break;
                case "Laser":
                    laserSelected = -1;
                    break;
                case "Punching":
                    punchSelected = -1;
                    break;
                case "Bending":
                    bendSelected = -1;
                    break;
                case "Welding":
                    weldSelected = -1;
                    break;
                case "Dressing":
                    buffSelected = -1;
                    break;
                case "Painting":
                    paintSelected = -1;
                    break;
                case "Packing":
                    packSelected = -1;
                    break;
                case "Stores":
                    storesSelected = -1;
                    break;
                case "Dispatch":
                    dispatchSelected = -1;
                    break;
                case "toolroom":
                    toolSelected = -1;
                    break;
                case "Cleaning":
                    cleaningSelected = -1;
                    break;
                case "Management":
                    managementSelected = -1;
                    break;
                    


            }
        }


    }
}
