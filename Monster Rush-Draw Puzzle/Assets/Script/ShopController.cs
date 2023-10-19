using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using OneHit.Framework;

public class ShopController : MonoBehaviour
{
    public Animator notice;
    public GameObject heroOn;
    public GameObject heroOff;
    public GameObject monsterOn;
    public GameObject monsterOff;
    public GameObject heroBox;
    public GameObject monsterBox;
    public List<ChooseHero> butHero = new List<ChooseHero>();
    public List<ChooseMonster> butMonster = new List<ChooseMonster>();
    public Button getGiftHero;
    public Button moreCoinHero;
    public Button getGiftMonster;
    public Button moreCoinMonster;
    public Gift giftHero;
    public Gift giftMonster;
    public Transform gold;
    public Text textGold;

    private int numberGold;
    private CanvasGroup alpha;
    private GameObject[] coin;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        alpha = GetComponent<CanvasGroup>();
        alpha.alpha = 0;
        DOTween.To(() => alpha.alpha, x => alpha.alpha = x, 1, 0.5f);
        numberGold = PlayerPrefs.GetInt("gold");
        textGold.text = numberGold.ToString();
        heroOn.SetActive(true);
        heroOff.SetActive(false);
        heroBox.SetActive(true);
        monsterOn.SetActive(false);
        monsterOff.SetActive(true);
        monsterBox.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        if(butHero.Count == 0)
        {
            getGiftHero.gameObject.SetActive(false);
            moreCoinHero.gameObject.SetActive(false);
        }
        if (butMonster.Count == 0)
        {
            getGiftMonster.gameObject.SetActive(false);
            moreCoinMonster.gameObject.SetActive(false);
        }
        if (heroBox.activeInHierarchy)
        {
            for (int i = 0; i < butHero.Count; i++)
            {
                if (!butHero[i].locked.activeInHierarchy)
                {
                    butHero.RemoveAt(i);
                }
            }
        }
        if (monsterBox.activeInHierarchy)
        {
            for (int i = 0; i < butMonster.Count; i++)
            {
                if (!butMonster[i].locked.activeInHierarchy)
                {
                    butMonster.RemoveAt(i);
                }
            }
        }
    }
    public void BackHome()
    {
        AudioManager.Play("Click");
        StartCoroutine(EffectBack());
    }
    IEnumerator EffectBack()
    {
        DOTween.To(() => alpha.alpha, x => alpha.alpha = x, 0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    public void Hero()
    {
        AudioManager.Play("Click");
        if (!heroBox.activeInHierarchy)
        {
            heroOn.SetActive(true);
            heroOff.SetActive(false);
            heroBox.SetActive(true);
            monsterOn.SetActive(false);
            monsterOff.SetActive(true);
            monsterBox.SetActive(false);
        }
    }
    public void Monster()
    {
        AudioManager.Play("Click");
        if (!monsterBox.activeInHierarchy)
        {
            heroOn.SetActive(false);
            heroOff.SetActive(true);
            heroBox.SetActive(false);
            monsterOn.SetActive(true);
            monsterOff.SetActive(false);
            monsterBox.SetActive(true);
        }
    }
    public void GetGiftHero()
    {
        AudioManager.Play("Click");
        if (numberGold >= 300)
        {
            int numberButton = Random.Range(0, butHero.Count - 1);
            giftHero.card = butHero[numberButton].card;
            giftHero.gameObject.SetActive(true);
        }
        else
        {
            notice.gameObject.SetActive(true);
            notice.SetTrigger("show");
        }
    }
    public void GetGiftMonster()
    {
        AudioManager.Play("Click");
        if (numberGold >= 300)
        {
            int numberButton = Random.Range(0, butMonster.Count - 1);
            giftMonster.card = butMonster[numberButton].card;
            giftMonster.gameObject.SetActive(true);
        }
        else
        {
            notice.gameObject.SetActive(true);
            notice.SetTrigger("show");
        }
    }
    public void MoreCoin()
    {
        AudioManager.Play("GainCoin");
        coin = CoinPooler.instance.GetPoolCoin();
        for (int i = 0; i < 5; i++)
        {
            if (coin[i] != null)
            {
                coin[i].transform.localScale = Vector3.one * 30;
                coin[i].transform.position = moreCoinHero.transform.position;
                coin[i].SetActive(true);
                coin[i].transform.DOMove(new Vector3(moreCoinHero.transform.position.x + Random.Range(-0.5f, 0.5f), moreCoinHero.transform.position.y + Random.Range(-0.5f, 0.5f), 1), 0.2f).SetEase(Ease.InOutQuad);
                coin[i].transform.DOScale(Vector3.one * 140, 0.2f).SetEase(Ease.InOutQuad);
            }
        }
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
        ChangeGold(200);
    }
    public void MoreCoinNotice()
    {
        AudioManager.Play("GainCoin");
        StartCoroutine(effectCoinNotice());
    }
    IEnumerator effectCoinNotice()
    {
        notice.SetTrigger("hide");
        yield return new WaitForSeconds(0.5f);
        notice.gameObject.SetActive(false);
        coin = CoinPooler.instance.GetPoolCoin();
        for (int i = 0; i < 5; i++)
        {
            if (coin[i] != null)
            {
                coin[i].transform.localScale = Vector3.one * 30;
                coin[i].transform.position = notice.transform.position;
                coin[i].SetActive(true);
                coin[i].transform.DOMove(new Vector3(notice.transform.position.x + Random.Range(-0.5f, 0.5f), notice.transform.position.y + Random.Range(-0.5f, 0.5f), 1), 0.2f).SetEase(Ease.InOutQuad);
                coin[i].transform.DOScale(Vector3.one * 140, 0.2f).SetEase(Ease.InOutQuad);
            }
        }
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
        ChangeGold(200);
    }
    public void ExitNotice()
    {
        AudioManager.Play("Click");
        StartCoroutine(effectExitNotice());
    }
    IEnumerator effectExitNotice()
    {
        notice.SetTrigger("hide");
        yield return new WaitForSeconds(0.5f);
        notice.gameObject.SetActive(false);
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
}
