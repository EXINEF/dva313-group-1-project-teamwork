# dva313-group-1-project-teamwork
This is the official Repository of DVA313 - Software Engineering 2: Project Teamwork - 2021 Group 1


░█████╗░░█████╗░░░░░░░██████╗░██╗██╗░░░░░░█████╗░████████╗
██╔══██╗██╔══██╗░░░░░░██╔══██╗██║██║░░░░░██╔══██╗╚══██╔══╝
██║░░╚═╝██║░░██║█████╗██████╔╝██║██║░░░░░██║░░██║░░░██║░░░
██║░░██╗██║░░██║╚════╝██╔═══╝░██║██║░░░░░██║░░██║░░░██║░░░
╚█████╔╝╚█████╔╝░░░░░░██║░░░░░██║███████╗╚█████╔╝░░░██║░░░
░╚════╝░░╚════╝░░░░░░░╚═╝░░░░░╚═╝╚══════╝░╚════╝░░░░╚═╝░░░

# CO-PILOT RESPOSITORY STRUCTURE (main branch)

# BUILDING THE CO-PILOT APP
The Co-Pilot project was created using Visual Studio 2019 and includes Visual Studio project files.

  To Build the Co-Pilot from the repository following steps are required:  
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
![VSModify](https://user-images.githubusercontent.com/58140569/149219372-0cac2d13-eaac-4486-a049-237e2fdecd0c.jpg)

  Under the Workloads tab navigate to the section Desktop & Mobile.  
  Install "Mobile development with .NET"  
![VSXamarin](https://user-images.githubusercontent.com/58140569/149220251-fe6d97d7-6373-47d2-b7a1-0805c3c5b130.jpg)
## Step 3: Installing Github extension for Visual Studio
First start the Visual Studio Installer.  
  Then select Modify on your Visual Studio installation.  
  Under the Individual components tab navigate to the section Uncategorized or enter "Github" in the search field.  
 
 ![VSGitHub](https://user-images.githubusercontent.com/58140569/149221522-8bace874-7a18-4826-9f40-97cb720692b1.jpg)
 
  Install "GitHubExtension for Visual Studio"  
## Step 4: Clone the Repository
Start Visual Studio.  
Select "Clone a repository"
![VSClone](https://user-images.githubusercontent.com/58140569/149223810-472be229-6e8b-444e-8c9c-0aba01074dbd.jpg)

Enter the repository URL and select an empty folder on your computer to place the repository in.  
Hit Clone.

You should now have a folder with the respoitory contents that can be seen in the Solution Explorer in Visual Studio.  

![VSSolutionExplorer](https://user-images.githubusercontent.com/58140569/149224401-98d40295-e89a-4093-92eb-cdd67b657d18.jpg)


## Step 5: Build:
Doubleclick the CopilotApp.sln in the solution explorer to open the project.  
With the project open rightclick the Solution 'CopilotApp' and hit Build.
![VSBuild](https://user-images.githubusercontent.com/58140569/149230741-2e9dc729-19b3-4906-a7f3-564038d8a3ba.jpg)


## Troubleshooting:
All of the other packages should come with the project as is. But incase of difficulties we are using the NuGet packages for MySqlConnector, Xamarin Essentials & Xamarin forms.   Should there be any troubles with these options you can install or update them by firstly opening the project by doubleclicking the CopilotApp.sln in the Solution Explorer then rightclicking the top Solution 'CopilotApp' in the solution explorer clicking Manage NuGet packages for Solution and installing or updating MySqlConnector, Xamarin Essentials & Xamarin forms.
![VSNugetSQL](https://user-images.githubusercontent.com/58140569/149230670-8b121ae5-45cc-4dd7-af26-600652c8d901.jpg)


# RUNNING THE CO-PILOT APP
Running the Co-Pilot app requires either the use of an android emulator or a plugged in Android tablet device.  
Any plugged in device should be recognized by Visual Studio and shown by the Run button dropdown menu.

![VSRun](https://user-images.githubusercontent.com/58140569/149230484-cbe65b7a-ca65-472c-9f15-2a4393fd85e5.jpg)


## Installing an emulator device (Optional)
In the top bar navigate to Tools -> Android -> Android Device Manager

![Emu1](https://user-images.githubusercontent.com/58140569/149231049-038292bf-88ff-4779-abcc-109f20f4819d.jpg)

Click the +New button in the Android Device Manager window.  
Select the details you want for the tablet device (image shows our setup).  
The creation of the device may take a minute to complete.  

![Emu2](https://user-images.githubusercontent.com/58140569/149231566-15a6a1a7-6c6f-422d-bf1f-296d89fad204.jpg)

The newly added emulator device should now show up in the Android Device Manager.  
Hit Start to start the emulator.  
Depending on hardware and feature like Hyper-V this process may also take a minute to perform.  

You should now in Visual Studio see the device in the Run dropdown list.
Select the desired device and Run.  

![VSRun](https://user-images.githubusercontent.com/58140569/149230484-cbe65b7a-ca65-472c-9f15-2a4393fd85e5.jpg)

