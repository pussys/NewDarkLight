using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour, IDropHandler
{

    private Image image;
    private Transform go;

    void OnEnable()
    {
        Transform forging = Resources.Load<Transform>("UIPrefab/ForgingItem");
        go = Instantiate(forging);
        image = go.GetComponent<Image>();
        if (image == null)
            image = gameObject.AddComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Sprite s = GetSprite(eventData);
        if (s != null)
            image.sprite = s;
        go.SetParent(transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
    }

    private Sprite GetSprite(PointerEventData eventData)
    {
        GameObject goSource = eventData.pointerDrag;
        if (goSource == null)
            return null;

        Image imgSource = eventData.pointerDrag.GetComponent<Image>();
        if (imgSource == null)
            return null;

        Drag DragSource = imgSource.GetComponent<Drag>();
        if (DragSource == null)
            return null;

        return imgSource.sprite;
    }
}
