using OneHit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMonster : MonoBehaviour
{
    public Card card;
    public Image charactor;
    public GameObject selecting;
    public GameObject locked;
    public Text textAds;
    //public Transform grid;

    private int numberAds;
    private string skinMonster;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        //PlayerPrefs.SetInt(card.name, 0);
        if (!PlayerPrefs.HasKey(card.name))
        {
            PlayerPrefs.SetInt(card.name, 0);
        }
        skinMonster = PlayerPrefs.GetString("skinMonster");
        numberAds = PlayerPrefs.GetInt(card.name);
        charactor.sprite = card.charactor;
        if (numberAds < card.numberAds)
        {
            locked.SetActive(true);
            charactor.color = new Color(0, 0, 0, 1);
            textAds.text = numberAds.ToString() + "/" + card.numberAds.ToString();
        }
        else
        {
            locked.SetActive(false);
            charactor.color = new Color(1, 1, 1, 1);
            if (card.name == skinMonster)
            {
                selecting.SetActive(true);
            }
            else
            {
                selecting.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        skinMonster = PlayerPrefs.GetString("skinMonster");
        numberAds = PlayerPrefs.GetInt(card.name);
        if (numberAds < card.numberAds)
        {
            locked.SetActive(true);
            charactor.color = new Color(0, 0, 0, 1);
            textAds.text = numberAds.ToString() + "/" + card.numberAds.ToString();
        }
        else
        {
            locked.SetActive(false);
            charactor.color = new Color(1, 1, 1, 1);
            if (card.name == skinMonster)
            {
                selecting.SetActive(true);
                GameManager.Instance.skeletonMonster = card.skeletonDataAsset;
                GameManager.Instance.timeAttack = card.timeAttack;
            }
            else
            {
                selecting.SetActive(false);
            }
        }
    }
    public void Choose()
    {
        AudioManager.Play("Click");
        if (numberAds < card.numberAds)
        {
            if (numberAds == card.numberAds - 1)
            {
                PlayerPrefs.SetString("skinMonster", card.name);
            }
            PlayerPrefs.SetInt(card.name, numberAds + 1);
            numberAds = PlayerPrefs.GetInt(card.name);
        }
        else
        {
            PlayerPrefs.SetString("skinMonster", card.name);
            if (card.name == skinMonster)
            {
                selecting.SetActive(true);
            }
            else
            {
                selecting.SetActive(false);
            }
        }
    }
}
