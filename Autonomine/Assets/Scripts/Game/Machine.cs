using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    private static int uniqueID = 0;
    public static int GenerateID() { return uniqueID++; }

    public int ID { private set; get; }

    protected bool runOnTick = true, actionTaken = false;
    protected Dictionary<string, object> memory;
    protected Dictionary<string, Command.Method> methods;

    public string Script { private set; get; }
    public void SetScript(string script) {
        this.Script = script;
        commands = ScriptParser.ParseCommandStrings(script);
    }
    private string[] commands;

    private void Start() {
        ID = GenerateID();
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

    private void OnMouseDown() {
        UIManager.ui.Focus(this);
    }
}
