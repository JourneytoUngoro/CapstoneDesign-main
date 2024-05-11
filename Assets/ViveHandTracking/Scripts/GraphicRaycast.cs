using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphicRaycast : BaseInputModule
{
    public GraphicRaycaster graphicRaycaster;
    private List<RaycastResult> raycastResults;
    private PointerEventData pointerEventData;
    public Camera raycastCamera;

    protected override void Start()
    {
        if (graphicRaycaster == null)
        {
            graphicRaycaster = GetComponent<GraphicRaycaster>();
        }

        pointerEventData = new PointerEventData(null);
        pointerEventData.position = new Vector2(raycastCamera.pixelWidth * 0.5f, raycastCamera.pixelHeight * 0.5f);
        raycastResults = new List<RaycastResult>();
    }

    private void Update()
    {
        graphicRaycaster.Raycast(pointerEventData, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (RaycastResult raycastResult in raycastResults)
            {
                Debug.Log(raycastResult.ToString());
                HandlePointerExitAndEnter(pointerEventData, raycastResult.gameObject);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ExecuteEvents.Execute(raycastResult.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
                    /*ExecuteEvents.Execute(raycastResult.gameObject, new BaseEventData(eventSystem), ExecuteEvents.pointerClickHandler);
                    ExecuteEvents.Execute(raycastResult.gameObject, new BaseEventData(eventSystem), ExecuteEvents.selectHandler);
                    ExecuteEvents.Execute(raycastResult.gameObject, new BaseEventData(eventSystem), ExecuteEvents.dragHandler);*/
                }
            }
        }
        else
        {
            HandlePointerExitAndEnter(pointerEventData, null);
            Debug.Log("Nothing");
        }

        raycastResults.Clear();
    }

    public override void Process() { }
    protected override void OnEnable() { }
    protected override void OnDisable() { }
}