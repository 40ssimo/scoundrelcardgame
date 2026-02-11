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

    [SerializeField] 
    private Vector2 _initialRectTransform;

    [SerializeField] 
    private GameObject _initialParent;
    
    private static string MAIN_CANVAS = "Main Canvas";
    private static string WEAPON_AREA = "Weapon Area";
    private static string BAREHAND_AREA = "Barehand Area";
    
    private void OnEnable()
    {
        _initialRectTransform = _rectTransform.anchoredPosition;
        _initialParent = transform.parent.gameObject;
        _mainCanvas = GameObject.FindGameObjectWithTag(MAIN_CANVAS);
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
        // Check "Weapon Area" or "Barehand Area"
        if(cardInstance.GetCardFunction() == CardData.CardFunction.Enemy && (area.CompareTag(WEAPON_AREA) || area.CompareTag(BAREHAND_AREA)))
        {
            transform.SetParent(area.transform);
            transform.SetAsLastSibling();
        } else
        {
            // back to initial position and parent if raycast doesn't detect any area
            transform.SetParent(_initialParent.transform);
            transform.SetAsLastSibling();
            _rectTransform.anchoredPosition = _initialRectTransform;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}