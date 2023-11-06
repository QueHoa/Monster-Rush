using OneHit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    [HideInInspector]
    public Card card;
    public ShopController shop;
    public SpriteRenderer charactor;
    public GameObject fireWork;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("show");
        charactor.sprite = card.charactor;
    }
    public void Effect()
    {
        AudioManager.Play("Congratulation");
        fireWork.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
        AudioManager.Play("Click");
        StartCoroutine(EffectExit());
    }
    IEnumerator EffectExit()
    {
        fireWork.SetActive(false);
        if (shop.heroBox.activeInHierarchy)
        {
            PlayerPrefs.SetInt(card.name, card.numberAds);
            PlayerPrefs.SetString("skinHero", card.name);
        }
        if (shop.monsterBox.activeInHierarchy)
        {
            PlayerPrefs.SetInt(card.name, card.numberAds);
            PlayerPrefs.SetString("skinMonster", card.name);
        }
        anim.SetTrigger("hide");
        yield return new WaitForSeconds(0.5f);
        shop.ChangeGold(-300);
        gameObject.SetActive(false);
    }
}
