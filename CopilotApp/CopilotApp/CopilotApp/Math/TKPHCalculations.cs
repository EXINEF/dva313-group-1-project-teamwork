using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using System.Threading.Tasks;


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

        //private decimal t_a=0; //the ambient temperature.
        //private decimal l=0;//the length driven in km
        //private decimal qm=0; //the avg weigth
        //private decimal qc=0; //payload weigth
        private decimal qv=0; //base weigth
        //private decimal h = 0;
        private double t_a = 0.0;
        private double l = 0.0;
        private double qm = 0.0;
        private double qc = 0.0;
        private double h = 0.0;
        private int t_ref = 38; //Reference temperature.
        private double k2;
        private int tracker = 0;
        private decimal l_k1 = 0;
        private decimal l_empt = 0;
        private decimal h_empt = 0;
        private decimal l_load = 0;
        private decimal h_load = 0;
        private double preSetTKPH;
        private Status res;
        private int n = 0;
        private int n_curr = 0;
        private double vm = 0;
        
       

        public TKPHCalculations()
        {
            


            //query to get current TKHP for that tire. Set id to the local machine's
             string query = "SELECT tkph FROM tpms_vehicle WHERE id = '" + MachineData.machineID + "'"; //ADD MODEL ASWELL. maybe
             MySqlDataReader reader = (Database.SendQuery(query));
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
             preSetTKPH = double.Parse(reader["tkph"].ToString());
            
           

            query = "SELECT weight FROM tpms_vehicle WHERE id = '"+ MachineData.machineID +"'"; //ADD MODEL ASWELL. maybe
            reader = Database.SendQuery(query);
            while (reader == null)
            {
                reader = (Database.SendQuery(query));
            }
            reader.Read();
            qv = decimal.Parse(reader["weight"].ToString());

           

        }


        
        /*
        public void Track()
        {
          

         
            if(l_empt != (decimal)MachineBusData.distanceDrivenEmpty)
            {
                l_empt = l_empt + ((decimal)MachineBusData.distanceDrivenEmpty-l_empt);
            }
            if(l_load != (decimal)MachineBusData.distanceDrivenLoaded)
            {
                l_load = l_load + ((decimal)MachineBusData.distanceDrivenEmpty-l_load);
            }
            
             if(h_empt != (decimal)MachineBusData.machineHoursEmpty)
            {
                h_empt = h_empt + ((decimal)MachineBusData.machineHoursEmpty-h_empt);
            }
            if(h_load != (decimal)MachineBusData.machineHoursLoaded)
            {
                h_load = h_load + ((decimal)MachineBusData.machineHoursLoaded-h_load);
            }
            
            
            //it will ad to qc everytime we call this function but the tracker variable helps up know how many times we have added to qc.
            //dividing qc with the number of times we called this func should give us a good avg of qc.
            qc = qc + (decimal)MachineBusData.payloadTonnes;

            //t_a = t_a + temperature
            t_a = t_a + (decimal)MachineData.ambientTemperature;

            //if()
          //  n_curr = MachineBusData.payloadBuckets-n;
           
            
            //adds to the counter so we can get a good average when doing calc. 
            tracker++;
           

            /*
          

        }*/
    


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
            string result = res.ToString();
            string sqlStatement = "UPDATE tpms_vehicle SET tire_specc = '"+ result +"' WHERE id = '"+ MachineData.machineID +"'";//assuming these is already a value in that column.
            int nrOfRowsAffected = Database.SendNonQuery(sqlStatement);
       
             
            return;
        }

        

        public void Calc()
        {
          
           //total length and hours driven during this period.
           // l = l_empt + l_load;
            //h = h_empt + h_load;
            l = MachineBusData.distanceDrivenEmpty+MachineBusData.distanceDrivenLoaded;
            h = MachineBusData.machineHoursEmpty+MachineBusData.machineHoursLoaded;
            n = MachineBusData.payloadBuckets;
            t_a = MachineData.ambientTemperature;
            qc = MachineBusData.payloadTonnes;

            //calculating the lenght it has gone during the amount of hours. Divide på 7 to get average per day.
            //.........  /7
            //decimal vm = ((l / h));
            if (h != 0)
            {
                vm = (((l * n) / h));
            }else
            {
                vm = 0;
            }
           
        
       
        

        


        //Calculate k2 where if t_a is higher than t_ref then we use formula (1). If t_a is below t_ref then use formula 2.
        //Good to know if t_a = t_ref then k2 will be = 1.
        /*if (t_a/(decimal)tracker > 38)
        {
            k2 = 1 / (1 - ((0.4 * ((double)t_a - t_ref)) / (double)vm));
        }
        else
        {
            k2 = 1 / (1 - ((0.25 * ((double)t_a - t_ref)) / (double)vm));
        }*/

        if(t_a > 38)
        {
            k2 = (1 / (1 - ((0.4 * (t_a - 38) / vm))));
        }
        else
        {
            k2 = (1 / (1 - ((0.25 * (t_a - 38) / vm))));
        }

      
            //assuming the tonnes are for the whole vehicle so we divide by 4 to get how much per tire.
            //qm = ((((qv/(decimal)tracker)/4) + (qc/4))/2);
            qm = (((double)qv/4) + (qc/4))/2;
            
            //Length split amongs seven days.
            //l = l / 7;

            //Divide it the N cycles per day to gain a good K1 value.
            /*n = n_curr;
            if(n == 0)
            {
                l_k1 = l;
            }
            else
            {
                l_k1 = l/(decimal)n;
            }
           */
            /*k1Values[Math.Ceiling(l_k1).ToString()]*/
            //Calculating the real site TKPH which in  this casé is the average per day for that specific week.
            //double TKPH = (double)qm * (double)vm * k2* k1Values[Math.Ceiling(l_k1).ToString()];
            double TKPH = qm * vm * k2* k1Values[Math.Ceiling(l).ToString()];
            Evaluate(TKPH);
            tracker = 0;
            l_empt = 0;
            h_empt = 0;
            l_load = 0;
            h_load = 0;
            qc=0;
            n_curr = 0;


            l = 0;
            h = 0;
            n = 0;
            t_a = 0;
        
        return;
        }

}
}


