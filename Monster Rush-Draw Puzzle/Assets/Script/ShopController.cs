using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject gold;
    public Text textGold;

    private int numberGold;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        numberGold = PlayerPrefs.GetInt("gold");
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
        textGold.text = numberGold.ToString();
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
        gameObject.SetActive(false);
    }
    public void Hero()
    {
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
        if(numberGold >= 300)
        {
            int numberButton = Random.Range(0, butHero.Count - 1);
            numberGold -= 300;
            PlayerPrefs.SetInt("gold", numberGold);
            giftHero.card = butHero[numberButton].card;
            giftHero.gameObject.SetActive(true);
        }
        else
        {
            gold.SetActive(false);
            notice.gameObject.SetActive(true);
            notice.SetTrigger("show");
        }
    }
    public void GetGiftMonster()
    {
        if (numberGold >= 300)
        {
            int numberButton = Random.Range(0, butMonster.Count - 1);
            numberGold -= 300;
            PlayerPrefs.SetInt("gold", numberGold);
            giftMonster.card = butMonster[numberButton].card;
            giftMonster.gameObject.SetActive(true);
        }
        else
        {
            gold.SetActive(false);
            notice.gameObject.SetActive(true);
            notice.SetTrigger("show");
        }
    }
    public void MoreCoin()
    {
        PlayerPrefs.SetInt("gold", numberGold + 200);
        
        numberGold = PlayerPrefs.GetInt("gold");
    }
    public void MoreCoinNotice()
    {
        PlayerPrefs.SetInt("gold", numberGold + 200);
        numberGold = PlayerPrefs.GetInt("gold");
        StartCoroutine(effectExitNotice());
    }
    public void ExitNotice()
    {
        StartCoroutine(effectExitNotice());
    }
    IEnumerator effectExitNotice()
    {
        notice.SetTrigger("hide");
        yield return new WaitForSeconds(0.5f);
        gold.SetActive(true);
        notice.gameObject.SetActive(false);
    }
}
