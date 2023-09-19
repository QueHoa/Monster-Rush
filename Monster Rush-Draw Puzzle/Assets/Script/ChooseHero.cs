using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseHero : MonoBehaviour
{
    public Card card;
    public Image charactor;
    public GameObject selecting;
    public GameObject locked;
    public Text textAds;
    //public Transform grid;

    private int numberAds;
    private string skinHero;
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
        skinHero = PlayerPrefs.GetString("skinHero");
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
            if (card.name == skinHero)
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
        skinHero = PlayerPrefs.GetString("skinHero");
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
            if (card.name == skinHero)
            {
                selecting.SetActive(true);
            }
            else
            {
                selecting.SetActive(false);
            }
        }
    }
    public void Choose()
    {
        if(numberAds < card.numberAds)
        {
            if (numberAds == card.numberAds - 1)
            {
                PlayerPrefs.SetString("skinHero", card.name);
            }
            PlayerPrefs.SetInt(card.name, numberAds + 1);
        }
        else
        {
            PlayerPrefs.SetString("skinHero", card.name);
            if (card.name == skinHero)
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
