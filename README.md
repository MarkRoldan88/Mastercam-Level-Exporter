# Mastercam-Level-Exporter
_Work in progress_

This is a 'NetHook' (chook) made for Mastercam 2021. 

It can be used to automate the process of exporting CAD entities within levels to individual files of a specified CAD format. 

Similar to the `Save Some` function within Mastercam; it would be used when you want to save a CAD entity as a separate file of a certain CAD format.

**TODO: Add screenshot**

## Compatible Mastercam Versions
- Mastercam 2021
- Mastercam 2020

_Note: It **may** be compatible with Mastercam 2018 & 2019 if the NETHook3_0.dll reference is updated to the proper file location for those versions; this has not been tested yet_

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
   
**If your C:\Program Files\Mastercam 2021\Mastercam directory _does not_ contain `NETHook3_0.dll`, make sure to copy it from the bin folder**

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
2. `Microsoft.Xaml.Behaviors`
3. `Level-Exporter.ft`

**If your C:\Program Files\Mastercam 2021\Mastercam directory _does not_ contain `NETHook3_0.dll`, make sure to copy it from the bin folder**
- It can be found in _\Level-Exporter\bin\x64\Debug_

## How to Use the Level Exporter
