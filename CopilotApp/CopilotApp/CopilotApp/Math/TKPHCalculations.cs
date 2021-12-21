using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CopilotApp
{
    public partial class TKPHCalculations
    {
        //All K1 values Gets loaded into the dictionary on startup from K1Loader.cs
        //Use like an array: k1Values["1"] = 1.0
        //                   k1Values["6"] = 1.04
        //                   3.0 * k1Values["6"] = 3.12
        private static Dictionary<string, double> k1Values = new Dictionary<string, double>();
        

        public TKPHCalculations()
        {

        }

        
    }
}
