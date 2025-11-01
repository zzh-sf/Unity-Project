using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardList : MonoBehaviour
{
    public List<Card> Cards;
    void Start() {
        //ShowCardList();
    }
    public void ShowCardList() {
        GetComponent<RectTransform>().DOLocalMoveY(475,2f);
        EnableCardList();
    }
    public void DisableCardList() {
        foreach (Card card in Cards) { 
        card.DisableCard();
        }
    }
    void EnableCardList() {
        foreach (Card card in Cards) { 
        card.EnableCard();
        }
    }
}
