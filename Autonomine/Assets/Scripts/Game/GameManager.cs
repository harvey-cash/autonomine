using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    private void Awake() {
        manager = this;
        InitialiseTick();
    }

    private void Start() {
        InitialisePlayer();
    }

    /* ~~~~~~ PLAYER ~~~~~~ */

    public PlayerMachine player;

    private void InitialisePlayer() {
        UIManager.ui.Focus(player);
    }

    /* ~~~~~~ GAME TICK ~~~~~~ */

    public UnityEvent onTick;

    private void InitialiseTick() {
        if (onTick == null) { onTick = new UnityEvent(); }
        InvokeRepeating("DoTick", Config.TICK_TIME, Config.TICK_TIME);
    }

    private void DoTick() {
        onTick.Invoke();
    }
}
