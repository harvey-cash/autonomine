using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScriptWindow : MonoBehaviour
{
    public InputField input;

    public bool IsFocused() {
        return input.isFocused;
    }

    public void LoadScript(string script) {
        input.text = script;
    }

    public string GetText() { return input.text; }
}
