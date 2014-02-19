----------------------------------------
 Virtual Controls Suite for Unity
 © 2012 Bit By Bit Studios, LLC
 Use of this software means you accept the Unity provided license agreement.  
 Please don't redistribute without permission :)
---------------------------------------------------------------
Thanks very much for purchasing Virtual Controls Suite!

Please see the Virtual Controls Suite homepage listed below for an overview of the package
and links to all the items listed below.

Email: 		support@bitbybitstudios.com
Homepage:	http://bitbybitstudios.com/portfolio/virtual-controls-suite
Forums:		http://bitbybitstudios.com/forum/categories/virtual-controls-suite-support
Reference:	http://bitbybitstudios.com/docs/virtual-controls-suite/
Tutorials: 	http://bitbybitstudios.com/2012/06/tutorials/virtual-controls-suite-tutorials/

-----------------
 Importing / Updating VCS
-----------------------------

1. Unity: File > New Scene
2. Locate VCS in the Asset Store window, and import.

-----------------
 Note to JavaScript Users
-----------------------------
Please see this page for more info on how to use VCS with JavaScript:
http://bitbybitstudios.com/forum/discussion/1/virtual-controls-suite-faq#q1

-----------------
 Using NGUI 
-----------------------------

VCS now includes the free "distributable" version of NGUI, located in this directory 
in the NGUI folder.  You may use the example prefabs and nguiSuite example scene
for already setup VCS controls.  If you have your own copy of NGUI, in a New Scene, 
you should delete the VCS supplied NGUI folder, and then import your NGUI package into the project.

NOTE: The free/distributable versions of NGUI do not allow for deployment to an iOS device.  You will need
to remove the VirtualControls/NGUI folder in order to deploy to an iOS device.

-----------------
 Using EZGUI 
-----------------------------

If you would like to use EZGUI with VCS, you will need to obtain that package
separately in order for VCS to work with them.  Also, the ezguiSuite example
scene may not have its scripts linked properly when opening, due to the inability
to distribute EZGUI along with VCS.  You will need to relink scripts in the scene.

After importing the EZGUI package, you will need to edit 3 lines in the
file VCPluginSettings.cs in order to enable EZGUI and disable NGUI.
That file has more details, but you just need to look for something like this:

//==============
// EZGUI - change the #if true to: 
// #if false
// if you want to use EZGUI Virtual Controls
//==============

-----------------
 NOT Using NGUI/EZGUI -- (Using GUITextures or custom UI)
-----------------------------

If you don't want to use NGUI or EZGUI, simply leave the package as is and use the GUITexture
related scripts and example scene.

Additionally, if you would like to delete the distributable version of NGUI included with VCS from your 
project, you will need to change 2 lines of code in order to get VCS to compile again.  

Please follow the instructions here:
http://bitbybitstudios.com/forum/discussion/1/virtual-controls-suite-faq#q3

-----------------
 Changelog
-----------------------------

1.5:
- Added example code of Unity's Standard Assets (mobile) FirstPersonControl.js converted for use with VCS: VCFirstPersonControl.txt
- Added PressBeganThisFrame and PressEndedThisFrame properties to VCButtonBase to increase parity with Unity's Input.GetButton related commands.
- Added public TouchWrapper accessor property to VCButtonBase.
- Fixed a bug where joystick's base location could erroneously set to (0, 0) for 1 frame after multiple 1 frame long touches.
- Fixed a bug where more than 5 new touches occurring in a single frame could trigger a null reference error.
- Fixed a bug where deltaPosition of mouse emulated touch in Editor only was not updated properly.

1.4:
- Added Playmaker Updater scripts for Playmaker support via GetProperty actions.
- Moved VCAnalogJoystickBase's Dragging property from protected to public, returns true when Joystick is pressed
- Fixed a bug where NGUI joysticks would in some cases sort behind their "base part" graphic during drag
- Fixed a bug where reloading a scene at runtime would cause controls with VCNames to erroneously report duplicates

1.3.1:
- Included more detailed note in readme for users using neither NGUI or EZGUI.

1.3:
- Added deltaPosition property to VCTouchWrapper class.
- Added positionAtTouchLocationAreaMin and Max properties for VCAnalogJoystickBase.
- Added useLateUpdate property to VCAnalogJoystickBase.
- Added RequireExclusiveTouch property to VCAnalogJoystickBase.

1.2:
- Added distributable version of NGUI to package
- Added TapCount and OnDoubleTap callback to VCAnalogJoystickBase

1.1:
- Added debug keys to all controls, see video here: http://youtu.be/JByiuqGlYKE

1.0:
- Initial implementation