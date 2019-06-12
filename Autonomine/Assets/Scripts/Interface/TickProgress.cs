using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickProgress : MonoBehaviour
{
    private Image progress;

    // Start is called before the first frame update
    void Start()
    {
        progress = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        progress.fillAmount = GameManager.manager.lerp;
    }
}
