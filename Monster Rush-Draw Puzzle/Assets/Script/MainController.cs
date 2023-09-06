using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject home;
    public GameObject loading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackHome()
    {
        StartCoroutine(loadingBackHome());
    }
    IEnumerator loadingBackHome()
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        home.SetActive(true);
    }
}
