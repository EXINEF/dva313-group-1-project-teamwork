using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using System.Threading.Tasks;


namespace CopilotApp
{
   
    public partial class TKPHCalculations
    {
        DatabaseL database = new DatabaseL();

        //All K1 values Gets loaded into the dictionary on startup from K1Loader.cs
        //Use like an array: k1Values["1"] = 1.0
        //                   k1Values["6"] = 1.04
        //                   3.0 * k1Values["6"] = 3.12
        public static Dictionary<string, double> k1Values = new Dictionary<string, double>();
       
       

        private enum Status //the status of tire
        {
            UNDER,
            NEUTRAL,
            OVER,
        }


        private decimal qv=0; //base weigth
        private double t_a = 0.0;
        private double l = 0.0;
        private double qm = 0.0;
        private double qc = 0.0;
        private double h = 0.0;
        private int t_ref = 38; //Reference temperature.
        private double k2;
        private int tracker = 0;
        private decimal l_k1 = 0;
        private double preSetTKPH;
        private Status res;
        private int n = 0;
        private double vm = 0;
        private double TKPH = 0;



        public TKPHCalculations()
        {
            


            //query to get current TKHP for that tire. Set id to the local machine's
             string query = "SELECT tkph FROM tpms_vehicle WHERE id = '" + MachineData.machineID + "'"; //ADD MODEL ASWELL. maybe
             MySqlDataReader reader = (database.SendQuery(query));
            while (reader == null)
            {
                reader = (database.SendQuery(query));
            }
            reader.Read();
             preSetTKPH = double.Parse(reader["tkph"].ToString());
            
           

            query = "SELECT weight FROM tpms_vehicle WHERE id = '"+ MachineData.machineID +"'"; //ADD MODEL ASWELL. maybe
            reader = database.SendQuery(query);
            while (reader == null)
            {
                reader = (database.SendQuery(query));
            }
            reader.Read();
            qv = decimal.Parse(reader["weight"].ToString());

           

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
            /*The query above is preffered if the DBMS allows that syntax.*/
            string result = res.ToString();
            string sqlStatement = "UPDATE tpms_vehicle SET tire_specc = '"+ result +"' WHERE id = '"+ MachineData.machineID +"'";//assuming these is already a value in that column.
            int nrOfRowsAffected = database.SendNonQuery(sqlStatement);
       
             
            return;
        }

        

        public void Calc()
        {
          

            l = MachineBusData.distanceDrivenEmpty+MachineBusData.distanceDrivenLoaded;
            h = MachineBusData.machineHoursEmpty+MachineBusData.machineHoursLoaded;
            n = MachineBusData.payloadBuckets;
            t_a = MachineData.ambientTemperature;
            qc = MachineBusData.payloadTonnes;


            if (h != 0)
            {
                vm = (((l * n) / h));
            }else
            {
                vm = 0;
            }

            if (vm != 0)
            {
                if (t_a > 38)
                {
                    k2 = (1 / (1 - ((0.4 * (t_a - 38) / vm))));
                }
                else
                {
                    k2 = (1 / (1 - ((0.25 * (t_a - 38) / vm))));
                }
            }
            else
            {
                k2 = 1;
            }
      
            //assuming the tonnes are for the whole vehicle so we divide by 4 to get how much per tire.
            qm = (((double)qv/4) + (qc/4))/2;

  
            /*k1Values[Math.Ceiling(l_k1).ToString()]*/
            //Calculating the real site TKPH which in  this casé is the average per day for that specific week.
  

            if (l != 0)
            {
                TKPH = qm * vm * k2 * k1Values[Math.Ceiling(l).ToString()];
            }
            else
            {
                TKPH = 0;
            }

            if (TKPH != 0)
            {
                Evaluate(TKPH);
            }
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


