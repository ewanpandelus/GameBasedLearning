# Game-Based Learning

This project concerns a game developed to supplement the Practical Algorithms course, at the University
of Glasgow. The game is a puzzle game developed in Unity. 

## Game Installer Download(For those at University of Glasgow)
https://gla-my.sharepoint.com/:u:/g/personal/2319069p_student_gla_ac_uk/EZFGXu5Iy4lMjhjjvo8cdQwB58LtDLTAU1BvOJmwh30o-g?e=vp381n

## File structure 
The file structure for the game takes a standard Unity template, the Scripts are stored in their own folder 'Scripts'. And the tests are stored in a 'Tests' folder, which contains a reference the the scripts in the form of an assembly file.
*IMPORTANT*, as this is a game, the project holds meta files for most of the files, so for example a .cs file in the scripts folder will have a meta file associated.
The raw survey data is stored in the folder SurveyData. GameBasedLearning folder holds the project files.
```
\2319069p
 \GameBasedLearning
  \Assets
   \Animations
   \Backgrounds
   \Clean Vector Icons
   \Fonts
   \Forest
   \Materials
   \Plugins
   \Prefabs
   \Scenes
   \Scripts
   \SoundClips
   \Sprites
   \SunnyLand
   \Tests
   \TextMesh Pro
   \tileset
  \Packages
  \ProjectSettings
 \SurveyData
```

## Build instructions/steps

 1. Download version 2019.4.21f1 of Unity Game Engine from https://unity.com/.
 2. unzip code and import project found from \GameBasedLearning, the root folder holds the survey data folder, the manual and license file, as well as the readme.
 3. Launch Unity Hub Application.
 4. In Unity Hub , go to Projects -> Add. Then browse to where that project is located. Select that project.
 5. Launch the project by double clicking on it, Unity Engine Application will now open.
 6. Navigate to  File -> Build and Run.
 7. Copy layout from BuildSettings.png, from within the submission folder, this step is very important and is hard to explain with text, which is why an image is provided. Add all scenes to build if none are within your Unity application.
 8. Click Build and Run.
 9. Run .exe which is built from Unity.

## Requirements

* Unity Game Engine version 2019.4.21f1 
* Tested on Windows 10


## Test steps

1. Follow steps in Build instructions to get project into Unity Game Engine.
2. Navigate to Window-> General -> Test Runner.
3. Navigate to 'PlayMode' tab.
4. Click run all tests.


## Running without Unity
The installer for the game BigOAdventure Setup(x86).exe can be double clicked, then follow the instructions to install onto a computer with Windows Operating System.
Your computer might not recognise the publisher, in this case, click 'more information' and follow through with the installation. The installed application can then be launched to run the game without the game engine. This will be included in the project submission for ease of access. To download the installer, click this link https://gla-my.sharepoint.com/:u:/g/personal/2319069p_student_gla_ac_uk/EZFGXu5Iy4lMjhjjvo8cdQwB58LtDLTAU1BvOJmwh30o-g?e=vp381n.
