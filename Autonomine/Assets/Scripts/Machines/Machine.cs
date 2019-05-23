using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Machine : MonoBehaviour
{
    private static int uniqueID = 0;
    public static int GenerateID() { return uniqueID++; }

    public int ID { private set; get; }

    protected bool runOnTick = true, actionTaken = false;
    protected Dictionary<string, object> memory;
    protected Dictionary<string, Command.Method> methods;


    /* ~~~~~~ INITIALISATION ~~~~~~~ */

    private void Start() {
        ID = GenerateID();
        SetupLabel();

        memory = new Dictionary<string, object>();
        methods = Library.GenerateLibrary(GetMethodsDict());

        GameManager.manager.onTick.AddListener(TickListener);
    }

    private void TickListener() {
        if (runOnTick && commands != null) {
            actionTaken = false;
            OnTick(); // allow sub class to put whatever it likes in memory, etc.
            (memory, _) = Command.Run(methods, memory, commands);
        }
    }

    public abstract Dictionary<string, Command.Method> GetMethodsDict();
    public abstract void OnTick();


    /* ~~~~~~ USER WRITTEN CODE ~~~~~~~ */

    private string[] commands;
    public string Script { private set; get; }
    public void SetScript(string script) {
        this.Script = script;
        commands = ScriptParser.ParseCommandStrings(script);
    }
    
    // Run one-offs (i.e. from terminal)
    public void RunCommands(string[] commandArray) {
        (memory, _) = Command.Run(methods, memory, commandArray);
    }

    /* ~~~~~~ LABEL AND SELECTING ~~~~~~~ */

    protected Canvas labelCanvas = null;
    protected Text labelText = null;

    private void SetupLabel() {
        labelCanvas = GetComponentInChildren<Canvas>();
        if (labelCanvas != null) {
            labelCanvas.enabled = false;
            labelText = labelCanvas.GetComponentInChildren<Text>();
        }

        SetName(DefaultName() + ": " + ID);
    }
    protected abstract string DefaultName();

    public string MachineName { private set; get; }
    public void SetName(string name) {
        MachineName = name;

        if (labelText != null) { labelText.text = name; }        
    }

    private bool focused = false;
    public void SetFocus(bool val) {
        focused = val;
        if (labelCanvas != null) { ShowLabel(val); }
    }
    private void ShowLabel(bool val) {
        labelCanvas.enabled = val;
    }

    private void OnMouseDown() {
        UIManager.ui.Focus(this);
    }

    private void OnMouseEnter() {
        if (labelCanvas != null) {
            ShowLabel(true);
        }
    }
    private void OnMouseExit() {
        if (labelCanvas != null && !focused) {
            ShowLabel(false);
        }
    }
}
