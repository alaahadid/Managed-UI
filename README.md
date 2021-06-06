# MANAGED USER INTERFACE
An advanced managed enviroment for C# desktop applications. 

### [DOWNLOAD LATEST RELEASE (VERSION)](https://github.com/alaahadid/Managed-UI/releases)
### [GETTING STARTED ? CLICK HERE](https://github.com/alaahadid/Managed-UI/wiki/Workflow-(Understanding-ManagedUI,-What-Is-...-%3F))
### [Donate](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=alahadid@gmail.com&item_name=Donation&currency_code=USD&bn=PP-DonationsBF)

![Snapshot 1](/screenshots/snap1.JPG?raw=true "Snapshot1")
![Snapshot 2](/screenshots/snap2.JPG?raw=true "Snapshot2")
![Snapshot 3](/screenshots/snap3.JPG?raw=true "Snapshot3")
![Snapshot 4](/screenshots/snap4.JPG?raw=true "Snapshot4")
![Snapshot 5](/screenshots/snap5.JPG?raw=true "Snapshot5")
![Snapshot 6](/screenshots/snap6.JPG?raw=true "Snapshot6")
![Snapshot 7](/screenshots/snap7.JPG?raw=true "Snapshot7")
![Snapshot 8](/screenshots/snap8.JPG?raw=true "Snapshot8")

## List Of Prorams Built Using ManagedUI
- Timer Counter Lister: A program that list and manage timers. <https://github.com/alaahadid/Timer-Counter-Lister>

## Introduction
ManagedUI (Managed User Interface) is a .net framework library written in C#, can be considered as a getting-started library for creating a very advanced C# desktop applications.

If a project needed to be like this:

- Editible and dynamic menu items and toolbars. Can give end-user options to edit main menu, toolbars, theme and shortcuts (hotkeys)
- Editible and dynamic tabs (control windows in the main window). Also you can give the end-user option to edit the tabs layout easily.
- Dynamic built-in settings engine. Allows end-user to manage your application easy, easy to create setting pages as well.
- Add-ons support, the project uses MEF (see here), use services instead of just add-reference style.
- Uses commands and command combinations instead of just calling methods and creating instances.
- Multilingual interface support built in. You can make your project multilingual or simply single language.
- An engine that can be used more than once to create more applications.
- Easy to deal with and coding.

You can consider ManagedUI as a .net framework desktop applications engine, you just add the library in your project, implement few stuff and your project is ready to go in minutes !!

Yes, in minutes, no kidding, you don't have to deal with windows forms designers anymore (you just use it when designing controls). 

You don't have to worry about creating instances of objects, adding references ....etc

Remember that MUI (we gonna call it MUI instead of ManagedUI from here on) is one way of making Desktop application, for example, your application must contain controls that will displayed in tabs in the main window, not sticky controls in one main window as simple applications.

MUI uses MEF technology to help you build your application that supports add-ons and more !! (read more about MEF here: <https://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx>)

Please note that MUI is one way of making desktop application, a way that uses commands and services for handling data, menu items representation and others stuff for handling GUI.

Also, check out the Demo project which include the basic code you need for your application.


## System Requirements: 
- Windows (R) XP SP3, Vista, 8, 8.1, 10; X86 or X64. (Supports all Windows versions that support .net framework 4) 
- .Net Framework 4 installed on target pc. 

### As developer, you need a good knowledge of: 
- Managed Extensibility Framework (MEF), read more here: <https://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx> 
- Localizing Windows Forms, read more here: <https://msdn.microsoft.com/en-us/library/y99d1cd3(v=vs.71).aspx> 

#### Note:

ManagedUI uses SlimDX from NuGet, it should be installed by default when using VisualStudio with NuGet Manager.
If Not, simply download SlimDX (included in the binary releases of ManagedUI) and add it as reference to ManagedUI.

## Features

- Editable and dynamic menu items and toolbars. Can give end-user options to edit main menu, toolbars, theme and shortcuts (hotkeys)
- Editable and dynamic tabs (control windows in the main window). Also you can give the end-user option to edit the tabs layout easily.
- Dynamic built-in settings engine. Allows end-user to manage your application easy, easy to create setting pages as well.
- Add-ons support, the project uses MEF (Managed Extensibility Framework), use services instead of just add-reference style.
- Uses commands and command combinations instead of just calling methods and creating instances.
- Multilingual interface support built in. You can make your project multilingual or simply single language.
- An engine that can be used more than once to create more applications.
- Easy to deal with and coding.

## Installation  

### For developers:
You'll need Visual Studio of course, any version should work, VS Community used for building this library.

- Just download the binary files from the website of MUI, extract the content of it somewhere in your pc
- You'll need 'ManagedUI.dll', 'ManagedUI.XML' and 'SlimDX.dll' (ManagedUI.XML is optional for documentation of the code, SlimDX is for handling hotkeys), start VS and make a windows forms project
- Add 'ManagedUI.dll' as reference to your project
- Follow the documentation for starting building your app
Or you can use the source code of MUI instead, it is better to use the source code since you can change the building configuration and debugging.
- Get the source from MUI source code control host
- Add ManagedUI project to your project solution and as reference to your project
- Follow the documentation for starting building your app

Notes:

- ManagedUI full source include demo project and tutorial project, explains every thing needed to build a complete project using MUI.
- When MUI starts, it create a settings folder for the project at the documents. This folder will be named as same as the ProjectTitle you give in the parameters of MUI. 

  For example: parameters.ProjectTitle = "My Project";

  Each time you close the app, all settings like menu, toolbars, tab controls ...etc settings will be saved in that folder.
  But when the app starts for the first time (i.e. end-user downloaded your application binaries and attempts to run it) and MUI cannot find this folder to load settings (we don't have it yet in target pc), 
  MUI will look in the ****app dir*** (where your application is installed) for these settings files. If it find them, it will load them so that these become the default settings.
  
  After finishing work with your project, Simply go to Documents folder (close the app first). You'll find folder named under your project title.
  Copy the content of this folder to make these files the default settings for Menu, Toolbars, TabControls layout, Shortcuts and Theme of your application.
  The files are: "controls.tcm", "menu.mim", "shortcuts.sm", "theme.mt" and "toolbars.tbm". Don't worry about "Exceptions" folder, it is generated during run time.
  
  ManagedUIUtilities.exe can be used as well to edit default menu, toolbars, shortcuts and theme. This tool is usefull when you
  finished with your application and want to edit the default configuration (default menu, toolbar ...etc) of your application.
  
  Since we can't edit the main menu for example (as we configured, the menu cannot be edited) then the main window will be fixed and cannot be changed.
  
### For users

Once ManagedUI is used for your app, all you need to do is to include ManagedUI in your app dirctory. ManagedUI requires native installation
of .Net framework 4 (beside your app requirements of course) thus nothing more need for installation and using of MUI.

In Release Folder:

- "ManagedUI Bin" include ManagedUI.dll and SlimDX.dll , can be added as reference to your application and ready to go. 
  This dll is built with .net 4 framework and X86 cpus. (YOU ONLY NEED THESE 2 DLLS FOR YOUR APPLICATION).
- "ManagedUI Demo" include a demo project built with ManagedUI. Just to take a look at MUI and what can be done with it.
- "ManagedUI Tutorial Project" include a complete working project built with ManagedUI.

Notes:
- ManagedUIUtilities.exe can be used to edit default menu, toolbars, shortcuts and theme. This tool is usefull when you
  finished with your application and want to edit the default configuration of your application.
- For developers, it is recommended to use source code instead. See above.

## Finally 
Thank you very much for using my library. I hope this one is a good use for you :) 