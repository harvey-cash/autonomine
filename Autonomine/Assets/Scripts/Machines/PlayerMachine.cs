using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMachine : Machine {
    protected override string DefaultName() { return "Player"; }

    public override Dictionary<string, Command.Method> GetMethodsDict() {

        Command.Method Help = (methods, memory, name, paramStrings, subscript) => {
            Terminal.terminal.Print(ScriptParser.DictionaryKeys(methods));
            return (memory, null);
        };

        Dictionary<string, Command.Method> methodsDict = new Dictionary<string, Command.Method>() {
            { "help", Help }
        };

        return methodsDict;
    }

    public override void OnTick() {
        
    }    
}
