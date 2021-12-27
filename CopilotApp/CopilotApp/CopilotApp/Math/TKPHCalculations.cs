using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CopilotApp
{
    //Thoughs,  maybe THPH should inherent from tire class?  then we have tire ID and such.
    public partial class TKPHCalculations
    {
        //All K1 values Gets loaded into the dictionary on startup from K1Loader.cs
        //Use like an array: k1Values["1"] = 1.0
        //                   k1Values["6"] = 1.04
        //                   3.0 * k1Values["6"] = 3.12
        private static Dictionary<string, double> k1Values = new Dictionary<string, double>();


        private enum Status
        {
            under,
            neutral,
            over,
        }


        public TKPHCalculations()
        {
            //Each instance cover one tire. 
            //on backend.  Maybe fill a total  L , H etc before calling or creating this object.

            //maybe have these methods in a class called tire? so we always store TKPH and current real site TKPH.

            //query here to get current TKHP for that tire. 
        }

        

        public void TKPHcalc(double t_a, double l, double h, double qm)
        {
        /*
        TKPHcalc -
        Here we calculate the real cite TKPH for one cycle. This will be multiplied by the coefficient k1 if the cycle lenght exceeds 5km 
        and k2 if the ambient temperature is below or above the referenced temperature of 38C. 

       Input:
        t = Ambient temperature. Should be gathered from the vehicle
        l = The lenght of a cycle
        h = The duration of a cycle

        qc = is the load per tyre in ton (TKPH) on a laden vehicle.
        qv = is the load per tyre in ton (TKPH) on an unladen vehicle
        Note: Assume Qv and Qc is given and the TKPH will be the same on each tire.

       k1 = the coeffecient give by the michelin document. This should be an array taken from a database where such values exists.

       Output:
        The calculated TKPH value for the current cycle.

       Note when calculating Qm for the future:
       "The Qm calculation should theoretically be made for
        each tyre. However, in practice, specifi c tyre loads are not
        normally available; therefore, this leads to the assumption
        that each tyre on the same axle carries an equal load.
        When calculating the average load per tyre on the front
        and the rear axles, the greatest value of Qm shall be used
        in TKPH (TMPH) calculation."
        */

        //Assume reference temperature is 38
        int t_ref = 38;
        double k2;

        //average load per tyre
        //Maybe have qm as argument to the functions.
        //double qm = (qc + qv) / 2;


        //The number of km covered in one cycle
        double vm = l / h;

        //Calculate k2 where if t_a is higher than t_ref then we use formula (1). If t_a is below t_ref then use formula 2.
        //Good to know if t_a = t_ref then k2 will be = 1.
        if (t_a > 38)
        {
            k2 = 1 / (1 - ((0.4 * (t_a - t_ref)) / vm));
        }
        else
        {
            k2 = 1 / (1 - ((0.25 * (t_a - t_ref)) / vm));
        }


        //Calculating the real site TKPH 
        double TKPH = qm * vm * k1Values[l.ToString()] * k2;
        }
        private Status TKPHeval(double prevTKPH,double curreTKPH)
        {
            //input is the TKPH for that tire(no real site TKPH which is calculated).
           if(prevTKPH*1.1 < curreTKPH)
            {
                 return Status.over;
            }
            if(prevTKPH*0.9 > curreTKPH)
            {
                return Status.under;
            }
            else
            {
                return Status.neutral;
            }
            
           
        }

        private void TKPHsend(double preTKPH,double currTKPH)
        {
            //Status s = TKPHeval(preTKPH);   
            //Query -->
        }




    //uppdate / add query to the DB.

}
}


