using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CopilotApp
{
    class AutomatedDataSending
    {
        bool isSending = false;
        int delayBetweenSendsSeconds = (60 * 1);
        DatabaseL database = new DatabaseL();

        public void StopSending()
        {
            isSending = false;
        }

        public async Task StartSending()
        {
            isSending = true;
            int sleepMilliseconds = delayBetweenSendsSeconds * 1000;
            Console.WriteLine("Starting the automated data sending process");
            Thread.Sleep(6000);
            while (isSending)
            {
                Console.WriteLine("Auto Sending Data, " + delayBetweenSendsSeconds + " seconds until next update.");

                int maxGeolocationDelayms = 5000; //ms;
                await GPS.UpdateCoordinates(maxGeolocationDelayms);
                await Task.Delay(maxGeolocationDelayms);

                Console.WriteLine("SendLiveMachineData();");
                await SendLiveMachineData();

                Console.WriteLine("SendLiveTireData();");
                await SendLiveTireData();

                Console.WriteLine("SendLiveSensorData();");
                await SendLiveSensorData();

                Console.WriteLine("SendLiveLocationData();");
                await SendLiveLocationData();

                Thread.Sleep(sleepMilliseconds);
            }

            await Task.CompletedTask;
        }

        public async Task SendLiveTireData()
        {
            string tire1Query = "UPDATE tpms_tire SET revolutions = '" + TireData.frontLeftTireRevolutions + "' WHERE id = '" + TireData.frontLeftTireID + "';";
            string tire2Query = "UPDATE tpms_tire SET revolutions = '" + TireData.frontRightTireRevolutions + "' WHERE id = '" + TireData.frontRightTireID + "';";
            string tire3Query = "UPDATE tpms_tire SET revolutions = '" + TireData.rearLeftTireRevolutions + "' WHERE id = '" + TireData.rearLeftTireID + "';";
            string tire4Query = "UPDATE tpms_tire SET revolutions = '" + TireData.rearRightTireRevolutions + "' WHERE id = '" + TireData.rearRightTireID + "';";

            string sqlQuery = tire1Query + tire2Query + tire3Query + tire4Query;

            int nrOfRowsAffected = database.SendNonQuery(sqlQuery);

            await Task.CompletedTask;
        }

        public async Task SendLiveSensorData()
        {
            //Cast the values to strings
            string frontLeftSensorID = SensorData.frontLeftSensorID;
            string frontLeftSensorPressure = SensorData.frontLeftSensorPressure.ToString(); //C# doubles will never be null so this is fine.
            string frontLeftSensorTemperature = SensorData.frontLeftSensorTemperature.ToString();
            
            string frontRightSensorID = SensorData.frontRightSensorID;
            string frontRightSensorPressure = SensorData.frontRightSensorPressure.ToString();
            string frontRightSensorTemperature = SensorData.frontRightSensorTemperature.ToString();
            
            string rearLeftSensorID = SensorData.rearLeftSensorID;
            string rearLeftSensorPressure = SensorData.rearLeftSensorPressure.ToString();
            string rearLeftSensorTemperature = SensorData.rearLeftSensorTemperature.ToString();
            
            string rearRightSensorID = SensorData.rearRightSensorID;
            string rearRightSensorPressure = SensorData.rearRightSensorPressure.ToString();
            string rearRightSensorTemperature = SensorData.rearRightSensorTemperature.ToString();

            database.SendSensorData(frontLeftSensorID, frontLeftSensorPressure, frontLeftSensorTemperature, null, null, MachineData.companyID, "0");
            database.SendSensorData(frontRightSensorID, frontRightSensorPressure, frontRightSensorTemperature, null, null, MachineData.companyID, "0");
            database.SendSensorData(rearLeftSensorID, rearLeftSensorPressure, rearLeftSensorTemperature, null, null, MachineData.companyID, "0");
            database.SendSensorData(rearRightSensorID, rearRightSensorPressure, rearRightSensorTemperature, null, null, MachineData.companyID, "0");

            await Task.CompletedTask;
        }

        public async Task SendLiveMachineData()
        {
            //Cast the values to strings
            string machineID = MachineData.machineID;
            string ambientTemperature = MachineData.ambientTemperature.ToString();
            string distanceDrivenEmpty = MachineBusData.distanceDrivenEmpty.ToString();
            string distanceDrivenLoaded = MachineBusData.distanceDrivenLoaded.ToString();
            string machineHoursEmpty = MachineBusData.machineHoursEmpty.ToString();
            string machineHoursLoaded = MachineBusData.machineHoursLoaded.ToString();
            string payloadTonnes = MachineBusData.payloadTonnes.ToString();
            string payloadBuckets = MachineBusData.payloadBuckets.ToString();
            string consumedFuel = MachineBusData.consumedFuel.ToString();
            string companyID = MachineData.companyID.ToString();

            database.SendMachineData(machineID, null, ambientTemperature, distanceDrivenEmpty, distanceDrivenLoaded, machineHoursEmpty,
                                              machineHoursLoaded, payloadTonnes, payloadBuckets, consumedFuel, null, null, null, null, null, companyID);

            await Task.CompletedTask;
        }

        public async Task SendLiveLocationData()
        {
            //Cast the values to strings
            string machineID = MachineData.machineID;
            string latitude = GPSData.latitude.ToString();
            string longitude = GPSData.longitude.ToString();

            database.SendLocationData(machineID, latitude, longitude);

            await Task.CompletedTask;
        }
    }
}
