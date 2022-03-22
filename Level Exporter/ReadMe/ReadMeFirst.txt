This template is a standard C# class library project for use with Mastercam.

This project has a reference to NETHook3_0.dll and optionally ToolNetApi.dll and SimAccessManaged.dll located in the root 
directory of your Mastercam installation. However, if you installed into a directory other than the default installation 
directory you may need to update the project references in order for your project to compile. 

This project will work with other versions of Mastercam just remove the references and add the ones from the version of
Mastercam you want to target.

The included Project Setup documents details setup for building debugging a NET-Hook.

**NOTE: Do not use spaces when naming your project as this will cause issues with loading resources
**NOTE: This NET-Hook class library targets the .NET 4.6.1 Framework x64 build.


Visual Studio 2019 Community Edition (Free) is recommended.
https://www.visualstudio.com/en-us/downloads/download-visual-studio-vs.aspx

** POST BUILD STEP **

copy "$(TargetPath)" "C:\Program Files\Mastercam 2021\Mastercam\chooks\$(TargetFileName)"
copy "$(ProjectDir)Resources\FunctionTable\$(TargetName).ft" "C:\Program Files\Mastercam 2021\Mastercam\chooks\$(TargetName).ft"

** DEBUGGING **

Start External Application -> C:\Program Files\Mastercam 2021\Mastercam\Mastercam.exe

Working Directory -> C:\Program Files\Mastercam 2021\Mastercam\

**NOTE: Edit paths above as needed to point to the installation of Mastercam you are targeting.

** RESOURCES **
Image resources can be added to the Level_Exporter\Resources\Assets folder with a build action of Resource.
Add the name of each resource to the AppConstants.cs file and use the StringToImageConvertor 
with an image binding (see MainView for an example).

** LOCALIZATION **
To add additional language support add a new resources file, for example:
Level_Exporter\properites\Resources.fr.resx
Level_Exporter\properites\Resources.fr-CA.resx
Level_Exporter\properites\Resources.en-GB.resx

The default localization is:
Level_Exporter\properites\Resources.resx