using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellphoneTweenControl : MonoBehaviour
{

    float upOrDown = 1f;
    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Q)){
            MoveObject();
        }
    }

    void MoveObject()
    {
        LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 396 * upOrDown, 0.3f);
        upOrDown *=-1f;
    }
}
