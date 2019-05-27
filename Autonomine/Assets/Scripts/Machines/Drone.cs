using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Machine
{
    public override (string, Func<object[], object>)[] GetMethods() {

        object Move(object[] parameters) {
            // Only allowed to move physically once per tick
            if (actionTaken) { return false; }

            int dX = Mathf.RoundToInt(Mathf.Clamp((float)parameters[0], -1, 1));
            int dZ = Mathf.RoundToInt(Mathf.Clamp((float)parameters[1], -1, 1));

            GameManager.manager.PlaceAtGridPos(this, dX, dZ, out bool success);

            actionTaken = true;

            return success;
        }

        return new (string, Func<object[], object>)[] {
            ("move", Move)
        };
    }

    public override string GetMethodsList() {
        return "success = move(dX,dZ)";
    }

    public override void OnTick() {
        
    }

    protected override string DefaultName() {
        return "Drone";
    }
}
