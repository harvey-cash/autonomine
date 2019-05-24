using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScriptInputField : InputField
{
    public void Activate(PointerEventData eventData) {
        OnPointerDown(eventData);

        /*
        IEnumerator waitThenDoShit(PointerEventData eData) {
            yield return new WaitForEndOfFrame();
            OnPointerClick(eData);
        }
        StartCoroutine(waitThenDoShit(eventData));
        */
    }

    public override void OnPointerDown(PointerEventData eventData) {
        Debug.Log(caretPosition);
        ActivateInputField();

        IEnumerator CaretToClickPos() {
            yield return new WaitForEndOfFrame();
            Vector2 pos = ScreenToLocal(eventData.position);
            selectionAnchorPosition = caretPosition = GetCharacterIndexFromPosition(pos) + m_DrawStart;

            UpdateLabel();
            eventData.Use();
        }

        StartCoroutine(CaretToClickPos());
        
        /*
        EventSystem.current.SetSelectedGameObject(gameObject, eventData);

        base.OnPointerDown(eventData);

        // Only set caret position if we didn't just get focus now.
        // Otherwise it will overwrite the select all on focus.

        Vector2 pos = ScreenToLocal(eventData.position);
        selectionAnchorPosition = caretPosition = GetCharacterIndexFromPosition(pos) + m_DrawStart;

        UpdateLabel();
        eventData.Use();
        */
    }
    
}
