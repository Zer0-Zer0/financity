// Fonte de inspiração: https://youtu.be/oOQvhIg0ntg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/ThemeSO", fileName = "ThemeSO")]
public class ThemeSO : ScriptableObject
{
    [Header("Primary")]
    public Color primaryBG;
    public Color primaryText;
  
    [Header("Secondary")]
    public Color secondaryBG;
    public Color secondaryText;

    [Header("Tertiary")]
    public Color tertiaryBG;
    public Color tertiaryText;

    [Header("Other")]
    public Color disabled;
}
