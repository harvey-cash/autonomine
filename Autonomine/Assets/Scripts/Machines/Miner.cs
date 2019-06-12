using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Machine
{
    private int ore, pos;
    private bool nearPrinter;

    private int planDirection;
    private void SetDirection(int direction) {
        planDirection = direction;
    }

    public override (string, Func<object[], object>)[] GetMethods() {

        object Move(object[] parameters) {
            planDirection = Mathf.RoundToInt(Mathf.Clamp((float)parameters[0], -1, 1));
            Debug.Log(planDirection);
            return null;
        }

        return new (string, Func<object[], object>)[] {
            ("move", Move)
        };
    }

    public override string GetMethodsList() {
        return "move(dX)";
    }

    public override void OnStart() {
        //throw new NotImplementedException();
    }

    public override void OnTick() {
        int planX = Mathf.RoundToInt(transform.position.x) + planDirection;
        if (planX >= GameManager.manager.railFirst 
            && planX <= GameManager.manager.railLast) {

            transform.localPosition = new Vector3(
                planX,
                transform.position.y, 
                transform.position.z
            );
        }

        pos = Mathf.RoundToInt(transform.position.x);
        nearPrinter = pos == Mathf.RoundToInt(GameManager.manager.player.transform.position.x);
        memory["pos"] = pos;
        memory["nearPrinter"] = nearPrinter;
    }

    protected override string DefaultName() {
        return "Miner";
    }

    public override string GetVariablesList() {
        return "int ore\nint pos\nbool nearPrinter";
    }
}
