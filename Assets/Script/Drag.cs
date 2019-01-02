using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TinyTeam.UI;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Image image;
    private GameObject go;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (go == null)
            return;
        
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            TTUIRoot.Instance.root as RectTransform,
            Input.mousePosition,
            TTUIRoot.Instance.uiCamera,
            out worldPos))
        {
            go.transform.position = worldPos;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (image.sprite == null)
        {
            Debug.LogError("Current component of 'Image' have none 'Sprite'.");
            return;
        }

        go = new GameObject("Draging");
        go.transform.SetParent(eventData.pointerDrag.transform.parent);

        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;

        Image goImg = go.AddComponent<Image>();
        goImg.sprite = image.sprite;
        goImg.raycastTarget = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(go);
        go = null;
    }
}
