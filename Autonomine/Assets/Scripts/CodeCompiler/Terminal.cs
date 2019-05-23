using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour
{
    public static Terminal terminal;

    public InputField input;
    public Text history;

    // temporary
    Dictionary<string, object> memory = new Dictionary<string, object>();
    readonly Dictionary<string, Command.Method> methods = new Dictionary<string, Command.Method>(Library.builtIns);

    // Start is called before the first frame update
    void Start()
    {
        terminal = this;
    }

    public void Submit() {
        UIManager.ui.RunOnMachine(input.text);
        input.text = "";
    }

    public void Print(string log) {
        history.text += log + "\n";
    }
}
