using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using System.Threading.Tasks;
using MoreMountains.NiceVibrations;
using OneHit.Framework;

public class PlayerControllerTutorial : MonoBehaviour
{
    public GamePlayTutorial gamePlayTutorial;
    public Weapon[] boxWeapon;
    public SkeletonAnimation player;
    public SkeletonAnimation weapon;
    public SkeletonAnimation monster;
    public DrawPath path;
    public SpriteRenderer endWeapon;
    public GameObject effectFigtht;
    public const float time = 2f;
    public GameColor color;
    public SpriteRenderer startPlace;
    public SpriteRenderer place;
    public Sprite[] colorPlace;
    public HapticTypes hapticTypes = HapticTypes.Failure;
    
    [HideInInspector]
    public bool swap;

    private bool hapticsAllowed = true;
    private MainController mainController;
    private int numberWeapon;
    private int stop;
    private int lose;
    private Tween moveTween;
    private Color blue = new Color(0, 0.8f, 1);
    private Color red = new Color(1, 0, 0);
    private Color yellow = new Color(1, 0.9f, 0);
    private float oldPos;
    private float timeAttack;
    private float monsterAttack;
    private int isVibrate;
    private string attack;
    private string weaponName;
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
        isVibrate = PlayerPrefs.GetInt("VibrateOn");
        monster.skeletonDataAsset = GameManager.Instance.skeletonMonster;
        monsterAttack = GameManager.Instance.timeAttack;
        MMVibrationManager.SetHapticsActive(hapticsAllowed);
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
        stop = 0;
        lose = 0;
        swap = false;
        weapon.gameObject.SetActive(false);
        endWeapon.gameObject.SetActive(true);
        endWeapon.transform.DOMoveY(endWeapon.transform.position.y - 0.25f, 1.2f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        transform.localScale = Vector3.one * 0.9f;
        oldPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= oldPos)
        {
            transform.localScale = Vector3.one * 0.9f;
        }
        else
        {
            transform.localScale = new Vector3(-0.9f, 0.9f, 1);
        }
        if (gamePlayTutorial.isStop && stop == 0)
        {
            moveTween.Kill();
            AudioManager.Stop("Walk");
            player.AnimationName = "idle_1";
            player.timeScale = 1;
            player.loop = true;
            weapon.AnimationName = "idle_1";
            weapon.timeScale = 1;
            weapon.loop = true;
            stop++;
        }  
        if (gamePlayTutorial.isLose)
        {
            moveTween.Kill();
            AudioManager.Stop("Walk");
            if(lose == 0)
            {
                player.AnimationName = "idle_1";
                player.timeScale = 1;
                player.loop = true;
                weapon.AnimationName = "idle_1";
                weapon.timeScale = 1;
                weapon.loop = true;
            }
            else
            {
                player.AnimationName = "dead_1";
                player.timeScale = 1;
                player.loop = false;
                weapon.AnimationName = "dead_1";
                weapon.timeScale = 1;
                weapon.loop = false;
            }
        }
        else
        {
            oldPos = transform.position.x;
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
        player.timeScale = path.pathLength * 1.2f;
        weapon.timeScale = path.pathLength * 1.2f;
        AudioManager.Play("Walk");
        player.Initialize(true);
        moveTween = transform.DOPath(path.positions, time, PathType.Linear).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                weapon.gameObject.SetActive(true);
                endWeapon.gameObject.SetActive(false);
                Vector3[] reversePathPositions = new Vector3[path.positions.Length];
                for (int i = 0; i < path.positions.Length; i++)
                {
                    reversePathPositions[i] = path.positions[path.positions.Length - 1 - i];
                }

                moveTween = transform.DOPath(reversePathPositions, time, PathType.Linear)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        AudioManager.Stop("Walk");
                        StartCoroutine(effectWin());
                    });
            });
    }
    IEnumerator effectWin()
    {
        path.ResetLine();
        gamePlayTutorial.isEnd = true;
        player.AnimationName = attack;
        player.timeScale = 1;
        player.loop = false;
        weapon.AnimationName = attack;
        weapon.timeScale = 1;
        weapon.loop = false;
        AudioManager.PlayOneShot(weaponName);
        yield return new WaitForSeconds(timeAttack - 0.5f);
        monster.AnimationName = "dead";
        monster.timeScale = 1;
        monster.loop = false;
        AudioManager.PlayOneShot("MonsterDie");
        yield return new WaitForSeconds(0.5f);
        player.AnimationName = "idle_1";
        player.timeScale = 1;
        player.loop = true;
        weapon.AnimationName = "idle_1";
        weapon.timeScale = 1;
        weapon.loop = true;
        yield return new WaitForSeconds(0.5f);
        monster.AnimationName = null;
        monster.timeScale = 1;
        monster.loop = false;
        gamePlayTutorial.numberWin++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lose++;
            AudioManager.PlayOneShot("Dead");
            gamePlayTutorial.isLose = true;
        }
        if (collision.CompareTag("Obstacle"))
        {
            lose++;
            Vector3 spawnPosition = (transform.position + collision.transform.position) / 2.0f;
            spawnPosition.y += 0.4f;
            gamePlayTutorial.isStop = true;
            StartCoroutine(EffectObstacle(spawnPosition));
        }
        if (collision.CompareTag("Monster"))
        {
            lose++;
            gamePlayTutorial.isStop = true;
            if(collision.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-0.9f, 0.9f, 1);
            }
            StartCoroutine(MonsterAttack());
        }
        if (isVibrate == 1)
        {
            MMVibrationManager.Haptic(hapticTypes, true, true, this);
        }
    }
    IEnumerator EffectObstacle(Vector3 effectPosition)
    {
        yield return new WaitForSeconds(0.05f);
        GameObject effect = (GameObject)Instantiate(effectFigtht, effectPosition, Quaternion.identity);
        Destroy(effect, 2f);
        player.AnimationName = "dead_2";
        player.timeScale = 1;
        weapon.AnimationName = "dead_2";
        weapon.timeScale = 1;
        yield return new WaitForSeconds(1.5f);
        player.AnimationName = "dead_1";
        player.timeScale = 1;
        player.loop = false;
        weapon.AnimationName = "dead_1";
        weapon.timeScale = 1;
        weapon.loop = false;
        AudioManager.PlayOneShot("Dead");
        gamePlayTutorial.isLose = true;
    }
    IEnumerator MonsterAttack()
    {
        player.AnimationName = "idle_1";
        player.timeScale = 1;
        player.loop = false;
        weapon.AnimationName = "idle_1";
        weapon.timeScale = 1;
        weapon.loop = false;
        yield return new WaitForSeconds(monsterAttack - 1.8f);
        AudioManager.PlayOneShot("MonsterAttack");
        yield return new WaitForSeconds(1.2f);
        player.AnimationName = "dead_1";
        player.timeScale = 1;
        player.loop = false;
        weapon.AnimationName = "dead_1";
        weapon.timeScale = 1;
        weapon.loop = false;
        AudioManager.PlayOneShot("Dead");
        gamePlayTutorial.isLose = true;
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
        weaponName = boxWeapon[numberWeapon].WeaponName();
    }
}
