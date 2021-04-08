# Game-Based Learning

This project concerns a game developed to supplement the Practical Algorithms course, at the Univeristy
of Glasgow. The game is a puzzle game developed in Unity. 

## File structure 
The file structure for the game takes a standard Unity template, the Scripts are stored in their own folder within 
an Assets folder --> GameBasedLearning\GameBasedLearning. And the tests are stored in a Tests folder, which contains a reference the the scripts in the form of an assembly file.
*IMPORTANT*, as this is a game, the project holds meta files for most of the files, so for example a .cs file in the scripts folder will have a meta file associated.

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
 3. Launch Unity Hub Application
 4. In Unity Hub , go to Projects -> Add . Then, browse to where that project is located. Select that project.
 5. Launch the project by double clicking on it, Unity Engine Application will now open
 6. Navigate to  File -> Build and Run 
 7. Copy layout from BuildSettings.png, from within the submission folder, this step is very important and is hard to explain with text, which is why an image is provided.
 8. Click Build and Run
 9. Run .exe which is built from Unity

## Requirements

* Unity Game Engine version 2019.4.21f1 
* Tested on Windows 10


## Test steps

1. Follow steps in Build instructions to get project into Unity Game Engine
2. Navigate to Window-> General -> Test Runner
3. Navigate to 'PlayMode' tab
4. Click run all tests




