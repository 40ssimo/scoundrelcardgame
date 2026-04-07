using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

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
    private GameObject _currentArea;
    
    private static string MAIN_CANVAS = "Main Canvas";
    private static string WEAPON_AREA = "Weapon Area";
    private static string BAREHAND_AREA = "Barehand Area";
    
    private void OnEnable()
    {
        _initialRectTransform = _rectTransform.anchoredPosition;
        _currentArea = transform.parent.gameObject;
        _mainCanvas = GameObject.FindGameObjectWithTag(MAIN_CANVAS);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Check for Heart Card
        GameObject detectedCard = eventData.pointerCurrentRaycast.gameObject;
        detectedCard.TryGetComponent(out CardPrefabAccess cardPrefabAccess);
        cardPrefabAccess.CardPrefab.TryGetComponent(out CardInstance cardInstance);
        if (cardInstance != null && cardInstance.GetCardFunction() == CardData.CardFunction.Heal)
        {
            detectedCard.gameObject.SetActive(false);
            return;
        }
        
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
        // [WEAPON AREA]
        
        // "Enemy Card" Check [WEAPON AREA]
        if(cardInstance.GetCardFunction() == CardData.CardFunction.Enemy && (area.CompareTag(WEAPON_AREA)))
        {
            area.TryGetComponent(out WeaponArea weaponArea);
            transform.SetParent(weaponArea.KilledEnemyPosition.transform);
            
            if (weaponArea.LastVictim == null)
            {
                _rectTransform.anchoredPosition = Vector2.zero;
                transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                _currentArea = area;
                weaponArea.LastVictim = _cardInstance;
                weaponArea.VictimCount += 1;
                weaponArea.CurrentVictimCardPosition +=  weaponArea.GetVictimCardInterval();
            }
            else
            {
                _rectTransform.anchoredPosition = Vector2.zero;
                transform.SetLocalPositionAndRotation(weaponArea.CurrentVictimCardPosition, Quaternion.identity);
                
                weaponArea.CurrentVictimCardPosition +=  weaponArea.GetVictimCardInterval();
                
                _currentArea = area;
                weaponArea.LastVictim = _cardInstance;
                weaponArea.VictimCount += 1;
            }
            
            //TO DO : make calculation later
            return;
        }
        
        // "Weapon Card" Check [WEAPON AREA]
        if(cardInstance.GetCardFunction() == CardData.CardFunction.Weapon && area.CompareTag(WEAPON_AREA))
        {
            area.TryGetComponent(out WeaponArea weaponArea);
            weaponArea.CurrentWeapon = cardInstance;
            transform.SetParent(weaponArea.WeaponPosition.transform);
            _rectTransform.anchoredPosition = Vector2.zero;
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            _currentArea = area;
            return;
        }
        
        // // Check "Weapon Area" or "Barehand Area" for "Enemy Card"
        // if(cardInstance.GetCardFunction() == CardData.CardFunction.Enemy && (area.CompareTag(WEAPON_AREA) || area.CompareTag(BAREHAND_AREA)))
        // {
        //     transform.SetParent(area.transform);
        //     transform.SetAsLastSibling();
        //     _currentArea = area;
        // }
        
        else
        {
            // back to initial position and parent if raycast doesn't detect any area
            transform.SetParent(_currentArea.transform);
            transform.SetAsLastSibling();
            _rectTransform.anchoredPosition = _initialRectTransform;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}