using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using OneHit.Framework;

public class WinGame : MonoBehaviour
{
    public GameManager gameManager;
    public MainController mainController;
    public HomeController homeController;
    public SkeletonAnimation player;
    public Transform gold;
    public Text textGold;
    public Animator panelGiftHero;
    public Animator panelGiftMonster;
    public GameObject loading;
    public GameObject effect;
    public SpriteRenderer charactorHero;
    public SpriteRenderer charactorMonster;
    public Transform textMoreGold;
    public Image progressBar;
    public Text textGift;
    public Button getCoin;

    private Animator anim;
    private GameObject level;
    private int unlockedLevelsNumber;
    private int numberGold;
    private int numberGift;
    private int numberCharactor;
    private string skinHero;
    private GameObject[] coin;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        getCoin.transform.DOScale(Vector3.one * 0.95f, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnEnable()
    {
        AudioManager.Play("Win");
        skinHero = PlayerPrefs.GetString("skinHero");
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        numberGold = PlayerPrefs.GetInt("gold");
        numberGift = PlayerPrefs.GetInt("giftWin");
        textGold.text = numberGold.ToString();
        textGift.text = numberGift.ToString() + "%";
        progressBar.fillAmount = (float)numberGift / 100;
        StartCoroutine(StartEffect());
        player.ClearState();
        player.initialSkinName = "Skin/" + skinHero + "_blue";
        player.AnimationName = "win";
        player.Initialize(true);
        PlayerPrefs.SetInt("gold", numberGold);
        anim.SetTrigger("show");
        getCoin.gameObject.SetActive(true);
        level = mainController.level;
    }
    IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(0.8f);
        AudioManager.Play("Confetti");
        effect.SetActive(true);
        ChangeGift();
        yield return new WaitForSeconds(0.8f);
        AudioManager.Play("GainCoin");
        coin = CoinPooler.instance.GetPoolCoin();
        for (int i = 5; i < 10; i++)
        {
            if (coin[i] != null)
            {
                coin[i].transform.localScale = Vector3.one * 30;
                coin[i].transform.position = textMoreGold.position;
                coin[i].SetActive(true);
                coin[i].transform.DOMove(new Vector3(textMoreGold.position.x + Random.Range(-1f, 1f), textMoreGold.position.y + Random.Range(-2f, 0.5f), 1), 0.3f).SetEase(Ease.InOutQuad);
                coin[i].transform.DOScale(Vector3.one * 140, 0.3f).SetEase(Ease.InOutQuad);
            }
        }
        yield return new WaitForSeconds(0.6f);
        for (int i = 5; i < 10; i++)
        {
            if (coin[i] != null)
            {
                coin[i].transform.DOMove(gold.position, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    AudioManager.Play("CoinMove");
                });
            }
        }
        yield return new WaitForSeconds(0.7f);
        if (level != null)
        {
            Destroy(level);
        }
        for (int i = 5; i < 10; i++)
        {
            if (coin[i] != null)
            {
                coin[i].SetActive(false);
            }
        }

        if (unlockedLevelsNumber != GameManager.numberLevel)
        {
            PlayerPrefs.SetInt("levelsUnlocked", unlockedLevelsNumber + 1);
        }
        else
        {
            PlayerPrefs.SetInt("levelsUnlocked", 1);
        }
        ChangeGold(50);
    }
    // Update is called once per frame
    void Update()
    {
        if(numberGift == 100)
        {
            numberGift = 0;
            StartCoroutine(EffectGift());
        }
    }
    IEnumerator EffectGift()
    {   
        numberCharactor = Random.Range(0, gameManager.card.Count - 1);
        yield return new WaitForSeconds(1.5f);
        if (gameManager.card[numberCharactor].GetItem(gameManager.card[numberCharactor].ItemType) == "hero")
        {
            charactorHero.sprite = gameManager.card[numberCharactor].charactor;
            panelGiftHero.gameObject.SetActive(true);
            panelGiftHero.SetTrigger("show");
        }
        if (gameManager.card[numberCharactor].GetItem(gameManager.card[numberCharactor].ItemType) == "monster")
        {
            charactorMonster.sprite = gameManager.card[numberCharactor].charactor;
            panelGiftMonster.gameObject.SetActive(true);
            panelGiftMonster.SetTrigger("show");
        }
        yield return new WaitForSeconds(0.5f);
        textGift.text = numberGift.ToString() + "%";
        progressBar.fillAmount = (float)numberGift / 100;
        PlayerPrefs.SetInt("giftWin", numberGift);
        yield return new WaitForSeconds(0.3f);
        AudioManager.Play("Congratulation");
    }
    public void GetCoin()
    {
        AudioManager.Play("GainCoin");
        coin = CoinPooler.instance.GetPoolCoin();
        for (int i = 0; i < 5; i++)
        {
            if (coin[i] != null)
            {
                coin[i].transform.localScale = Vector3.one * 30;
                coin[i].transform.position = getCoin.transform.position;
                coin[i].SetActive(true);
                coin[i].transform.DOMove(new Vector3(getCoin.transform.position.x + Random.Range(-0.5f, 0.5f), getCoin.transform.position.y + Random.Range(-0.5f, 0.5f), 1), 0.2f).SetEase(Ease.InOutQuad);
                coin[i].transform.DOScale(Vector3.one * 140, 0.2f).SetEase(Ease.InOutQuad);
            }
        }
        getCoin.gameObject.SetActive(false);
        StartCoroutine(EffectGold());
    }
    IEnumerator EffectGold()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 5; i++)
        {
            if (coin[i] != null)
            {
                coin[i].transform.DOMove(gold.position, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    AudioManager.Play("CoinMove");
                });
            }
        }
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < 5; i++)
        {
            if (coin[i] != null)
            {
                coin[i].SetActive(false);
            }
        }
        ChangeGold(100);
        yield return new WaitForSeconds(0.8f);
        anim.SetTrigger("hide");
        effect.SetActive(false);
        mainController.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    public void Next()
    {
        AudioManager.Play("Click");
        if (level != null)
        {
            Destroy(level.gameObject);
        }
        for (int i = 0; i < 5; i++)
        {
            if (coin[i] != null)
            {
                coin[i].SetActive(false);
            }
        }
        StartCoroutine(Hide());
    }
    IEnumerator Hide()
    {
        anim.SetTrigger("hide");
        effect.SetActive(false);
        mainController.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    public void BackHome()
    {
        AudioManager.Play("Click");
        if (level != null)
        {
            Destroy(level.gameObject);
        }
        StartCoroutine(Hide2());
    }
    IEnumerator Hide2()
    {
        anim.SetTrigger("hide");
        for (int i = 0; i < 5; i++)
        {
            if (coin[i] != null)
            {
                coin[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(0.1f);
        loading.SetActive(true);
        effect.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        homeController.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void NoThank()
    {
        AudioManager.Play("Click");
        if (panelGiftHero.gameObject.activeInHierarchy)
        {
            StartCoroutine(HideGiftHero());
        }
        if (panelGiftMonster.gameObject.activeInHierarchy)
        {
            StartCoroutine(HideGiftMonster());
        }
    }
    public void Claim()
    {
        AudioManager.Play("Click");
        PlayerPrefs.SetInt(gameManager.card[numberCharactor].name, gameManager.card[numberCharactor].numberAds);
        if (panelGiftHero.gameObject.activeInHierarchy)
        {
            StartCoroutine(HideGiftHero());
        }
        if (panelGiftMonster.gameObject.activeInHierarchy)
        {
            StartCoroutine(HideGiftMonster());
        }
    }
    IEnumerator HideGiftHero()
    {
        panelGiftHero.SetTrigger("hide");
        yield return new WaitForSeconds(0.65f);
        panelGiftHero.gameObject.SetActive(false);
    }
    IEnumerator HideGiftMonster()
    {
        panelGiftMonster.SetTrigger("hide");
        yield return new WaitForSeconds(0.65f);
        panelGiftMonster.gameObject.SetActive(false);
    }
    public void ChangeGold(int goldChange)
    {
        textGold.transform.DOScale(Vector3.one * 1.3f, 0.2f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
        int endValue = numberGold + goldChange;
        DOTween.To(() => numberGold, x => numberGold = x, endValue, 0.4f)
            .OnUpdate(() =>
            {
                textGold.text = numberGold.ToString();
            });
        PlayerPrefs.SetInt("gold", endValue);
    }
    public void ChangeGift()
    {
        int endValue = numberGift + 20;
        DOTween.To(() => numberGift, x => numberGift = x, endValue, 0.8f)
            .OnUpdate(() =>
            {
                textGift.text = numberGift.ToString() + "%";
                progressBar.fillAmount = (float)numberGift / 100;
            });
        PlayerPrefs.SetInt("giftWin", endValue);
    }
}
