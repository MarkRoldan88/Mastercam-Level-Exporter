# Mastercam-Level-Exporter
_Work in progress_

This is a 'NetHook' (chook) made for Mastercam 2021. 

It can be used to automate the process of exporting CAD entities within levels to individual files of a specified CAD format. Most Mastercam formats are supported. 

Similar to the `Save Some` function within Mastercam; it would be used when you want to save a CAD entity as a separate file of a certain CAD format.


![image](https://user-images.githubusercontent.com/56398786/168148534-0477e054-0162-43f8-878a-c3cb4bcdffe2.png)


## Compatible Mastercam Versions
- Mastercam 2021
- Mastercam 2020

_Note: This chook **may** be compatible with Mastercam 2018 & 2019 if the NETHook3_0.dll reference is updated to the proper file location for those versions; this has not been tested yet_

## How To Install the Chook
_Proper artifacts will be created/deployed soon so the user will not have to download and use visual studio_
1. Clone this repository
2. Install Visual Studio 2019
3. In Visual Studio, open `Level-Exporter.sln` (located in _Mastercam-Level-Exporter/Level-Exporter_)
4. Follow Method 1 **or** 2 below 

---

### Method 1
1. Set configuration to Release/x64
   - ![image](https://user-images.githubusercontent.com/56398786/167949149-41a62689-8424-4a14-9275-b24b919c4f72.png)

2. Build the Solution
   - ![image](https://user-images.githubusercontent.com/56398786/167949983-9376219f-2600-4433-85b0-faf60ca41602.png)

3. Navigate to the release folder (_Level-Exporter/bin/x64/Release_)
   - It should look something like this
   - ![image](https://user-images.githubusercontent.com/56398786/167950976-4468a736-714f-4885-9a51-5e74d8af23c9.png)
   
4. Copy `Level-Exporter.dll`, `Microsoft.Xaml.Behaviors.dll` into the Chooks directory for your Mastercam version
   - The Chooks directory is usually located at _C:\Program Files\Mastercam 2021\Mastercam\chooks_

5. Navigate to _Level-Exporter\Resources\FunctionTable_ & copy `Level-Exporter.ft` into the Chooks directory (same as above)
   
⚠️ **If your C:\Program Files\Mastercam 2021\Mastercam directory _does not_ contain `NETHook3_0.dll`, make sure to copy it from the bin folder**

---
The Mastercam chooks directory (_C:\Program Files\Mastercam 2021\Mastercam\chooks_) should now contain
1. `Level-Exporter.dll`
2. `Microsoft.Xaml.Behaviors`
3. `Level-Exporter.ft`

---
### Method 2 
**_Note: Visual Studio must be run in Adminstrator mode due to post build events which copy the necessary files to the Mastercam directory_**
1. Set configuration to Debug/x64
   - ![image](https://user-images.githubusercontent.com/56398786/167948586-6b4ac143-0f16-42ed-8d0d-9403d89ec6ae.png)

2. Build the Solution
   - ![image](https://user-images.githubusercontent.com/56398786/167949983-9376219f-2600-4433-85b0-faf60ca41602.png)
   - Visual studio post build events will try to copy the necessary files to the proper Mastercam directories

---
The Mastercam chooks directory (_C:\Program Files\Mastercam 2021\Mastercam\chooks_) should now contain
1. `Level-Exporter.dll`
2. `Level-Exporter.ft`
3. `Microsoft.Xaml.Behaviors.dll`

⚠️**If your C:\Program Files\Mastercam 2021\Mastercam directory _does not_ contain `NETHook3_0.dll`, make sure to copy it from the bin folder**
- It can be found in _\Level-Exporter\bin\x64\Debug_

## How to Use the Level Exporter
1. Organize the CAD entities of your Mastercam session into different levels
   - ⚠️ _Levels that have no entities are ignored, and will be cleared from **Mastercam**_
3. When naming levels in Mastercam, avoid using symbols (such as ()%#* etc.)
   - If the level names in Mastercam are left blank, they will be named 'level' in the level-exporter
4. Open the Level-Exporter by using the _Run Add-In_ feature under the _Home_ tab
5. Select the checkbox next to the levels you would like to export (select the checkbox in the header to select or deselect all levels)
6. Select a CAD format from the drop down menu
   - If Stl is selected, the default resolution is 0.02; value must be a value between 0.02 - 2.0
   - ⚠️ If Iges is selected, a dialogue for a comment will open in **Mastercam** after clicking export; you must fill in a comment and press enter.
7. Select a destination/output directory
   - ⚠️ If this is left blank, a `Save Some` dialogue will open for every level selected; you can then select the name, file extension, and destination for each level.
