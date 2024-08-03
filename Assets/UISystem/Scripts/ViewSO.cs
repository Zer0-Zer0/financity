// Fonte de inspiração: https://youtu.be/oOQvhIg0ntg
using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/ViewSO", fileName = "ViewSO")]
public class ViewSO : ScriptableObject
{
    public ThemeSO theme;
    public RectOffset padding;
    public float spacing;
}
