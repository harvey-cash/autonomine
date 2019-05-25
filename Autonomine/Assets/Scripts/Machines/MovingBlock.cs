using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : Machine {
    protected override string DefaultName() { return "MovingBlock"; }

    private object Move(object[] parameters) {
        // Only allowed to move physically once per tick
        if (actionTaken) { return null; }

        float x = (float)parameters[0];
        float z = (float)parameters[1];

        transform.Translate(new Vector3(x, 0, z));
        actionTaken = true;

        return null;
    }

    public override (string, Func<object[], object>)[] GetMethods() {
        return new (string, Func<object[], object>)[] {
            ("move", Move)
        };
    }

    public override string GetMethodsList() {
        return "move(dX,dZ)";
    }

    public override void OnTick() {
        memory["xPos"] = transform.position.x;
        memory["zPos"] = transform.position.z;        
    }
}
