using DG.Tweening;
using OneHit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitMove : MonoBehaviour
{
    public GamePlay gamePlay;
    public SetLineObstacle line;
    public float time;

    private Tween moveTween;
    private int move;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = line.point[0].position;
        move = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlay.isMove && move == 0)
        {
            Move();
            move++;
        }
        if (gamePlay.isStop)
        {
            moveTween.Kill();
        }
        if (gamePlay.isEnd || gamePlay.isLose)
        {
            line.ResetLine();
            moveTween.Kill();
        }
    }
    private void Move()
    {
        moveTween = transform.DOPath(line.positions, time, PathType.Linear).SetEase(Ease.Linear);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveTween.Kill();
    }
}
