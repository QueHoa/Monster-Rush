using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    public GamePlay gamePlay;
    public Weapon[] boxWeapon;
    public Transform hero;
    public SkeletonAnimation player;
    public SkeletonAnimation weapon;
    public SkeletonAnimation monster;
    public DrawPath path;
    public SpriteRenderer endWeapon;
    public const float time = 2f;
    public GameColor color;
    public SpriteRenderer startPlace;
    public SpriteRenderer place;
    public Sprite[] colorPlace;

    [HideInInspector]
    public bool swap;

    private MainController mainController;
    private int numberWeapon;
    private Tween moveTween;
    private Color blue = new Color(0, 0.8f, 1);
    private Color red = new Color(1, 0, 0);
    private Color yellow = new Color(1, 1, 0);
    private float oldPos;
    private float timeAttack;
    private string attack;
    private string skinHero;
    private string skinMonster;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        skinHero = PlayerPrefs.GetString("skinHero");
        skinMonster = PlayerPrefs.GetString("skinMonster");
        monster.skeletonDataAsset = GameManager.Instance.skeletonMonster;
        if (color == GameColor.Blue)
        {
            place.sprite = colorPlace[0];
            player.ClearState();
            player.initialSkinName = "Skin/" + skinHero + "_blue";
            monster.ClearState();
            monster.initialSkinName = skinMonster + "_blue";
            startPlace.color = blue;
        }
        if (color == GameColor.Red)
        {
            place.sprite = colorPlace[1];
            player.ClearState();
            player.initialSkinName = "Skin/" + skinHero + "_red";
            monster.ClearState();
            monster.initialSkinName = skinMonster + "_red";
            startPlace.color = red;
        }
        if (color == GameColor.Yellow)
        {
            place.sprite = colorPlace[2];
            player.ClearState();
            player.initialSkinName = "Skin/" + skinHero + "_yellow";
            monster.ClearState();
            monster.initialSkinName = skinMonster + "_yellow";
            startPlace.color = yellow;
        }
        player.AnimationName = "idle_1";
        player.Initialize(true);
        monster.AnimationName = "idle";
        monster.Initialize(true);
        RandomWeapon(color);

        mainController = GameManager.Instance.mainController;
        swap = false;
        weapon.gameObject.SetActive(false);
        endWeapon.gameObject.SetActive(true);
        endWeapon.transform.DOMoveY(endWeapon.transform.position.y - 0.25f, 1.2f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        oldPos = hero.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (hero.position.x <= oldPos)
        {
            hero.localScale = Vector3.one;
        }
        else
        {
            hero.localScale = new Vector3(-1, 1, 1);
        }
        oldPos = hero.position.x;
        if (gamePlay.isLose)
        {
            moveTween.Kill();
        }
        if (swap)
        {
            RandomWeapon(color);
            swap = false;
        }
    }
    public void MoveToEnd()
    {
        mainController.backHome.interactable = false;
        mainController.skipLevel.interactable = false;
        mainController.iconAds.interactable = false;
        mainController.reload.interactable = false;
        player.AnimationName = "run";
        weapon.AnimationName = "run";
        player.timeScale = 5;
        weapon.timeScale = 5;
        player.Initialize(true);
        moveTween = hero.DOPath(path.positions, time, PathType.Linear).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                weapon.gameObject.SetActive(true);
                endWeapon.gameObject.SetActive(false);
                Vector3[] reversePathPositions = new Vector3[path.positions.Length];
                for (int i = path.positions.Length - 1; i >= 0; i--)
                {
                    reversePathPositions[path.positions.Length - 1 - i] = path.positions[i];
                }

                hero.DOPath(reversePathPositions, time, PathType.Linear)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        StartCoroutine(effectWin());
                    });
            });
    }
    IEnumerator effectWin()
    {
        path.ResetLine();
        player.AnimationName = attack;
        player.timeScale = 1;
        player.loop = false;
        weapon.AnimationName = attack;
        weapon.timeScale = 1;
        weapon.loop = false;
        yield return new WaitForSeconds(timeAttack);
        monster.AnimationName = "dead";
        monster.timeScale = 1;
        monster.loop = false;
        player.AnimationName = "idle_1";
        player.timeScale = 1;
        player.loop = true;
        weapon.AnimationName = "idle_1";
        weapon.timeScale = 1;
        weapon.loop = true;
        yield return new WaitForSeconds(1.1f);
        monster.AnimationName = null;
        monster.timeScale = 1;
        monster.loop = false;
        gamePlay.numberWin++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.AnimationName = "dead_1";
            player.timeScale = 1;
            player.loop = false;
            weapon.AnimationName = "dead_1";
            weapon.timeScale = 1;
            weapon.loop = false;
            gamePlay.isLose = true;
        }
    }
    public void RandomWeapon(GameColor color)
    {
        numberWeapon = Random.Range(0, boxWeapon.Length - 1);
        weapon.ClearState();
        weapon.initialSkinName = boxWeapon[numberWeapon].GetFullSkinPath(color);
        weapon.AnimationName = "idle_1";
        weapon.Initialize(true);
        endWeapon.sprite = boxWeapon[numberWeapon].GetSprite(color);
        attack = boxWeapon[numberWeapon].GetAttackAnimationName();
        timeAttack = boxWeapon[numberWeapon].TimeAttack();
    }
}
