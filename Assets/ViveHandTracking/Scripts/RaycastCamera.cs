using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ViveHandTracking;
using ViveHandTracking.Sample;

public class RaycastCamera : MonoBehaviour
{
    public GameObject raycastCamera;
    private SelectionLaser[] selectionLasers;

    private void Awake()
    {
        selectionLasers = GetComponents<SelectionLaser>();
        raycastCamera = GetComponentInChildren<Camera>(true).gameObject;
    }

    private void Update()
    {
        bool noHands = true;

        foreach (SelectionLaser selectionLaser in selectionLasers)
        {
            if (selectionLaser.laser.activeSelf)
            {
                var hand = selectionLaser.isLeft ? GestureProvider.LeftHand : GestureProvider.RightHand;
                /*raycastCamera.transform.position = hand.pinch.pinchStart;
                raycastCamera.transform.rotation = hand.pinch.pinchRotation;*/
                noHands = false;
                break;
            }
        }

        raycastCamera.SetActive(!noHands);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastCamera.transform.position, raycastCamera.transform.forward * 1000);
    }
}
