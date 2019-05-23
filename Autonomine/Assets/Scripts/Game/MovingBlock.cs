using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : Machine
{
    public override Dictionary<string,Command.Method> GetMethodsDict() {
        Command.Method MoveBlock = (methods, memory, name, paramStrings, subscript) => {
            object[] parameters = Command.EvaluateParameters(methods, paramStrings, memory);

            // Only allowed to move physically once per tick
            if (actionTaken) { return (memory, null); }

            float x = (float)parameters[0];
            float z = (float)parameters[1];

            transform.Translate(new Vector3(x, 0, z));
            actionTaken = true;

            return (memory, null);
        };

        Dictionary<string, Command.Method> methodsDict = new Dictionary<string, Command.Method>() {
            {"move", MoveBlock }
        };

        return methodsDict;
    }

    public override void OnTick() {
        memory["xPos"] = transform.position.x;
        memory["zPos"] = transform.position.z;        
    }
}
