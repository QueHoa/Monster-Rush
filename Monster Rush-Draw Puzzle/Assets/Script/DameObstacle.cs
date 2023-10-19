using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DameObstacle : MonoBehaviour
{
    public GamePlay gamePlay;
    public Transform start;
    public Transform end;
    public float time;

    private Tween moveTween;
    private int move;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = start.position;
        move = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlay.isMove && move == 0)
        {
            moveTween = transform.DOMove(end.position, time).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            move = 1;
        }
        if (gamePlay.isLose || gamePlay.isStop)
        {
            moveTween.Kill();
        }
    }
}
