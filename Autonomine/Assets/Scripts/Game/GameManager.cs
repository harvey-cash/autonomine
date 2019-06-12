using System;
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

    public float TICK_TIME = 1f;

    public UnityEvent onTick;
    public float time { private set; get; }
    public float lerp { private set; get; }

    private void InitialiseTick() {
        if (onTick == null) { onTick = new UnityEvent(); }
    }
    
    private void Update() {
        DoTick();
    }
    
    private void DoTick() {
        time += Time.deltaTime;
        lerp = time / TICK_TIME;

        if (time >= TICK_TIME) {
            lerp = 1;
            time = 0;
            onTick.Invoke();
        }
    }

    /* ~~~~~~ GRID ~~~~~~~ */

    public int railFirst = 1;
    public int railLast = 5;
}
