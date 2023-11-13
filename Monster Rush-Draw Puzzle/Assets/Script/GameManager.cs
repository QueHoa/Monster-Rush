using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OneHit.Framework;
using Spine.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public MainController mainController;
    public HomeController homeController;
    public GameObject winGame;
    public GameObject loseGame;
    public List<Card> card = new List<Card>();
    public Transform boxMonster;
    [HideInInspector]
    public SkeletonDataAsset skeletonMonster;
    [HideInInspector]
    public float timeAttack;

    public GameObject onVibrate;
    public GameObject offVibrate;
    public GameObject onMusic;
    public GameObject offMusic;
    public GameObject onSound;
    public GameObject offSound;
    [HideInInspector]
    public int isVibrate;
    [HideInInspector]
    public int isMusic;
    [HideInInspector]
    public int isSound;

    private int unlockedLevelsNumber;
    private string skinMonster;
    // Start is called before the first frame update
    void Start()
    {
        isMusic = PlayerPrefs.GetInt("MusicOn");
        isSound = PlayerPrefs.GetInt("SoundOn");
        isVibrate = PlayerPrefs.GetInt("VibrateOn");
        if (isMusic == 1)
        {
            onMusic.SetActive(true);
            offMusic.SetActive(false);
        }
        else
        {
            onMusic.SetActive(false);
            offMusic.SetActive(true);
        }
        if (isSound == 1)
        {
            onSound.SetActive(true);
            offSound.SetActive(false);
        }
        else
        {
            onSound.SetActive(false);
            offSound.SetActive(true);
        }
        if (isVibrate == 1)
        {
            onVibrate.SetActive(true);
            offVibrate.SetActive(false);
        }
        else
        {
            onVibrate.SetActive(false);
            offVibrate.SetActive(true);
        }
    }
    private void Awake()
    {
        Input.multiTouchEnabled = false;
        Instance = this;
        //PlayerPrefs.SetInt("levelsUnlocked", 100);
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", 1);
        }
        //PlayerPrefs.SetString("skinHero", "Captain");
        if (!PlayerPrefs.HasKey("skinHero"))
        {
            PlayerPrefs.SetString("skinHero", "Captain");
        }
        //PlayerPrefs.SetString("skinMonster", "Babanleena");
        if (!PlayerPrefs.HasKey("skinMonster"))
        {
            PlayerPrefs.SetString("skinMonster", "Babanleena");
        }
        //PlayerPrefs.SetInt("giftWin", 0);
        if (!PlayerPrefs.HasKey("giftWin"))
        {
            PlayerPrefs.SetInt("giftWin", 0);
        }
        //PlayerPrefs.SetInt("gold", 0);
        if (!PlayerPrefs.HasKey("gold"))
        {
            PlayerPrefs.SetInt("gold", 0);
        }
        if (!PlayerPrefs.HasKey("MusicOn"))
        {
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        if (!PlayerPrefs.HasKey("SoundOn"))
        {
            PlayerPrefs.SetInt("SoundOn", 1);
        }
        if (!PlayerPrefs.HasKey("VibrateOn"))
        {
            PlayerPrefs.SetInt("VibrateOn", 1);
        }
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
    }

    // Update is called once per frame
    void Update()
    {
        isMusic = PlayerPrefs.GetInt("MusicOn");
        isSound = PlayerPrefs.GetInt("SoundOn");
        isVibrate = PlayerPrefs.GetInt("VibrateOn");
        if (isMusic == 1)
        {
            AudioManager.PlayBGM();
        }
        skinMonster = PlayerPrefs.GetString("skinMonster");
        for (int i = 0; i < boxMonster.childCount; i++)
        {
            if (boxMonster.GetChild(i).GetComponent<ChooseMonster>().card.name == skinMonster)
            {
                skeletonMonster = boxMonster.GetChild(i).GetComponent<ChooseMonster>().card.skeletonDataAsset;
                timeAttack = boxMonster.GetChild(i).GetComponent<ChooseMonster>().card.timeAttack;
            }
        }
        for (int i = 0; i < card.Count; i++)
        {
            if (PlayerPrefs.GetInt(card[i].name) == card[i].numberAds)
            {
                card.RemoveAt(i);
            }
        }
    }
    public void SetMusic()
    {
        AudioManager.UpdateMusic();
        AudioManager.Play("Click");
        if (isMusic == 1)
        {
            onMusic.SetActive(false);
            offMusic.SetActive(true);
            PlayerPrefs.SetInt("MusicOn", 0);
        }
        else
        {
            onMusic.SetActive(true);
            offMusic.SetActive(false);
            PlayerPrefs.SetInt("MusicOn", 1);
        }
    }
    public void SetVibrate()
    {
        AudioManager.Play("Click");
        if (isVibrate == 1)
        {
            onVibrate.SetActive(false);
            offVibrate.SetActive(true);
            PlayerPrefs.SetInt("VibrateOn", 0);
        }
        else
        {
            onVibrate.SetActive(true);
            offVibrate.SetActive(false);
            PlayerPrefs.SetInt("VibrateOn", 1);
        }
    }
    public void SetSound()
    {
        if (isSound == 1)
        {
            onSound.SetActive(false);
            offSound.SetActive(true);
            PlayerPrefs.SetInt("SoundOn", 0);
        }
        else
        {
            onSound.SetActive(true);
            offSound.SetActive(false);
            PlayerPrefs.SetInt("SoundOn", 1);
        }
        AudioManager.Play("Click");
    }
}
