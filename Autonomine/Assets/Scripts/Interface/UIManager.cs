using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager ui;

    private void Awake() {
        ui = this;
    }

    /* ~~~~~ UI WINDOWS ~~~~~ */
    public ScriptWindow scriptWindow;

    public bool ScriptFocused() { return scriptWindow.IsFocused(); }

    /* ~~~~~ ACCESS MACHINES ~~~~~~~ */

    public Machine focusedMachine;

    public void Focus(Machine machine) {
        if (focusedMachine == null || machine.ID != focusedMachine.ID) {
            focusedMachine = machine;

            ShowScript(focusedMachine.Script);
        }
    }

    //ToDo: alert about saving previous script?
    public void ShowScript(string script) {
        scriptWindow.LoadScript(script);
    }

    public void OnApply() {
        if (focusedMachine == null) {
            Debug.Log("No machine selected!");
            return;
        }

        focusedMachine.SetScript(scriptWindow.GetText());
    }
}
