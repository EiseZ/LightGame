using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrbUI : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public string color;

    public bool orbEnabled;

    public void HandleDrag(BaseEventData data)
    {
        if (!orbEnabled) {
            return;
        }
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position
        );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void HandleStartDrag(BaseEventData data) {
        if (!orbEnabled) {
            return;
        }
        PointerEventData pointerData = (PointerEventData)data;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);
        for (int index = 0; index < raycastResults.Count; index++)
        {
            RaycastResult curRaycastResult = raycastResults[index];
            if (curRaycastResult.gameObject.CompareTag("OrbSlot")) {
                OrbSlot orbSlotScript = curRaycastResult.gameObject.GetComponent<OrbSlot>();
                orbSlotScript.changeColor("none");
            }
        }
    }

    public void HandleDrop(BaseEventData data)
    {
        if (!orbEnabled) {
            return;
        }
        PointerEventData pointerData = (PointerEventData)data;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);
        for (int index = 0; index < raycastResults.Count; index++)
        {
            RaycastResult curRaycastResult = raycastResults[index];
            if (curRaycastResult.gameObject.CompareTag("OrbSlot")) {
                OrbSlot orbSlotScript = curRaycastResult.gameObject.GetComponent<OrbSlot>();
                orbSlotScript.changeColor(color);
                transform.position = curRaycastResult.gameObject.transform.position;
            }
        }
    }
}
