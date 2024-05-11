using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAddCollider : MonoBehaviour
{
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        transform.gameObject.AddComponent<BoxCollider>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y, 1);
    }
}
