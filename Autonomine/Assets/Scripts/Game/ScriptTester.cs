using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTester : MonoBehaviour
{
    public Text scriptTest;

    public void RunScript() {
        Command[] commands = ScriptParser.ParseCommandStrings(scriptTest.text);
        Dictionary<string, object> memory = new Dictionary<string, object>();
        Dictionary<string, Method> methods = MethodBuilder.GenerateLibrary(null);
        (memory, _) = SandSharp.Run(methods, memory, commands);
    }
}
