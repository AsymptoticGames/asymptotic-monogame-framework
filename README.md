# Asymptotic Monogame Framework

A framework for video game creation that has everything already implemented, except for the gameplay.  This framework includes menues, controls, settings, and more. All you need to do to make a complete, release-ready game is to implement the gameplay.

This framework is built upon MonoGame 3.5 and is currently supported on Windows.

This is under [CC by 3.0 license](https://creativecommons.org/licenses/by/3.0/). You can use and modify any of this code, even commercially, under the condition that you give Asymptotic Games appropriate credit.

##Installation Instructions

 1. Download and install Visual Studio 2015 Community Edition from Microsoft Website [here](https://www.microsoft.com/en-us/download/details.aspx?id=48146) ([Direct Download Link](https://www.microsoft.com/en-us/download/confirmation.aspx?id=48146))
 2. Download and install MonoGame 3.5 for Visual Studio from MonoGame Website [here](http://www.monogame.net/2016/03/17/monogame-3-5/) ([Direct Download Link](http://www.monogame.net/releases/v3.5.1/MonoGameSetup.exe))
 3. Download and unzip this project to the desired location on your computer (You can also pull this repo from GitHub)
 4. Open Visual Studio 2015, Click File->Open->Project/Solution, Navigate to the location where you unzipped this project, and open asymptotic-monogame-framework/Windows/AsymptoticMonoGameFramework.sln

##What it does

![Framework Example](http://asymptoticgames.com/images/framework-example.gif)

###Splash Screen

Shows company logo screen on game boot up. Can easily be set to any image.

###Main Menu

3 buttons on main menu right now.  Start Game, Settings, and Exit Game.  Buttons are simple to add to any menu.

#####Start Game

Opens the Game Settings Menu, where you can select a difficulty for the game.  I just used difficulty as an example.  It's easy to put anything in this screen and have the buttons on this screen affect the variables you will use for gameplay.
  
#####Settings

![Settings Example](http://asymptoticgames.com/images/settings-example.gif)

######Graphical Settings

 - Resolution

Any resolution is supported.  You can keep the default options or manually enter new options.  The game will only show the user resolution options that are supported with their monitor size.

 - Full Screen
  
Setting the game to full screen automatically sets the resolution to the window size and makes the game full screen.

Another thing to note: The first time a user boots it up, it will open in windowed mode and automatically set the resolution to the 2nd largest option that is supported by their monitor size.
 
######Audio Settings

 - Music Volume
 - Sound Effect Volume
 
Any music or sound effects you play are just multiplied by their respective values.
 
######Controls

- Player X Gamepad Controls
  
You can set if you want to support gamepad or not, or how many gamepads you want to support if you are doing multiplayer.  Each player has their own set of gamepad controls, which can be set individually or you can apply one gamepad's controls to all gamepads.  Buttons, Joysticks, and boolean values are supported for gamepad controls.
  
 - Keyboard/Mouse Controls (Keyboard keys, Mouse Clicks, and boolean values supported)
  
Keyboard Keys, Mouse Clicks, and boolean values are supported for keyboard/mouse controls
  
#####Exit Game

Exits to desktop

###Gameplay

No gameplay implemented (This is where you come in!)

#####Pause Menu

 - Back to Gameplay
 - Same settings as Main Menu
 - Exit to Main Menu

###Settings/Save Games

Saves any variables you want to an xml document to the local machine. Loads those variables into your game when you boot it up.  

Saved Settings already implemented for you

 - Resolution
 - Full Screen
 - Sound Effect Volume
 - Music Volume
 - All Gamepad Controls
 - All Keyboard/Mouse Controls
 
Any of these settings that you set in the Game Settings Menu will stay set that way the next time you load the game.

Anything you want can be saved via these saved settings as long as it's a string, int, or arrays of one of those.

###Sprite Animations

Easy to animate sprites when using sprite sheets.

##Coming Soon

 - Preset Controls
 - Mac Support
 - Linux Support
 
Mac and Linux support might be very close to working right now. I just have no way of testing them at the moment.

##Games Created with the Asymptotic MonoGame Framework

[![Cavern Crumblers Greenlight](http://asymptoticgames.com/images/logo-small.png)](http://www.caverncrumblersgame.com)

[![Cavern Crumblers Greenlight](http://asymptoticgames.com/images/greenlight-widget-image.png)](http://steamcommunity.com/sharedfiles/filedetails/?id=860585134)

##Contact Links

Website: [www.asymptoticgames.com](http://www.asymptoticgames.com)

Email: [support@asymptoticgames.com](support@asymptoticgames.com)
