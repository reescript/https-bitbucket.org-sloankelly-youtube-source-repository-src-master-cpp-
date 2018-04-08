using System;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

    public string resourceFile = "script";

    public string defaultLanguage = "en";

    public string overrideLanguage = "";

    public string[] GetText(string textKey)
    {
        string[] tmp = new string[] { };
        if (lines.TryGetValue(textKey, out tmp))
            return tmp;

        return new string[] { "<color=#ff00ff>MISSING TEXT FOR '" + textKey + "'</color>" };
    }

    private void Awake()
    {
        var json = LoadScriptFile();
        var voText = JsonUtility.FromJson<VoiceOverText>(json);

        foreach (var t in voText.lines)
        {
            lines[t.key] = t.line;
        }
    }

    private string LoadScriptFile()
    {
        var countryCode = LanguageHelper.Get2LetterISOCodeFromSystemLanguage();
        if (!string.IsNullOrEmpty(overrideLanguage))
        {
            countryCode = overrideLanguage;
        }

        var codes = new string[] { countryCode, defaultLanguage };

        foreach (var code in codes)
        {
            string scriptFileName = resourceFile + "." + code;
            var textAsset = Resources.Load<TextAsset>(scriptFileName);
            if (textAsset != null)
            {
                return textAsset.text;
            }
        }

        return "";
    }
}
