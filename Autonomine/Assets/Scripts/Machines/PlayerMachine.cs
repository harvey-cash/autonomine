using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMachine : Machine {
    protected override string DefaultName() { return "Player"; }

    private object PrintHelp(object[] parameters) {
        Terminal.terminal.Print(ScriptParser.DictionaryKeys(methods));
        return null;
    }

    public override (string, Func<object[], object>)[] GetMethods() {
        return new (string, Func<object[], object>)[] {
            ("help", PrintHelp)
        };
    }

    public override string GetMethodsList() {
        return "help()";
    }

    public override void OnTick() {
        
    }    
}
