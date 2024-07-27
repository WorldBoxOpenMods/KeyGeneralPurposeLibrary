# KeyGeneralPurposeLibrary

KeyGeneralPurposeLibrary is a library mod which contains a lot of the older WorldBox related functionality of KeyGUI.

## Building

The project, as uploaded on this repo, cannot be built out of the box. It assumes that all of it's dependency DLLs like BepInEx and a publicised Assembly-CSharp.dll file are located within a Libraries directory in the parent directory of KeyGeneralPurposeLibrary.
To be able to build it, you need to either replicate this file structure or edit the .csproj file to contain dependency paths which work on your device.
