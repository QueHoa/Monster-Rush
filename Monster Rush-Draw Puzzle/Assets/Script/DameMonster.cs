using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Spine.Unity;

public class DameMonster : MonoBehaviour
{
    public GamePlay gamePlay;
    public SkeletonAnimation monster;
    public Transform start;
    public Transform end;
    public GameObject danger;
    public float time;

    private Tween moveTween;
    private int move;
    private string skinMonster;
    // Start is called before the first frame update
    void Start()
    {
        skinMonster = PlayerPrefs.GetString("skinMonster");
        monster.skeletonDataAsset = GameManager.Instance.skeletonMonster;
        monster.ClearState();
        monster.initialSkinName = skinMonster + "_blue";
        monster.AnimationName = "run";
        monster.timeScale = 1;
        monster.Initialize(true);
        transform.position = start.position;
        move = 0;
        danger.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlay.isMove && move == 0)
        {
            Move();
            danger.SetActive(false);
            move++;
        }
        if (gamePlay.isEnd || gamePlay.isLose)
        {
            moveTween.Kill();
            monster.AnimationName = "idle";
            monster.timeScale = 1;
            monster.loop = true;
            monster.Initialize(true);
        }
    }
    private void Move()
    {
        moveTween = transform.DOMove(end.position, time)
            .OnComplete(() =>
            {
                transform.position = start.position;
                Move();
            });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        moveTween.Kill();
        if(transform.position.x < collision.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (transform.position.x > collision.transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        monster.AnimationName = "attack";
        monster.timeScale = 1;
        monster.loop = false;
        monster.Initialize(true);
    }
}
