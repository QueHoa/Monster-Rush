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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
        StartCoroutine(EffectExit());
    }
    IEnumerator EffectExit()
    {
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
        gameObject.SetActive(false);
    }
}
