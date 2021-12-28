﻿using MySqlConnector;
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
       
       

        private enum Status //the status of tire
        {
            UNDER,
            NEUTRAL,
            OVER,
        }

        private double t_a=0; //the ambient temperature.
        private double l=0;//the length driven in km
        private double qm=0; //the avg weigth
        private double qc=0; //payload weigth
        private double qv=0; //base weigth
        private double h = 0;
        private int t_ref = 38; //Reference temperature.
        private double k2;
        private int tracker = 0;
        private double l_empt = 0;
        private double h_empt = 0;
        private double l_load = 0;
        private double h_load = 0;
        private double preSetTKPH;
        private Status res;
        
       

        public TKPHCalculations()
        {
            //query to get current TKHP for that tire. Set id to the local machine's
             string query = "SELECT tkph FROM tpms_vehicle WHERE id = '123'"; //ADD MODEL ASWELL.
             MySqlDataReader reader = (Database.SendQuery(query)).Result;
             preSetTKPH = double.Parse(reader["tkph"].ToString());

             query = "SELECT weigth FROM tpms_vehicle WHERE id = '123',"; //ADD MODEL ASWELL.
             reader = (Database.SendQuery(query)).Result;
             qv = double.Parse(reader["weight"].ToString());

            
      

        }


        

        public void Track()
        {
          

           //Obstacle: how do we measure distanceDriven. do we give the change or how do we track that?
            if(l_empt != MachineBusData.distanceDrivenEmpty)
            {
                l_empt = l_empt + (MachineBusData.distanceDrivenEmpty-l_empt);
            }
            if(l_load != MachineBusData.distanceDrivenLoaded)
            {
                l_load = l_load + (MachineBusData.distanceDrivenEmpty-l_load);
            }
            
             if(h_empt != MachineBusData.machineHoursEmpty)
            {
                h_empt = h_empt + (MachineBusData.machineHoursEmpty-h_empt);
            }
            if(l_load != MachineBusData.machineHoursLoaded)
            {
                h_load = h_load + (MachineBusData.machineHoursLoaded-h_load);
            }
            
            
            //it will ad to qc everytime we call this function but the tracker variable helps up know how many times we have added to qc.
            //dividing qc with the number of times we called this func should give us a good avg of qc.
            qc = qc + MachineBusData.payloadTonnes;

            //t_a = t_a + temperature
            t_a = t_a + 10;

            //adds to the counter so we can get a good average when doing calc. 
            tracker++;
           

            /*
            TKPHcalc -
            Here we calculate the real cite TKPH. This will be multiplied by the coefficient k1 if the cycle lenght exceeds 5km 
            and k2 if the ambient temperature is below or above the referenced temperature of 38C. 

           Input:
         */

        }



        private void Evaluate(double rsTKPH)
        {
             
            

            //if the real site TKPH is higher than 10% of the pre calculated TKPH then it is over specced. 
            //If real site TKPH is below by 10% or lower then it is overspecced.
            if((preSetTKPH*1.1) < rsTKPH)
             {
                  res = Status.OVER;
             }
             else if((preSetTKPH*0.9) > rsTKPH)
             {
                 res = Status.UNDER;
             }else
             {
                 res = Status.NEUTRAL;  
             }
                 
             

            //Do SQLCOMMAND to UPDATE spec in DB.
            /*
             IF NOT EXISTS(SELECT tkph FROM tpms_vehicle WHERE id = 1)
                BEGIN
	            UPDATE tpms_vehicle SET tkph = 123 WHERE tpms_vehicle = 1
                END
            ELSE
                BEGIN
	            INSERT INTO tpms_vehicle(tkph) VALUES (321)
                END
            */
          
            //Prefer to to do a query like above. 
           // string sqlStatement = "INSERT INTO tpms_vehicle(tkph) VALUES ("+ res +")";
           string sqlStatement = "UPDATE tpms_vehicle SET tkph =  '"+ res +"' WHERE id = '123'"; //using update as we assume there is a column.
           int nrOfRowsAffected = Database.SendNonQuery(sqlStatement).Result;
       
             
            return;
        }

        

        public void Calc()
        {
        
        //total length and hours driven during this period.
        l = l_empt + l_load;
        h = h_empt + h_load;
        
        //calculating the lenght it has gone during the amount of hours. Divide på 7 to get average per day.
        double vm = ((l / h)/7);
       
        

        


        //Calculate k2 where if t_a is higher than t_ref then we use formula (1). If t_a is below t_ref then use formula 2.
        //Good to know if t_a = t_ref then k2 will be = 1.
        if (t_a/tracker > 38)
        {
            k2 = 1 / (1 - ((0.4 * (t_a - t_ref)) / vm));
        }
        else
        {
            k2 = 1 / (1 - ((0.25 * (t_a - t_ref)) / vm));
        }

            qm = (((qv/tracker) + qc)/2);
            
            l = l / 7;

            //Calculating the real site TKPH which in this casé is the average per day for that specific week.
            double TKPH = qm * vm * k1Values[Math.Ceiling(l).ToString()] * k2;

            Evaluate(TKPH);
            tracker = 0;
            l_empt = 0;
            h_empt = 0;
            l_load = 0;
            h_load = 0;
            qc=0;
        
        return;
        }

}
}


