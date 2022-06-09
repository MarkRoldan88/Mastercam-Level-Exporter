# Mastercam-Level-Exporter
🚧 _Work in progress_ 🚧

This is a 'NetHook' (chook) made for Mastercam 2021. 

It can automate the process of exporting CAD entities within levels to individual files of a specified CAD format. 

Similar to the `Save Some` function within Mastercam; it would be used when you want to save a CAD entity as a separate file of a certain CAD format.

![image](https://user-images.githubusercontent.com/56398786/169858267-b608f504-eaf1-44e9-93dc-861174a00dc4.png)

_Note: If you clone or fork this repo and you are using Visual Studio, there are post build events which require admin access; therefore you must either run visual studio as admin or remove the post build events if building in debug mode._

## Compatible Mastercam Versions & CAD Formats
- Mastercam 2021
- Mastercam 2020

❔ _Note: This chook **may** be compatible with Mastercam 2018 & 2019 if the NETHook3_0.dll reference is updated to the proper file location for those versions; this has not been tested yet_

### Supported CAD Formats

![image](https://user-images.githubusercontent.com/56398786/169664626-0a59fc1c-6c99-43e2-85bb-57e4aa9f55a5.png)


## Installation

Download the `Level-Exporter.InstallerMC2021.msi` from [**releases**](https://github.com/MarkRoldan88/Mastercam-Level-Exporter/releases) and choose the Mastercam 2020 or 2021 chooks directory. 

Usually located at _C:\Program Files\Mastercam 2021\Mastercam\chooks_

---

### Alternate Installation Method
Download the zip from [**releases**](https://github.com/MarkRoldan88/Mastercam-Level-Exporter/releases) and extract the files to the Mastercam Chooks directory, usually located at:

_C:\Program Files\Mastercam 2021\Mastercam\chooks_

The Mastercam chooks directory should contain
1. `Level-Exporter.dll`
2. `Microsoft.Xaml.Behaviors`
3. `Level-Exporter.ft`

## Debugging the NEThook
⚠️ **_Note: Visual Studio must be run in Adminstrator mode due to post build events which copy the necessary files to the Mastercam directories_**

💡 See the Readme folder (_Level-Exporter/ReadMe_) within the project for additional context on debugging
1. Clone this repository
2. Install Visual Studio 2019
3. In Visual Studio, open `Level-Exporter.sln` (located in _Mastercam-Level-Exporter/Level-Exporter_)
4. Right click the Level-Exporter project and set the _Debug_ properties
   - Make sure to point to the proper Mastercam.exe and working directory
   - ![image](https://user-images.githubusercontent.com/56398786/169348490-4a2ef4b5-d280-4f1d-bbf7-8b6e39537fa9.png)
6. Set configuration to _Debug/x64_
   - ![image](https://user-images.githubusercontent.com/56398786/167948586-6b4ac143-0f16-42ed-8d0d-9403d89ec6ae.png)
7. Click the Start button or press F5
   - Visual studio post build events will try to copy the necessary files to the proper Mastercam directories
   - A debug session of Mastercam will open and you will be able to set breakpoints and test things out.
8. If you get errors related to the `NETHook3_0.dll`, you may have to update the reference, see the Readme folder.

⚠️ _Note: The `NETHook3_0.dll` is usually one level up from the chooks folder: C:\Program Files\Mastercam 2021\Mastercam_


## Adding the Level-Exporter to the Mastercam ribbon or context menu
![image](https://user-images.githubusercontent.com/56398786/168441340-8f80355a-5355-46fe-bc56-6b34d2ea7bdb.png)

By adding it to the context menu, it will appear in the right click menu.

![image](https://user-images.githubusercontent.com/56398786/168441385-d90cb989-ff90-46ab-9601-41f20677e78b.png)


![image](https://user-images.githubusercontent.com/56398786/168441412-7c02b20f-5162-4d85-9e1a-faf83fa2c990.png)

## How to Use the Level Exporter
1. Organize the CAD entities of your Mastercam session into different levels
   - ⚠️ _Levels that have no entities are ignored, and will be cleared from **Mastercam**_
   - When naming levels in Mastercam, avoid using symbols (such as ()%#* etc.)
   - If the level names in Mastercam are left blank, they will be named 'level' in the level-exporter
   - You can name levels within the level-exporter, to use for the file export (this will not affect the level names within Mastercam)
2. Open the Level-Exporter by using the `Run Add-In` feature under the `Home` tab, and look for the `level-exporter.dll`
   -  Alternatively if you've added it to the Mastercam ribbon, click on the icon
   -  Alternatively if you've added it to the context menu, right click and select the Level-Exporter
4. Click on the `Get Mastercam Levels` button
   -  If you update the levels in Mastercam, you can click the `Sync Mastercam levels` button to update the list in the level-exporter
5. Select the checkbox next to the levels you would like to export (select the checkbox in the header to select or deselect all levels)
6. Select a CAD format from the drop down menu
   - If Stl is selected: 
     - Default resolution is 0.02 
     - Value must be a value between 0.02 - 5.0; a lower value means a _bigger_ file size.
     - If set to 0, Mastercam's system tolerance will be used (from Mastercam configuration)
   - ⚠️ If Iges is selected, a dialogue for a comment will open in **Mastercam** after clicking export; you must fill in a comment and press enter.
7. Select a destination/output directory
   - ⚠️ If this is left blank, a `Save Some` dialogue will open for every level selected; you can then select the name, file extension, and destination for each level.
   - The level names in the _level-exporter_ will be used for the exported file names.
8. Click `Export Levels`

🥳 📁 Each selected level will be saved as a separate CAD file to the specified directory. 🏁
