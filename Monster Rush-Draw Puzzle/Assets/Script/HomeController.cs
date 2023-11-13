using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using OneHit.Framework;

public class HomeController : MonoBehaviour
{
    public MainController mainController;
    public Transform butPlay;
    public Transform butShop;
    public GameObject panelSetting;
    public GameObject heroPaticle;
    public GameObject loading;
    public GameObject shop;
    public Text textGold;

    private int unlockedLevelsNumber;
    private int numberGold;
    // Start is called before the first frame update
    void Start()
    {
        butPlay.DOScale(Vector3.one * 0.95f, 0.65f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        butShop.DORotate(new Vector3(0, 0, 4), 0.7f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnEnable()
    {
        heroPaticle.SetActive(true);
        butPlay.localScale = Vector3.one;
        butShop.rotation = Quaternion.Euler(0, 0, -4);
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        numberGold = PlayerPrefs.GetInt("gold");
    }

    // Update is called once per frame
    void Update()
    {
        numberGold = PlayerPrefs.GetInt("gold");
        textGold.text = numberGold.ToString();
    }
    public void PlayGame()
    {
        AudioManager.Play("NewLevel");
        StartCoroutine(EffectPlay());
    }
    IEnumerator EffectPlay()
    {
        heroPaticle.SetActive(false);
        loading.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        mainController.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Shop()
    {
        AudioManager.Play("Click");
        heroPaticle.SetActive(false);
        shop.SetActive(true);
    }
    public void Setting()
    {
        AudioManager.Play("Click");
        panelSetting.SetActive(true);
    }
}
