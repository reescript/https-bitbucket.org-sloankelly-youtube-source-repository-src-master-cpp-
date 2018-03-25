using System;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

    public string resourceFile = "script";

    public string[] GetText(string textKey)
    {
        string[] tmp = new string[] { };
        if (lines.TryGetValue(textKey, out tmp))
            return tmp;

        return new string[] { "<color=#ff00ff>MISSING TEXT FOR '" + textKey + "'</color>" };
    }

    private void Awake()
    {
        var textAsset = Resources.Load<TextAsset>(resourceFile);
        var voText = JsonUtility.FromJson<VoiceOverText>(textAsset.text);

        foreach (var t in voText.lines)
        {
            lines[t.key] = t.line;
        }
    }
}
