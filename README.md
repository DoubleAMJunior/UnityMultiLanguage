# UnityMultiLanguage
This is a pack of components that helps by labeling UI texts to have multiple text values such as different values for different languages throughout the labels.

## How to use
These components use Json files to save and load "key”: “value" pairs, the keys then can be used as label for unity UI texts and then load the corresponding value in the chosen file to be used for the said unity UI text.  
This repository contains a sample project that uses the components in order to deploy the multi-language texts, you can copy and use the scripts in the Scripts folder in any other project to use them.

### Setup Manual
In order to use the multi-language texts you should copy all 3 scripts (JsonToDictionary, LanguageManager and MultiLanguageText) to your project. After importing the scripts you need to have a game object named LanguageManager with LanguageManager behaviour added in every scene you want to use the multi-language texts. 
This LanguageManager behaviour has a list called AvailableLanguageList, where you can add references to your desired language Json file; You can also add a font to each selected language which will be used on all UI text components with it. 
CurrentLanguageInUseIndex indicates the current setting in use for texts so you only need to add a MultiLanguageText behaviour to unity game objects with UIText components and set a desired label in the MultiLanguageText behaviour so the UI Text's text value would be set by the value of label found in the current language Json file at run time.
This steps and the setups that have been done and can be viewed in SampleScene found in Scenes folder and 3 predefined language Json files for English, Portuguese and German can be found in Resources folder.
### scripting 
If the LanguageManager game object is created, LanguageManager will create a static reference to itself named "Instance" that can be used to access to LanguageManager's functionalities. The functionality you want to use in scripts is the function ChangeLanguageTo(int langIndex) which changes the current Language setting to another one indexed by langIndex in the AvailableLanguageList that contains a list of language Json files.
There is also an event triggered whenever a language file has been changed, you can subscribe to this event by using onLanguageChanged public event.

### Errors 
Most problems right now are due to LanguageManager game object not being available in the scene and the language Json file not being able to get parsed. For the second problem there are some errors and warnings implemented that will be issued in runtime.

### Thank you
Thank you for using this components for any additonal help you can contact me by email: aliabbasi1377@gmail.com or in discord:BeegUnrOel#0837
Special thanks to João Paulo and Omar Ismail for helping with the language Json files and Mohammad Alinejad for helping reformatting this markdown source.