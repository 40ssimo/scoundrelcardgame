using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] 
    private CardInstance _cardInstance;
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField] 
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private GameObject _mainCanvas;
    
    private static string MAINCANVAS = "Main Canvas";
    
    private void OnEnable()
    {
        _mainCanvas = GameObject.FindGameObjectWithTag(MAINCANVAS);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        _canvasGroup.blocksRaycasts = false;
        transform.SetParent(_mainCanvas.transform);
        transform.SetAsLastSibling();
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
    
    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject detectedArea = eventData.pointerCurrentRaycast.gameObject;
        
        CheckArea(_cardInstance, detectedArea);
    }

    public void CheckArea(CardInstance cardInstance, GameObject area)
    {
        if (cardInstance.GetCardFunction() == CardData.CardFunction.Enemy && (area.CompareTag("Weapon Area") || area.CompareTag("Barehand Area")))
        {
            transform.SetParent(area.transform);
            transform.SetAsLastSibling();
        }
    }
}