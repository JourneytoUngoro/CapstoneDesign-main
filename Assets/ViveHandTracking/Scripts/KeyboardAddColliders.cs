using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardAddColliders : MonoBehaviour
{
    private Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>(true);
        foreach (Button button in buttons)
        {
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            button.gameObject.AddComponent<BoxCollider>();
            BoxCollider boxCollider = button.GetComponent<BoxCollider>();
            boxCollider.size = new Vector3(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y, 1);
            boxCollider.center = new Vector3((0.5f - rectTransform.pivot.x) * boxCollider.size.x, (0.5f - rectTransform.pivot.y) * boxCollider.size.y, 0.0f);
        }
    }
}
