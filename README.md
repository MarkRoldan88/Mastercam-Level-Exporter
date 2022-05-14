# Mastercam-Level-Exporter
_Work in progress_

This is a 'NetHook' (chook) made for Mastercam 2021. 

It can be used to automate the process of exporting CAD entities within levels to individual files of a specified CAD format. Most Mastercam formats are supported. 

Similar to the `Save Some` function within Mastercam; it would be used when you want to save a CAD entity as a separate file of a certain CAD format.

![image](https://user-images.githubusercontent.com/56398786/168148534-0477e054-0162-43f8-878a-c3cb4bcdffe2.png)

🧑‍🎨 Colors will be updated soon

## Compatible Mastercam Versions
- Mastercam 2021
- Mastercam 2020

_Note: This chook **may** be compatible with Mastercam 2018 & 2019 if the NETHook3_0.dll reference is updated to the proper file location for those versions; this has not been tested yet_

## How To Install
Download files from [**releases**](https://github.com/MarkRoldan88/Mastercam-Level-Exporter/releases) and place them into the Mastercam Chooks directory, usually located at:

_C:\Program Files\Mastercam 2021\Mastercam\chooks_

The Mastercam chooks directory should contain
1. `Level-Exporter.dll`
2. `Microsoft.Xaml.Behaviors`
3. `Level-Exporter.ft`

⚠️ **If your C:\Program Files\Mastercam 2021\Mastercam directory _does not_ contain `NETHook3_0.dll`, copy it from the release**

---
### Alternate Installation Method
⚠️ **_Note: Visual Studio must be run in Adminstrator mode due to post build events which copy the necessary files to the Mastercam directories_**

💡 Feel free to also reference the Readme folder within the project for additional context
1. Clone this repository
2. Install Visual Studio 2019
3. In Visual Studio, open `Level-Exporter.sln` (located in _Mastercam-Level-Exporter/Level-Exporter_)
4. Set configuration to _Debug/x64_
   - ![image](https://user-images.githubusercontent.com/56398786/167948586-6b4ac143-0f16-42ed-8d0d-9403d89ec6ae.png)
5. Build the Solution
   - ![image](https://user-images.githubusercontent.com/56398786/167949983-9376219f-2600-4433-85b0-faf60ca41602.png)
   - Visual studio post build events will try to copy the necessary files to the proper Mastercam directories

---

The Mastercam chooks directory (_C:\Program Files\Mastercam 2021\Mastercam\chooks_) should now contain
1. `Level-Exporter.dll`
2. `Level-Exporter.ft`
3. `Microsoft.Xaml.Behaviors.dll`

⚠️**If your C:\Program Files\Mastercam 2021\Mastercam directory _does not_ contain `NETHook3_0.dll`, make sure to copy it from the bin folder**
- It can be found in _\Level-Exporter\bin\x64\Debug_

## Adding the Level-Exporter to the Mastercam ribbon or context menu
![image](https://user-images.githubusercontent.com/56398786/168441340-8f80355a-5355-46fe-bc56-6b34d2ea7bdb.png)

By adding it to the context menu, it will appear in the menu that appears when you right click.

![image](https://user-images.githubusercontent.com/56398786/168441385-d90cb989-ff90-46ab-9601-41f20677e78b.png)


![image](https://user-images.githubusercontent.com/56398786/168441412-7c02b20f-5162-4d85-9e1a-faf83fa2c990.png)

## How to Use the Level Exporter
1. Organize the CAD entities of your Mastercam session into different levels
   - ⚠️ _Levels that have no entities are ignored, and will be cleared from **Mastercam**_
   - When naming levels in Mastercam, avoid using symbols (such as ()%#* etc.)
   - If the level names in Mastercam are left blank, they will be named 'level' in the level-exporter
2. Open the Level-Exporter by using the `Run Add-In` feature under the `Home` tab, and look for the `level-exporter.dll`
   -  Alternatively if you've added it to the Mastercam ribbon, click on the icon
   -  If you've added it to the context menu, right click and select the Level-Exporter
4. Click on the `Get Mastercam Levels` button
   -  If you update the levels in Mastercam, you can click the `Sync Mastercam levels` button to update the list in the level-exporter
5. Select the checkbox next to the levels you would like to export (select the checkbox in the header to select or deselect all levels)
6. Select a CAD format from the drop down menu
   - If Stl is selected, the default resolution is 0.02; value must be a value between 0.02 - 2.0
   - ⚠️ If Iges is selected, a dialogue for a comment will open in **Mastercam** after clicking export; you must fill in a comment and press enter.
7. Select a destination/output directory
   - ⚠️ If this is left blank, a `Save Some` dialogue will open for every level selected; you can then select the name, file extension, and destination for each level.
8. Click `Export Levels`

Each selected level will be saved as a separate CAD file to the specified directory.
