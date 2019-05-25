using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScriptInputField : InputField
{
    public override void OnPointerDown(PointerEventData eventData) {
        ActivateInputField();

        IEnumerator CaretToClickPos() {
            yield return new WaitForEndOfFrame();

            #pragma warning disable CS0618 // Type or member is obsolete
            Vector2 pos = ScreenToLocal(eventData.position);
            #pragma warning restore CS0618 // Type or member is obsolete
            selectionAnchorPosition = caretPosition = GetCharacterIndexFromPosition(pos) + m_DrawStart;

            UpdateLabel();
            eventData.Use();
        }

        StartCoroutine(CaretToClickPos());
    }
    
}
