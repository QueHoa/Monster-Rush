using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGiftWin : MonoBehaviour
{
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.SetTrigger("show");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
