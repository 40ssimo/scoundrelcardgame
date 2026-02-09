using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private GameObject _mainCanvas;
    private static string MAINCANVAS = "Main Canvas";

    private void Awake()
    {
        _mainCanvas = GameObject.FindGameObjectWithTag(MAINCANVAS);
        CardInstance cardInstance = GetComponent<CardInstance>();
        _rectTransform = cardInstance.GetCardImage().GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        transform.SetParent(_mainCanvas.transform.transform);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }
}