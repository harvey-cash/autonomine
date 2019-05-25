using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILabel : MonoBehaviour
{
    static Vector3 offset;

    private Machine machine;
    private RectTransform rect;
    public Text nameText;
    public Text methodsText;

    private void Awake() {
        rect = GetComponent<RectTransform>();        
    }

    public void Initialise(Machine machine, Vector3 localPosition) {
        transform.SetParent(UIManager.ui.labelsCanvas.transform);
        this.machine = machine;
        offset = localPosition - machine.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rect.position = Camera.main.WorldToScreenPoint(machine.transform.position + offset);
    }

    public void Show(bool show) {
        GetComponent<Image>().enabled = show;
        nameText.enabled = show;
        methodsText.enabled = show;
    }

    public void SetName(string name) {
        nameText.text = name;
    }

    public void SetMethods(string methodsList) {
        methodsText.text = methodsList;
    }
}
