using System;
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
    protected Dictionary<string, Method> methods;


    /* ~~~~~~ INITIALISATION ~~~~~~~ */

    private void Start() {
        ID = GenerateID();
        SetupLabel();

        memory = new Dictionary<string, object>();
        methods = MethodBuilder.GenerateLibrary(GetMethods());

        GameManager.manager.onTick.AddListener(TickListener);
    }

    private void TickListener() {
        if (runOnTick && commands != null) {
            actionTaken = false;
            OnTick(); // allow sub class to put whatever it likes in memory, etc.
            (memory, _) = SandSharp.Run(methods, memory, commands);
        }
    }

    public abstract (string, Func<object[], object>)[] GetMethods();
    public abstract string GetMethodsList();
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
        (memory, _) = SandSharp.Run(methods, memory, commandArray);
    }

    /* ~~~~~~ LABEL AND SELECTING ~~~~~~~ */

    protected UILabel uiLabel;

    private void SetupLabel() {
        uiLabel = Instantiate(UIManager.ui.labelPrefab).GetComponent<UILabel>();
        uiLabel.Initialise(this, transform.position + new Vector3(1, 1, 0) * transform.localScale.y);
        uiLabel.Show(false);
        uiLabel.SetMethods(GetMethodsList());
        SetName(DefaultName() + ": " + ID);
    }
    protected abstract string DefaultName();

    public string MachineName { private set; get; }
    public void SetName(string name) {
        MachineName = name;
        uiLabel.SetName(name);
    }

    private bool focused = false;
    public void SetFocus(bool val) {
        focused = val;
        uiLabel.Show(val);
    }

    private void OnMouseDown() {
        UIManager.ui.Focus(this);
    }

    private void OnMouseEnter() {
        uiLabel.Show(true);
    }
    private void OnMouseExit() {
        if (!focused) {
            uiLabel.Show(false);
        }
    }
}
