using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager ui;

    private UIColourScheme colourScheme;

    private void Awake() {
        ui = this;
        colourScheme = GetComponent<UIColourScheme>();
    }

    /* ~~~~~ UI WINDOWS ~~~~~ */

    public Canvas labelsCanvas;
    public GameObject labelPrefab;

    public ScriptWindow scriptWindow;
    public InputField nameInputField;

    public bool ScriptFocused() { return scriptWindow.IsFocused(); }

    /* ~~~~ RUN CODE ON MACHINE ~~~~ */

    public void RunOnMachine(string script) {
        if (focus == null) { Debug.Log("No machine focused!"); return; }

        Terminal.terminal.Print("<" + focus.MachineName + "> " + script);
        Command[] commands = ScriptParser.ParseCommandStrings(script);
        focus.RunCommands(commands);
    }

    /* ~~~~~ ACCESS MACHINES ~~~~~~~ */

    public Machine focus;

    public void Focus(Machine machine) {
        if (focus == null || machine.ID != focus.ID) {
            machine.SetFocus(true);
            if (focus != null) { focus.SetFocus(false); }
            focus = machine;

            nameInputField.text = focus.MachineName;
            ShowScript(focus.Script);
        }
    }

    //ToDo: alert about saving previous script?
    public void ShowScript(string script) {
        scriptWindow.LoadScript(script);
    }

    public void OnSubmitScript() {
        if (focus == null) {
            Debug.Log("No machine selected!");
            return;
        }

        focus.SetScript(scriptWindow.GetText());
    }

    public void OnSubmitName() {
        if (focus != null) { focus.SetName(nameInputField.text); }
    }
}
