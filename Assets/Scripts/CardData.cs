using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData",  order = 1)]
public class CardData : ScriptableObject
{
    public int cardValue;
    public House cardHouse;
    public CardFunction cardFunction;
    public Sprite cardImage;
    
    public enum House
    {
        Heart,
        Diamond,
        Spade,
        Club,
    }

    public enum CardFunction
    {
        Heal,
        Weapon,
        Enemy,
    }
}
