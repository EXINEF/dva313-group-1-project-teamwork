using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CopilotApp
{
	public class Calculations
	{


		public Calculations()
		{

		}

		public async Task run()
		{
			//This block will calculate and update DB by indicating if the real site TKPH is under/over specced or neutral.
			//With an alternate implementation, it would update every 7th day.
			TKPHCalculations tkph = new TKPHCalculations();
			LifespanCalc ls = new LifespanCalc();
			


			while (true)
			{

				ls.CalcL();
				tkph.Calc();
				Thread.Sleep(6000);

			}
			await Task.CompletedTask;
		}

	}
}





