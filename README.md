# Senior Design User Manual
![Ventana Logo](https://github.com/VentanaIoT/Ventana/blob/master/images/Ventana%20Favicons/android-icon-192x192.png?raw=true)
# Executive Summary
Technology in the home is hard to use. In modern homes, internet connected devices surround people, from smart TVs to wireless speakers. Controlling these different devices means using multiple clunky and inconsistent applications across various types of devices, making the experience miserable. Using Microsoft HoloLens, the team created Ventana, an open source platform to find, connect, track, and control these existing internet connected devices.
 
The three main elements of Ventana include the platform that allows these internet connected devices to speak to the HoloLens, a multi-faceted method for tracking device position, and an application for the HoloLens that allows the mixed reality headset to control these devices. Ventana allows people to intuitively interact with internet connected devices in the home through simple, customizable holographic controllers.

# System Overview and Installation
The Ventana Project is broken up into three main parts. The Target, the Viewer, and the HoloHub (our server). This section walks through the system as a whole and how the separate parts interat with eachother.
## Overview block diagram
### HoloLens Application
![HoloLens](https://github.com/VentanaIoT/Ventana/blob/master/images/Image%20uploaded%20from%20iOS.jpg?raw=true)

### HoloLens App Scene Structure
![HoloLens Scene](https://github.com/VentanaIoT/Ventana/blob/master/images/HoloLens%20App%20Scene%20Structure.png?raw=true)

## User Interface
### Overview
There are two ways to interact with Ventana. The Holographic Interface, where the user interacts with and places holographic controllers, which is handled on the HoloLens. The setup and pairing process is handled by the HoloHub web application.

### Ventana - Running on HoloLens
Within the Ventana HoloLens application all of the interactions are based on the controllers placed in the users home. These Controllers can be extended, but the controllers built for the project include the following.
#### Music Controller
![Music Controller Screenshot](https://raw.githubusercontent.com/VentanaIoT/Ventana/master/images/Music%20Controller%20Screenshot.png =300x)
The Music controller has a similar set of controls to any popular music player. The Top slab of the controller displays album artwork and the bottom slab is the control panel. This layout is consistant across all of the controllers in this project.

The bottom panel contains play, pause (only displays when music is playing), skip, previous, Title, Artist, and the volume slider. Each are operated by airtapping, which is consistant accross all types of controllers.
#### Light Controller
![Light Controller Screenshot](https://raw.githubusercontent.com/VentanaIoT/Ventana/master/images/Light%20Controller%20Screenshot.png =300x)
The Light Controller has a simple set of controls and the same two piece layout of the music controller. The top slab is the non-functional representation of a light bulb conveying that it is a light controller while the bottom is the control panel. The control panel consists of a toggle on/off switch and a brightness slider.

#### Power Strip Controller
![Power Strip Controller Screenshot](https://raw.githubusercontent.com/VentanaIoT/Ventana/master/images/Power%20Strip%20Controller%20Screenshot.png =300x)
The power strip controller has the same layout as the above two, but the only controls for the power strip are two toggle buttons on the control panel.


### HoloHub Web Application
The HoloHub web application allows the user to pair supported devices to the HoloHub to be controlled in Ventana. The web application has two use cases.
1. Pairing of supported internet connected devices.
2. Spawning new controllers for paired internet connected devices.

![Web App Storyboard](https://raw.githubusercontent.com/VentanaIoT/Ventana/master/images/Web%20App%20Storyboard.png)

## Installation
### Overview
Ventana is designed to work with the smart devices already found in a user's home, primarily Wink-connected devices and the Sonos music system. In additon, developers can build on top of the Ventana platform and add support for other vendors as well. 

### Prerequisites 

The four major components to get started are the HoloLens, a Raspberry Pi 3, a Windows machine, and a Ventana-compatible device. This user manual assumes that the Ventan-compatible devices have already been setup and are operational per its respective instructions. 

### Installation of the HoloHub

Getting the server deployed on the Raspberry Pi 3 is the first step to running Ventana.

#### Installing Windows 10 IoT Core
Navigate to [*Getting Started*](https://developer.microsoft.com/en-us/windows/iot/Docs/GetStarted/rpi3/sdcard/stable/GetStartedStep1.htm). Only complete Steps 1 and 2 from the Microsoft website *Getting Started* wizard.

1. From a Windows 10 machine, Download and install [Windows 10 IoT Core Dashboard](http://go.microsoft.com/fwlink/?LinkID=708576).
2. Download, flash and install Windows 10 IoT Core onto the Raspberry Pi 3 board using the IoT Core Dashboard.
![Windows IoT Dashboard Setup View](https://az835927.vo.msecnd.net/sites/iot/Resources/images/IoTDashboard/IoTDashboard_SetupPage.PNG)
3. Connect the board to the network.
4. Configure the board.
 > Remember: When connecting the Raspberry Pi to a power source, ensure that the USB power adapter is rated to provided 5V/2.1Amps 

#### Installing Node and Configuring the RPI3 Settings

1. Download [ARM Node.js](https://github.com/nodejs/node-chakracore/releases) and extract. The file should end with `*-win-arm.zip`.
2. From the Windows IoT Dashboard. Select the RPI3 device, right-click on *Launch PowerShell* and login.

![Device Picker](https://az835927.vo.msecnd.net/sites/iot/Resources/images/IoTDashboard/IoTDashboard_RightClickMenu.PNG)

3. Create folder on root called "Nodejs" and copy `node.exe` and `chakracore.dll` files to this folder.
4. Visit the [Ventana HoloHub repository](https://www.github.com/VentanaIoT/holohub) and clone the repository to the host computer.
4. Continuing on the host computer, open the Ventana HoloHub respository folder downloaded. Using the `npm.exe` executable, execute `npm install --production` within the root of the target folder.
6. Create folder on RPI3 (e.g. "Projects\HoloHub") and copy the entire content of the Ventana HoloHub repository folder to this folder.
7. Visit the [Ventana Node-Sonos-HTTP-API](https://github.com/VentanaIoT/node-sonos-http-api) respository, clone the project and again using the ChakraJS `npm.exe` executable, execute `npm install --production` within the root of the target folder.
8. Create another folder on the PRI3 ("Projects/node-sonos-http-api/") and copy the entire contents of the Ventana HoloHub repository folder into this folder.

There should be a directory tree on the RPI3 resembling the following:
```
C:/    
│
└───Node/
│   │   node.exe
│   │   chakracore.dll
│   
└───Projects/
    │
    │___node-sonos-http-api/
    │   │    server.js
    │   │    ...
    │
    └───HoloHub/
        │   server.js
        │   ...
        │___node_modules/
    
```
9. Edit the firewall permissions to allow external connections to access the HoloHub server. Execute on the RPI3 `netsh advfirewall firewall add rule name="Node.js" dir=in action=allow program="C:\Node\node.exe" enable="yes"` 
10. Verify installation by executing a single, standalone server, `C:\Node\node.exe server.js` from the HoloHub root directory.
11. Enable automatic execution of both servers by entering:
11.1.`schtasks /create /tn "HoloHub" /tr "c:\Node\node.exe c:\Projects\HoloHub\server.js" /sc onstart /ru SYSTEM`
11.2.`schtasks /create /tn "SonosHTTP" /tr "C:\Node\node.exe C:\Projects\node-sonos-http-api\server.js" /sc onstart /ru SYSTEM`
12.Restart the RPI3 via the Windows IoT Dashboard
13. Setup a static IP address to the RPI3 via the DHCP setting on the router being used in the home. Review the router user manual for instructions on how to set a static IP on the specific device being used. Record the static IP address as it will be used later in the setup of the Ventana.
14. If Wink is being utilized in this environment, modify the config.json file inside */Projects/HoloHub/*. Change thge host address with the RPI3 static address set in step 13. Enter the Wink API credentials here as well. 
> Note: Never unplug or forcefully shut down the RPi3 running Windows 10 IoT Core. This can cause the system files to get corrupt and prevent the OS from booting up.
 
### Installation of Ventana on the HoloLens

In order to run Ventana, one must clone the github repo into a directory of their choice using the following command:
```bash
git clone https://github.com/VentanaIoT/Ventana.git
```
once the project has been successfully cloned, it will be able to be opened using the Unity file explorer.

to avoid issues with dependencies, one must ensure that the HoloLens application settings are set correctly using [this](https://developer.microsoft.com/en-us/windows/mixed-reality/holograms_101e) reference.

a shortcut to the above would be to select `HoloToolkit>Configure>Apply HoloLens Project Settings` from the drop down menu and relaunch the project. 

If HoloToolkit does not appear, you will have to go to `File>Build Settings` select Windows Store (make sure you have the right components installed if you don't see it) and set up the project to use 

SDK: Universal 10
Target Device: HoloLens
UWP Build Type: D3D
Build and Run: Local Machine
Unity C# Project: True

Once this is done, select Switch Platform at the bottom of the Build Settings window and build the project in unity by hitting the play button at the top. 

Once the project is verified to compile, it can be built to run on the HoloLens.

To run on the HoloLens, select `File>Build Settings`.
In the menu that appears, a user must ensure that both the Welcome Scene found at `Assets/Ventana/Scenes/WelcomeScene` and the Ventana Scene found at `Assets/Ventana/Scenes/Ventana` are included in the build. if not, they must open both in succession and select `build open scenes` to add them.

If the steps above are followed correctly, the user will now able to build the project into a folder of their choice. Once building is complete, a user may open the folder they chose and select the `Ventana.sln` in order to open it. This will make Visual Studio start up. 


Once Visual Studio finishes launching, the project must be built to run on a x86 device. This can be chosen from the run menu on the top of the application. 


At this point the HoloLens must be connected via USB to the computer, and a user may press the Debug button in Visual Studio.

This runs the Application on the HoloLens. 
#### Configurating the Environment

- The user's computer needs Visual Studio, Unity, and Vuforia. Information about downloading all this software can be found on Microsoft's Webpage, [Install the Tools](https://developer.microsoft.com/en-us/windows/mixed-reality/install_the_tools). Ventana is currently built in Unity 5.5, so that is the recommended version to download.
- The user can access Ventana from Github. The repository can be found [here](https://github.com/VentanaIoT/Ventana).

#### Deploying to the HoloLens

- Open Unity and select "Open." Then, navigate to the local folder that contains Ventana. 
- Select File in the top left corner, then uild & un. For information on configuring the export settings in Unity, review Chapter 1 of the [Holograms 101 Tutorial](https://developer.microsoft.com/en-us/windows/mixed-reality/holograms_101). This will produce a Visual Studio Solution that can be opened in Visual Studio.
- Open the Visual Studio Solution, and again refer to the Holograms 101 tutorial linked above for the Visual Studio settings to deploy Ventana to the HoloLens. 

#### Running the Application

- Once Ventana has been deployed to the HoloLens, one can start the application starting at the Start Menu. Navigate through the apps list air tapping on *Ventana* once it has been located. Air tap or say *Place* to place the app in the world. 

## Setup

### Pairing New Devices to the HoloHub
In order to pair a new device of the user's to the HoloHub, the user needs to open the Ventana IoT Web Application. The application will open to the splash screen below, which shows the two currently supported vendors, and the user needs to select with device manufacturer corresponds with the device he or she is trying to connect.

![Add Device Screenshot](https://i.imgur.com/i5xHiUS.png =300x) 

#### Pairing Sonos Device
Once the "Connect to Sonos Devices" button has been pressed in the application, if the user has any Sonos devices on the network that the HoloHub is connected to, a screen similar to the one shown below will appear.

![Add Sonos Living Room](https://i.imgur.com/3ikEsBy.png =300x)


#### Pairing Wink Device
Once the "Connect to Wink Devices" button has been pressed in the application, the user will be prompted to enter his or her Wink log-in credentials. This step  is important because in the server set-up process, the tokens for that user are set in this step. Without those tokens, the HoloHub will be unable to connect to the Wink Servers to make Wink API Requests.

![Wink Log-in](https://i.imgur.com/kZIkxKq.png =300x)

Once the user has logged into his or her Wink account, a screen with the Wink devices that are supported by Ventana will appear, with the option to add the device.

![Wink Add Device](https://i.imgur.com/i6Y6fFF.png =300x)

With the name of the device, the device type will also appear and a button that gives the user the option to "Pair Device". Upon clicking that button, the VuMark for the device will appear and the user will be able to spawn the controller like he or she did for the Sonos device.


# Operation of Project

For the first time, and most instances thereafter, the user should refer to operating mode 1. Refer to operating mode 2 for information on troubleshooting possible issues.

## Operating Mode 1: Normal

### Using the HoloLens

Users familiar with the HoloLens and basic gestures can skip this step. When a user first puts on the HoloLens, ensure the headband is adjusted, using the knob in the back. The HoloLens should feel tight, but comfortable, on the user's head, similar to the fit of a visor. The user can then adjust where the HoloLens sits on the user's nose by pushing or pulling on the front of the HoloLens. This is especially helpful for user's with glasses. Press the power button on the back left side of the HoloLens. From the start menu, select the Learn Gestures application, or say aloud, "Hey Cortana, open Learn Gestures." Once the user has the HoloLens fitted properly and has learned the built-in gestures recognizeable by the HoloLens' sensors, the user is ready to use Ventana.
    
### Using Ventana on the HoloLens

`1.` Open the HoloLens's start menu and select Ventana, or say aloud, "Hey Cortana, open Ventana."

`2.` Tap the "Start New Session" button, upon opening the application for the first time. For any subsequent session, the user has the option of tapping either "Load Last Session" or "Start New Session." The user should tap the "Load Last Session" button if the user wishes to restore the holograms, and their locations, from the previous session of Ventana. If the user does not wish to restore the holograms from the previous session, the user should tap "Start New Session," and any old holograms will be cleared.

![image alt](https://github.com/VentanaIoT/Ventana/blob/master/images/20170404_154352_HoloLens.jpg?raw=true![](https://i.imgur.com/Kv0tahl.JPG) "Ventana Welcome Scene")

`3.` Use the Web Portal, from the set-up process, to access the VuMarks for each device in the home supported by Ventana. It is recommended that the user open the Web Portal on a smartphone. 

`4.` Once a VuMark is open on the Web Portal, the user should hold the smartphone up to about eye level. When the HoloLens recognizes the VuMark, the appropriate hologram to control the device associated with the VuMark will appear. The user can then interact with the buttons on the hologram associated with the VuMark. For example, if the hologram controls a light bulb, the user can tap on the yellow button to toggle the light bulb on and off. 

`5.` If the user would like to place a hologram in the home that is not attached to a VuMark, the user can "click and hold" to clone the hologram. The "click and hold" gesture is a regular tap, but the user holds the index finger and thumb together for about one second, and then releases. Due to the HoloLen's recognition of this hold gesture, the VuMark needs to remain as still as possible, and be kept at a large enough distance from the user that the HoloLens can detect the user's hand. If the user has dificulty cloning the controller, ensure the user's hand is far enough away from the HoloLens to be detected. If this is difficult for the user to do, while simultaneously holding a smartphone, it is recommended that the user put the smartphone on a countertop.

`6.` The cloned hologram can moved to the user's desired location in a room, by dragging the body of the hologram. The "drag gesture" is similar to "click and hold," as the user holds the index finger and thumb together, and then any movement of the user's hand will correspond to movement of the hologram. When the user is satisfied with the position of the hologram, then the user releases the index finger and thumb, so they are no longer together. A white spatial mapping mesh will appear when the user is dragging the hologram, and the mesh will disappear when the user has finished dragging the hologram.

![image alt](https://github.com/VentanaIoT/Ventana/blob/master/images/20170404_155106_HoloLens.jpg?raw=true![](https://i.imgur.com/f9L0Iml.JPG) "Edit Mode Dragging")

`7.` Resize hologram as necessary, with any of the scaling handles on the corners of the hologram. Similar to the "drag gesture" to move the hologram, tap on one of the scaling handles, and hold down while moving hand either inward, to make the hologram smaller, or outward, ot make the hologram bigger, then release when satisfied.

![image alt](https://github.com/VentanaIoT/Ventana/blob/master/images/20170404_155023_HoloLens.jpg?raw=true![](https://i.imgur.com/84xwhjs.JPG) "Scale")

`8.` Tap the green "Done" button, when the user is satisfied with the location and size of the hologram. Now, the user can interact with the buttons on the hologram to control the device associated with it. 

`9.` If the user wishes to enter edit mode again, the user gazes down below the controller. A more button will appear that the user can tap to enter edit mode again. The user can move the hologram, resize it, remove a hologram from the world. The red "Delete" button removes the hologram, and its World Anchor. If the user wants another cloned hologram, they can refer to step 5, using the VuMarks on the Web Portal to clone the hologram again. 

![image alt](https://github.com/VentanaIoT/Ventana/blob/master/images/20170404_155012_HoloLens.jpg?raw=true![](https://i.imgur.com/dxOMW99.JPG) "More Button")

`10.` When the user finishes interacting with the holograms, the application can be exited. The user does a bloom gesture, finds Ventana's application tile, and taps the remove button in the top right corner or the tile, to close the application. The user's holograms will automatically be saved in their last locations when the user exits the application, in case the user choose to "Load Last Sesson," when launching Ventana next time. 

## Operating Mode 2: Abnormal

### Server Down
If Ventana crashes without warning on the HoloLens when the user tries to use one of the buttons to control a device, it is possible that the server may be down. To determine if the server is down, Ventana can be run in debug mode. Connect the HoloLens to Visual Studio, as in the initial deployment of the application. From the drop down menu at the top, select "Debug" instead of "Release." When the application deploys, Visual Studio will show the debug statements in the editor. If a couldn't connect to web socket exception appears, then the server is down. Relaunch the HoloHub, and the Ventana should not crash, once the server is back up and running.

### Spatial Mapping Failing

The Ventana Logo will be shown when HoloLens can't make a spacial map of a room. 

==IDK how to fix this sOS sOS==


### Using while Streaming Video

Since Vuforia accesses the HoloLen's camera, the live preview feature of mixed reality capture is unavailable while Vuforia is being used. The record video option, however, can be utilized. Once the user is done using Vuforia, and will no longer need to look at VuMarks, the user can say, "Stop Recognition." The white light on top of the HoloLens will turn off, and live preview will be available. If the user wishes to use Vuforia again, ensure that live preview is turned off, and then the user can say, "Start Recognition." 

# Technical Background
## Target
The Target is an image with embedded code which the HoloLens uses to spawn new controllers into the world. Also known as a VuMark, the target consists of three main parts that allow the HoloLens to detect it. Developed with [VuForia](https://www.vuforia.com), an augmented reality tool kit which is implemented on the HoloLens. Vuforia allows Ventana to see the VuMark through the HoloLens, recognize it, and then project a new holographhic controller on top of it.
![VuMark Example](https://raw.githubusercontent.com/VentanaIoT/Ventana/master/images/0.png =300x)
**An example of the Ventana VuMark**

The target itself has been designed in [Adobe Illustrator](http://www.adobe.com/products/illustrator.html), a tool for creating vector art, along with a template provided by VuForia. The three main parts consist of the following.
1. The central area is the background, purposefully useless for the HoloLens and primarily acts as a visual marker for users to recognize that it is a VuMark.
2. The next portion, the ring around the background is the code. The HoloLens reads this combination of black and white elements and translates them into hex. Then, depending on the value of the code, the above example is zero, the Ventana application will display the holographic controller assosiated with that value. This way the Application only needs to recognize a single type of image with changing code instead of needing a cache of dozens of different images assosiated with each device.
3. The Border. This outer thick black line is what the HoloLens uses to track the position of the VuMark. Using contrast detection it looks for the contrast between the inner side of the black border with the white background. In the example above, there is a cut off edge at the top of the border, this is called an assymetry marker. The assymetry marker is used to mark an area that is not rotationally symmetrical to improve tracking and set the orientation for the HoloLens to see.

![VuMark Template](https://github.com/VentanaIoT/Ventana/blob/master/images/Screen%20Shot%202017-04-07%20at%2011.52.53%20AM.png?raw=true)
**VuForia VuMark Creation Template**

Above is the template used for creating the Ventana VuMark. The pink circles within code elements validate their size and location. The transparent pink stroke validates the location of the contour between the border and background, which needs a defined amount of uninteruped white space on the inside to act as an efficient tracker.

The Ventana VuMark can hold up to 9 bytes of data and can contain any string of bits. This is overkill for this project, but if there are any expansions to this project where contributors want to encode a large hex string, this design will accomidate.

## Viewer
The viewer consists of the holograms that the user will interact with through the HoloLens, including adding functionality to prefabs in Unity to produce the holograms for Ventana. The necessary assets and scripts currently used, as well as the steps to be taken to create a hologram for a new device are explained in this section.

### Making And Inserting New Controllers For Ventana
*When should a new controller be added?*
* When the currently implemented controllers inherently lack a specific control type that is essential for the IoT device it needs to control.
* A user wants to create a different feel for their home.

*How hard is it to add new models for a controller*
* Not very hard if everything stated is taken into consideration.


For adding new control models, a user should have already created a 3D mesh asset file known as an FBX file as shown prior. If a user has an FBX that they would like to both import into Ventana and customize to fit their needs (i.e a different type of control interface than what is currently available) it should be imported into unity under the ```Assets/Resources/Ventana/Prefabs/Models``` directory in unity. 
The steps for this action are as follows:
 1. Open Ventana in Unity
 2. Under `Assets` select import asset
 3. Navigate to the directory the FBX is in
 4. Select the FBX file and Hit `Import`

It is best practice to place the FBX file in the ```Assets/Resouces/Ventana/Prefabs/Models``` folder found under the Ventana project directory.

Once a model has been successfully added to the project, a user must then create a new controller **prefab** object under the ```Assets/Resources/Ventana/Prefabs/``` directory of the cloned project directory structure. It is imperative that a prefab object for the desired controller be made in this location. This is where the `ModelController` class points to, and it manages the link between the Ventana configuration file and the physical controllers that are spawned at runtime.

For those unfamiliar with what a prefab is in unity, it is essentially a collection of 3D models and specific behaviours attached to them that a programmer architects together to perform a specific function in a Unity experience.

Making a prefab is as easy as navigating to a directory in the project tab in unity and right-clicking on the file explorer and selecting `Create>Prefab`. This will make an empty prefab object in which one can drag the model that was imported 
![Prefab](https://github.com/VentanaIoT/Ventana/blob/master/images/Prefab%20Empty%20.png?raw=true)
**Adding an empty prefab object** 
&nbsp;
&nbsp;

![Prefab2](https://github.com/VentanaIoT/Ventana/blob/master/images/Prefab%20drag.png?raw=true)
dragging a model to the empty prefab object.

at this point the prefab object has been created but no scripts or functionally have been added to the object. It is at this stage where custom materials could be added to the object, or Unity `MonoBehaviour` scripts could be added.

&nbsp;

Side Note: 
The fastest way to create a prefab is to drag the imported model into the scene hierarchy which will instantiate a model in the scene. A user may then drag the root object of the model to the project tab directory explorer. Once this action is completed, Unity will automatically generate a prefab.

### Adding API requests to Prefabs

Now that a new prefab has been created, a user will need to add functionality as they see fit. Part of this functionality is utilizing the parts that the Ventana team has created in order to interface with the HoloHub server.

Within the codebase for the HoloLens application, one can find the `VentanaRequestFactory` class. This class is a singleton that is persistent across the scene it is instantiated in. In addition, it contains helpful functions that allow a user to utilize currently implemented modules or create new ones. 

Currently, Ventana Request Factory implements methods Request the HoloHub to interact with compatible devices described in the following chapter.

The basic GameObject hierarchy that can be found within Ventana currentl is as follows:

```C#
BaseVentanaController subclassComponent;
EditModeController controllerComponent; //that controls Dragging WorldAnchors + Deletion
|
|
|_ _ Any Scripts that make your controller work.
```

Following this style will allow a user to write their scripts in a way that the child objects can notify the BaseVentanaController subclass of any changes. Once the child objects of the controller send events, the subclass can issue commands using the VentanaRequestFactory. 

An example of this is:

1. Inside of the BaseVentanaButton class that implements the IInputClickHandled interface from Microsoft's HoloToolkit
```C#
public void OnInputClicked(InputClickedEventData eventData) {
        gameObject.SendMessageUpwards("makeAPIRequest", gameObject.name);
}
```
2. Inside a Controller class 

```C#
void makeAPIRequest(string child) { //bubbled from child 
        requestFactory = VentanaRequestFactory.Instance;
    switch ( child ) {
        case "play": StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(playCommand, VentanaID, null));
        break;
        case "pause": StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(playCommand, VentanaID, null));
        break;
        case "next": StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(nextCommand, VentanaID, null));
        break;
        case "previous": StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(previousCommand, VentanaID, null));
        break;
        case "status": StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(statusCommand, this.VentanaID, GetRequestCompleted));
        break;
        default:
        break;
    }
}
```
Currently there are two types of request functions in VentanaRequestFactory. http GET and POST.
```C#
public IEnumerator GetFromMusicAPIEndpoint(string action, int id, Action<VentanaInteractable> callback)
public IEnumerator PostToMusicAPIEndpoint(string action, int id, string data)

public IEnumerator GetFromLightAPIEndpoint(string action, int id, Action<VentanaInteractable> callback)
public IEnumerator PostToLightAPIEndpoint(string action, int id, string data)

public IEnumerator GetFromPowerAPIEndpoint(string action, int id) 
public IEnumerator PostPowerAPIEndpoint(string action, int id) 
```

A user may define new functions for new API endpoints that they have defined in order to integrate them with the HoloHub.

please note that new functions should only be created when the existing API functions inherently lack key aspects to make the IoT device work. 

see below an example of http request code:
```C#
public IEnumerator PostToMusicAPIEndpoint(string action, int id, string data) {
        StringBuilder url = new StringBuilder(HoloHubURI);
        url.Append(MusicEndpoint);
        url.Append(action + "/");
        url.ToString());
        url.Append(id.ToString());
        url.Append("/");

        Dictionary<string, string> request = new Dictionary<string, string>();
        request.Add("value", data);
        UnityWebRequest holoHubRequest = UnityWebRequest.Post(url.ToString(), request);
        yield return holoHubRequest.Send();
        if ( !holoHubRequest.isError ) {
            Debug.Log("WWW Ok!: " + responseString);
        } else {
            Debug.Log("WWW Error: " + holoHubRequest.error);
        }
    }

```


### Interacting with Buttons
The user feedback, such as the highlighting of buttons when the user gazes on them, as well as the audio feedback when the user taps a button are implemented in Unity with the BaseVentanaButton.cs script. Each specific controller has a script that inherits from this, i.e. the buttons of the music controller have a MusicButtonHandler.cs script and the buttons of the light controller have a LightButtonHandler.cs script. 

Using raycast, the position of the user's gaze can be retrieved from the HoloLens. When the raycast of the user corresponds to a button, the OnFocusEnter function is called, and then the mesh of that button is replaced with a material that produces the highlighted effect. When the user's raycast changes, the OnFocusExit function is called, and then the button's mesh material is replaced with the original texture, so that it is no longer highlighted. 

To enable audio feedback, each Unity scene requires an audio listener. This is attached to the HoloLensCamera object in the Ventana Scene. It is added in Unity using the Component menu, selecting Audio, then Audio Listener. An Audio Source, also located under the Component menu, is added to each button that should produce an audio sound. Once the Audio Source is added to the button, use the Inspector tab to drag the desired sound to the AudioClip field and the Click Sound field. There are various options for possible sounds in Ventana's Resources folder that can be applied. When the button is clicked by the user, the OnInputClicked function is called, and the sound is played.

For future holograms to control new devices, the BaseVentanaButton.cs script, shown below, needs to be added to each button.
```C#
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVentanaButton : MonoBehaviour, IInputClickHandler, IFocusable {
    public AudioClip clickSound;
    protected AudioSource source;
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;

    public void OnFocusEnter() {
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        Debug.Log("Clicked " + gameObject.name);
        gameObject.SendMessageUpwards("makeAPIRequest", gameObject.name);
        source.PlayOneShot(clickSound, 1F);
    }

    public void DisableInteraction(bool yes) {
        if (yes) {
            gameObject.GetComponent<Collider>().enabled = false;
        } else {
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }

    // Use this for initialization
    void Start() {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }
```

The Audio Source component, described above, also needs to be added. The variable fields need to be populated with material and sounds, within the red boxes in the image below, and then functionality described above will be added to it.

![image alt](https://github.com/VentanaIoT/Ventana/blob/master/images/buttoninspector.PNG?raw=true![](https://i.imgur.com/JHSfVuI.png) =300x)
**Inspector for a Button on the Music Controller**

### Sliders
The current slider implementation in Ventana is both robust and limited. Currently there is a vestigious music controller model that all controllers who use sliders utilize for its slider model. What this means is that the specific model, `SliderSlaveUserForItsSliderModels`,  cannot be deleted as of right now. 

Sliders utilize the `KnobHandler.cs` script which can be found under `Assets/Ventana/Scripts/Interaction/MusicController`.


KnobHandler implements from Microsoft HoloToolkit's `HandDraggable.cs` script as a base class for its functionality. KobHandler uses the HandDraggable moving logic to detect when and where it should go when a user begins interacting with the knob. What KnobHandler does is both limit, and quantize values to send through a BaseVentanaController. This is done inside the Update method of the KnobHandler script. It looks at the current position of the Knob within the parent that a user defines in the editor and relates that to the overall distance.

```C#
public override void Update() {
    base.Update();
    //Debug.Log("BOUNDS: " + bounds.x + " " + bounds.y + " " + bounds.z);
    bounds = normalizedVector;
    bounds.Scale(scale);
    //we know we want to keep the x and y position at a certain place, only want the y offset. so lets constantly keep putting this thing there
    if ( isDragging ) {
        var position = gameObject.transform.localPosition;
        Vector3 origin = baseLocation;       
        //gives x y z values for the size. we want to go .5 times the axis of freedom max.
        float[] allowedThreshold = new float[3];
                allowedThreshold[0] = bounds.x * 0.5f;
                allowedThreshold[1] = bounds.y * 0.5f;
                allowedThreshold[2] = bounds.z * 0.5f;               
        if ( !allowX ) {
            //change x to be the origin 
            position.x = origin.x;
        } else { //allowX
            if ( position.x > allowedThreshold[0] + origin.x ) { //positive offset
                //just set the position of x to be the max....
                position.x = allowedThreshold[0];
            } else if ( position.x < origin.x - allowedThreshold[0] ) {
                position.x = -allowedThreshold[0];
            }
        }
        if ( !allowY ) {
            //change y to be the origin
            position.y = origin.y;
        } else { //allowY
            if ( position.y > allowedThreshold[1] + origin.y ) { //positive offset
                //just set the position of x to be the max....
                position.y = allowedThreshold[1];

            } else if ( position.y < origin.y - allowedThreshold[1] ) {
                position.y = -allowedThreshold[1];
            }
        }
        if ( !allowZ ) {
            position.z = origin.z;
        } else { //allowZ
            if ( position.z > allowedThreshold[2] + origin.z ) { //positive offset
                //just set the position of x to be the max....
                position.z = allowedThreshold[2];

            } else if ( position.z < origin.z - allowedThreshold[2] ) {
                position.z = -allowedThreshold[2];
            }
        }

        gameObject.transform.localPosition = position;
        gameObject.transform.localRotation = baseRotation;

        //only do it in the x direction cause it seems to not work on others....
        //only do this calculation about each second...

        // If the next update is reached

        if ( Time.time >= nextActionTime ) {
            //Debug.Log(Time.time + ">=" + nextActionTime);
            // Change the next update (current second+1)
            nextActionTime = Mathf.FloorToInt(Time.time) + period;
            // Call your fonction
            if ( shouldExecute ) { // this is where I perform calculations
                performLevelCalculations();
            } else {
                shouldExecute = true;
            }
        }
    }
    } else {
        //stuff to do when not dragging...
        baseLocation = gameObject.transform.localPosition;
        baseRotation = gameObject.transform.localRotation;
    }
}


override public void StopDragging() {
    base.StopDragging();
    gameObject.transform.localRotation = baseRotation;
    gameObject.transform.localPosition = baseLocation;
    shouldExecute = false;

    source.PlayOneShot(clickSound, 1F);


}

public override void StartDragging() {
    base.StartDragging();
    //wait X seconds before you start doing any calcs;
    shouldExecute = false;
    baseLocation = gameObject.transform.localPosition;
    baseRotation = gameObject.transform.localRotation;

    source.PlayOneShot(clickSound, 1F);

}

public void performLevelCalculations() {
    Vector3 origin = baseLocation;
    Vector3 currentLocation = gameObject.transform.localPosition;
    var bounds = containerObject.GetComponent<MeshRenderer>().bounds.size.normalized;
    bounds.Scale(new Vector3(0.001f, 0.001f, 0.001f));
    SliderLevels sliders = new SliderLevels();
    //gives x y z values for the size. we want to go .5 times the axis of freedom max.
    float[] allowedThreshold = new float[3];
    allowedThreshold[0] = bounds.x * 0.5f;
    allowedThreshold[1] = bounds.y * 0.5f;
    allowedThreshold[2] = bounds.z * 0.5f;

    if ( allowX ) {
    //calculate what % of the allowed direction im at.
    //0-30% +1pt 31-60% +2pts 61-100% +3pts if to the right
    //0-30% -1pt 31-60% -2pts 61-100% -3pts if to the left 

        if (currentLocation.x < origin.x ) { //left side of origin
            var delta = Mathf.Abs(currentLocation.x - origin.x);
            if ( delta > (0.61f * allowedThreshold[0]) ) {
                Debug.Log("LEVEL 3 DECREASE");
                sliders.XAxisLevel = -3;
            } else if ( delta > (0.31f * allowedThreshold[0]) && delta < (0.60f * allowedThreshold[0]) ) {
                Debug.Log("LEVEL 2 DECREASE");
                sliders.XAxisLevel = -2;
            } else if (delta > (0.01f * allowedThreshold[0]) && delta < (0.30f * allowedThreshold[0])) {
                Debug.Log("LEVEL 1 DECREASE");
                sliders.XAxisLevel = -1;
            } else {
                //do nothing weird numbers....
                sliders.XAxisLevel = 0;
            }

        } else if (currentLocation.x >= origin.x  ) { //right side of origin
            var delta = Mathf.Abs(currentLocation.x - origin.x);
            if ( delta > (0.61f * allowedThreshold[0]) ) {
                Debug.Log("LEVEL 3 INCREASE");
                sliders.XAxisLevel = 3;
            } else if ( delta > (0.31f * allowedThreshold[0]) && delta < (0.60f * allowedThreshold[0]) ) {
                Debug.Log("LEVEL 2 INCREASE");
                sliders.XAxisLevel = 2;
            } else if ( delta > (0.01f * allowedThreshold[0]) && delta < (0.30f * allowedThreshold[0]) ) {
                Debug.Log("LEVEL 1 INCREASE");
                sliders.XAxisLevel = 1;
            } else {
                //do nothing weird numbers....
                sliders.XAxisLevel = 0;
            }
        }
    }

    if ( allowY ) {
        if ( currentLocation.y < origin.y ) { //left side of origin
            var delta = Mathf.Abs(currentLocation.y - origin.y);
            if ( delta > (0.61f * allowedThreshold[1]) ) {
                Debug.Log("LEVEL 3 DECREASE");
                sliders.YAxisLevel = -3;
            } else if ( delta > (0.31f * allowedThreshold[1]) && delta < (0.60f * allowedThreshold[1]) ) {
                Debug.Log("LEVEL 2 DECREASE");
                sliders.YAxisLevel = -2;
            } else if ( delta > (0.01f * allowedThreshold[1]) && delta < (0.30f * allowedThreshold[1]) ) {
                Debug.Log("LEVEL 1 DECREASE");
                sliders.YAxisLevel = -1;
            } else {
                //do nothing weird numbers....
                sliders.YAxisLevel = 0;
            }
        } else if ( currentLocation.y >= origin.y ) { //right side of origin
            var delta = Mathf.Abs(currentLocation.y - origin.y);
            if ( delta > (0.61f * allowedThreshold[1]) ) {
                Debug.Log("LEVEL 3 INCREASE");
                sliders.YAxisLevel = 3;
            } else if ( delta > (0.31f * allowedThreshold[1]) && delta < (0.60f * allowedThreshold[1]) ) {
                Debug.Log("LEVEL 2 INCREASE");
                sliders.YAxisLevel = 2;
            } else if ( delta > (0.01f * allowedThreshold[1]) && delta < (0.30f * allowedThreshold[1]) ) {
                Debug.Log("LEVEL 1 INCREASE");
                sliders.YAxisLevel = 1;
            } else {
                //do nothing weird numbers....
                sliders.YAxisLevel = 0;
            }
        }

    } else {
    //...
    }

    if ( allowZ ) {
        if ( currentLocation.z < origin.z ) { //left side of origin
            var delta = Mathf.Abs(currentLocation.z - origin.z);
            if ( delta > (0.61f * allowedThreshold[2]) ) {
                Debug.Log("LEVEL 3 DECREASE");
                sliders.ZAxisLevel = -3; 
            } else if ( delta > (0.31f * allowedThreshold[2]) && delta < (0.60f * allowedThreshold[2]) ) {
                Debug.Log("LEVEL 2 DECREASE");
                sliders.ZAxisLevel = -2;                                                    
            } else if ( delta > (0.01f * allowedThreshold[2]) && delta < (0.30f * allowedThreshold[2]) ) {
                Debug.Log("LEVEL 1 DECREASE");
                sliders.ZAxisLevel = -1;
            } else {
                //do nothing weird numbers....
                sliders.ZAxisLevel = 0;
            }
        } else if ( currentLocation.z >= origin.z ) { //right side of origin
            var delta = Mathf.Abs(currentLocation.z - origin.z);
        if ( delta > (0.61f * allowedThreshold[2]) ) {
                Debug.Log("LEVEL 3 INCREASE");
                sliders.ZAxisLevel = 3;
        } else if ( delta > (0.31f * allowedThreshold[2]) && delta < (0.60f * allowedThreshold[2]) ) {
                Debug.Log("LEVEL 2 INCREASE");
                sliders.ZAxisLevel = 2;                                                   
        } else if ( delta > (0.01f * allowedThreshold[2]) && delta < (0.30f * allowedThreshold[2]) ) {
                Debug.Log("LEVEL 1 INCREASE");
           sliders.ZAxisLevel = 1;
            } else {
                 //do nothing weird numbers....
                 sliders.ZAxisLevel = 0;
            }
        }

    } else {    }

    if (sliders.XAxisLevel != 0 || sliders.YAxisLevel != 0 || sliders.ZAxisLevel != 0) {
        HandleSliderChangeRequest(sliders);
    }
}
```

One may think about this as an array of zones
```
                  |  -3  |  -2  |  -1  |  +1  |  +2  |  +3  | 
```
after a user defined period of time expires, the knob will see where it is relative to its parent. Using this information, it will dictate what zone of increase or decrease it belongs and kindly asks the BaseVentanaController subclass to take care of it:
```C# 
protected void HandleSliderChangeRequest(SliderLevels levels) {
    gameObject.SendMessageUpwards("OnSliderChangeRequest", levels);
}
```
User defined values for sliders:
![image alt](https://github.com/VentanaIoT/Ventana/blob/master/images/sliders.png?raw=true)
&nbsp;

An example of a slider working:
![image alt](https://github.com/VentanaIoT/Ventana/blob/master/images/SliderHalfway.jpg?raw=true)



### Edit Mode

Edit Mode allows the user to move the hologram to a new location, change the size of the hologram, or delete the hologram from the user's world.

![image alt](https://github.com/VentanaIoT/Ventana/blob/master/images/20170404_155038_HoloLens.jpg?raw=true![](https://i.imgur.com/H2MIoNN.jpg))
**Edit mode picture taken on the HoloLens**

To implement edit mode, a tap to place container is the parent of each of the buttons, including the delete/done buttons and each of the four scaling handles. The EditModeController.cs script is attached to the parent of the "ttpcontainer." Messages are sent upwards from each of the scripts on the buttons and the EditModeController.cs script has functions associated with each of the messages. The delete/done buttons behavior is encapsulated in a ddScript.cs. Each of the four scaling handles has a scalingHandler.cs script associated with it. The HandDraggable.cs script is enabled when the enters edit mode. The HandDraggable.cs script is from HoloToolkit and is located in the appendix at the end of the document. To enter edit mode, the more button that appears at the bottom of a controller in regular mode has an Interactible.cs script associated with it.

The Interactible.cs script was based off a script from HoloToolkit. The script requires the InteractibleManager.cs be added to one of the managers in the Ventana Scene. 

This allows the Interactible.cs script, shown below, to detect when the user's gaze is on the more button. When the user is looking, the more button's renderers are true, so the more button is shown, and the user can click on it. When the user clicks on it, the "MoreButtonClicked" message is sent upwards to EditModeController.cs to enter into edit mode. When the user is not looking at the more button, its renderers are false, so the more button is not shown. 
```C#
using UnityEngine;
using HoloToolkit.Unity.InputModule;

/// <summary>
/// The Interactible class flags a Game Object as being "Interactible".
/// Determines what happens when an Interactible is being gazed at.
/// </summary>
public class Interactible : MonoBehaviour, IInputClickHandler
{
    private Material[] defaultMaterials;  

    void Start()
    {
        defaultMaterials = GetComponent<Renderer>().materials;

        // Add a BoxCollider if the interactible does not contain one.
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        // Show the more buttons to start, can be initialized to false if we don't want to show them
        Renderer[] renderer = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renderer)
            child.enabled = true;
    }

    void GazeEntered()
    {
        // Debug.Log("GazeEntered");
        Renderer[] renderer = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renderer)
            child.enabled = true;
    }

    void GazeExited()
    {
       // Debug.Log("GazeExited");
 
        Renderer[] renderer = GetComponentsInChildren<Renderer>();
        foreach (Renderer child in renderer)
            child.enabled = false;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //Debug.Log("<color=yellow>EY BAY BAY</color>");

        // Send a more button clicked message to EditModeController script
        gameObject.SendMessageUpwards("MoreButtonClicked");
    }
}
```
In edit mode, the ddScript.cs, shown below, contains the functionality for each of the delete/done buttons. When the either button is pressed, the "ddButtonClicked" message is sent upwards, along with the gameobject name. EditModeController.cs handles the message differently based on the gameobject name, either "Delete Button" or "Done Button." The script also inherits from IFocusable, and uses OnFocusEnter and OnFocusExit functions to disable/enable the HandDraggable.cs script accordingly. This prevents the HandDraggable.cs script from interfering with the user tapping either of the delete or done buttons. This is also implemented in scalingHandler.cs for the same reason. 
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class ddScript : MonoBehaviour, IInputClickHandler, IFocusable {
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;

    // Use this for initialization
    void Start () {
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("button pressed");
        gameObject.SendMessageUpwards("ddButtonClicked", gameObject.name);
    }

    public void OnFocusEnter() {
        gameObject.SendMessageUpwards("DisableHandDraggable");
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.SendMessageUpwards("EnableHandDraggable");
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }
}
```

The scalingHandler.cs script, shown below, inherits from the manipulation handler class, from HoloToolkit. The manipulation gesture allows the user to tap a scaling handle and when the user's hand is moved, the event data is captured by the HoloLens. With this data, a vector is calculated and a new value for the parent's local scale can be set. 
```C#
using System;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class scalingHandler : MonoBehaviour, IManipulationHandler, IFocusable
{
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;
    public Transform parentObject;
    [Tooltip("Speed at which the object is resized.")]
    [SerializeField]
    float ResizeSpeedFactor = 1.0f;

    [SerializeField]
    float ResizeScaleFactor = 0.75f;

    [Tooltip("When warp is checked, we allow resizing of all three scale axes - otherwise we scale each axis by the same amount.")]
    [SerializeField]
    bool AllowResizeWarp = false;

    [Tooltip("Minimum resize scale allowed.")]
    [SerializeField]
    float MinScale = 0.0f;

    [Tooltip("Maximum resize scale allowed.")]
    [SerializeField]
    float MaxScale = 0.7f;

    private Vector3 lastScale;

    private Vector3 lastManipulationPosition;


    [SerializeField]
    bool resizingEnabled = true;
    void Start()
    {
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    public void SetResizing(bool enabled)
    {
        resizingEnabled = enabled;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        gameObject.SendMessageUpwards("scaleStarted");
        lastScale = parentObject.localScale;
        InputManager.Instance.PushModalInputHandler(gameObject);
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if ( resizingEnabled ) {
            Resize(eventData.CumulativeDelta);
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        gameObject.SendMessageUpwards("scaleEnded");
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }
    void Resize(Vector3 newScale)
    {
        
        Vector3 camHandDelta = Camera.main.transform.InverseTransformDirection(newScale);
        // send data to EditModeController.cs once the manipulation gesture is updated
        //gameObject.SendMessageUpwards("scaleButtonClicked", newScale);
        float resizeX, resizeY, resizeZ;
        //if we are warping, honor axis delta, else take the x

        resizeX = resizeY = resizeZ = camHandDelta.x * ResizeScaleFactor;
        resizeX = Mathf.Clamp(lastScale.x + resizeX, MinScale, MaxScale);
        resizeY = Mathf.Clamp(lastScale.y + resizeY, MinScale, MaxScale);
        resizeZ = Mathf.Clamp(lastScale.z + resizeZ, MinScale, MaxScale);
        parentObject.localScale = Vector3.Lerp(parentObject.localScale,
            new Vector3(resizeX, resizeY, resizeZ),
            ResizeSpeedFactor);

    }

    public void OnFocusEnter() {
        gameObject.SendMessageUpwards("DisableHandDraggable");
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.SendMessageUpwards("EnableHandDraggable");
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }
}
```

In EditModeController.cs, shown below, each of the messages sent upwards by the other scripts has its own function. The script ensures that the more buttons are set active initially, and the other buttons are set inactive. This toggles when the more button is pressed, and then returns when the done button is pressed.
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine.VR.WSA;
using HoloToolkit.Unity;
using System;

[ExecuteInEditMode]
public class EditModeController : MonoBehaviour {

    public Transform moreButtons;
    public Transform deleteDone;
    public Transform scaleHandles;
    public bool scaleEnabled = false;
    public bool scaleModeTriggered = false;
    public AudioClip clickSound;
    public Vector3 lastScale;
    private HandDraggable handDraggable;
    private AudioSource source;


    [Tooltip("Speed at which the object is resized.")]
    [SerializeField]
    float ResizeSpeedFactor = 1.0f;

    [SerializeField]
    float ResizeScaleFactor = 0.75f;

    [Tooltip("When warp is checked, we allow resizing of all three scale axes - otherwise we scale each axis by the same amount.")]
    [SerializeField]
    bool AllowResizeWarp = false;

    [Tooltip("Minimum resize scale allowed.")]
    [SerializeField]
    float MinScale = 0.05f;

    [Tooltip("Maximum resize scale allowed.")]
    [SerializeField]
    float MaxScale = 0.7f;

    // Use this for initialization
    void Start () {
        source = gameObject.GetComponent<AudioSource>();
        // initialize to regular mode, with tap to place controls inactive
        moreButtons.gameObject.SetActive(true);
        deleteDone.gameObject.SetActive(false);
        scaleHandles.gameObject.SetActive(false);

       
        handDraggable = gameObject.GetComponent<HandDraggable>();

        if (handDraggable != null ) {
            handDraggable.StoppedDragging += HandDraggable_StoppedDragging;
            handDraggable.StartedDragging += HandDraggable_StartedDragging;
        }

    }

    private void HandDraggable_StartedDragging() {
        SpatialMappingManager.Instance.DrawVisualMeshes = true;
        Debug.Log(gameObject.name + " : Removing existing world anchor if any.");
        WorldAnchorManager.Instance.RemoveAnchor(gameObject);
    }

    private void HandDraggable_StoppedDragging() {
        SpatialMappingManager.Instance.DrawVisualMeshes = false;

        // Add world anchor when object placement is done.
        BaseVentanaController bvc = gameObject.GetComponent<BaseVentanaController>();
        if ( bvc ) {
            string currentTime = DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString();
            string savedAnchorName = bvc.VentanaID + ":" + gameObject.transform.lossyScale.x.ToString()+ ":" + currentTime;
            Debug.Log("<color=yellow>Name: </color>" + savedAnchorName);

            WorldAnchorManager.Instance.AttachAnchor(gameObject, savedAnchorName);
        }
        source.PlayOneShot(clickSound, 1F);
    }



    // Update is called once per frame
    void Update () {
		if ( scaleModeTriggered ) {
            moreButtons.gameObject.SetActive(false);
            deleteDone.gameObject.SetActive(true);
            scaleHandles.gameObject.SetActive(true);
            gameObject.BroadcastMessage("DisableInteraction", true);
        } else {
            moreButtons.gameObject.SetActive(true);
            deleteDone.gameObject.SetActive(false);
            scaleHandles.gameObject.SetActive(false);
            gameObject.BroadcastMessage("DisableInteraction", false);
        }
	}

    void MoreButtonClicked()
    {
        //Debug.Log("More button clicked");
        // More button clicked so tap to place mode should be active
        moreButtons.gameObject.SetActive(false);
        deleteDone.gameObject.SetActive(true);
        scaleHandles.gameObject.SetActive(true);
        //Enable Tap To Place and Hand Dragable here.
        
        if ( handDraggable != null ) {
            handDraggable.enabled = true;
        }

        scaleModeTriggered = true;
        source.PlayOneShot(clickSound, 1F);
    }

    void ddButtonClicked(string child)
    {
        if (child.Equals("Done Button"))
        {
           // Debug.Log("Done button clicked");
           // Done button clicked so regular mode should be active
            moreButtons.gameObject.SetActive(true);
            deleteDone.gameObject.SetActive(false);
            scaleHandles.gameObject.SetActive(false);
            scaleModeTriggered = false;
            
            if ( handDraggable != null ) {
                handDraggable.enabled = false;
            }

        }

        if (child.Equals("Delete Button"))
        {
            //Debug.Log("Delete button clicked");
            // gonna have to remove world anchor here
            if (handDraggable != null)
            {
                // had to make anchorManager public instead of protected in ttp
                WorldAnchor wa = gameObject.GetComponent<WorldAnchor>();
                if ( wa ) {
                    WorldAnchorManager.Instance.AnchorStore.Delete(wa.name);
                }
               
            }
            scaleModeTriggered = false;
            Destroy(gameObject);
        }

        source.PlayOneShot(clickSound, 1F);
    }

    void scaleStarted()
    {
        // manipulation gesture started so get the current scale
        //turn off draggable behaviours
        
    }

    void scaleEnded() {
        BaseVentanaController bvc = gameObject.GetComponent<BaseVentanaController>();
        if ( bvc ) {
            Debug.Log(gameObject.name + " : Removing existing world anchor if any after scaling");
            WorldAnchorManager.Instance.RemoveAnchor(gameObject);
            string currentTime = DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString();
            string savedAnchorName = bvc.VentanaID + ":" + gameObject.transform.lossyScale.x.ToString() + ":" + currentTime;
            Debug.Log("<color=yellow>Name: </color>" + savedAnchorName);

            WorldAnchorManager.Instance.AttachAnchor(gameObject, savedAnchorName);
        }
    }

    void EnableHandDraggable() {
        if ( handDraggable ) { handDraggable.enabled = true; }
    }

    void DisableHandDraggable() {
        if ( handDraggable ) { handDraggable.enabled = false; }
    }
    
    void scaleButtonClicked(Vector3 newScale)
    {
        // manipulation gesture ended, calculate and set the new scale 
        /* https://www.billmccrary.com/holotoolkit-simple-dragresizerotate/ modified from HandResize.cs */

        float resizeX, resizeY, resizeZ;
        //if we are warping, honor axis delta, else take the x
        if (AllowResizeWarp)
        {
            resizeX = newScale.x * ResizeScaleFactor;
            resizeY = newScale.y * ResizeScaleFactor;
            resizeZ = newScale.z * ResizeScaleFactor;
        }
        else
        {
            resizeX = resizeY = resizeZ = newScale.x * ResizeScaleFactor;
        }

        resizeX = Mathf.Clamp(lastScale.x + resizeX, MinScale, MaxScale);
        resizeY = Mathf.Clamp(lastScale.y + resizeY, MinScale, MaxScale);
        resizeZ = Mathf.Clamp(lastScale.z + resizeZ, MinScale, MaxScale);

        Vector3 newTransform = new Vector3(resizeX, resizeY, resizeZ);

        Transform currentTransform = gameObject.GetComponent<Transform>();
        //Debug.Log("before scale" + currentTransform);
        currentTransform.localScale = Vector3.Lerp(transform.localScale, newTransform, ResizeSpeedFactor);
        //Debug.Log("scale:" + currentTransform.localScale);
    }
}
```

To implement edit mode on any new controllers, the "ttpcontainer" needs to be added to the new controller. The new controller will be a child of the parent in the ttpcontainer. A new prefab can be made from this, as detailed in the "Making a Prefab" section above. 

### World Anchors

World Anchors are a built-in functionality in both Unity and the HoloLens' HoloToolkit. The World Anchor Store archives a unique id and the location of a gameobject. The World Anchor Manager, available from HoloToolkit, and located under Managers in the Ventana Scene hierarchy, facilitates this functionality for the application. AnchorLoader.cs ensures that when Ventana is started all the appropriate holograms are loaded. This script works in combination with functions in the EditModeController.cs, as well as a persistent gameobject. The persistent gameobject is initialized in the Welcome Scene to relay information to the Ventana Scene, regarding whether the user would like to start a new session or load the last session.

In EditModeController.cs, the HandDraggable_StartedDragging and HandDraggable_StoppedDragging functions update the World Anchor Store. In HandDraggable_StartedDragging, the existing world anchor for the gameobject needs to be deleted because the gameobject is about to be moved to a new position. In HandDraggable_StoppedDragging, a new world anchor for the gameobject needs to be created because the gameobject's position is fixed, for the time being. This is where the id for the gameobject's world anchor is created, using a string that consists of the gameobject's Ventana ID, current scale, and the current time. The time is added to the string to ensure that the id is always unique.   

In the AnchorLoader.cs script shown below, if the user decides to load the last session, the ids in the World Anchor Store are iterated through, and the appropriate model controller is loaded in the position associated with that id, by parsing the id string into it's Ventana ID, the scale of the gameobject, and the time the world anchor was saved. If the user decides to start a new session, the World Anchor Store is cleared, and no model controllers are loaded.
```C#
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.VR.WSA.Persistence;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections.Generic;
using System;
using System.Globalization;

public class AnchorLoader : MonoBehaviour
{
    bool loaded;

    private void Start()
    {
        WorldAnchorStore.GetAsync(OnWorldAnchorStoreLoaded);
    }

    private void OnWorldAnchorStoreLoaded(WorldAnchorStore store) {

        var persistentGameObject = GameObject.Find("persistentGameObject");
        persistentGameObjectScript persistentScript = persistentGameObject.GetComponent<persistentGameObjectScript>();

        if (persistentScript.loadWorldAnchors)
        {
            Debug.Log("LOADING WORLD ANCHORS");
            var ids = store.GetAllIds();

            foreach (var id in ids)
            {

                char[] delimiterChars = { ':' };
                string[] anchorInfo = id.ToString().Split(delimiterChars);
                Debug.Log("<color=yellow>Anchor ID:" + anchorInfo[0] + " Lossy Scale: " + anchorInfo[1] + " Creation Time: " + anchorInfo[2]);

                ModelController mc = ModelController.Instance;
                int integerID = Convert.ToInt32(anchorInfo[0]);
                try
                {
                    GameObject go = mc.GetPrefabWithId(integerID);
                    BaseVentanaController bvc = go.GetComponent<BaseVentanaController>();
                    if (bvc)
                    {
                        bvc.OnVumarkFound();
                        bvc.VentanaID = integerID;
                    }
                    HandDraggable hd = go.AddComponent<HandDraggable>();
                    hd.enabled = false;
                    hd.RotationMode = HandDraggable.RotationModeEnum.OrientTowardUserAndKeepUpright;
                    hd.IsDraggingEnabled = true;

                    float scaleVal = float.Parse(anchorInfo[1], CultureInfo.InvariantCulture.NumberFormat);

                    go.transform.localScale = new Vector3(scaleVal, scaleVal, scaleVal);
                    store.Load(id, go);
                }
                catch (Exception ex )
                {
                    Debug.Log("[Anchor Loader] "+ ex.Message);
                }
            }
        } 
        else
        {
            store.Clear();
            Debug.Log("World Anchor Store CLEARED");
        }
    }
    private void Update()
    {
    }
}
```

### Websockets 

The Ventana HoloLens Application needed a way to update metadata about its controllers from the HoloHub servers. It found a solution through the Socket.IO library for Node.js applications. There was no good Socket.IO library for the HoloLens, so the Ventana team developed their own implementation of a simple websocket client for a UWP application that can only consume messages from the HoloHub server.

The first attempt at using Socket.IO came from this [unity component](https://www.assetstore.unity3d.com/en/#!/content/21721). What became apparent about this component is that only works in Unity and the UWP code only works on the HoloLens.

With that in mind, and for development purposes, the Team utilized compiler directives to only compile the right version of the Socket.IO code for the platform that it was being deployed to using the following directive structure.

```C#
#if !UNITY_EDITOR
//Unity Code
#else
//HoloLens Code
#endif
```

In essence the Team defined the same class for the HoloLens UWP code as what was provided by the unity component mentioned above. A basic skelleton for the UWP code was created using [this Microsoft sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/WebSocket). 

The code relies on the UWP **MessageWebSocket** class to esablish a connection to the socket client on the Raspberry Pi 3. 

A vanilla websocket connection can be opened using code resembling 
```C#
private async Task ConnectWebsocket() {
    websocket = new MessageWebSocket();
    Uri server = new Uri(HoloHubWS);

    websocket.Control.MessageType = SocketMessageType.Utf8;
    websocket.MessageReceived += Websocket_MessageReceived;
    websocket.Closed += Websocket_Closed;
    try {
        await websocket.ConnectAsync(server);
        isConnected = true;
        writer = new DataWriter(websocket.OutputStream);
    }
    catch ( Exception ex ) // For debugging
    {
        // Error happened during connect operation.
        websocket.Dispose();
        websocket = null;
        Debug.Log("[SocketIOComponent] " + ex.Message);

        if ( ex is COMException ) {
            Debug.Log("Send Event to User To tell them we are unable to connect to Pi");
        }
        return;
    }
}
```

where the HoloHubWS value is a string resembling `ws://192.168.0.xxx:xxx/socket.io/?EIO=3&transport=websocket`

This address was acquired through analyzing network traffic with a site connected to a HoloHub websocket. 

once connected, a user can verify the connection on the HoloHub by seeing a ```Client Connected...``` message.

#### Receiving Messages

Once a websocket connection has been established any controller listening to Ventana websocket messages will check if the incoming transmission matches the channel name that they are expecting using a parser and event system that the team developed.

Important Information to note:

websocket emitted messages come in different types
1. &nbsp;0{<connection data>} -- this is an acknowledgement that the client connected with the server.
2. &nbsp;42{"Channel_Name", {JSON}} -- this marks a message sent from the emit function from the HoloHub server.

3. &nbsp;using a MessageWebSocket UWP class outputs everything as a string.

In Ventana, there is a class called **VentanaSocketParser** which is in charge of receiving the string response from the HoloHub and parsing it into an object that Ventana can use to input into an event queue.

To access the websocket connection, one need only but to call the shared instance of it within the VentanaRequestFactory.
```C#
    SocketIOComponent socc = VentanaRequestFactory.Instance.socket;
```

Once the user has the shared SocketIOComponent reference. They must use the pair of functions that a Ventana user that wants to consume channel events can call from within itself.

These are:  
```C#
public void On(string ev, Action<SocketIOEvent> callback) 
public void Off(string ev, Action<SocketIOEvent> callback)
```

Calling On requires having both the channel name to listen to and a callback within the class implementing this channel listener to execute. 

Example usage:
```C#
socket = VentanaRequestFactory.Instance.socket;
socket.On("push", HandlePush); //HandlePush is a function I declared
```

Calling Off is important to do before the object referencing the socket connection is destroyed, it merely removes the references in the event Queue declared inside of SocketIOComponent.


With Websockets in place, the Ventana Application is now able to update multiple music controllers at once. However, There are more possibilities for this functionality than just updating music controllers that new users can take advantage of.

Below are a couple of examples of websockets at play

Everything can be found in the **SocketIOComponent** class except supporting classes.
![cutiful](https://github.com/VentanaIoT/Ventana/blob/master/images/showing%20WS.png?raw=true)

After selecting the next previous button one can see the text change to match the song that was previously playing. 

![cutie2](https://github.com/VentanaIoT/Ventana/blob/master/images/Showing%20WS2.png?raw=true)

An example of websockets changing album art is shown below.

![cutie3](https://github.com/VentanaIoT/Ventana/blob/master/images/Showing%20WS3.png?raw=true)

This reality capture shows the album artwork changing to match the song that, at that point in time, began playing.



## HoloHub (Server)
### HoloHub Requirements
The HoloHub itself is a Raspberry Pi running Windows 10 IoT Core with Node.js modules that run the server software.

A mongoDB database is requred for storing the paired objects on the HoloHub. An account can be created at [mlab.com](https://mlab.com) where a hosted MongoDB database can be setup on an Azure running cloud server for no cost.

#### Node Dependencies

- body-parser: ~1.0.1
- ejs: ^2.5.6
- express: ~4.0.0
- express-session: ^1.15.1
- grant: ^3.7.1
- grant-express: ^3.7.1
- mongoose: ~4.6.8
- morgan: ~1.0.0
- pubnub: ^4.5.0
- socketio: ^1.0.0

### VentanaDM Schema
The Ventana Device Model schema defines the attributes that a HoloHub object must have in order to be supported and thus, paired into the platform. The required elements for a paired device are: 
- VuMark ID: This is the HoloHub unique identifier and the way the HoloLens Ventana application requests a device.
- Device_ID: The is the vendor identifier. Requests to Vendor modules will utilize this ID
- Device_type: This specifies what kind of controller this device will need based on the compatibility list.
- Device_Name: A user friendly name given to the object setup on the Vendor side.
- Controller: The Ventana relative path to the holographic controller for this object
- Vendor: The name of the Vendor
- Vendor_Logo: Recognizable image for the HoloHub app to display.

### Currently Supported Vendors 
The HoloHub server is designed to be modular, which allows for developers to contribute to this open-source project. In order to add a new vendor that is not currently supported, a vendor module would need to be developed and it can use the skeleton of the Sonos or Wink module, in sonos.js or wink.js respectively.

| Device Name                       | Vendor Module | Compatiblity | Controller            |
|-----------------------------------|---------------|--------------|-----------------------|
| Sonos speaker devices (All)       | Sonos         | Verified     | Music Controller      |
| Dimmable Lights (Wink-compatible) | Wink          | Verified     | Light Controller      |
| Quirky Power Strip                | Wink          | Verified     | PowerStrip Controller |

#### Sonos Devices

Sonos devices work on the Local Area Network (LAN) and are accessible exclusively via a locally connecgted device. The HoloHub is designed to automatically detect and keep track of the Sonos devices connected on the network and provides full compatilbity for the controller-provided functions on Ventana. This process is aided by the Node-Sonos-HTTP-API server running in Node as described in the installation portion of this manual. 

The way the HoloHub handles Sonos device requests involves standardizing the endpoints that communicate with the HoloLens to play toggle, skip, previous, volume, album art, and status. These endpoints then get translated into its respective endpoints on the sonos http api server module which will translate the requests into a Sonos compliant request directly to the speakers. In addition a websocket system has been put into place which allows for real-time tracking of the changes in state of any Sonos device connected on the network. This coupling requires that both the HoloHub and the Node-Sonos-HTTP-API servers are running locally and simultaneously on the Raspberry Pi 3. 

##### API Endpoints for Sonos

|        Endpoint       | Method |   Params  |                              Body                             |                              Response                             |
|:---------------------:|:------:|:---------:|:-------------------------------------------------------------:|:-----------------------------------------------------------------:|
|           /           |   GET  |     -     |                               -                               | JSON object with all Sonos Devices based on the Ventana DM Schema |
|           /           |  POST  |     -     |  JSON object with all fields required for Ventana DM Schemas  |                SonosDM object created!, Status 200                |
|    /byId/:VuMarkID    |   GET  | vumark_id |                               -                               |                       SonosDM objet if found                      |
|    /byId/:VuMarkID    |   PUT  | vumark_id |                 SonosDM object getting updated                |                                                                   |
|   /status/:VuMarkID   |   GET  | vumark_id |                               -                               |             Sonos Music Response (HoloLens compatible)            |
| /playtoggle/:VuMarkID |   GET  | vumark_id |                               -                               |                        Sonos Music Response                       |
|   /foward/:VuMarkID   |   GET  | vumark_id |                               -                               |                        Sonos Music Response                       |
| /reverse/:VuMarkID    | GET    | vumark_id | -                                                             | Sonos Music Response                                              |
| /volume/:VuMarkID     | POST   | vumark_id | Postitive or Negative integer increment to the current volumn | Sonos Music Response                                              |
| /devices              | GET    | -         | -                                                             | All Sonos devices on the network by Vendor ID                     |
| /pushnotification     | POST   | -         | Sonos Vendor Device Object                                    | ok                                                                |


#### Wink Devices
The Wink module itself is composed of endpoints that the HoloHub server and the Ventana HoloLens application use to interact with the Wink specifc devices and glean information about the state of the user's devices. All Wink supported devices that have a power field can be easily integrated into the existing server, due to the modularity of the endpoints and the Wink Schema.

##### API Endpoints for Wink
| Endpoint  | Method | Params | Body | Response | 
| :----: | :----: | :--: | :----: | :-----: |
| / | GET | none | none | Error message if the User isn't connected to Wink; Wink objects if they are connected to the account |
| / | POST | none | JSON object with all fields required for Ventana DM Schemas | A response message that is either an Error or a Success |
| /wink_devices | GET | none | none | JSON Object with an array of objects with the Wink Schema information for each Wink Device on the user's account; On Error responds with Error message |
| /devices | GET | none | none | JSON Object with two arrays, one of paired device objects and one of unpaired device objects|
| /status/:vumark_id | GET | vumark_id | none | JSON object of the status of the requested Wink device; On error an error message will respond |
| /change_power/:vumark_id | POST | vumark_id | The outlet number is passed in the body of the request | JSON message on whether the power change state was successful or if an error occurred |
| /change_brightness/:vumark_id | POST | vumark_id | none | JSON message that is either a successful change in brightness or an error |

The endpoints above should all be referenced by the url of BASE_SERVER + '/wink' before the endpoint. Paired devices are defined as devices that have been added to the Vumark database and can be controlled with Ventana. Unpaired devices are Wink devices that are on the user's Wink account but have not been added to the Ventana application.

##### Light Bulbs
The GE Link Light Bulbs are the light bulb device that Ventana currently supports, but theoretically any light bulb supported by Wink should be supported as well. Wink splits devices into categories and every device in the light bulb category has the 'power' field in the object. This is the field that the change_power endpoint interacts with. The server receives a toggle request of a specific vumark_id from the Ventana application, it queries for the Wink device associated with that vumark_id, requests the current power state, and then toggles that state. The [Wink Documentation](http://docs.wink.apiary.io/#) details the specifications for all the devices and the fields with states that can be updated. Light bulbs that are dimmable can have brightness adjustments, by hitting the change_brightness endpoint in the Wink module. The Ventana application sends a vumark_id and a value to adjust the brightness by, the server finds that device, gets its current brightness level, and sends an updated brightness level that has been adjusted by the value sent by the Ventana application. Any light bulb with a brightness field, documented in the Wink Documentation, will support this brightness adjusting feature. 

##### Powerstrips
The Pivot Power Genius Powerstrip by GE and Quirky is the powerstrip device that Ventana currently supports. Due to Wink's device structure, any powerstrip with two smart outlets will be supported by Ventana. The powerstrip has four outlets, but only the first two can be power controlled by Wink. When the Ventana application hits the change_power endpoint for the powerstrip, it sends the outlet number which allows the server to toggle the power of the correct output in the same manner the power is toggled for the Wink light bulbs. 

### Adding a New Vendor to the HoloHub

Assuming the controller for Ventana has been created (See [*Making and Inserting New Controllers for Ventana*](#making-and-inserting-new-controllers-for-ventana)), one will also need to create the cooresponding module to support the commands on the HoloHub. Adding a new module is simple and just requires a HTTP REST-compatible vendor API, a new route file, as well as any nessesary models needed to store the Vendor device on the HoloHub.

#### Defining the Model

This is a standard template for what a new vendor schema looks like. There might be necessary additions to the attributes based on Vendor requirments to associate the device, but at a minimum the attributes in the object below are nessesary for proper function of the objects paired to the HoloHub.

```JS
var mongoose     = require('mongoose');
var Schema       = mongoose.Schema;

var VendorSchema   = new Schema({
    _id: String,			
    device_id: String,      
    device_type: String,    
    device_name: String,
    controller: String,     
    vendor_logo: String,
    vendor: String
});

module.exports = mongoose.model('VendorDM', VendorSchema);
```
#### Defining the Routes

Routes allow the HoloHub to respond to requests from the HoloLens and translate it to the vendor API server. The structure can vary for the request structure, but essentially all follow this convention:
1. Function to requests from HoloLens
2. Transform the request data from HoloHub IDs to Vendor IDs
3. Submit request to Vendor API to trigger desired action.
4. Process response, and response to initial HoloLens request.

First create a new file in the /routes/ folder in the HoloHub root directory. Convention states to name the file `[Vendor].js`. 

Then utilize this template to get started. The required endpoints are GET *vendor/*, and POST and *vendor/*

Depending on the vendor API type, certain structures will have to change. In addition to this example, it is recommened to follow the convention in the other routes files *sonos.js* and *wink.js*.

```JS
var express = require('express');
var router = express.Router();
var request = require('request');

var VendorDM = require('../app/models/VENDORMODEL');


var VENDOR_API_URL = "ENTER VENDOR API BASE URL HERE"


// Convert vendor response into Vendor HoloHub Object friendly response
function responseSummary(body, callback){
  
  // Convert JSON response to VendorDM object

  return callback(sonosSendData);
}

//Convert a Vumark ID to a Vendor Device ID (the group/device name)
function getDeviceIDbyVumarkID(vumark_id, callback){
  
  //Get a vendor object. If not found return null, otherwise return name
  VendorDM.findById(vumark_id, function(err, sonos){
      if (err){
        console.log(err);
        return callback(null);
      }
      if (sonos){
        return callback(sonos.device_id);
      }
      else{
        return callback(null);
      }
    });
};

//Convert a Device ID (the group/device name) to a Vumark ID
function getVumakIDbyDeviceID(device_id, callback){
  // Get Vendor object by device_id, if found return id (VuMark) otherwise null
  VendorDM.findOne({"device_id": device_id}, function(err, sonos){
    if (err){
      console.log(err);
      return callback(null);
    }
    if (sonos){
      return callback(sonos._id);
    }
    else{
      return callback(null);
    }
  });
};

// Get all objects and Create a new Sonos Music Object
router.route('/')

  .get(function(req, res) {   //GET all paired sonos device.

    VendorDM.find(function(err, sonos) {
              if (err)
                  res.send(err);
              
              res.json(sonos);
          });
  })

  .post(function(req, res){ 
    
    /* 
      Process new object POST request. This
      will include the device_id (VuMark ID) and the
      device_name
    */
    
    var vendor = new VendorDm();  // Create new instance of a sonos object

    if("_id" in req.body)
      vendor._id = req.body._id
    if("device_id" in req.body) {
      vendor.device_id = req.body.device_id;
      vendor.device_name = req.body.device_id;
    }
    if("vendor_logo" in req.body)
      vendor.vendor_logo = req.body.vendor_logo
    
    //if("controller" in req.body)
    vendor.controller = "Ventana/Prefabs/[Name of new controller]";
    vendor.device_type = req.body.device_type;

    // Verify object exists against Vendor API
    request('VENDOR API ENDPOINT', function (error, response, body) {
      if (!error && response.statusCode == 200 && response.body != 'Not Connected or Invalid') {
          vendor.save(function(err) {
              if (err){
                res.send(err);
              }
              else{
                res.json({ message: 'VendorDM object created!' });
              };
          });     
      }
      else {
            res.send(statusCode=500, "Not valid or Connected");
      };
    });
  });
```

# Cost Breakdown

#### Project Costs for a Beta Ventana User
| Item | Quantity | Description | Unit Cost |
|:-:|:----:| :------------ |:----|
| 1 | 1 |  Microsoft HoloLens | 3000 |
| 2 | 1 | Raspberry Pi 3  | 40 |
| 3 | 1 | Router | 60 |
| 4 | 1 | Sonos Play 1: Compact Smart Speaker | 169 |
| 5| 1 | Wink Hub | 50 |
| 6| 1 | GE Link Light Bulb | 20 |
| 7 | 1 | Pivot Power Genius Power Strip | 26 |
|  | | _Total Cost:_ |_3365_ |

The cost breakdown assumes the user purchases all of the devices currently supported by Ventana. However, if a user only has GE light bulbs supported by the Wink Hub, and does not wish to purchase a Sonos speaker, the user can still use the application. Conversely, Ventana supports multiples of the same device, so if a user would like to control more than one Sonos speaker, the user can purchase more than one. These variations would make the cost breakdown fluctuate, depending on the user. The HoloLens still represents the major component of the budget.  Since makers represent Ventana's targeted user  base, it is possible that the user already has a HoloLens development kit, as well as some of the necesary set-up equipment, such as a router and Raspberry Pi 3. Alternatively, without a HoloLens, the user can utilize Unity's scene preview, and the HoloLens emulator to view some of Ventana's functionality. As makers expand upon the project, the devices supported by Ventana will increase, which represents more additional items to be added to the cost breakdown.

# Appendices
## The Ventana Team
![image alt](https://github.com/VentanaIoT/Ventana/blob/master/images/IMG_0048.JPG?raw=true![](https://i.imgur.com/Kv0tahl.JPG) "Ventana Team Picture")

From left to right: EJ, Tess, Allison, Johan, and Santiago on the day of functional testing

_EJ_
EJ is studying Computer Engineering at Boston University. He's interested in new technology and the business models that arise from innovation. He is passionate about up-and-coming products, and always keep updated on the newest technologies. As a a car enthusiast, He enjoys spending a lot of my free time looking at and driving different cars.

_Tess Gauthier_
Tess is originally from western Massachusetts, and is majoring in electrical engineering, with a minor in computer engineering. She is Conference Chair of the Society of Women Engineers at Boston University. After graduation, she will be moving to Wilmington, North Carolina, and joining GE Power as a member of their Edison Engineering Development Program.  

_Allison_
While majoring in computer engineering, Allison is a Captain and the President of the BU Women's Water Polo team as well as a student advisor in the College of Engineering. After gradutation, Allison is joining Microsoft's Cloud and Enterprise group as a Software Engineer on the Intune team in Cambridge, MA.

_Johan_
Johan is a student advisor in the College of Engineering, where he's worked hard to bring various projects to completion both in academia and industry. His long term plans will lead him to study more about Human Computer Interaction. After graduation, Johan will be joining the Wayfair Next team where he will continue to pursue this type of HCI work in industry. 

_Santiago_ 
Following my passion for technology and innovation, I have worked to take advantage of every opportunity, and maximize the impact of turning ideas into actions in both nonprofit and for-profit ventures. Santiago will be joining Accenture Security team as a Security Consulting Senior Analyst in Boston, MA.


## Appendix 

# Appendix 

## Ventana Reference Assets

The HandDraggable.cs script, shown below, allows the user to move the hologram to a new location. 
#### HandDraggable.cs

```C#
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using System;

namespace HoloToolkit.Unity.InputModule
{
    /// <summary>
    /// Component that allows dragging an object with your hand on HoloLens.
    /// Dragging is done by calculating the angular delta and z-delta between the current and previous hand positions,
    /// and then repositioning the object based on that.
    /// </summary>
    public class HandDraggable : MonoBehaviour,
                                 IFocusable,
                                 IInputHandler,
                                 ISourceStateHandler
    {
        /// <summary>
        /// Event triggered when dragging starts.
        /// </summary>
        public event Action StartedDragging;

        /// <summary>
        /// Event triggered when dragging stops.
        /// </summary>
        public event Action StoppedDragging;

        [Tooltip("Transform that will be dragged. Defaults to the object of the component.")]
        public Transform HostTransform;

        [Tooltip("Scale by which hand movement in z is multipled to move the dragged object.")]
        public float DistanceScale = 2f;
        
        public enum RotationModeEnum
        {
            Default,
            LockObjectRotation,
            OrientTowardUser,
            OrientTowardUserAndKeepUpright
        }

        public RotationModeEnum RotationMode = RotationModeEnum.Default;

        public bool IsDraggingEnabled = true;

        private Camera mainCamera;
        protected bool isDragging;
        private bool isGazed;
        private Vector3 objRefForward;
        private Vector3 objRefUp;
        private float objRefDistance;
        private Quaternion gazeAngularOffset;
        private float handRefDistance;
        private Vector3 objRefGrabPoint;

        private Vector3 draggingPosition;
        private Quaternion draggingRotation;

        private IInputSource currentInputSource = null;
        private uint currentInputSourceId;

        public virtual void Start()
        {
            if (HostTransform == null)
            {
                HostTransform = transform;
            }

            mainCamera = Camera.main;
        }

        private void OnDestroy()
        {
            if (isDragging)
            {
                StopDragging();
            }

            if (isGazed)
            {
                OnFocusExit();
            }
        }

        public virtual void Update()
        {
            if (IsDraggingEnabled && isDragging)
            {
                UpdateDragging();
            }
        }

        /// <summary>
        /// Starts dragging the object.
        /// </summary>
        public virtual void StartDragging()
        {
            if (!IsDraggingEnabled)
            {
                return;
            }

            if (isDragging)
            {
                return;
            }

            // Add self as a modal input handler, to get all inputs during the manipulation
            InputManager.Instance.PushModalInputHandler(gameObject);

            isDragging = true;
            //GazeCursor.Instance.SetState(GazeCursor.State.Move);
            //GazeCursor.Instance.SetTargetObject(HostTransform);

            Vector3 gazeHitPosition = GazeManager.Instance.HitInfo.point;
            Vector3 handPosition;
            currentInputSource.TryGetPosition(currentInputSourceId, out handPosition);

            Vector3 pivotPosition = GetHandPivotPosition();
            handRefDistance = Vector3.Magnitude(handPosition - pivotPosition);
            objRefDistance = Vector3.Magnitude(gazeHitPosition - pivotPosition);

            Vector3 objForward = HostTransform.forward;
            Vector3 objUp = HostTransform.up;

            // Store where the object was grabbed from
            objRefGrabPoint = mainCamera.transform.InverseTransformDirection(HostTransform.position - gazeHitPosition);

            Vector3 objDirection = Vector3.Normalize(gazeHitPosition - pivotPosition);
            Vector3 handDirection = Vector3.Normalize(handPosition - pivotPosition);

            objForward = mainCamera.transform.InverseTransformDirection(objForward);       // in camera space
            objUp = mainCamera.transform.InverseTransformDirection(objUp);       		   // in camera space
            objDirection = mainCamera.transform.InverseTransformDirection(objDirection);   // in camera space
            handDirection = mainCamera.transform.InverseTransformDirection(handDirection); // in camera space

            objRefForward = objForward;
            objRefUp = objUp;

            // Store the initial offset between the hand and the object, so that we can consider it when dragging
            gazeAngularOffset = Quaternion.FromToRotation(handDirection, objDirection);
            draggingPosition = gazeHitPosition;

            StartedDragging.RaiseEvent();
        }

        /// <summary>
        /// Gets the pivot position for the hand, which is approximated to the base of the neck.
        /// </summary>
        /// <returns>Pivot position for the hand.</returns>
        private Vector3 GetHandPivotPosition()
        {
            Vector3 pivot = Camera.main.transform.position + new Vector3(0, -0.2f, 0) - Camera.main.transform.forward * 0.2f; // a bit lower and behind
            return pivot;
        }

        /// <summary>
        /// Enables or disables dragging.
        /// </summary>
        /// <param name="isEnabled">Indicates whether dragging shoudl be enabled or disabled.</param>
        public void SetDragging(bool isEnabled)
        {
            if (IsDraggingEnabled == isEnabled)
            {
                return;
            }

            IsDraggingEnabled = isEnabled;

            if (isDragging)
            {
                StopDragging();
            }
        }

        /// <summary>
        /// Update the position of the object being dragged.
        /// </summary>
        private void UpdateDragging()
        {
            Vector3 newHandPosition;
            currentInputSource.TryGetPosition(currentInputSourceId, out newHandPosition);

            Vector3 pivotPosition = GetHandPivotPosition();

            Vector3 newHandDirection = Vector3.Normalize(newHandPosition - pivotPosition);

            newHandDirection = mainCamera.transform.InverseTransformDirection(newHandDirection); // in camera space
            Vector3 targetDirection = Vector3.Normalize(gazeAngularOffset * newHandDirection);
            targetDirection = mainCamera.transform.TransformDirection(targetDirection); // back to world space

            float currenthandDistance = Vector3.Magnitude(newHandPosition - pivotPosition);

            float distanceRatio = currenthandDistance / handRefDistance;
            float distanceOffset = distanceRatio > 0 ? (distanceRatio - 1f) * DistanceScale : 0;
            float targetDistance = objRefDistance + distanceOffset;

            draggingPosition = pivotPosition + (targetDirection * targetDistance);

            if (RotationMode == RotationModeEnum.OrientTowardUser || RotationMode == RotationModeEnum.OrientTowardUserAndKeepUpright) 
            {
                draggingRotation = Quaternion.LookRotation(HostTransform.position - pivotPosition);
            }
            else if (RotationMode == RotationModeEnum.LockObjectRotation)
            {
                draggingRotation = HostTransform.rotation;
            }
            else // RotationModeEnum.Default
            {
                Vector3 objForward = mainCamera.transform.TransformDirection(objRefForward); // in world space
                Vector3 objUp = mainCamera.transform.TransformDirection(objRefUp);   // in world space
                draggingRotation = Quaternion.LookRotation(objForward, objUp);
            }

            // Apply Final Position
            HostTransform.position = draggingPosition + mainCamera.transform.TransformDirection(objRefGrabPoint);
            // Apply Final Rotation
            HostTransform.rotation = draggingRotation;
            if (RotationMode == RotationModeEnum.OrientTowardUserAndKeepUpright)		
            {		
                Quaternion upRotation = Quaternion.FromToRotation(HostTransform.up, Vector3.up);		
                HostTransform.rotation = upRotation * HostTransform.rotation;		
            }
        }

        /// <summary>
        /// Stops dragging the object.
        /// </summary>
        public virtual void StopDragging()
        {
            if (!isDragging)
            {
                return;
            }

            // Remove self as a modal input handler
            InputManager.Instance.PopModalInputHandler();

            isDragging = false;
            currentInputSource = null;
            StoppedDragging.RaiseEvent();
        }

        public void OnFocusEnter()
        {
            if (!IsDraggingEnabled)
            {
                return;
            }

            if (isGazed)
            {
                return;
            }

            isGazed = true;
        }

        public void OnFocusExit()
        {
            if (!IsDraggingEnabled)
            {
                return;
            }

            if (!isGazed)
            {
                return;
            }

            isGazed = false;
        }

        public void OnInputUp(InputEventData eventData)
        {
            if (currentInputSource != null &&
                eventData.SourceId == currentInputSourceId)
            {
                StopDragging();
            }
        }

        public void OnInputDown(InputEventData eventData)
        {
            if (isDragging)
            {
                // We're already handling drag input, so we can't start a new drag operation.
                return;
            }

            if (!eventData.InputSource.SupportsInputInfo(eventData.SourceId, SupportedInputInfo.Position))
            {
                // The input source must provide positional data for this script to be usable
                return;
            }

            currentInputSource = eventData.InputSource;
            currentInputSourceId = eventData.SourceId;
            StartDragging();
        }

        public void OnSourceDetected(SourceStateEventData eventData)
        {
            // Nothing to do
        }

        public void OnSourceLost(SourceStateEventData eventData)
        {
            if (currentInputSource != null && eventData.SourceId == currentInputSourceId)
            {
                StopDragging();
            }
        }
    }
}
```
### HandResize.cs
```C#
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class HandResize : MonoBehaviour, IManipulationHandler
{
    [Tooltip("Speed at which the object is resized.")]
    [SerializeField]
    float ResizeSpeedFactor = 1.5f;

    [SerializeField]
    float ResizeScaleFactor = 1.5f;

    [Tooltip("When warp is checked, we allow resizing of all three scale axes - otherwise we scale each axis by the same amount.")]
    [SerializeField]
    bool AllowResizeWarp = false;

    [Tooltip("Minimum resize scale allowed.")]
    [SerializeField]
    float MinScale = 0.5f;

    [Tooltip("Maximum resize scale allowed.")]
    [SerializeField]
    float MaxScale = 4f;

    [SerializeField]
    bool resizingEnabled = true;

    Vector3 lastScale;

    public void SetResizing(bool enabled)
    {
        resizingEnabled = enabled;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        InputManager.Instance.PushModalInputHandler(gameObject);
        lastScale = transform.localScale;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (resizingEnabled)
        {
            Resize(eventData.CumulativeDelta);

            //sharing & messaging
            //SharingMessages.Instance.SendResizing(Id, eventData.CumulativeDelta);
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }
    void Resize(Vector3 newScale)
    {
        float resizeX, resizeY, resizeZ;
        //if we are warping, honor axis delta, else take the x
        if (AllowResizeWarp)
        {
            resizeX = newScale.x * ResizeScaleFactor;
            resizeY = newScale.y * ResizeScaleFactor;
            resizeZ = newScale.z * ResizeScaleFactor;
        }
        else
        {
            resizeX = resizeY = resizeZ = newScale.x * ResizeScaleFactor;
        }

        resizeX = Mathf.Clamp(lastScale.x + resizeX, MinScale, MaxScale);
        resizeY = Mathf.Clamp(lastScale.y + resizeY, MinScale, MaxScale);
        resizeZ = Mathf.Clamp(lastScale.z + resizeZ, MinScale, MaxScale);

        transform.localScale = Vector3.Lerp(transform.localScale,
            new Vector3(resizeX, resizeY, resizeZ),
            ResizeSpeedFactor);
    }
}
```
### VentanaExtensions.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VentanaExtensions {
    public static void DestroyChildren(this Transform root) {
        int childCount = root.childCount;
        for ( int i = 0; i < childCount; i++ ) {
            GameObject.Destroy(root.GetChild(0).gameObject);
        }
    }
}
```

### BaseVentanaController.cs
```C# 
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVentanaButton : MonoBehaviour, IInputClickHandler, IFocusable {
    public AudioClip clickSound;
    protected AudioSource source;
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;

    public void OnFocusEnter() {
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }

    public void OnInputClicked(InputClickedEventData eventData) {
        Debug.Log("Clicked " + gameObject.name);
        gameObject.SendMessageUpwards("makeAPIRequest", gameObject.name);
        source.PlayOneShot(clickSound, 1F);
    }

    public void DisableInteraction(bool yes) {
        if (yes) {
            gameObject.GetComponent<Collider>().enabled = false;
        } else {
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }

    // Use this for initialization
    void Start() {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }
}
```
### PowerStripButtonHandler.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class PowerStripButtonHandler : BaseVentanaButton {
    
}
```

### PowerStripController.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentanaPowerStripController : BaseVentanaController {

    private string poweredCommand = "change_power";
    private string statusCommand = "status";
    // Use this for initialization
    protected new void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected new void Update() {
        base.Update();
    }

    public override void OnVumarkFound() {
        base.OnVumarkFound();
    }

    public override void OnVumarkLost() {
        base.OnVumarkLost();
    }
    void makeAPIRequest(string child) {
        VentanaRequestFactory requestFactory = VentanaRequestFactory.Instance;
        switch ( child ) {
            case "Toggle0":
            Debug.Log("Toggled 0");
            StartCoroutine(requestFactory.PostToLightAPIEndpoint(poweredCommand, VentanaID, "0"));
            break;
            case "Toggle1":
            Debug.Log("Toggled 1");
            StartCoroutine(requestFactory.PostToLightAPIEndpoint(poweredCommand, VentanaID, "1"));
            break;
            default:
            break;
        }
    }
}
```

### ddScript.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class ddScript : MonoBehaviour, IInputClickHandler, IFocusable {
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;

    // Use this for initialization
    void Start () {
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("button pressed");
        gameObject.SendMessageUpwards("ddButtonClicked", gameObject.name);
    }

    public void OnFocusEnter() {
        gameObject.SendMessageUpwards("DisableHandDraggable");
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.SendMessageUpwards("EnableHandDraggable");
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }
}
```

### EditModeController.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using UnityEngine.VR.WSA;
using HoloToolkit.Unity;
using System;

[ExecuteInEditMode]
public class EditModeController : MonoBehaviour {

    public Transform moreButtons;
    public Transform deleteDone;
    public Transform scaleHandles;
    public bool scaleEnabled = false;
    public bool scaleModeTriggered = false;
    public AudioClip clickSound;
    public Vector3 lastScale;
    private HandDraggable handDraggable;
    private AudioSource source;


    [Tooltip("Speed at which the object is resized.")]
    [SerializeField]
    float ResizeSpeedFactor = 1.0f;

    [SerializeField]
    float ResizeScaleFactor = 0.75f;

    [Tooltip("When warp is checked, we allow resizing of all three scale axes - otherwise we scale each axis by the same amount.")]
    [SerializeField]
    bool AllowResizeWarp = false;

    [Tooltip("Minimum resize scale allowed.")]
    [SerializeField]
    float MinScale = 0.05f;

    [Tooltip("Maximum resize scale allowed.")]
    [SerializeField]
    float MaxScale = 0.7f;

    // Use this for initialization
    void Start () {
        source = gameObject.GetComponent<AudioSource>();
        // initialize to regular mode, with tap to place controls inactive
        moreButtons.gameObject.SetActive(true);
        deleteDone.gameObject.SetActive(false);
        scaleHandles.gameObject.SetActive(false);

       
        handDraggable = gameObject.GetComponent<HandDraggable>();

        if (handDraggable != null ) {
            handDraggable.StoppedDragging += HandDraggable_StoppedDragging;
            handDraggable.StartedDragging += HandDraggable_StartedDragging;
        }

    }

    private void HandDraggable_StartedDragging() {
        SpatialMappingManager.Instance.DrawVisualMeshes = true;
        Debug.Log(gameObject.name + " : Removing existing world anchor if any.");
        WorldAnchorManager.Instance.RemoveAnchor(gameObject);
    }

    private void HandDraggable_StoppedDragging() {
        SpatialMappingManager.Instance.DrawVisualMeshes = false;

        // Add world anchor when object placement is done.
        BaseVentanaController bvc = gameObject.GetComponent<BaseVentanaController>();
        if ( bvc ) {
            string currentTime = DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString();
            string savedAnchorName = bvc.VentanaID + ":" + gameObject.transform.lossyScale.x.ToString()+ ":" + currentTime;
            Debug.Log("<color=yellow>Name: </color>" + savedAnchorName);

            WorldAnchorManager.Instance.AttachAnchor(gameObject, savedAnchorName);
        }
        source.PlayOneShot(clickSound, 1F);
    }



    // Update is called once per frame
    void Update () {
		if ( scaleModeTriggered ) {
            moreButtons.gameObject.SetActive(false);
            deleteDone.gameObject.SetActive(true);
            scaleHandles.gameObject.SetActive(true);
            gameObject.BroadcastMessage("DisableInteraction", true);
        } else {
            moreButtons.gameObject.SetActive(true);
            deleteDone.gameObject.SetActive(false);
            scaleHandles.gameObject.SetActive(false);
            gameObject.BroadcastMessage("DisableInteraction", false);
        }
	}

    void MoreButtonClicked()
    {
        //Debug.Log("More button clicked");
        // More button clicked so tap to place mode should be active
        moreButtons.gameObject.SetActive(false);
        deleteDone.gameObject.SetActive(true);
        scaleHandles.gameObject.SetActive(true);
        //Enable Tap To Place and Hand Dragable here.
        
        if ( handDraggable != null ) {
            handDraggable.enabled = true;
        }

        scaleModeTriggered = true;
        source.PlayOneShot(clickSound, 1F);
    }

    void ddButtonClicked(string child)
    {
        if (child.Equals("Done Button"))
        {
           // Debug.Log("Done button clicked");
           // Done button clicked so regular mode should be active
            moreButtons.gameObject.SetActive(true);
            deleteDone.gameObject.SetActive(false);
            scaleHandles.gameObject.SetActive(false);
            scaleModeTriggered = false;
            
            if ( handDraggable != null ) {
                handDraggable.enabled = false;
            }

        }

        if (child.Equals("Delete Button"))
        {
            //Debug.Log("Delete button clicked");
            // gonna have to remove world anchor here
            if (handDraggable != null)
            {
                // had to make anchorManager public instead of protected in ttp
                WorldAnchor wa = gameObject.GetComponent<WorldAnchor>();
                if ( wa ) {
                    WorldAnchorManager.Instance.AnchorStore.Delete(wa.name);
                }
               
            }
            scaleModeTriggered = false;
            Destroy(gameObject);
        }

        source.PlayOneShot(clickSound, 1F);
    }

    void scaleStarted()
    {
        // manipulation gesture started so get the current scale
        //turn off draggable behaviours
        
    }

    void scaleEnded() {
        BaseVentanaController bvc = gameObject.GetComponent<BaseVentanaController>();
        if ( bvc ) {
            Debug.Log(gameObject.name + " : Removing existing world anchor if any after scaling");
            WorldAnchorManager.Instance.RemoveAnchor(gameObject);
            string currentTime = DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString();
            string savedAnchorName = bvc.VentanaID + ":" + gameObject.transform.lossyScale.x.ToString() + ":" + currentTime;
            Debug.Log("<color=yellow>Name: </color>" + savedAnchorName);

            WorldAnchorManager.Instance.AttachAnchor(gameObject, savedAnchorName);
        }
    }

    void EnableHandDraggable() {
        if ( handDraggable ) { handDraggable.enabled = true; }
    }

    void DisableHandDraggable() {
        if ( handDraggable ) { handDraggable.enabled = false; }
    }
    
    void scaleButtonClicked(Vector3 newScale)
    {
        // manipulation gesture ended, calculate and set the new scale 
        /* https://www.billmccrary.com/holotoolkit-simple-dragresizerotate/ modified from HandResize.cs */

        float resizeX, resizeY, resizeZ;
        //if we are warping, honor axis delta, else take the x
        if (AllowResizeWarp)
        {
            resizeX = newScale.x * ResizeScaleFactor;
            resizeY = newScale.y * ResizeScaleFactor;
            resizeZ = newScale.z * ResizeScaleFactor;
        }
        else
        {
            resizeX = resizeY = resizeZ = newScale.x * ResizeScaleFactor;
        }

        resizeX = Mathf.Clamp(lastScale.x + resizeX, MinScale, MaxScale);
        resizeY = Mathf.Clamp(lastScale.y + resizeY, MinScale, MaxScale);
        resizeZ = Mathf.Clamp(lastScale.z + resizeZ, MinScale, MaxScale);

        Vector3 newTransform = new Vector3(resizeX, resizeY, resizeZ);

        Transform currentTransform = gameObject.GetComponent<Transform>();
        //Debug.Log("before scale" + currentTransform);
        currentTransform.localScale = Vector3.Lerp(transform.localScale, newTransform, ResizeSpeedFactor);
        //Debug.Log("scale:" + currentTransform.localScale);
    }
}

```

### scalingHandler.cs
```C#
using System;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class scalingHandler : MonoBehaviour, IManipulationHandler, IFocusable
{
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;
    public Transform parentObject;
    [Tooltip("Speed at which the object is resized.")]
    [SerializeField]
    float ResizeSpeedFactor = 1.0f;

    [SerializeField]
    float ResizeScaleFactor = 0.75f;

    [Tooltip("When warp is checked, we allow resizing of all three scale axes - otherwise we scale each axis by the same amount.")]
    [SerializeField]
    bool AllowResizeWarp = false;

    [Tooltip("Minimum resize scale allowed.")]
    [SerializeField]
    float MinScale = 0.0f;

    [Tooltip("Maximum resize scale allowed.")]
    [SerializeField]
    float MaxScale = 0.7f;

    private Vector3 lastScale;

    private Vector3 lastManipulationPosition;

    private bool shouldRespond = false;


    [SerializeField]
    bool resizingEnabled = true;
    void Start()
    {
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    public void SetResizing(bool enabled)
    {
        resizingEnabled = enabled;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        gameObject.SendMessageUpwards("scaleStarted");
        lastScale = parentObject.localScale;
        InputManager.Instance.PushModalInputHandler(gameObject);
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if ( resizingEnabled ) {
            Resize(eventData.CumulativeDelta);
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        gameObject.SendMessageUpwards("scaleEnded");
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
    }
    void Resize(Vector3 newScale)
    {
        
        Vector3 camHandDelta = Camera.main.transform.InverseTransformDirection(newScale);
        // send data to EditModeController.cs once the manipulation gesture is updated
        //gameObject.SendMessageUpwards("scaleButtonClicked", newScale);
        float resizeX, resizeY, resizeZ;
        //if we are warping, honor axis delta, else take the x

        resizeX = resizeY = resizeZ = camHandDelta.x * ResizeScaleFactor;
        resizeX = Mathf.Clamp(lastScale.x + resizeX, MinScale, MaxScale);
        resizeY = Mathf.Clamp(lastScale.y + resizeY, MinScale, MaxScale);
        resizeZ = Mathf.Clamp(lastScale.z + resizeZ, MinScale, MaxScale);
        parentObject.localScale = Vector3.Lerp(parentObject.localScale,
            new Vector3(resizeX, resizeY, resizeZ),
            ResizeSpeedFactor);

    }

    public void OnFocusEnter() {
        gameObject.SendMessageUpwards("DisableHandDraggable");
        shouldRespond = true;
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.SendMessageUpwards("EnableHandDraggable");
        shouldRespond = false;
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }
}
```

### SpawnBehaviourScript.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using HoloToolkit.Unity.SpatialMapping;

public class SpawnBehaviourScript : MonoBehaviour, IHoldHandler {
    #region PUBLIC_MEMBERS
    public GameObject prefabObject;
    public bool shouldSpawn = false;
    public Vector3 scaleMultiplier;
    public Vector3 placementPosition;
    public int ControllerID;
    public string prefabName;

    #endregion //PUBLIC_MEMBERS

    #region PRIVATE_MEMBERS

    private const float DOUBLE_TAP_MAX_DELAY = 0.5f;
    //seconds
    private float mTimeSinceLastTap = 0;

    #endregion //PRIVATE_MEMBERS


    #region PROTECTED_MEMBERS

    protected int mTapCount = 0;
    protected int spawnCount = 0;

    #endregion //PROTECTED_MEMBERS


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTapCount = 0;
        mTimeSinceLastTap = 0;
    }

    void Update()
    {
        //Debug.Log("sb update reached");
    }

    #endregion //MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS

    private void HandleTap()
    {
        if (mTapCount == 1)
        {
            mTimeSinceLastTap += Time.deltaTime;
            if (mTimeSinceLastTap > DOUBLE_TAP_MAX_DELAY)
            {
                // too late for double tap, 
                // we confirm it was a single tap
                OnSingleTapConfirmed();

                // reset touch count and timer
                mTapCount = 0;
                mTimeSinceLastTap = 0;
            }
        }
        else if (mTapCount == 2)
        {
            // we got a double tap
            OnDoubleTap();

            // reset touch count and timer
            mTimeSinceLastTap = 0;
            mTapCount = 0;
        }

        
    }

    #endregion // PRIVATE_METHODS


    #region PROTECTED_METHODS

    /// <summary>
    /// This method can be overridden by custom (derived) TapHandler implementations,
    /// to perform special actions upon single tap.
    /// </summary>
    protected virtual void OnSingleTap()
    {
        Debug.Log("sb OST reached");
    }

    protected virtual void OnSingleTapConfirmed()
    {
        Debug.Log("sb OSTC reached");
    }

    protected virtual void OnDoubleTap()
    {
        Debug.Log("sb ODT reached");
        if ( shouldSpawn )
        {
            //Copying Controller...
            GameObject prefabObjectClone = GameObject.Instantiate(gameObject);
            Vector3 cam = Camera.main.transform.forward.normalized;
            Vector3 current = gameObject.transform.position;
            prefabObjectClone.transform.position = new Vector3(current.x + (cam.x * .05f), current.y + (cam.y * .05f), current.z + (cam.z * .05f));
            Vector3 globalScale = gameObject.transform.lossyScale;
            prefabObjectClone.transform.localScale = new Vector3(globalScale.x * 1.35f, globalScale.y * 1.35f, globalScale.z * 1.35f);
            prefabObjectClone.transform.rotation = gameObject.transform.rotation;
            EditModeController edit = prefabObjectClone.GetComponent<EditModeController>();
            edit.scaleModeTriggered = true;

            Destroy(prefabObjectClone.GetComponent<SpawnBehaviourScript>());

            HandDraggable hd = prefabObjectClone.AddComponent<HandDraggable>();
            hd.enabled = true;
            hd.RotationMode = HandDraggable.RotationModeEnum.OrientTowardUserAndKeepUpright;
            hd.IsDraggingEnabled = true;

        }
    }

    /*
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("<color=yellow>EY BAY BAY</color>");
        mTapCount++;
        HandleTap();


    }
    */
    // replacing double tap to spawn with click and hold
    public void OnHoldStarted(HoldEventData eventData)
    {

    }

    public void OnHoldCompleted(HoldEventData eventData)
    {
        OnDoubleTap();
    }

    public void OnHoldCanceled(HoldEventData eventData)
    {

    }
    #endregion // PROTECTED_METHODS
}
```

### TapToPlace.cs
```C#
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HoloToolkit.Unity.SpatialMapping
{
    /// <summary>
    /// The TapToPlace class is a basic way to enable users to move objects 
    /// and place them on real world surfaces.
    /// Put this script on the object you want to be able to move. 
    /// Users will be able to tap objects, gaze elsewhere, and perform the
    /// tap gesture again to place.
    /// This script is used in conjunction with GazeManager, GestureManager,
    /// and SpatialMappingManager.
    /// TapToPlace also adds a WorldAnchor component to enable persistence.
    /// </summary>

    public class TapToPlace : MonoBehaviour, IInputClickHandler
    {
        [Tooltip("Supply a friendly name for the anchor as the key name for the WorldAnchorStore.")]
        public string SavedAnchorFriendlyName = "SavedAnchorFriendlyName";

        [Tooltip("Place parent on tap instead of current game object.")]
        public bool PlaceParentOnTap;

        [Tooltip("Specify the parent game object to be moved on tap, if the immediate parent is not desired.")]
        public GameObject ParentGameObjectToPlace;

        /// <summary>
        /// Keeps track of if the user is moving the object or not.
        /// Setting this to true will enable the user to move and place the object in the scene.
        /// Useful when you want to place an object immediately.
        /// </summary>
        [Tooltip("Setting this to true will enable the user to move and place the object in the scene without needing to tap on the object. Useful when you want to place an object immediately.")]
        public bool IsBeingPlaced;

        /// <summary>
        /// Manages persisted anchors.
        /// </summary>
        protected WorldAnchorManager anchorManager;

        /// <summary>
        /// Controls spatial mapping.  In this script we access spatialMappingManager
        /// to control rendering and to access the physics layer mask.
        /// </summary>
        protected SpatialMappingManager spatialMappingManager;

        protected virtual void Start()
        {
            // Make sure we have all the components in the scene we need.
            anchorManager = WorldAnchorManager.Instance;
            if (anchorManager == null)
            {
                Debug.LogError("This script expects that you have a WorldAnchorManager component in your scene.");
            }

            spatialMappingManager = SpatialMappingManager.Instance;
            if (spatialMappingManager == null)
            {
                Debug.LogError("This script expects that you have a SpatialMappingManager component in your scene.");
            }

            if (anchorManager != null && spatialMappingManager != null)
            {
                anchorManager.AttachAnchor(gameObject, SavedAnchorFriendlyName);
            }
            else
            {
                // If we don't have what we need to proceed, we may as well remove ourselves.
                Destroy(this);
            }

            if (PlaceParentOnTap)
            {
                if (ParentGameObjectToPlace != null && !gameObject.transform.IsChildOf(ParentGameObjectToPlace.transform))
                {
                    Debug.LogError("The specified parent object is not a parent of this object.");
                }

                DetermineParent();
            }
        }

        protected virtual void Update()
        {
            // If the user is in placing mode,
            // update the placement to match the user's gaze.
            if (IsBeingPlaced)
            {
                // Do a raycast into the world that will only hit the Spatial Mapping mesh.
                Vector3 headPosition = Camera.main.transform.position;
                Vector3 gazeDirection = Camera.main.transform.forward;

                RaycastHit hitInfo;
                if (Physics.Raycast(headPosition, gazeDirection, out hitInfo, 30.0f, spatialMappingManager.LayerMask))
                {
                    // Rotate this object to face the user.
                    Quaternion toQuat = Camera.main.transform.localRotation;
                    toQuat.x = 0;
                    toQuat.z = 0;

                    // Move this object to where the raycast
                    // hit the Spatial Mapping mesh.
                    // Here is where you might consider adding intelligence
                    // to how the object is placed.  For example, consider
                    // placing based on the bottom of the object's
                    // collider so it sits properly on surfaces.
                    if (PlaceParentOnTap)
                    {
                        // Place the parent object as well but keep the focus on the current game object
                        Vector3 currentMovement = hitInfo.point - gameObject.transform.position;
                        ParentGameObjectToPlace.transform.position += currentMovement;
                        ParentGameObjectToPlace.transform.rotation = toQuat;
                    }
                    else
                    {
                        gameObject.transform.position = hitInfo.point;
                        gameObject.transform.rotation = toQuat;
                    }
                }
            }
        }

        public virtual void OnInputClicked(InputClickedEventData eventData)
        {
            // On each tap gesture, toggle whether the user is in placing mode.
            IsBeingPlaced = !IsBeingPlaced;

            // If the user is in placing mode, display the spatial mapping mesh.
            if (IsBeingPlaced)
            {
                spatialMappingManager.DrawVisualMeshes = true;

                Debug.Log(gameObject.name + " : Removing existing world anchor if any.");

                anchorManager.RemoveAnchor(gameObject);
            }
            // If the user is not in placing mode, hide the spatial mapping mesh.
            else
            {
                spatialMappingManager.DrawVisualMeshes = false;
                // Add world anchor when object placement is done.
                anchorManager.AttachAnchor(gameObject, SavedAnchorFriendlyName);
            }
        }

        private void DetermineParent()
        {
            if (ParentGameObjectToPlace == null)
            {
                if (gameObject.transform.parent == null)
                {
                    Debug.LogError("The selected GameObject has no parent.");
                    PlaceParentOnTap = false;
                }
                else
                {
                    Debug.LogError("No parent specified. Using immediate parent instead: " + gameObject.transform.parent.gameObject.name);
                    ParentGameObjectToPlace = gameObject.transform.parent.gameObject;
                }
            }
        }
    }
}
```
### VentanaSpatialProcessor.cs
```C#
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

/// <summary>
/// The SurfaceManager class allows applications to scan the environment for a specified amount of time 
/// and then process the Spatial Mapping Mesh (find planes, remove vertices) after that time has expired.
/// </summary>
public class VentanaSpacialProcessor : Singleton<VentanaSpacialProcessor> {
    [Tooltip("When checked, the SurfaceObserver will stop running after a specified amount of time.")]
    public bool limitScanningByTime = true;

    [Tooltip("How much time (in seconds) that the SurfaceObserver will run after being started; used when 'Limit Scanning By Time' is checked.")]
    public float scanTime = 30.0f;

    [Tooltip("Material to use when rendering Spatial Mapping meshes while the observer is running.")]
    public Material defaultMaterial;

    [Tooltip("Optional Material to use when rendering Spatial Mapping meshes after the observer has been stopped.")]
    public Material secondaryMaterial;

    [Tooltip("Minimum number of floor planes required in order to exit scanning/processing mode.")]
    public uint minimumFloors = 1;

    [Tooltip("Minimum number of wall planes required in order to exit scanning/processing mode.")]
    public uint minimumWalls = 1;

    /// <summary>
    /// Indicates if processing of the surface meshes is complete.
    /// </summary>
    private bool meshesProcessed = false;

    /// <summary>
    /// GameObject initialization.
    /// </summary>
    private void Start() {
        // Update surfaceObserver and storedMeshes to use the same material during scanning.
        SpatialMappingManager.Instance.SetSurfaceMaterial(defaultMaterial);

        // Register for the MakePlanesComplete event.
        SurfaceMeshesToPlanes.Instance.MakePlanesComplete += SurfaceMeshesToPlanes_MakePlanesComplete;
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update() {
        // Check to see if the spatial mapping data has been processed
        // and if we are limiting how much time the user can spend scanning.
        if ( !meshesProcessed && limitScanningByTime ) {
            // If we have not processed the spatial mapping data
            // and scanning time is limited...

            // Check to see if enough scanning time has passed
            // since starting the observer.
            if ( limitScanningByTime && ((Time.time - SpatialMappingManager.Instance.StartTime) < scanTime) ) {
                // If we have a limited scanning time, then we should wait until
                // enough time has passed before processing the mesh.
            } else {
                // The user should be done scanning their environment,
                // so start processing the spatial mapping data...

                /* TODO: 3.a DEVELOPER CODING EXERCISE 3.a */

                // 3.a: Check if IsObserverRunning() is true on the
                // SpatialMappingManager.Instance.
                if ( SpatialMappingManager.Instance.IsObserverRunning() ) {
                    // 3.a: If running, Stop the observer by calling
                    // StopObserver() on the SpatialMappingManager.Instance.
                    SpatialMappingManager.Instance.StopObserver();
                }

                // 3.a: Call CreatePlanes() to generate planes.
                CreatePlanes();

                // 3.a: Set meshesProcessed to true.
                meshesProcessed = true;
            }
        }
    }

    /// <summary>
    /// Handler for the SurfaceMeshesToPlanes MakePlanesComplete event.
    /// </summary>
    /// <param name="source">Source of the event.</param>
    /// <param name="args">Args for the event.</param>
    private void SurfaceMeshesToPlanes_MakePlanesComplete(object source, System.EventArgs args) {

        // Collection of floor and table planes that we can use to set horizontal items on.
        List<GameObject> horizontal = new List<GameObject>();

        // Collection of wall planes that we can use to set vertical items on.
        List<GameObject> vertical = new List<GameObject>();

        // Get all floor and table planes by calling
        // SurfaceMeshesToPlanes.Instance.GetActivePlanes().
        // Assign the result to the 'horizontal' list.
        horizontal = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Table | PlaneTypes.Floor);

        // Get all wall planes by calling
        // SurfaceMeshesToPlanes.Instance.GetActivePlanes().
        // Assign the result to the 'vertical' list.
        vertical = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Wall);

        // Check to see if we have enough horizontal planes (minimumFloors)
        // and vertical planes (minimumWalls), to set holograms on in the world.
        if ( horizontal.Count >= minimumFloors && vertical.Count >= minimumWalls ) {
            // We have enough floors and walls to place our holograms on...

            // Let's reduce our triangle count by removing triangles
            // from SpatialMapping meshes that intersect with our active planes.
            // Call RemoveVertices().
            // Pass in all activePlanes found by SurfaceMeshesToPlanes.Instance.
            RemoveVertices(SurfaceMeshesToPlanes.Instance.ActivePlanes);

            // We can indicate to the user that scanning is over by
            // changing the material applied to the Spatial Mapping meshes.
            // Call SpatialMappingManager.Instance.SetSurfaceMaterial().
            // Pass in the secondaryMaterial.
            SpatialMappingManager.Instance.SetSurfaceMaterial(secondaryMaterial);

            // We are all done processing the mesh, so we can now
            // initialize a collection of Placeable holograms in the world

        } else {
            // We do not have enough floors/walls to place our holograms on...

            // 3.a: Re-enter scanning mode so the user can find more surfaces by 
            // calling StartObserver() on the SpatialMappingManager.Instance.
            SpatialMappingManager.Instance.StartObserver();

            // Re-process spatial data after scanning completes by
            // re-setting meshesProcessed to false.
            meshesProcessed = false;
        }
    }

    /// <summary>
    /// Creates planes from the spatial mapping surfaces.
    /// </summary>
    private void CreatePlanes() {
        // Generate planes based on the spatial map.
        SurfaceMeshesToPlanes surfaceToPlanes = SurfaceMeshesToPlanes.Instance;
        if ( surfaceToPlanes != null && surfaceToPlanes.enabled ) {
            surfaceToPlanes.MakePlanes();
        }
    }

    /// <summary>
    /// Removes triangles from the spatial mapping surfaces.
    /// </summary>
    /// <param name="boundingObjects"></param>
    private void RemoveVertices(IEnumerable<GameObject> boundingObjects) {
        RemoveSurfaceVertices removeVerts = RemoveSurfaceVertices.Instance;
        if ( removeVerts != null && removeVerts.enabled ) {
            removeVerts.RemoveSurfaceVerticesWithinBounds(boundingObjects);
        }
    }

    /// <summary>
    /// Called when the GameObject is unloaded.
    /// </summary>
    private void OnDestroy() {
        if ( SurfaceMeshesToPlanes.Instance != null ) {
            SurfaceMeshesToPlanes.Instance.MakePlanesComplete -= SurfaceMeshesToPlanes_MakePlanesComplete;
        }
    }
}
```

### DynamicDataSetLoader.cs
```C#
using UnityEngine;
using System.Collections;

using Vuforia;
using System.Collections.Generic;


public class DynamicDataSetLoader : MonoBehaviour {
    // specify these in Unity Inspector
    public GameObject augmentationObject = null;  // you can use teapot or other object
    public string dataSetName = "";  //  in the StreamingAssets folder ... StreamingAssets/QCAR/DataSetName

    // Use this for initialization
    void Start() {
        
        /*VuforiaARController vb = VuforiaARController.Instance;
        vb.RegisterVuforiaStartedCallback(LoadDataSet);
       */
    }

    void LoadDataSet() {

        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        DataSet dataSet = objectTracker.CreateDataSet();

        if ( dataSet.Load(dataSetName, VuforiaUnity.StorageType.STORAGE_APPRESOURCE) ) {

            objectTracker.Stop();  // stop tracker so that we can add new dataset

            if ( !objectTracker.ActivateDataSet(dataSet) ) {
                // Note: ImageTracker cannot have more than 100 total targets activated
                Debug.Log("<color=yellow>Failed to Activate DataSet: " + dataSetName + "</color>");
            }

            if ( !objectTracker.Start() ) {
                Debug.Log("<color=yellow>Tracker Failed to Start.</color>");
            }

            int counter = 0;

            IEnumerable<TrackableBehaviour> tbs = TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
            foreach ( TrackableBehaviour tb in tbs ) {
                if ( tb.name == "New Game Object" ) {

                    // change generic name to include trackable name
                    tb.gameObject.name = ++counter + ":DynamicImageTarget-" + tb.TrackableName;

                    // add additional script components for trackable
                    tb.gameObject.AddComponent<DefaultTrackableEventHandler>();
                    tb.gameObject.AddComponent<TurnOffBehaviour>();

                    if ( augmentationObject != null ) {
                        // instantiate augmentation object and parent to trackable
                        GameObject augmentation = (GameObject)GameObject.Instantiate(augmentationObject);
                        augmentation.transform.parent = tb.gameObject.transform;
                        augmentation.transform.localPosition = new Vector3(0f, 0f, 0f);
                        augmentation.transform.localRotation = Quaternion.identity;
                        augmentation.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                        augmentation.gameObject.SetActive(true);
                    } else {
                        Debug.Log("<color=yellow>Warning: No augmentation object specified for: " + tb.TrackableName + "</color>");
                    }
                }
            }
        } else {
            Debug.LogError("<color=yellow>Failed to load dataset: '" + dataSetName + "'</color>");
        }
    }
}
```

### KnobHandler.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

[ExecuteInEditMode]
public class KnobHandler : HandDraggable, IFocusable {
    //only allow y displacment up to a quarter of the rail's width in either direction... 
    //then just return to the position it was at. 
    [Tooltip("Where in the local Coordinate system this object will return to")]
    private Vector3 baseLocation; //location to return to.
    private Quaternion baseRotation;
    public bool allowX, allowY, allowZ;
    public GameObject containerObject;
    public Vector3 bounds;

    private float nextActionTime = 1.0f;
    public float period = 1.0f;
    private bool shouldExecute = false;

    public AudioClip clickSound;
    private AudioSource source;
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;
    public Vector3 scale = new Vector3(0.0009f, 0.0009f, 0.0009f);
    private Vector3 normalizedVector;

    override public void Start() {
        base.Start();
        gameObject.SetActive(false);
        baseLocation = gameObject.transform.localPosition;
        baseRotation = gameObject.transform.localRotation;
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localRotation = Quaternion.identity;
        source = GetComponent<AudioSource>();
        normalizedVector = gameObject.GetComponentInParent<Renderer>().bounds.size.normalized;
        bounds = normalizedVector;
        bounds.Scale(scale);
        gameObject.transform.localRotation = baseRotation;
        gameObject.transform.localPosition = baseLocation;
        gameObject.SetActive(true);


    }
    
    public void OnFocusEnter() {
        base.OnFocusEnter();
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        base.OnFocusExit();
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }

    public override void Update() {
        base.Update();
        //Debug.Log("BOUNDS: " + bounds.x + " " + bounds.y + " " + bounds.z);

        bounds = normalizedVector;
        bounds.Scale(scale);
        //Debug.Log(gameObject.transform.parent.localScale);
        //Debug.Log(gameObject.transform.localPosition.x + gameObject.transform.parent.localPosition.x);
        //we know we want to keep the x and y position at a certain place, only want the y offset. so lets constantly keep putting this thing there
        if ( isDragging ) {
            {
                var position = gameObject.transform.localPosition;

                //Debug.Log(baseRotation.x);
                //Debug.Log(baseRotation.y);
                //Debug.Log(baseRotation.z);


                Vector3 origin = baseLocation;
                
                //gives x y z values for the size. we want to go .5 times the axis of freedom max.
                float[] allowedThreshold = new float[3];

                allowedThreshold[0] = bounds.x * 0.5f;
                allowedThreshold[1] = bounds.y * 0.5f;
                allowedThreshold[2] = bounds.z * 0.5f;
                /*Debug.Log("POSITION: " + position.x + " " + position.y + " " + position.z);
                Debug.Log("BOUNDS: " + bounds.x + " " + bounds.y + " " + bounds.z);
                Debug.Log("MIDRANGE: " + allowedThreshold[0] + " " + allowedThreshold[1] + " " + allowedThreshold[2]);
                */

                if ( !allowX ) {
                    //change x to be the origin 
                    position.x = origin.x;

                } else { //allowX
                    if ( position.x > allowedThreshold[0] + origin.x ) { //positive offset
                        //just set the position of x to be the max....
                        position.x = allowedThreshold[0];

                    } else if ( position.x < origin.x - allowedThreshold[0] ) {
                        position.x = -allowedThreshold[0];
                    }
                }

                if ( !allowY ) {
                    //change y to be the origin
                    position.y = origin.y;
                } else { //allowY
                    if ( position.y > allowedThreshold[1] + origin.y ) { //positive offset
                        //just set the position of x to be the max....
                        position.y = allowedThreshold[1];

                    } else if ( position.y < origin.y - allowedThreshold[1] ) {
                        position.y = -allowedThreshold[1];
                    }
                }

                if ( !allowZ ) {
                    position.z = origin.z;
                } else { //allowZ
                    if ( position.z > allowedThreshold[2] + origin.z ) { //positive offset
                        //just set the position of x to be the max....
                        position.z = allowedThreshold[2];

                    } else if ( position.z < origin.z - allowedThreshold[2] ) {
                        position.z = -allowedThreshold[2];
                    }
                }

                gameObject.transform.localPosition = position;
                gameObject.transform.localRotation = baseRotation;


                //do we want to do this right here? i guess start a CoRoutine to tell sonos to turn the fuck up...
                //only do it in the x direction cause it seems to not work on others....
                //only do this calculation about each second...

                // If the next update is reached

                if ( Time.time >= nextActionTime ) {
                    //Debug.Log(Time.time + ">=" + nextActionTime);
                    // Change the next update (current second+1)
                    nextActionTime = Mathf.FloorToInt(Time.time) + period;
                    // Call your fonction
                    if ( shouldExecute ) { // this is where I perform calculations
                        performLevelCalculations();
                    } else {
                        shouldExecute = true;
                    }
                }
            }



    } else {
            //stuff to do when not dragging...
            baseLocation = gameObject.transform.localPosition;
            baseRotation = gameObject.transform.localRotation;
        }
    }


    override public void StopDragging() {
        base.StopDragging();
        gameObject.transform.localRotation = baseRotation;
        gameObject.transform.localPosition = baseLocation;
        shouldExecute = false;


        source.PlayOneShot(clickSound, 1F);


    }

    public override void StartDragging() {
        base.StartDragging();
        //wait X seconds before you start doing any calcs;
        shouldExecute = false;
        baseLocation = gameObject.transform.localPosition;
        baseRotation = gameObject.transform.localRotation;

        source.PlayOneShot(clickSound, 1F);

    }

    public void performLevelCalculations() {
        Vector3 origin = baseLocation;
        Vector3 currentLocation = gameObject.transform.localPosition;
        var bounds = containerObject.GetComponent<MeshRenderer>().bounds.size.normalized;
        bounds.Scale(new Vector3(0.001f, 0.001f, 0.001f));
        SliderLevels sliders = new SliderLevels();
        //gives x y z values for the size. we want to go .5 times the axis of freedom max.
        float[] allowedThreshold = new float[3];
        allowedThreshold[0] = bounds.x * 0.5f;
        allowedThreshold[1] = bounds.y * 0.5f;
        allowedThreshold[2] = bounds.z * 0.5f;

        if ( allowX ) {
            //calculate what % of the allowed direction im at.
            //0-30% +1pt 31-60% +2pts 61-100% +3pts if to the right
            //0-30% -1pt 31-60% -2pts 61-100% -3pts if to the left 

            if (currentLocation.x < origin.x ) { //left side of origin
                var delta = Mathf.Abs(currentLocation.x - origin.x);
                if ( delta > (0.61f * allowedThreshold[0]) ) {
                    Debug.Log("LEVEL 3 DECREASE");
                    sliders.XAxisLevel = -3;
                } else if ( delta > (0.31f * allowedThreshold[0]) && delta < (0.60f * allowedThreshold[0]) ) {
                    Debug.Log("LEVEL 2 DECREASE");
                    sliders.XAxisLevel = -2;
                } else if (delta > (0.01f * allowedThreshold[0]) && delta < (0.30f * allowedThreshold[0])) {
                    Debug.Log("LEVEL 1 DECREASE");
                    sliders.XAxisLevel = -1;
                } else {
                    //do nothing weird numbers....
                    sliders.XAxisLevel = 0;
                }

            } else if (currentLocation.x >= origin.x  ) { //right side of origin
                var delta = Mathf.Abs(currentLocation.x - origin.x);
                if ( delta > (0.61f * allowedThreshold[0]) ) {
                    Debug.Log("LEVEL 3 INCREASE");
                    sliders.XAxisLevel = 3;
                } else if ( delta > (0.31f * allowedThreshold[0]) && delta < (0.60f * allowedThreshold[0]) ) {
                    Debug.Log("LEVEL 2 INCREASE");
                    sliders.XAxisLevel = 2;
                } else if ( delta > (0.01f * allowedThreshold[0]) && delta < (0.30f * allowedThreshold[0]) ) {
                    Debug.Log("LEVEL 1 INCREASE");
                    sliders.XAxisLevel = 1;
                } else {
                    //do nothing weird numbers....
                    sliders.XAxisLevel = 0;
                }
            }
        }

        if ( allowY ) {
            if ( currentLocation.y < origin.y ) { //left side of origin
                var delta = Mathf.Abs(currentLocation.y - origin.y);
                if ( delta > (0.61f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 3 DECREASE");
                    sliders.YAxisLevel = -3;
                } else if ( delta > (0.31f * allowedThreshold[1]) && delta < (0.60f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 2 DECREASE");
                    sliders.YAxisLevel = -2;
                } else if ( delta > (0.01f * allowedThreshold[1]) && delta < (0.30f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 1 DECREASE");
                    sliders.YAxisLevel = -1;
                } else {
                    //do nothing weird numbers....
                    sliders.YAxisLevel = 0;
                }
            } else if ( currentLocation.y >= origin.y ) { //right side of origin
                var delta = Mathf.Abs(currentLocation.y - origin.y);
                if ( delta > (0.61f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 3 INCREASE");
                    sliders.YAxisLevel = 3;
                } else if ( delta > (0.31f * allowedThreshold[1]) && delta < (0.60f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 2 INCREASE");
                    sliders.YAxisLevel = 2;
                } else if ( delta > (0.01f * allowedThreshold[1]) && delta < (0.30f * allowedThreshold[1]) ) {
                    Debug.Log("LEVEL 1 INCREASE");
                    sliders.YAxisLevel = 1;
                } else {
                    //do nothing weird numbers....
                    sliders.YAxisLevel = 0;
                }
            }

        } else {
            //...
        }
        
        if ( allowZ ) {
            if ( currentLocation.z < origin.z ) { //left side of origin
                var delta = Mathf.Abs(currentLocation.z - origin.z);
                if ( delta > (0.61f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 3 DECREASE");
                    sliders.ZAxisLevel = -3; 
                } else if ( delta > (0.31f * allowedThreshold[2]) && delta < (0.60f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 2 DECREASE");
                    sliders.ZAxisLevel = -2;                                                    
                } else if ( delta > (0.01f * allowedThreshold[2]) && delta < (0.30f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 1 DECREASE");
                    sliders.ZAxisLevel = -1;
                } else {
                    //do nothing weird numbers....
                    sliders.ZAxisLevel = 0;
                }
            } else if ( currentLocation.z >= origin.z ) { //right side of origin
                var delta = Mathf.Abs(currentLocation.z - origin.z);
                if ( delta > (0.61f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 3 INCREASE");
                    sliders.ZAxisLevel = 3;
                } else if ( delta > (0.31f * allowedThreshold[2]) && delta < (0.60f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 2 INCREASE");
                    sliders.ZAxisLevel = 2;                                                   
                } else if ( delta > (0.01f * allowedThreshold[2]) && delta < (0.30f * allowedThreshold[2]) ) {
                    Debug.Log("LEVEL 1 INCREASE");
                    sliders.ZAxisLevel = 1;
                } else {
                    //do nothing weird numbers....
                    sliders.ZAxisLevel = 0;
                }
            }

        } else {

        }

        if (sliders.XAxisLevel != 0 || sliders.YAxisLevel != 0 || sliders.ZAxisLevel != 0) {
            HandleSliderChangeRequest(sliders);
        }
    }

    protected void HandleSliderChangeRequest(SliderLevels levels) {
        //pls change state based on slider levels
        //for this one i'm just going to send a message to the root object...
        gameObject.SendMessageUpwards("OnSliderChangeRequest", levels);
    }

    public void DisableInteraction(bool yes) {
        if ( yes ) {
            gameObject.GetComponent<Collider>().enabled = false;
        } else {
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }

    public struct SliderLevels {
        public int XAxisLevel, YAxisLevel, ZAxisLevel;
    }
}
```

### MusicButtonHandler.cs
```C#
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using System;

public class MusicButtonHandler : BaseVentanaButton {
   

}
```

### VentanaMusicController.cs
```C#
using UnityEngine;
using System.Collections;
using Vuforia;
using System;
[ExecuteInEditMode]
public class VentanaMusicController : BaseVentanaController  {
    public GameObject playButton;
    public GameObject pauseButton;
    public bool isMusicPlaying = false;
    public bool isModelShowing = false;

    public int volumeMultiplier = 2;

    private string playCommand = "playtoggle";
    private string statusCommand = "status";
    private string nextCommand = "forward";
    private string previousCommand = "reverse";
    VentanaRequestFactory requestFactory;


    // Use this for initialization
    void Start() {
        base.Start();
        requestFactory = VentanaRequestFactory.Instance;
        requestAlbum();
    }

    // Use this for initialization
    void makeAPIRequest(string child) { //bubbled from child 
        requestFactory = VentanaRequestFactory.Instance;
        switch ( child ) {
            case "play":
            Debug.Log("Bubbled play");
            StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(playCommand, VentanaID, null));
            break;
            case "pause":
            Debug.Log("Bubbled pause");
            StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(playCommand, VentanaID, null));
            break;
            case "next":
            Debug.Log("Bubbled next");
            StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(nextCommand, VentanaID, null));
            break;
            case "previous":
            Debug.Log("Bubbled previous");
            StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(previousCommand, VentanaID, null));
            break;
            case "status":
            //Debug.Log("Bubbled albumArt");
            StartCoroutine(requestFactory.GetFromMusicAPIEndpoint(statusCommand, this.VentanaID, GetRequestCompleted));
            break;
            default:
            Debug.Log("No good bubble called");
            break;
        }

    }
    void requestAlbum() {
        makeAPIRequest("status");
    }

    // Update is called once per frame
    void Update() { 
        if ( isModelShowing ) {
            if ( !isMusicPlaying ) {
                SetPlayButton();
            } else {
                SetPauseButton();
            }
        }
    }
    public void SetPauseState() {
        isMusicPlaying = false;
        SetPlayButton();
    }

    public void SetPlayState() {
        isMusicPlaying = true;
        SetPauseButton();
    }
    public void SetPauseButton() {
        playButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void SetPlayButton() {
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }
    

    public void GetRequestCompleted(VentanaInteractable ventana) {
        SonosInfo info = ventana as SonosInfo;
        isMusicPlaying = !info.isPaused;
        BroadcastMessage("OnURLSent", ventana);
    }

    void OnSliderChangeRequest(KnobHandler.SliderLevels levels) {
        VentanaRequestFactory requestFactory = VentanaRequestFactory.Instance;
        Debug.Log("Requesting a: " + levels.XAxisLevel + (levels.XAxisLevel > 0 ? " increase" : " decrease"));
        int baseLevel = levels.XAxisLevel * volumeMultiplier;
        StartCoroutine(requestFactory.PostToMusicAPIEndpoint("volume", VentanaID, (levels.XAxisLevel > 0 ? "+" : "") + baseLevel.ToString()));

    }

    public override void OnVumarkFound() {
        base.OnVumarkFound();
        isModelShowing = true;
    }

    public override void OnVumarkLost() {
        base.OnVumarkLost();
        isModelShowing = false;
    }

    
}
```

### LightButtonController.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class LightButtonHandler : BaseVentanaButton {
    
}
```

### VentanaLightController.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentanaLightController : BaseVentanaController {

    private string poweredCommand = "change_power";
    private string brightCommand = "change_brightness";
    private string colorCommand = "color";
    private string statusCommand = "status";

    public int brightnessMultipler = 2;
    // Use this for initialization
    protected new void Start() {
        base.Start();
    }

    // Update is called once per frame
    protected new void Update() {
        base.Update();
    }

    public override void OnVumarkFound() {
        base.OnVumarkFound();
    }

    public override void OnVumarkLost() {
        base.OnVumarkLost();
    }
    void makeAPIRequest(string child) {
        VentanaRequestFactory requestFactory = VentanaRequestFactory.Instance;
        switch ( child ) {
            case "light":
            StartCoroutine(requestFactory.PostToLightAPIEndpoint(poweredCommand, VentanaID, ""));
            break;
            default:
            break;
        }
    }

    void OnSliderChangeRequest(KnobHandler.SliderLevels levels) {
        VentanaRequestFactory requestFactory = VentanaRequestFactory.Instance;
        Debug.Log("Requesting a: " + levels.XAxisLevel + (levels.XAxisLevel > 0 ? " increase" : " decrease"));
        int baseLevel = levels.XAxisLevel * brightnessMultipler;
        StartCoroutine(requestFactory.PostToLightAPIEndpoint(brightCommand, VentanaID, baseLevel.ToString()));

    }
}
```
### Args.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//literally for global variables

public class Args : MonoBehaviour {
    public static string HOLOHUB_IP = "192.168.0.108";
    public static string PREFAB_LOCATION = "Ventana/Prefabs/"; //in Resources Folder
    public static string HOLOHUB_ADDRESS = "http://192.168.0.108:8081";
    public static string HOLOHUB_WEBSOCKET_ADDRESS = "ws://192.168.0.108:4200/socket.io/?EIO=3&transport=websocket";
    public static string VENTANA_MARK_CONFIG_FILE_LOCATION = "Ventana/VentanaConfig.json";
    public static string VENTANA_DATA_SET_NAME = "QCAR\\VentanaTargets.xml";
   

}
```

### BaseVentanaController.cs
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseVentanaController : MonoBehaviour, IVentanaVumarkEventHandler {

    public int VentanaID = -1;
    public string ControllerName;
    
    // Use this for initialization
    protected void Start () {
        Debug.Log("ID: " + VentanaID);
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}

    public virtual void OnVumarkFound() {

    }

    public virtual void OnVumarkLost() {

    }
}
```


### MusicInfo.cs
```C#
using UnityEngine;
using System.Collections;

[System.Serializable]
public class SonosInfo : VentanaInteractable {
    //PLAYING --> playing
    //PAUSED_PLAYBACK --> paused
    public bool isPaused {
        get {
            switch ( current_transport_state ) {
                case "PLAYING":
                return false;
                case "PAUSED_PLAYBACK":
                return true;
                default:
                return true;
            }
        }
    }
    public string album;
    public string artist;
    public string title;
    public string uri;
    public int playlist_position;
    public string duration;
    public string position;
    public string album_art;
    public string current_transport_state = "";
    public string metadata;

    public static SonosInfo CreateFromJSON(string jsonString) {
        return JsonUtility.FromJson<SonosInfo>(jsonString);
    }

    public override string ToString() {
        return "Album: " + album + " Artist: " + artist + " Title: " + title +
                " URI: " + uri + " Playlist Position: " + playlist_position + " Duration " +
                duration + " Position: " + position + " Album Art URL: " + album_art + " Metadata: " + metadata + " Paused: " + isPaused;
    }

    // Given JSON input:
    // {"name":"Dr Charles","lives":3,"health":0.8}
    // this example will return a PlayerInfo object with
    // name == "Dr Charles", lives == 3, and health == 0.8f.

}



public class VentanaInteractable {

}
```

## HoloHub Reference Assets

### Routing Modules 

#### Sonos Routing Module
```JS
var express = require('express');
var router = express.Router();
var request = require('request');
var SonosDM = require('../app/models/sonosController');


var SONOS_HTTP_SERVER = BASESERVER + ":5005"
// var SONOS_HTTP_SERVER = 'http://192.168.0.108' + ":5005"


// Convert Sonos response into Sonos HoloHub Object friendly response
function responseSummary(body, callback){
  sonosRequestData = body;
  var sonosSendData = {}

  sonosSendData["album"] = sonosRequestData.currentTrack.album;
  sonosSendData["artist"] = sonosRequestData.currentTrack.artist;
  sonosSendData["title"] = sonosRequestData.currentTrack.title;
  sonosSendData["current_transport_state"] = sonosRequestData.playbackState;
  sonosSendData["uri"] = sonosRequestData.currentTrack.uri;
  sonosSendData["playlist_position"] = sonosRequestData.trackNo;
  sonosSendData["duration"] = sonosRequestData.currentTrack.duration;
  sonosSendData["position"] = sonosRequestData.elapsedTimeFormatted;
  sonosSendData["metadata"] = sonosRequestData.elapsedTime;
  sonosSendData["album_art"] = sonosRequestData.currentTrack.absoluteAlbumArtUri;

  return callback(sonosSendData);
}

//Convert a Vumark ID to a Sonos Device ID (the group/device name)
function getDeviceIDbyVumarkID(vumark_id, callback){
  
  //Get a sonos object. If not found return null, otherwise return name
  SonosDM.findById(vumark_id, function(err, sonos){
      if (err){
        console.log(err);
        return callback(null);
      }
      if (sonos){
        return callback(sonos.device_id);
      }
      else{
        return callback(null);
      }
    });
};

//Convert a Device ID (the group/device name) to a Vumark ID
function getVumakIDbyDeviceID(device_id, callback){
  // Get Sonos object by device_id, if found return id (VuMark) otherwise null
  SonosDM.findOne({"device_id": device_id}, function(err, sonos){
    if (err){
      console.log(err);
      return callback(null);
    }
    if (sonos){
      return callback(sonos._id);
    }
    else{
      return callback(null);
    }
  });
};

// Get all sonos objects and Create a new Sonos Music Object
router.route('/')

  .get(function(req, res) {   //GET all paired sonos device.

    SonosDM.find(function(err, sonos) {
              if (err)
                  res.send(err);
              
              res.json(sonos);
          });
  })

  .post(function(req, res){ 
    
    /* 
      Process new sonos object POST request. This
      will include the device_id (VuMark ID) and the
      device_name (Sonos API ID "in this case a string")
    */

    // TODO: ######## CHECK TO SEE IF DEVICE ALREADY EXISTS IN RECORD!!!!! ##############
    
    var sonos = new SonosDM();  // Create new instance of a sonos object

    if("_id" in req.body)
      sonos._id = req.body._id
    if("device_id" in req.body) {
      sonos.device_id = req.body.device_id;
      sonos.device_name = req.body.device_id;
    }
    if("vendor_logo" in req.body)
      sonos.vendor_logo = req.body.vendor_logo
    
    //if("controller" in req.body)
    sonos.controller = "Ventana/Prefabs/MusicController";
    sonos.vendor = "1" ; //specific for sonos devices
    // For sonos. save device state
    sonos.device_type = 'Sonos Speaker';

    // Lookup sonos state data calling device_id. Verify that the connection can be made.
    request(BASESERVER + ":" + port + '/sonos/status/' + sonos.device_id + '?skiplookup=true', function (error, response, body) {
      if (!error && response.statusCode == 200 && response.body != 'Not Started or Connected') {
          sonos.save(function(err) {
              if (err){
                res.send(err);
              }
              else{
                res.json({ message: 'SonosDM object created!' });
              };
          });     
      }
      else {
            res.send(statusCode=500, "Not Started or Connected");
      };
    });
  });

  //Need to add a PUT for the HoloLens to update the JSON file reference so that controller String is saved.

//Update Device or Get Device by Vuforia ID
router.route('/byId/:vumark_id')

  .get(function(req, res){
    //Get the Sonos Object by ID
    SonosDM.findById(parseInt(req.params.vumark_id), function(err, sonos){
      if (err){
        res.send(err);
      }
      res.json(sonos);
    });
  })

  .put(function(req, res){
    SonosDM.findByIdAndUpdate(parseInt(req.params.vumark_id), req.body, function(err,sonos){
      if (err){
        res.send(err);
      }
      res.json(sonos);
    });
  });


// GET Status by Vumark ID
router.get('/status/:vumark_id', function(req, res) {
  // Toggle Playback
  var sonosRequestData;

  //Initial device setup query to get sonos state data (Special case where vumark_id == device_id when skiplookup flag is set)
  if(req.query.skiplookup){
     request(SONOS_HTTP_SERVER + "/" + req.params.vumark_id + '/state', function (error, response, body) {
        if (!error && response.statusCode == 200) {
            console.log(body); // Print the response page.
            
            responseSummary(JSON.parse(body), function(responseJson){
                res.json(responseJson);
            });
        }
        else {
            res.send(500, "Not Started or Connected")
        }
      });
  }
  // Get sonos state data based on a vumark ID (needs to get converted to device_id)
  else
  {
    getDeviceIDbyVumarkID(req.params.vumark_id, function(device_id){
      request(SONOS_HTTP_SERVER + "/" + device_id + '/state', function (error, response, body) {
        if (!error && response.statusCode == 200) {
            /* DEBUG CONSOLE */
            console.log(body); // Print the response page.
            responseSummary(JSON.parse(body), function(responseJson){
                res.json(responseJson);
            });
        }
        else {
            res.send(500, "Not Started or Connected")
        }
      });
    });
  }
});

// Toggle playback (Automatically loggles as needed)
router.get('/playtoggle/:vumark_id', function(req, res) {
  
  // Toggle Playback
  getDeviceIDbyVumarkID(req.params.vumark_id, function(device_id){
    // Call the status endpoint to see the current playback state of the sonos device. Set skiplookup flag to true to avoid double vumark lookup.
    request(BASESERVER + ':' +  port + '/sonos/status/' + device_id + '?skiplookup=true', function (error, response, body) {
      if (!error && response.statusCode == 200 && response.body != 'Not Started or Connected') {
        sonosRequestData = JSON.parse(body);
        if (sonosRequestData["current_transport_state"] == 'PAUSED_PLAYBACK'){
          request(SONOS_HTTP_SERVER + '/' + device_id + '/play', function (error, response, body) {
            if (!error && response.statusCode == 200) {
                console.log(body) // Print the response page.
            }
          });
          res.send(body);
        }
        else{
          request(SONOS_HTTP_SERVER + '/' + device_id + '/pause', function (error, response, body) {
            if (!error && response.statusCode == 200) {
                console.log(body) // Print the response page.
            }
          });
          res.send(body)
        }
      }
      else {
        res.send(500, "Not Started or Connected");
      }
    });
  });
});

// Skip current song
router.get('/forward/:vumark_id', function(req,res) {
  getDeviceIDbyVumarkID(req.params.vumark_id, function(device_id){
    request(SONOS_HTTP_SERVER + '/' + device_id + '/next', function (error, response, body) {
      if (!error && response.statusCode == 200) {
          console.log(body) // Print the response page.
      }
      res.send(body)
    });
  });
});

// Rewind Song/playlist
router.get('/reverse/:vumark_id', function(req, res){
  getDeviceIDbyVumarkID(req.params.vumark_id, function(device_id){
    request(SONOS_HTTP_SERVER + '/' + device_id + '/previous', function (error, response, body) {
      if (!error && response.statusCode == 200) {
          console.log(body) // Print the response page.
      }
      res.send(body)
    });
  });
});

// Volume Control
router.post('/volume/:vumark_id', function(req, res){
  getDeviceIDbyVumarkID(req.params.vumark_id, function(device_id){
    request(SONOS_HTTP_SERVER + '/' + device_id + '/volume/' + req.body.value, function(error, response, body){
      if (!error && response.statusCode == 200) {
          console.log(body) // Print the response page.
          res.send(body);
      }
      else{
        res.send("Error", statusCode=500);
      };
    });
  });
});


/******  TODO FIX THIS TO USE THE CORRECT CALLBACK REQUEST FUNCTION ******/
// Get all sonos devices on the network -- SONOS CALL
router.get('/devices', function(req, res){
  var sonosDevices = {'paired_devices': [], 'unpaired_devices': []} 
  var connectedDevices = {}
  
  // Retrieve all devices paired with the HoloHub, place into a dictionary {device_id: _id}
  request(BASESERVER + ':' +  port + '/sonos/', {timeout: 500}, function(error, response, body){
    if(!error && response.statusCode == 200) {
      var temp1 = JSON.parse(body);
      temp1.forEach(function(arrayItem){
        connectedDevices[arrayItem.device_id] = arrayItem;
      });

      // Discover all sonos devices on the network
      request(SONOS_HTTP_SERVER + '/' + 'zones', {timeout: 500}, function (error1, response, body) {
        if (!error1 && response.statusCode == 200) {
            var sonosRequestData = JSON.parse(body);

            sonosRequestData.forEach( function(arrayItem) {
              //If device name is in connectedDevices, then device is paired -- show w/ it's vumark ID
              if(arrayItem.coordinator.roomName in connectedDevices){
                sonosDevices.paired_devices.push(connectedDevices[arrayItem.coordinator.roomName]);
              }        
              else
              {
                var temp1 = {
                  "device_id": arrayItem.coordinator.roomName,
                  "device_type": "Sonos Speaker",
                  "device_name": arrayItem.coordinator.roomName,
                  "controller": "Ventana/Prefabs/MusicController",
                  "vendor": '1',
                  "vendor_logo": 'https://lh6.googleusercontent.com/-Px2Steg_XRM/AAAAAAAAAAI/AAAAAAAAFa4/kpB3EVdNHGw/s0-c-k-no-ns/photo.jpg'
                }
                sonosDevices.unpaired_devices.push(temp1);
              }
            });
            res.json(sonosDevices);       
        }
        else{
          console.log(error1);
          res.send("Error", statusCode=500);
        };
      });
    }
    else{
      console.log(error);
      res.send("Error " + error, statusCode=500);
    };
  });
});


/** SONOS Socket.IO Push notification service **/

//Server endpoint that recieves state changes from Sonos HTTP Server (Push Notifications)
router.post('/pushnotification', function(req,res){
  //Send state change to Socket.IO connected clients
  var sonosResponse = {};
  // Filter out notification requests such that only song-state changes take place. 
  if(req.body.type == 'transport-state') {
    //Retrieve standardized sonos object value
    getVumakIDbyDeviceID(req.body.data.roomName, function(_id){
    // Igonore devices that haven't been setup in HoloHub
      if (_id != null){
        responseSummary(req.body.data.state, function(responseJson){
          sonosResponse[_id] = responseJson;
          var options = {
            method: 'POST',
            url: BASESERVER + ':' + port + '/socketsend',
            body: sonosResponse,
            json: true
          };
      
          //Send push notification request to /socketsend endpoint (Socket.IO emitter)
          request(options, function (error, response, body) {
            //#### DEBUG ####
            //console.log(sonosResponse)
            //console.log("Push Notification Sent");
            if(error || response.statusCode != 200){
              console.log(error);
            }
          });
          res.send("ok");
        });
      }
      else
        res.send("error");
    });
  };
});


module.exports = router;
```
#### Wink Routing Modules

```JS
var express = require('express');
var router = express.Router();
var request = require('request');
var WinkDM = require('../app/models/winkController');
var setup = require('../setup');
var WINK_HTTP_SERVER = "https://api.wink.com/"

// convert Wink response into Wink HoloHub Object friendly response
function winkSummary(body, callback) {
    winkRequestData = body;
    var winkSendData = {}

    winkSendData["device_type"] = winkRequestData.data.object_type + "s";
    winkSendData["device_id"] = winkRequestData.data.object_id;
    winkSendData["vendor_logo"] = winkRequestData.data.vendor_logo;

    if (winkRequestData.data.object_type == "powerstrip"){
        winkSendData["outlets"] = [];
        winkRequestData.data.outlets.forEach(function (item, index){
            var tempWink = {};
            tempWink["outlet_id"] = item.outlet_id;
            tempWink["outlet_index"] = item.outlet_index;
            tempWink["name"] = item.name;
            winkSendData.outlets[index] = tempWink;
        })
    }

    winkSendData["name"] = winkRequestData.data.name;
    winkSendData["_id"] = winkRequestData.data._id;

    return callback(winkSendData);
}

// Convert a vumark_id to a Wink device ID (the device_type/device_id/name)
function getDeviceIDbyVumarkID(vumark_id, callback) {

    //gets a wink object, if one doesn't exist with that vumark_id, return null
    WinkDM.findById(vumark_id, function(err, wink) {
        if (err) {
            console.log(err);
            return callback(null);
        } else if (wink) {
            return callback(wink);
        } else {
            return callback(null);
        }
    });
};

function getVumarkByDeviceID(device_id, callback) {

    WinkDM.findOne({"device_id": device_id}, function(err, wink){
        if (err) {
            console.log(err);
            return callback(null);
        } else if (wink) {
            return callback(wink._id); //if the obj exists return its vu id
        } else {
            return callback(null);
        }
    });
};

// GET shows we are connected to wink
// POST processes new wink object
router.route('/')

    .get(function(req,res){
        if (WINK_AUTHORIZATION != null) {
            //console.log(WINK_AUTHORIZATION)
            WinkDM.find(function(err, sonos) {
              if (err)
                  res.send(err);
              
              res.json(sonos);
            });
        } else {
            //console.log("WINK_AUTHORIZATION is null");
            res.json({message: 'Not connected to Wink!'});
        }
    })

    .post(function(req,res) {
        var wink = new WinkDM(); //new instance of wink object

        if (req.body._id != null){
            wink._id = req.body._id; //vumark id
        }
        if (req.body.device_id != null) {
            wink.device_id = req.body.device_id;
        }
        if (req.body.device_type != null){
            wink.device_type = req.body.device_type;
        }
        if (req.body.device_name != null){
            wink.device_name = req.body.device_name;
        }
        if (req.body.vendor_logo != null) {
            wink.vendor_logo = req.body.vendor_logo;
        }
        if (req.body.vendor != null) {
            wink.vendor = req.body.vendor;
        }
        else{
            wink.vendor = "2"; //vendor is wink
        }

        // wink controller is path to hololens VentanaConfig.json
        if (wink.device_type == "light_bulbs") {
            wink.controller = "Ventana/Prefabs/LightController";
        } else if (wink.device_type == "powerstrips") {
            wink.controller = "Ventana/Prefabs/PowerStripController";
        } else {
            wink.controller = null;
        }

        request({
            method: 'GET',
            url: WINK_HTTP_SERVER + wink._doc.device_type + "/" + wink._doc.device_id,
            headers: {
            'Content-Type': 'application/json',
            'Authorization': WINK_AUTHORIZATION
            },
        }, function(error, response, body) {
            if (!error && response.statusCode == 200) {
                wink.save(function(err) {
                    if (err) {
                        res.send(err);
                    } else {
                        res.json({message: 'WinkDM object created!'});
                    };
                });
                // res.json((JSON.parse(body)).data);
            } else {
                console.log("error in 'wink/' POST: " + response.statusCode)
                //res.send(statusCode=500, "Not Started or Connected");
                res.send({message : "this didn't work"});
            };
        });
    });

// get all wink devices connected to the account logged in
router.get('/wink_devices', function(req, res){

    //conosole.log("Authorization: " + WINK_AUTHORIZATION);
    var winkDevices = {'device_list': []}
    request({
        method: 'GET',
        url: WINK_HTTP_SERVER + 'users/me/wink_devices',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': WINK_AUTHORIZATION
        },
    }, function(error, response, body){
        if (!error && response.statusCode == 200) {
            //console.log('Status:', response.statusCode);
            //console.log('Headers:', JSON.stringify(response.headers));
            //console.log('Response:', JSON.parse(body));
         
            var winkResponseBody = JSON.parse(body);

            winkResponseBody.data.forEach( function (item, index) {
                var deviceTemp = {};
                if (item.light_bulb_id != null){
                    deviceTemp["device_id"] = item.light_bulb_id;
                    deviceTemp["device_type"] = 'light_bulbs';
                } else if (item.powerstrip_id != null){
                    deviceTemp["device_id"] = item.powerstrip_id;
                    deviceTemp["device_type"] = 'powerstrips';
                }/* else if (item.manufacturer_device_model == "wink_hub") {
                    deviceTemp["device_id"] = item.hub_id;
                    deviceTemp["device_type"] = 'hubs';
                }*/ else {
                    console.log("Device type not supported");
                }
                deviceTemp["name"] = item.name;         //Kept for legacy. Need to test for removal
                deviceTemp["device_name"] = item.name;
                deviceTemp["vendor_logo"] = item.vendor_logo;
                /*getVumarkByDeviceID(deviceTemp["device_id"], function (returnObject){
                    if (returnObject != null) {
                        // this device has a vumark id linked to item
                        deviceTemp["_id"] = returnObject;
                    } else {
                        deviceTemp["_id"] = null;
                    }
                });*/
                winkDevices.device_list[index] = deviceTemp;
            });

            res.json(winkDevices)
        } else {
            res.send(500, "Not started or connected");
        }
    });
     
});

//GET all devices connected to the HoloHub
router.get('/devices', function(req, res){
    var winkDevices = {'paired_devices': [], 'unpaired_devices': []};
    var connectedDevices = {};

    // Retrieve all devices paired with the HoloHub, place into a dictionary {device_id: _id}
    request(BASESERVER + ':' +  port + '/wink/', function(error, response, body){
        if(!error && response.statusCode == 200) {
        var temp1 = JSON.parse(body);
        temp1.forEach(function(arrayItem){
            connectedDevices[arrayItem.device_id] = arrayItem;
        });

        // Discover all Wink devices on the Wink.COM
        request(BASESERVER + ':' +  port + '/wink/wink_devices/', function(error, response, body){
            if (!error && response.statusCode == 200) {
                var sonosRequestData = JSON.parse(body)['device_list'];

                sonosRequestData.forEach( function(arrayItem) {
                //If device name is in connectedDevices, then device is paired -- show w/ it's vumark ID
                if(arrayItem.device_id in connectedDevices){
                    winkDevices.paired_devices.push(connectedDevices[arrayItem.device_id]);
                }        
                else
                {
                    if(arrayItem.device_type in setup.supportedDevices){
                        var temp1 = {
                            "device_id": arrayItem.device_id,
                            "device_type": arrayItem.device_type,
                            "device_name": arrayItem.device_name,
                            "controller": setup.supportedDevices[arrayItem.device_type],
                            "vendor_logo": "https://www.winkapp.com/assets/mediakit/wink-logo-icon-knockout-50235153b274cdf35ef39fb780448596.png",
                            "vendor": 2
                        }
                        winkDevices.unpaired_devices.push(temp1);
                    }
                    
                }
                });
                res.json(winkDevices);       
            }
            else{
            error = error1;
            };
        });
        }
        else{
        console.log(error);
        res.send("Error " + error, statusCode=500);
        };
    });
});

// gets the light_bulb status for the particular id
router.get('/status/:vumark_id', function(req, res) {
    
    getDeviceIDbyVumarkID(req.params.vumark_id, function(returnObject) {
        var device_id;
        var device_type;
        
        if (returnObject == null) {
            res.send({"message": "Invalid Wink vumark ID"});
        } else {
            device_id = returnObject._doc.device_id;
            device_type = returnObject._doc.device_type;
        }

        request({
            method: 'GET',
            url: WINK_HTTP_SERVER + device_type + '/' + device_id,
            //url: WINK_HTTP_SERVER + 'light_bulbs/' + req.params.vumark_id, //hardcoded with light bulbs rn
            headers: {
                'Content-Type': 'application/json',
                'Authorization': WINK_AUTHORIZATION
            },
            json: true
        }, function(error, response, body) {
            if (!error && response.statusCode == 200) {     
                //console.log('Status:', response.statusCode);
                //console.log('Headers:', JSON.stringify(response.headers));
                //console.log('Response:', body);
                //console.log(JSON.stringify(body.data.desired_state));
                winkSummary(body, function(winky){
                    //this will wait for winkSummary response to happen
                    winky["_id"] = req.params.vumark_id;

                    res.json(winky);
                });

                } else {
                    res.send(500, "Not started or connected")
                    //res.json({ message: 'Light bulb id ' + req.body.device_id });
            }
        });

    });

});

// uses PUT to change the state for the particular device
router.post('/change_power/:vumark_id', function(req, res) {
    
    getDeviceIDbyVumarkID(req.params.vumark_id, function(returnObject){
        var device_id;
        var device_type;
        
        if (returnObject == null) {
            res.send({"message": "Invalid Wink vumark ID"});
        } else {
            device_id = returnObject._doc.device_id;
            device_type = returnObject._doc.device_type;
        }

        var last_state; 

        request({
            method: 'GET',
            url: WINK_HTTP_SERVER + device_type + '/' + device_id,
            headers: {
                'Content-Type': 'application/json', 
                Authorization : WINK_AUTHORIZATION
            },
            json: true
          }, function(error, response, body) {
            if (!error && response.statusCode == 200) {     
                if (device_type == "powerstrips"){
                    device_type = "outlets";
                    device_id = body.data.outlets[req.body.value].outlet_id;
                    last_state = body.data.outlets[req.body.value].powered;
                } else {
                    last_state = body.data.last_reading.powered;
                }

                if (last_state == true) {
                    new_state = { "desired_state" : {"powered" : false}};
                } else {
                    new_state = { "desired_state" : {"powered" : true}};
                }

                var options = {
                    method: 'PUT',
                    url: WINK_HTTP_SERVER + device_type + '/' + device_id + '/desired_state',
                    headers: {
                        'Content-Type': 'application/json', 
                        Authorization : WINK_AUTHORIZATION
                    },
                    body: new_state,
                    json: true
                };
                
                request(options, function (error, response, body) {
                    if (!error && response.statusCode == 200) {
                        res.send({ message: 'Change power state'});
                    } else {
                        console.log(error + ' ' + response.statusCode)
                        res.json({ message: 'Error change power state'});
                    }        
                });
            } else {
                    res.send(500, "Not successful change_power")
            }
        });

    });

});

// changes brightness
router.post('/change_brightness/:vumark_id', function(req, res) {
    
    getDeviceIDbyVumarkID(req.params.vumark_id, function(returnObject){
        var device_id;
        var device_type;
        
        if (returnObject == null) {
            res.send({"message": "Invalid Wink vumark ID"});
        } else {
            device_id = returnObject._doc.device_id;
            device_type = returnObject._doc.device_type;
        }

        var state;
        var amount_change_brightness = req.body.value/100.0;

        request({
            method: 'GET',
            url: WINK_HTTP_SERVER + device_type + '/' + device_id,
            headers: {
                'Content-Type': 'application/json', 
                //'Authorization': req.body.Authorization 
                Authorization : WINK_AUTHORIZATION
            },
            json: true
          }, function(error, response, body) {
            if (!error && response.statusCode == 200) {
                state = parseFloat(body.data.last_reading.brightness);

                if (amount_change_brightness != 0) {
                     state += amount_change_brightness;
                    if (state < 0) {
                        state = 0.0;
                    } else if (state > 1.0) {
                        state = 1.0;
                    }
                }
                
                new_state = { "desired_state" : {"brightness" : state}};

                var options = {
                    method: 'PUT',
                    //url: WINK_HTTP_SERVER + req.body.device_type + '/' + req.body.device_id + '/desired_state',
                    url: WINK_HTTP_SERVER + device_type + '/' + device_id + '/desired_state',
                    headers: {
                        'Content-Type': 'application/json', 
                        //'Authorization': req.body.Authorization 
                        Authorization : WINK_AUTHORIZATION
                    },
                    //body: req.body,
                    body: new_state,
                    json: true
                };

                //console.log(JSON.stringify(new_state));
                
                request(options, function (error, response, body) {
                    if (!error && response.statusCode == 200) {
                        //console.log("request", options.body);
                        //console.log('Status:', response.statusCode);
                        //console.log('Headers:', JSON.stringify(response.headers));
                        //console.log('Response:', body);
                        res.send({ message: 'Change Brightness'});
                    } else {
                        console.log(error + ' ' + response.statusCode)
                        res.json({ message: 'Error in changing brightness'});
                    }        
                });
            } else {
                    res.send(500, "Not Successful change_brightness")
            }
        });

    });

});

module.exports = router;
```
### Database Models
#### Sonos Model Schema
```JS
var mongoose     = require('mongoose');
var Schema       = mongoose.Schema;

var SonosSchema   = new Schema({
    _id: String,			// THIS IS THE Vumark ID
    device_id: String,
    device_type: String,
    device_name: String,    //equal to device_id
	controller: String,
    vendor_logo: String,
    vendor: String
});

module.exports = mongoose.model('SonosDM', SonosSchema);
```
#### Wink Model Schema
```JS
var mongoose     = require('mongoose');
var Schema       = mongoose.Schema;

var WinkSchema   = new Schema({
    _id: String,			// THIS IS THE Vumark ID
    device_id: String,      // device_id used by Wink
    device_type: String,    // device_type used by Wink
    device_name: String,
	controller: String,      // unnecessary, setup?
    vendor_logo: String,
    vendor: String
});

module.exports = mongoose.model('WinkDM', WinkSchema);
```
### Server and Setup Implementation

#### Server Main Operations
```JS
// call the packages we need
var express    = require('express');
var bodyParser = require('body-parser');

//OAUTH
var session = require('express-session')
var Grant = require('grant-express')
var grant = new Grant(require('./config.json'))
var app = express();

//Socket.IO 
var server = require('http').createServer(app); 
var io = require('socket.io')(server);
io.set('transports', ['websocket']);

// //PubNub Notifications
// var PubNub = require('pubnub')
// var pubnub = new PubNub({
//     subscribeKey: "sub-c-f7bf7f7e-0542-11e3-a5e8-02ee2ddab7fe",
//     ssl: true
// });

// pubnub.addListener({
//     status: function(statusEvent) {
//         if (statusEvent.category === "PNConnectedCategory") {
//             console.log("Connected to nubPub");
//         }
//     },
//     message: function(message) {
//         newDesiredState = JSON.parse(message).desired_state
//         console.log("New Message!!", message);
//     }
// })      
    
//     console.log("Subscribing..");
// pubnub.subscribe({
//         channels: ['ab6a481d06f81d80acfab707eddb42bf60faf75e|light_bulb-2566198|user-616119'] 
// });

// Session for Grant OAUTH
app.use(session({
    secret:'3245tr,gfewere4re3e4d98eyoiul438p',
    resave: true,
    saveUninitialized: false
}))
app.use(grant)

//Morgan Logging
var morgan = require('morgan');
app.use(morgan('dev')); // log requests to the console

// Configure body parser
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

// Set BASE global variables
port = process.env.PORT || 8081; // set our port
BASESERVER = 'http://localhost';


// wink tokens
WINK_ACCESS_TOKEN = "";
WINK_REFRESH_TOKEN = "";
WINK_AUTHORIZATION = 'bearer 82pXcnWl6h-5wPyTIrBJBYqxve-ZHih7';

// connect to our database
var mongoose = require('mongoose');
mongoose.connect('mongodb://ventana:Pistachio1@ds054999.mlab.com:54999/ventana');

// View Engine
app.set('view engine', 'ejs');
app.use(express.static('public'))

// Setup JS
var setup = require('./setup');

// Add Routers (Modules)
var sonos = require('./routes/sonos');
var wink = require('./routes/wink');
app.use('/wink', wink);
app.use('/sonos', sonos);

// GET Wink OAUTH response - via Grant
// Grant OAUTH request: http://{serverURL}/connect/{module i.e wink}/
// Responses successful authentications to http://{serverURL}/handle_wink_callback/
app.get('/handle_wink_callback', function (req, res) {
  //console.log(req.query)
  //console.log(req.query.access_token)
  //console.log(req.query.refresh_token)
  WINK_ACCESS_TOKEN = req.query.access_token;
  WINK_REFRESH_TOKEN = req.query.refresh_token;
  WINK_AUTHORIZATION = req.query.raw.data.token_type + ' ' + WINK_ACCESS_TOKEN;
  //console.log(WINK_AUTHORIZATION)
  //res.end(JSON.stringify(req.query, null, 2))
  //Handle wink callback will redirect to Sonos devices.
  setup.getWink(function(devices){
        console.log(devices);

        if(devices['unpaired']){

            devices['unpaired'].forEach(function(device) {
             console.log(device);
            });

            res.render('pages/add', {"devices":devices['unpaired'], "host": req.get('host')});

        }
        else{
            res.render('pages/add', {"devices":null, "host": req.get('host')});
        } 
    });
});

// Server Base Endpoint -- SETUP Dashboard

app.get('/', function(req, res) {
    setup.getDevices(function(devices){
        console.log(devices);

        if(devices['paired'].length > 0){

            devices['paired'].forEach(function(device) {
             console.log(device);
            });

            if (req.query.remove){
                res.render('pages/delete', {"devices":devices['paired'], "host": req.get('host')})
            }
            else{ 
                res.render('pages/index', {"devices":devices['paired'], "host": req.get('host')});
            }

        }
        else{
            res.render('pages/index', {"devices":null, "host": req.get('host')});
        }
        
    });
  //res.json({ message: 'Connected to Server' });
});

app.get('/remove/:_id', function(req, res){

    setup.removeDevice(req.params._id, function(response){
        console.log(response);
        setup.getDevices(function(devices){
            console.log(devices);

            if(devices['paired'].length > 0){

                devices['paired'].forEach(function(device) {
                    console.log(device);
                });
                
                res.render('pages/index', {"devices":devices['paired'], "host": req.get('host')});

            }
            else{
                res.render('pages/index', {"devices":null, "host": req.get('host')});
            }
            
        });
    });
});

app.get('/vendors', function(req, res){
    var requestWink;
    if (WINK_ACCESS_TOKEN == "" | WINK_REFRESH_TOKEN == ""){
        //Request Wink Credentials
         requestWink = true;
    }
    else{
        requestWink = false;
    }
    res.render('pages/vendors', {'requestWink': requestWink, "host": req.get('host')}); 
});

app.get('/vumark/:_id/:name', function(req, res){
    res.render('pages/vumark', {"vumarkid": req.params._id, "name": req.params.name, "host": req.get('host') });
});

app.get('/addSonos', function(req, res) {
    setup.getSonos(function(devices){
        console.log(devices);

        if(devices != null && devices['unpaired'].length > 0){

            devices['unpaired'].forEach(function(device) {
             console.log(device);
            });

                res.render('pages/add', {"devices":devices['unpaired'], "host": req.get('host')});

        }
        else{
            res.render('pages/add', {"devices":null, "host": req.get('host')});
        }
        
    });
});

app.get('/savenew/:vendor', function(req, res){
    setup.getUsedIds(function(values){
        //console.log(values);
        if (req.params.vendor != "1" && req.params.vendor != "2") {
            res.json({"message" : "incorrect vendor id entered"});
        } else {

            var newId = 1;
            while (newId < 16) {
                if (values.includes(newId.toString())) {
                    newId++;
                } else {
                    break;
                }
            }

            //console.log("New ID to use: " + newId);

            if (newId >= 16) {
                res.send({"message": "The number of vumark ids has been exhausted"});
            }
            // Based on vendor, create object in the correct SonosDM or WinkDM object
            // vendor == 1 -- sonos
            // vendor == 2 -- wink
            // Will recieve value from url query parameters:
            // - device_name
            // - device_type
            // - [all the things in the sonos/wink object]
            // - assign it an _id that is not in the the `getUsedIDs` list less than 15.
            var object = {
                '_id' : newId.toString(),
                'device_id' : req.query.device_id,
                'device_type' : req.query.device_type,
                'vendor' : req.params.vendor,
                'vendor_logo': req.query.vendor_logo,
                'device_name': req.query.device_name,
                'controller': req.query.controller
            }
            
            setup.saveNewDevice(object, function(returnValue){
                //console.log(returnValue);
                if (returnValue == null){
                    // Create new view for errors.
                    res.json({"message" : "unsuccessful saving of new device"});
                } else {
                    // successfully saved device with id == returnValue
                    res.redirect('../../');
                    //res.json({"message" : "success! with id #" + returnValue});
                }
            });
        } 
    });
        
        //res.json({ message: 'test'});       
});

app.get('/addWink', function(req, res) {
    setup.getWink(function(devices){
        console.log(devices);

        if(devices != null && devices['unpaired'].length > 0){
            
            devices['unpaired'].forEach(function(device) {
             console.log(device);
            });

            res.render('pages/add', {"devices":devices['unpaired'], "host": req.get('host')});

        }
        else{
            res.render('pages/add', {"devices":null, "host": req.get('host')});
        }
        
    });
  //res.json({ message: 'Connected to Server' });
});

// used by the Ventana application to get config of paired devices
app.get('/holoconfig', function(req, res){

    setup.getConfig(function(returnJSON) {
        //console.log(JSON.stringify(returnJSON));
        
        if (returnJSON != null) {
            res.send(returnJSON);
        } else {
            res.send({"message": "Something went wrong in holoconfig endpoint"})
        }
    });

});



// Socket.IO POST endpoint to send a sockets message
app.post('/socketsend', function(req, res) {
    //Socket IO client connected
    // io.emit('push', {'data': 'Hi Santy!'});
    io.emit('push', req.body);
    res.send("ok");
});

// catch 404 and forwarding to error handler
app.use(function(req, res, next) {
    var err = new Error('Not Found');
    err.status = 404;
    next(err);
});

/// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
    app.use(function(err, req, res, next) {
        res.status(err.status || 500);
        res.render('error', {
            message: err.message,
            error: err
        });
    });
}

// production error handler
// no stacktraces leaked to user
app.use(function(err, req, res, next) {
    res.status(err.status || 500);
    res.json({
        message: err.message,
        error: {}
    });
});

//Server-side requested socket send request
io.on('connection', function(client) {  
    console.log('Client connected...');
    /* ### TESTING CODE ### */
    io.on('beep', function(action){
        console.log('beep heard')
    });
    
});



module.exports = app;

// START THE SERVER
// =============================================================================
app.listen(port);
server.listen(4200);
console.log('Magic happens on port ' + port);
console.log('Sockets wizardy on port 4200');
```

#### Server Setup Class
```JS
var request = require('request');
var SonosDM = require('./app/models/sonosController');
var WinkDM = require('./app/models/winkController');

module.exports = {

  supportedDevices: {
        "Sonos Speaker": "Ventana/Prefabs/MusicController",
        "light_bulbs": "Ventana/Prefabs/LightController",
        "powerstrips":"Ventana/Prefabs/PowerStripController"
    },

  getDevices: function (callback) {
    // Get Wink Data 

    var paired = [];
    var unpaired = [];

    request(BASESERVER + ":" +  port + "/sonos/devices", function(error, response, body){
        if(response.statusCode == 200){
            var responseJson = JSON.parse(body);

            var sonosPaired = responseJson["paired_devices"];
            var sonosUnpaired = responseJson["unpaired_decies"];

            if(sonosPaired){
                paired = paired.concat(sonosPaired);
            }

            if(sonosUnpaired){
                unpaired = unpaired.concat(sonosUnpaired);
            }
        }
        request(BASESERVER + ":" +  port + "/wink/devices", function(error, response, body){
            if(!error){
                var responseJson = JSON.parse(body);

                var winkPaired = responseJson["paired_devices"];
                var winkUnPaired = responseJson["unpaired_devices"];

                if(winkPaired){
                    paired = paired.concat(winkPaired);
                }

                if(winkUnPaired){
                    unpaired = unpaired.concat(winkUnPaired);
                }

                return callback({"paired": paired, "unpaired": unpaired});
            }
        });  
    });
  },

  // gets a json specific for the HoloLens required config file
  getConfig: function(callback) {

    var configJSON = {}; 

    configJSON["User"] = "VentanaUser"; //hard coded for now, until I know how it updates
    configJSON["VentanaMarks"] = []

    var vmIndex = 0; //uses to index the VentanaMarks

    request(BASESERVER + ":" +  port + "/sonos/devices", function(error, response, body){
        if(response.statusCode == 200){
            var responseJson = JSON.parse(body);
            
            var sonosPaired = responseJson["paired_devices"];

            sonosPaired.forEach(function(item, index) {
                var tempSonos = {};
                tempSonos["id"] = "0x0" + (item._id).toString(16);
                tempSonos["name"] = item.device_name;
                tempSonos["path"] = item.controller;
                configJSON.VentanaMarks[vmIndex] = tempSonos;
                vmIndex++;
            });

        }
        request(BASESERVER + ":" +  port + "/wink/devices", function(error, response, body){
            if(!error){
                var responseJson = JSON.parse(body);
                
                var winkPaired = responseJson["paired_devices"];

                winkPaired.forEach(function(item, index){
                    var tempWink = {};
                    tempWink["id"] = "0x0" + (item._id).toString(16);
                    tempWink["name"] = item.device_name;
                    tempWink["path"] = item.controller;
                    configJSON.VentanaMarks[vmIndex] = tempWink; 
                    vmIndex++;
                });
            }
            return callback(configJSON); //even if no paired devices, the structure for config is still sent back
        });  
    });

 
  },

  getSonos: function (callback) {
    // Get Sonos Data 

    var paired = [];
    var unpaired = [];

    request(BASESERVER + ":" +  port + "/sonos/devices", function(error, response, body){
        if(response.statusCode == 200){
            var responseJson = JSON.parse(body);

            var sonosPaired = responseJson["paired_devices"];
            var sonosUnpaired = responseJson["unpaired_devices"];

            if(sonosPaired.length > 0){
                paired = paired.concat(sonosPaired);
            }

            if(sonosUnpaired.length > 0){
                unpaired = unpaired.concat(sonosUnpaired);
            }  

            return callback({"paired": paired, "unpaired": unpaired});
        }
        else {
            return callback({"paired": [], "unpaired": []});
        }
        

    });
  },

  getWink: function (callback) {
    // Get Wink Data 

    var paired = [];
    var unpaired = [];

    request(BASESERVER + ":" +  port + "/wink/devices", function(error, response, body){
        if(error){
            // No Wink Devices Found
            return callback({"paired": [], "unpaired": []});
        }
        var responseJson = JSON.parse(body);

        var winkPaired = responseJson["paired_devices"];
        var winkUnPaired = responseJson["unpaired_devices"];

        if(winkPaired){
            paired = paired.concat(winkPaired);
        }

        if(winkUnPaired){
            unpaired = unpaired.concat(winkUnPaired);
        }

        return callback({"paired": paired, "unpaired": unpaired});

    });
    
  },

  getUsedIds: function (callback) {
    // get ids (vumark) that have been used
    var ids = []

    WinkDM.find(function(err, wink) {
        if (!err){
            wink.forEach(function(id){
                ids.push(id["_doc"]["_id"])
            });
                SonosDM.find(function(err, sonos) {
                if (!err){
                    sonos.forEach(function(id){
                        ids.push(id["_doc"]["_id"])
                    });

                    return callback(ids);
                }
            }).select('_id');
        }
    }).select('_id');
   
   },

   saveNewDevice: function (object, callback) {

        console.log(JSON.stringify(object));
        if (object == null) {
            console.log("ERROR: object to save was NULL");
            return callback(null);
        } else {
            if (object.vendor == "1") {
                // vendor is sonos
                request({
                    method: 'POST',
                    url: BASESERVER + ":" +  port + "/sonos/",
                    body: {
                        '_id' : object._id,
                        'device_id' : object.device_id,
                        'controller' : object.controller,
                        'vendor_logo' : 'https://lh6.googleusercontent.com/-Px2Steg_XRM/AAAAAAAAAAI/AAAAAAAAFa4/kpB3EVdNHGw/s0-c-k-no-ns/photo.jpg'
                    },
                    json: true
                }, function(error, response, body){
                    if (!error && response.statusCode == 200) {
                        return callback(object._id);
                    } else {
                        return callback(null);
                    } 
                });

            } else if (object.vendor == "2") {
                // vendor is wink
                request({
                    method: 'POST',
                    url: BASESERVER + ":" +  port + "/wink/",
                    body: {
                        '_id' : object._id,
                        'device_id' : object.device_id,
                        'device_type': object.device_type,
                        'device_name' : object.device_name,
                        'controller': object.controller,
                        'vendor_logo' : 'https://www.winkapp.com/assets/mediakit/wink-logo-icon-knockout-50235153b274cdf35ef39fb780448596.png',
                        'vendor': object.vendor
                    },
                    json: true
                }, function(error, response, body){
                    if (!error && response.statusCode == 200) {
                        return callback(object._id);
                    } else {
                        return callback(null);
                    } 
                });

            } else {
                // vendor not sonos or wink
                console.log("Incorrect vendor number entered: " + object.vendor + " is not a supported number");
            }

        }
   },

  removeDevice: function(id, callback) {
      //Remove a device. magically. I don't know how this will work. Try/Catch?

      //Is it sonos?
      SonosDM.findById(id, function(err, res){
        if (err | !res){
            WinkDM.findById(id, function(err, res){
                if (err | !res){
                    return callback(null);
                }
            }).remove().exec();
        } else{   // Found in Sonos
            //
        }
      }).remove().exec()

      return callback("ok");
  }
};
```

### HoloHub Web Application

#### Add a Device (add.ejs)
```JS
<!-- views/pages/add.ejs -->

<!DOCTYPE html>
<html lang="en">
<head>
    <% include ../partials/head %>
</head>
<body class="container">

    <% include ../partials/nav %>

    <main>
        <!-- Page Content -->
        <div class="container">

        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12 text-center"  style="margin-top: -40px;"">
                <h1 class="page-header text-center">Add Device
                </h1>
            </div>
        </div>
        <!-- /.row -->

        <%if (!devices) { %>
            <div class="row">
                <div class="col-lg-12">
                    <h3 class="page-header">No devices found! Please check your network and connections.
                    </h3>
                </div>
            </div>
        <% } else { %>

            <% devices.forEach(function(device) { %>

                <div class="row">
                    <div class="col-xs-8 vcenter">

                        <%if ("device_name" in device) { %>
                            <h3> <%= device.device_name %> </h3>
                        <% } else{ %>
                            <h3> <%= device.device_id %> </h3>
                        <% } %>
                        <h4> <%= device.device_type %> </h4>  
                    </div>
                    <div class="col-xs-4 vcenter" style="padding-top: 30px;">
                        <a class="btn btn-primary" href="http://<%= host %>/savenew/<%= device.vendor %>/?device_id=<%= device.device_id %>&device_type=<%= device.device_type %>&device_name=<%= device.device_name %>&controller=<%= device.controller %>&vendor_logo=<%= device.vendor_logo %>"> Pair Device <span class="glyphicon glyphicon-chevron-right"></span></a>
                    </div>
                </div>
                <!-- /.row -->

                <hr>
            
            <% }); %>
        <% }; %>


    </main>

    [... abstracted ...]
    
</body>
</html>
```

#### Remove a Device (delete.ejs)
```JS
<!-- views/pages/index.ejs -->

<!DOCTYPE html>
<html lang="en">
<head>
    <% include ../partials/head %>
</head>
<body class="container">

    <% include ../partials/nav %>

    <main>
        <!-- Page Content -->
        <div class="container">

        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12 text-center"  style="margin-top: -40px;">
                <h1 class="page-header">Remove a Device
                </h1>
            </div>
        </div>
        <!-- /.row -->

        <% devices.forEach(function(device) { %>

            <div class="row">
                <div class="col-xs-3 vcenter">
                    <img class="img-responsive" src="<%= device.vendor_logo %>" height="400" alt="">
                </div>
                <div class="col-xs-5 text-center vcenter">

                    <%if ("device_name" in device) { %>
                        <h4> <%= device.device_name %> </h4>
                    <% } else{ %>
                        <h4> <%= device.device_id %> </h4>
                    <% } %>
                    <h4> <%= device.device_type %> </h4>  
                </div>
                <div class="col-xs-3 vcenter" style="padding-top: 10px;">

                    <%if ("device_name" in device) { %>
                        <a class="btn btn-danger" onclick="return confirm('Removing this device will require you to remove it from the HoloLens manually. Are you sure you want to continue?');" href="http://<%= host %>/remove/<%= device._id %>">Delete</a>
                    <% } else{ %>
                        <a class="btn btn-danger" onclick="return confirm('Removing this device will require you to remove it from the HoloLens manually. Are you sure you want to continue?');" href="http://<%= host %>/remove/<%= device._id %>">Delete</a>
                    <% } %>
                </div>
            </div>
            <!-- /.row -->

            <hr>
        
        <% }); %>


    </main>

    [... abstracted ...]
    
</body>
</html>
```
#### Main App View (index.ejs)
```JS
<!-- views/pages/index.ejs -->

<!DOCTYPE html>
<html lang="en">
<head>
    <% include ../partials/head %>
</head>
<body class="container">

    <% include ../partials/nav %>

    <main>
        <!-- Page Content -->
        <div class="container">

        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">Device List
                </h1>
            </div>
        </div>
        <!-- /.row -->
        <%if (!devices) { %>
            <div class="row">
                <div class="col-lg-12">
                    <h3 class="page-header">No Devices Paired. Add a new device to get started!
                    </h3>
                </div>
            </div>
        <% } else { %> 
            <% devices.forEach(function(device) { %>

                <div class="row">
                    <div class="col-xs-3 vcenter">
                        <img class="img-responsive" src="<%= device.vendor_logo %>" height="400" alt="">
                    </div>
                    <div class="col-xs-5 text-center vcenter">

                        <%if ("device_name" in device) { %>
                            <h4> <%= device.device_name %> </h4>
                        <% } else{ %>
                            <h4> <%= device.device_id %> </h4>
                        <% } %>
                        <h4> <%= device.device_type %> </h4>  
                    </div>
                    <div class="col-xs-3 vcenter" style="padding-top: 10px;">

                        <%if ("device_name" in device) { %>
                            <a class="btn btn-primary" href="http://<%= host %>/vumark/<%= device._id %>/<%= device.device_name %>">Open <br /> VuMark<span class="glyphicon glyphicon-chevron-right"></span></a>
                        <% } else{ %>
                            <a class="btn btn-primary" href="http://<%= host %>/vumark/<%= device._id %>/<%= device.device_id %>">Open <br /> VuMark<span class="glyphicon glyphicon-chevron-right"></span></a>
                        <% } %>
                    </div>
                </div>
                <!-- /.row -->

                <hr>
            
            <% }); %>
        <% }; %>


    </main>

    [... abstracted ...]
    
</body>
</html>
```

#### Vendor Selection View
```JS
<!-- views/pages/index.ejs -->

<!DOCTYPE html>
<html lang="en">
<head>
    <% include ../partials/head %>
</head>
<body class="container">

    <% include ../partials/nav %>

    <main>
        <!-- Page Content -->
        <div class="container">

        <!-- Page Heading -->
        <div class="row">
            <div class="col-lg-12 text-center"  style="margin-top: -40px;"">
                <h1 class="page-header text-center">Add Device
                </h1>
            </div>
        </div>
        <!-- /.row -->

        <!-- Project One -->
        <div class="row">
            <div class="col-xs-4 vcenter">
                <img class="img-responsive" src="https://lh6.googleusercontent.com/-Px2Steg_XRM/AAAAAAAAAAI/AAAAAAAAFa4/kpB3EVdNHGw/s0-c-k-no-ns/photo.jpg" height="200" alt="">
            </div>
            <div class="col-xs-8 vcenter" style="padding-top: 30px;">
                <a class="btn btn-primary" href="http://<%= host %>/addSonos/"> Connect to Sonos Devices <span class="glyphicon glyphicon-chevron-right"></span></a>
            </div>
        </div>
        <!-- /.row -->

        <hr>

        <!-- Project Two -->
        <div class="row">
            <div class="col-xs-4 vcenter">
                <img class="img-responsive" src="https://www.winkapp.com/assets/mediakit/wink-logo-icon-knockout-50235153b274cdf35ef39fb780448596.png" height="200" alt="">
            </div>
            <div class="col-xs-8 vcenter" style="padding-top: 30px;">
                <% if(requestWink) { %>
                    <a class="btn btn-primary" href="http://<%= host %>/connect/wink/"> Connect to Wink Devices <span class="glyphicon glyphicon-chevron-right"></span></a>
                <% }else { %>
                    <a class="btn btn-primary" href="http://<%= host %>/addWink/"> Connect to Wink Devices <span class="glyphicon glyphicon-chevron-right"></span></a>
                <% }; %> 
            </div>
        </div>
        <!-- /.row -->


    [... abstracted ...]
    
</body>
</html>
```
