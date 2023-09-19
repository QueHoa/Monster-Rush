using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;

public class WinGame : MonoBehaviour
{
    public GameManager gameManager;
    public MainController mainController;
    public HomeController homeController;
    public SkeletonAnimation player;
    public GameObject gold;
    public Text textGold;
    public GameObject panelGiftHero;
    public GameObject panelGiftMonster;
    public GameObject loading;
    public GameObject[] effect;
    public SpriteRenderer charactorHero;
    public SpriteRenderer charactorMonster;
    public Image progressBar;
    public Text textGift;
    public Button getCoin;
    private Animator anim;
    private int unlockedLevelsNumber;
    private int numberGold;
    private int numberGift;
    private int numberCharactor;
    private string skinHero;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        skinHero = PlayerPrefs.GetString("skinHero");
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        numberGold = PlayerPrefs.GetInt("gold");
        numberGift = PlayerPrefs.GetInt("giftWin");
        /*for (int i = 0; i < effect.Length; i++)
        {
            effect[i].SetActive(true);
        }*/
        player.ClearState();
        player.initialSkinName = "Skin/" + skinHero + "_blue";
        player.AnimationName = "win";
        player.Initialize(true);
        gold.SetActive(true);
        numberGold += 50;
        numberGift += 20;
        PlayerPrefs.SetInt("gold", numberGold);
        anim.SetTrigger("show");
        getCoin.gameObject.SetActive(true);
        getCoin.transform.DOScale(Vector3.one * 0.95f, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    // Update is called once per frame
    void Update()
    {
        textGold.text = numberGold.ToString();
        textGift.text = numberGift.ToString() + "%";
        progressBar.fillAmount = (float)numberGift / 100;
        if(numberGift == 100)
        {
            StartCoroutine(EffectGift());
        }
        if (mainController.numberPlaying == unlockedLevelsNumber)
        {
            PlayerPrefs.SetInt("levelsUnlocked", unlockedLevelsNumber + 1);
        }
        PlayerPrefs.SetInt("giftWin", numberGift);
    }
    IEnumerator EffectGift()
    {
        numberCharactor = Random.Range(0, gameManager.card.Count - 1);
        yield return new WaitForSeconds(1.5f);
        if (gameManager.card[numberCharactor].GetItem(gameManager.card[numberCharactor].ItemType) == "hero")
        {
            charactorHero.sprite = gameManager.card[numberCharactor].charactor;
            panelGiftHero.SetActive(true);
        }
        if (gameManager.card[numberCharactor].GetItem(gameManager.card[numberCharactor].ItemType) == "monster")
        {
            charactorMonster.sprite = gameManager.card[numberCharactor].charactor;
            panelGiftMonster.SetActive(true);
        }
        numberGift = 0;
    }
    public void GetCoin()
    {
        numberGold += 100;
        PlayerPrefs.SetInt("gold", numberGold);
        getCoin.gameObject.SetActive(false);
    }
    public void Next()
    {
        Transform Level = mainController.transform.Find(mainController.numberPlaying.ToString() + "(Clone)");
        if (Level != null)
        {
            Destroy(Level.gameObject);
        }
        mainController.numberPlaying++;
        StartCoroutine(Hide());
    }
    IEnumerator Hide()
    {
        anim.SetTrigger("hide");
        yield return new WaitForSeconds(0.1f);
        /*for (int i = 0; i < effect.Length; i++)
        {
            effect[i].SetActive(false);
        }*/
        gold.SetActive(false);
        GameObject loadedPrefab = Resources.Load<GameObject>(mainController.numberPlaying.ToString());
        GameObject level = Instantiate(loadedPrefab, mainController.transform);
        level.transform.SetParent(mainController.transform, false);
        mainController.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void BackHome()
    {
        Transform Level = mainController.transform.Find(mainController.numberPlaying.ToString() + "(Clone)");
        if (Level != null)
        {
            Destroy(Level.gameObject);
        }
        StartCoroutine(Hide2());
    }
    IEnumerator Hide2()
    {
        anim.SetTrigger("hide");
        yield return new WaitForSeconds(0.1f);
        loading.SetActive(true);
        /*for (int i = 0; i < effect.Length; i++)
        {
            effect[i].SetActive(false);
        }*/
        yield return new WaitForSeconds(0.7f);
        homeController.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void NoThank()
    {
        if (panelGiftHero.activeInHierarchy)
        {
            panelGiftHero.SetActive(false);
        }
        if (panelGiftMonster.activeInHierarchy)
        {
            panelGiftMonster.SetActive(false);
        }
    }
    public void Claim()
    {
        PlayerPrefs.SetInt(gameManager.card[numberCharactor].name, gameManager.card[numberCharactor].numberAds);
        if (panelGiftHero.activeInHierarchy)
        {
            panelGiftHero.SetActive(false);
        }
        if (panelGiftMonster.activeInHierarchy)
        {
            panelGiftMonster.SetActive(false);
        }
    }
}
