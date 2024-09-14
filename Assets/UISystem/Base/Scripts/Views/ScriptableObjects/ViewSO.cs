/*
Fontes de inspiração:
    https://youtu.be/oOQvhIg0ntg
    https://youtu.be/4gUeUCdeiq8
*/

using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = "View_", menuName = "ScriptableObjects/UISystem/View")]
    public class ViewSO : ScriptableObject
    {
        public PaddingSO paddingData;
    }
}