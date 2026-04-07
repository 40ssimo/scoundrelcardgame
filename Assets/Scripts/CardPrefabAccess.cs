using System;
using UnityEngine;

public class CardPrefabAccess : MonoBehaviour
{
    [SerializeField]
    private GameObject _cardPrefab;
    
    public GameObject CardPrefab => _cardPrefab;
}
