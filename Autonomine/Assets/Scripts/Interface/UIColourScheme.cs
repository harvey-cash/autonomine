using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIColourScheme : MonoBehaviour
{
    public Color p, s, t;
    public ColorBlock selectables;

    public List<Image> primaryGroup = new List<Image>(), 
        secondaryGroup = new List<Image>(), 
        tertiaryGroup = new List<Image>();

    public List<Selectable> selectableGroup = new List<Selectable>();

    private void OnGUI() {
        SetColourScheme(p, s, t, selectables);
    }

    public void SetColourScheme(Color primary, Color secondary, Color tertiary, 
        ColorBlock select) {
        SetColourGroup(primaryGroup, primary);
        SetColourGroup(secondaryGroup, secondary);
        SetColourGroup(tertiaryGroup, tertiary);


        for (int i = 0; i < selectableGroup.Count; i++) {
            if (selectableGroup[i] != null) {
                selectableGroup[i].colors = select;
            }
        }
    }

    private void SetColourGroup(List<Image> group, Color colour) {
        for (int i = 0; i < group.Count; i++) {
            if (group[i] != null) {
                group[i].color = colour;
            }
        }
    }
}
