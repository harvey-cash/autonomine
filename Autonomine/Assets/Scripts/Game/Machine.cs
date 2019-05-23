using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    private static int uniqueID = 0;
    public static int GenerateID() { return uniqueID++; }

    public int ID { private set; get; }

    protected bool actionTaken = false;
    protected Dictionary<string, object> memory;
    protected Dictionary<string, Command.Method> methods;

    protected string[] commands = null;
    public void SetCommands(string[] commands) { this.commands = commands; }

    private void Start() {
        ID = GenerateID();
        memory = new Dictionary<string, object>();
        methods = new Dictionary<string, Command.Method>(Library.builtIns);

        GameManager.manager.onTick.AddListener(DoTick);
    }

    private void DoTick() {
        actionTaken = false;
        if (commands != null) {
            OnTick(); // allow sub class to put whatever it likes in memory, etc.
            (memory, _) = Command.Run(methods, memory, commands);
        }
    }

    public abstract Command.Method[] ExposeMethods();
    public abstract void OnTick();
}
