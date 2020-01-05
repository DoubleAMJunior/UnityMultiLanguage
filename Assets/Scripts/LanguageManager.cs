//created by AliAbbasiMoghaddam 2020
using System;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private static LanguageManager _instance;
  
    //gets the unique refrence from language manager game object in the scene
    public static LanguageManager Instance
    {
        get
        {
            if (_instance==null)
            {
                GameObject go=GameObject.Find("LanguageManager");
                _instance = go.GetComponent<LanguageManager>();
            }
            return _instance;
        }
    }
    
    public int currentLanguageInUseIndex;
    public List<LangaageEntry> AvailableLanguageList;
    
    
    
    private Dictionary<int, Dictionary<string, string>> loadedLanguages= new Dictionary<int, Dictionary<string, string>>();

    
    public delegate void onChangeLanguage();

    //event that is called when the language is changed multi language text should be listening 
    //to this event and change their text to the new language setting
    public static event onChangeLanguage onLanguageChanged;

    //function called to change the current language setting 
    public void ChangeLanguageTo(int langIndex)
    {
        if (!loadedLanguages.ContainsKey(langIndex))
        {
            if (langIndex<AvailableLanguageList.Count && langIndex>=0)
            {
                Dictionary<string, string> requestedLang;
                new JsonToDictionary(AvailableLanguageList[langIndex].languageJsonFile, out requestedLang);
                loadedLanguages.Add(langIndex,requestedLang);
            }
            else
            {
                Debug.LogError("Requested index from Language Manager does not exist.");
            }
        }
        currentLanguageInUseIndex = langIndex;
        onLanguageChanged();
    }


    //function used by texts to get their corresponding value from their label  
    public string getValue(string label)
    {
        string ans = loadedLanguages[currentLanguageInUseIndex][label];
        if (ans!=null)
        {
            return ans;
        }
        Debug.LogError(label+"Was Not Found in "+currentLanguageInUseIndex+"th json file",
            AvailableLanguageList[currentLanguageInUseIndex].languageJsonFile);
        return label;
    }

    //used by text to acquire the font for current language setting
    public Font getFont()
    {
        return AvailableLanguageList[currentLanguageInUseIndex].font;
    }

    private void Awake()
    {
        ChangeLanguageTo(currentLanguageInUseIndex);
    }
}

//helper class to serialize data about the language settings and show them in inspector
[Serializable]
public class LangaageEntry
{
    public TextAsset languageJsonFile;
    public Font font;
}