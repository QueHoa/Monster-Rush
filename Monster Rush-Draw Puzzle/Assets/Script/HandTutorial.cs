using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTutorial : MonoBehaviour
{
    public Transform start;
    public Transform end;
    // Start is called before the first frame update
    void Start()
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
        transform.DOMove(end.position, 1.2f).SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                transform.position = start.position;
                MoveHand();
            });
    }
}
