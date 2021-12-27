using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace CopilotApp
{
    class LifespanCalc
    {
        public LifespanCalc() { }


        private double Func(double x)
        {
            //This formula is based of using non linear regression and curve fit it into a polynomial function. This takes the precentage lost from base pressure and 
            //returns the precentage lost from lifespan.
            return Math.Pow(x, 4) + 3.743 * Math.Pow(x, 3) - 3.882 * Math.Pow(x, 2) - 0.2472 * x + 4.436 * Math.Pow(10, -5);  
        }

        public void Calc()
        {
            /*SELECT base*/
            //sql take base pressure from all 4 wheels.
           // precentage = frontRightTireBaselinePressure/baselinepressure

            /*IF NOT EXISTS(select* __)
                begin
                    
                end
         */}
    }
}
