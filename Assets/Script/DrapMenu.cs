using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrapMenu : MonoBehaviour, IDragHandler
{

    public void OnDrag(PointerEventData eventData)
    {

        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            TTUIRoot.Instance.root as RectTransform,
            Input.mousePosition,
            TTUIRoot.Instance.uiCamera,
            out worldPos))
        {
            transform.position = worldPos;
        }
    }
}
