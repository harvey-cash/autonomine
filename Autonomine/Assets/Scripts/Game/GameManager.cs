using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public UnityEvent onTick;

    // Start is called before the first frame update
    private void Awake() {
        manager = this;

        if (onTick == null)
            onTick = new UnityEvent();

        InvokeRepeating("DoTick", Config.TICK_TIME, Config.TICK_TIME);
    }

    private void DoTick() {
        onTick.Invoke();
    }
}
