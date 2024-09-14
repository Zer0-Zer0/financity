/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UISystem
{
    [CreateAssetMenu(fileName = "Text_", menuName = "ScriptableObjects/UISystem/Text")]
    public class TextSO : ScriptableObject
    {
        public TMP_FontAsset font;
        public float size;
    }
}