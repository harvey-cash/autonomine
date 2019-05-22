using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScriptWindow : MonoBehaviour, IPointerClickHandler // 2
{
    public InputField input;

    public void OnPointerClick(PointerEventData eventData) // 3
     {
        input.Select();
        input.ActivateInputField();
    }
}
