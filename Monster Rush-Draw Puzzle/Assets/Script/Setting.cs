using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneHit.Framework;

public class Setting : MonoBehaviour
{
    public HomeController home;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("show");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetExit()
    {
        AudioManager.Play("Click");
        StartCoroutine(Exit());
    }
    IEnumerator Exit()
    {
        anim.SetTrigger("hide");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
