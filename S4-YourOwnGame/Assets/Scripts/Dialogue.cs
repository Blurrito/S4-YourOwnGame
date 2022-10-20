using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string Name;
    public string String;
    public float ScreenTime;

    public Dialogue() { }
}

[System.Serializable]
public class Language
{
    public string Name;
    public List<Dialogue> DialogueLines;
    
    public Language() { }

    public Dialogue GetDialogueByName(string Name) => DialogueLines.FirstOrDefault(x => x.Name == Name);
}

[System.Serializable]
public class DialogueFile
{
    public List<Language> Languages;

    public DialogueFile() { }

    public Dialogue GetDialogueByName(string DialogueName, string SelectedLanguage)
    {
        Language TargetLanguage = Languages.FirstOrDefault(x => x.Name == SelectedLanguage);
        if (TargetLanguage != null)
            return TargetLanguage.GetDialogueByName(DialogueName);
        return null;
    }
}