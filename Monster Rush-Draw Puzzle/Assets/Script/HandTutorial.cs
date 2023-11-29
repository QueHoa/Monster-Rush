using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTutorial : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public HandTutorial hand;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.position = start.position;
        MoveHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void MoveHand()
    {
        transform.DOMove(end.position, 1.8f).SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                if(hand != null)
                {
                    hand.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                }
                if (hand == null)
                {
                    transform.DOKill();
                    Debug.Log("haha");
                    transform.position = start.position;
                    MoveHand();
                }
            });
    }
}
