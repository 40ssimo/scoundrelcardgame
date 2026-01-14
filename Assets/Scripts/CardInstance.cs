using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class CardInstance : MonoBehaviour
{
    [SerializeField]
    private CardData cardData;
    [SerializeField]
    private int cardValue;
    [SerializeField]
    private CardData.House cardHouse;
    [SerializeField]
    private CardData.CardFunction cardFunction;
    [SerializeField]
    private Image cardImage;

    private void Start()
    {
        SetupCardData();
    }

    private void SetupCardData()
    {
        cardValue = cardData.cardValue;
        cardHouse = cardData.cardHouse;
        cardFunction = cardData.cardFunction;
        cardImage.sprite = cardData.cardImage;
    }
}
