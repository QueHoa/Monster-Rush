using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;

public class LoseGame : MonoBehaviour
{
    public MainController mainController;
    public HomeController homeController;
    public SkeletonAnimation player;
    public GameObject loading;
    public GameObject gold;
    public Image progressBar;
    public Text textGift;
    public Button skipLevel;
    private Animator anim;
    private int unlockedLevelsNumber;
    private int numberGift;
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
        numberGift = PlayerPrefs.GetInt("giftWin");
        textGift.text = numberGift.ToString() + "%";
        progressBar.fillAmount = (float)numberGift / 100;
        anim.SetTrigger("show");
        player.ClearState();
        player.initialSkinName = "Skin/" + skinHero + "_blue";
        player.AnimationName = "dead_2";
        player.Initialize(true);
        gold.SetActive(true);
        skipLevel.transform.DOScale(Vector3.one * 0.95f, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Skip()
    {
        if (mainController.numberPlaying == unlockedLevelsNumber)
        {
            PlayerPrefs.SetInt("levelsUnlocked", unlockedLevelsNumber + 1);
        }
        Transform Level = mainController.transform.Find(mainController.numberPlaying.ToString() + "(Clone)");
        if (Level != null)
        {
            Destroy(Level.gameObject);
        }
        mainController.numberPlaying++;
        StartCoroutine(Hide());
    }
    public void Replay()
    {
        Transform Level = mainController.transform.Find(mainController.numberPlaying.ToString() + "(Clone)");
        if (Level != null)
        {
            Destroy(Level.gameObject);
        }
        
        StartCoroutine(Hide());
    }
    IEnumerator Hide()
    {
        anim.SetTrigger("hide");
        yield return new WaitForSeconds(0.1f);
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
}
