# dva313-group-1-project-teamwork
This is the official Repository of DVA313 - Software Engineering 2: Project Teamwork - 2021-2022 Group 1


░█████╗░░█████╗░░░░░░░██████╗░██╗██╗░░░░░░█████╗░████████╗
██╔══██╗██╔══██╗░░░░░░██╔══██╗██║██║░░░░░██╔══██╗╚══██╔══╝
██║░░╚═╝██║░░██║█████╗██████╔╝██║██║░░░░░██║░░██║░░░██║░░░
██║░░██╗██║░░██║╚════╝██╔═══╝░██║██║░░░░░██║░░██║░░░██║░░░
╚█████╔╝╚█████╔╝░░░░░░██║░░░░░██║███████╗╚█████╔╝░░░██║░░░
░╚════╝░░╚════╝░░░░░░░╚═╝░░░░░╚═╝╚══════╝░╚════╝░░░░╚═╝░░░

# CO-PILOT DESCRIPTION
This is a Android tablet device application meant to be used in a wheel loader type machine.  
The application is made up of three different pages or views that may be displayed to the wheel loader operator.  

## Main page
This is the primary view. This page gives an overview of the machine and it's tires.  
Each tire has two values associated with them: the temperature and the pressure.  
The page uses color coding and any values marked in green indicates that the values are within the recommended range.
Should either value be outside the recommended range they, and the tire will be marked in yellow for slight deviations and red for severe deviations from the recommended values. 
By default on startup no values will be shown as the display values are bound to the actual internal values waiting to be recieved. These values however can be simulated from the simulator.

## Tire page
From the Main page the user may navigate to the Tire page for each of the individual tires by pressing the desired tire on the Main page.
When inside the Tire page the user may enter details for the tires ID, baseline pressure, fill material and tread depth.
Pressing OK from here will send the data to the online database followed by the user being returned to the Main page.
Pressing Cancel till return the user to the Main Page.

## Simulator page
This page allows the user to create a complete mock-up of an entire vehicle including tires and sensors.
The buttons at the bottom allows the user to send the entered data to the database, it should be noted that any queries made by the application is bound by the rules of the database and all constraints apply. Only filled in fields will be sent or copied.   
By sending data the application will attempt to update the data for the entered ID for either the machine, tire or sensor. If no such ID exists in the database it will be created.  
The data entered in the simulator may also be copied over to the active Co-Pilot app by pressing the top button. However it also allows for the changing of the IDs of the machine, tires and sensors and should be exercised with caution.

## CO-PILOT REPOSITORY STRUCTURE (main branch)
  ## CopilotApp.Android & CopilotApp.iOS
  These folders mainly contain Xamarin autogenerated platform specific instructions for compiling.  
  However the folder "CopilotApp.Android/Resources/drawable/" is where all the graphics/images for the project is stored.  
  
  ## CopilotApp/
  Here we have eight folders containing the code that we have implemented.
  
  ### Database
  Functions for sending SQL queries and commands as well as custom functions for managing sending of machine, tire and sensor data.
  Database.cs is static and accessible from anywhere, use with caution as it may cause conflicts with multithreaded calls.  
  DatabaseL.cs is a local instantiable version of the Database.

  ### GPS
  Function that grabs the current GPS location of the Android device.
  
  ### LiveData
  All the actual data that is used by the Co-Pilot separated into classes for machine, tires and sensors.  
  Also contains the automated data sending functionality which runs contiuously in the background and the threshold values to indicate warning levels for temperature and pressure.
  
  ### MainPage
  Design and code for diplay values and controlling/calculating which graphics and display values to show for the main page. Also the notification system.
  
  ### Math
  Functions for calculating the TKPH (Tonne Kilometre Per Hour) and tire life.  
  The loader for the k1 coefficients.  
  Function for automated updating of tire specc and tire life values on the database.
  
  ### SimulatorPage
  Design and display values for the simulator.  
  Functions for sending simulator data to database and copying data to Co-Pilot.
  
  ### Startup
  The Co-Pilot startup sequence.  
  Function for loading the necessary machine data from database.  
  Function for loading the tire data for the current machine ID from the database.  

  ### TirePage
  Design and display values for the TirePage.  
  Functionality for sending the entered tiredata to the database.

# BUILDING THE CO-PILOT APP
The Co-Pilot project was created using Visual Studio 2019 and includes Visual Studio project files.

To Build the Co-Pilot from the repository the following steps are required:  
  (I): Install Visual Studio 2019 (although other versions should probably work as well.)  
  (II): Install Xamarin for Visual Studio  
  (III): Install Github extension for Visual Studio  
  (IV): Clone the repository  
  (V): Build  
  (VI): Troubleshooting

## Step 1: Installing Visual Studio
Visual Studio can be downloaded an installed from https://visualstudio.microsoft.com/downloads/ (Latest) or https://docs.microsoft.com/en-us/visualstudio/releases/2019/system-requirements (Visual Studio 2019)
  When the installer is downloaded, run the installer and install Visual Studio and the .NET Framework
 
## Step 2: Installing Xamarin
First start the Visual Studio Installer.  
  Then select Modify on your Visual Studio installation:  
![VSModify](https://user-images.githubusercontent.com/58140569/149240652-50825edf-2f6f-45c3-b407-f24556c1dd17.jpg)

  Under the Workloads tab navigate to the section Desktop & Mobile.  
  Install "Mobile development with .NET"  
![VSXamarin](https://user-images.githubusercontent.com/58140569/149220251-fe6d97d7-6373-47d2-b7a1-0805c3c5b130.jpg)

## Step 3: Installing Github extension for Visual Studio
First start the Visual Studio Installer.  
  Then select Modify on your Visual Studio installation.  
  Under the Individual components tab navigate to the section Uncategorized or enter "Github" in the search field.  
 
 ![VSGitHub](https://user-images.githubusercontent.com/58140569/149221522-8bace874-7a18-4826-9f40-97cb720692b1.jpg)
 
  Install "GitHub Extension for Visual Studio"  

## Step 4: Clone the Repository
Start Visual Studio.  
Select "Clone a repository"
![VSClone](https://user-images.githubusercontent.com/58140569/149223810-472be229-6e8b-444e-8c9c-0aba01074dbd.jpg)

Enter the repository URL and select an empty folder on your computer to place the repository in.  
Hit Clone.

![VSClone2](https://user-images.githubusercontent.com/58140569/149240843-dea340bd-f54c-404e-b2c2-289d48b55192.jpg)

You should now have a folder with the repository contents that can be seen in the Solution Explorer in Visual Studio.  

![VSSolutionExplorer](https://user-images.githubusercontent.com/58140569/149224401-98d40295-e89a-4093-92eb-cdd67b657d18.jpg)

## Step 5: Build:
Double click the CopilotApp.sln in the solution explorer to open the project.  
With the project open right click the "Solution 'CopilotApp'" and hit Build.
![VSBuild](https://user-images.githubusercontent.com/58140569/149230741-2e9dc729-19b3-4906-a7f3-564038d8a3ba.jpg)


## Troubleshooting:
All of the other packages should come with the project as is. But incase of difficulties we are using the NuGet packages for MySqlConnector, Xamarin Essentials & Xamarin forms.   Should there be any troubles with these options you can install or update them by firstly opening the project by double clicking the CopilotApp.sln in the Solution Explorer then right clicking the top Solution 'CopilotApp' in the solution explorer clicking Manage NuGet packages for Solution and installing or updating MySqlConnector, Xamarin Essentials & Xamarin forms.
![VSNugetSQL](https://user-images.githubusercontent.com/58140569/149230670-8b121ae5-45cc-4dd7-af26-600652c8d901.jpg)

# RUNNING THE CO-PILOT APP
Running the Co-Pilot app requires either the use of an Android emulator or a plugged in Android tablet device.  
Any plugged in device should be recognized by Visual Studio and shown by the run button dropdown menu.

![VSRun](https://user-images.githubusercontent.com/58140569/149230484-cbe65b7a-ca65-472c-9f15-2a4393fd85e5.jpg)

## Installing an emulator device (Optional)
In the top bar navigate to Tools -> Android -> Android Device Manager

![Emu1](https://user-images.githubusercontent.com/58140569/149231049-038292bf-88ff-4779-abcc-109f20f4819d.jpg)

Click the +New button in the Android Device Manager window.  
Select the details you want for the tablet device (image shows our setup).  
The creation of the device may take a minute to complete.  

![Emu2](https://user-images.githubusercontent.com/58140569/149231566-15a6a1a7-6c6f-422d-bf1f-296d89fad204.jpg)

The newly added emulator device should now show up in the Android Device Manager.  

![Emu3](https://user-images.githubusercontent.com/58140569/149232582-06e4d9da-20c0-4b62-bfa1-061daca07442.jpg)

Hit Start to start the emulator.  
Depending on hardware and feature like Hyper-V this process may also take a minute to perform.  

You should now in Visual Studio see the device in the run dropdown menu.
Select the desired device and run.  

![VSRun](https://user-images.githubusercontent.com/58140569/149230484-cbe65b7a-ca65-472c-9f15-2a4393fd85e5.jpg)
