using OneHit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    [HideInInspector]
    public int numberPlaying;
    [HideInInspector]
    public int numberRank;
    [HideInInspector]
    public bool swap;
    [HideInInspector]
    public GameObject level;

    public GameObject home;
    public GameObject loading;
    public GameObject[] rank;
    public Button backHome;
    public Button skipLevel;
    public Button iconAds;
    public Button reload;
    public Text textLevel;
    public int numerLevel;
    public InputField inputField;

    private int unlockedLevelsNumber;

    void Awake()
    {
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
    }
    private void OnEnable()
    {
        GameObject loadedPrefab = Resources.Load<GameObject>(unlockedLevelsNumber.ToString());
        level = Instantiate(loadedPrefab);
        numberPlaying = unlockedLevelsNumber;
        backHome.interactable = true;
        skipLevel.interactable = true;
        iconAds.interactable = true;
        reload.interactable = true;
        swap = false;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < numberRank)
            {
                rank[i].SetActive(true);
                if (i == numberRank - 1)
                {
                    rank[i].transform.localScale = Vector3.one * 1.1f;
                }
                else
                {
                    rank[i].transform.localScale = Vector3.one;
                }
            }
            else
            {
                rank[i].SetActive(false); ;
            }
        }
        textLevel.text = "level " + unlockedLevelsNumber.ToString();
    }
    public void BackHome()
    {
        AudioManager.Play("Click");
        StartCoroutine(loadingBackHome());
    }
    IEnumerator loadingBackHome()
    {
        loading.SetActive(true);
        if (level != null)
        {
            Destroy(level);
        }
        yield return new WaitForSeconds(0.7f);
        home.SetActive(true);
        gameObject.SetActive(false);
    }
    public void SwapWeapon()
    {
        AudioManager.Play("Click");
        swap = true;
    }
    public void Skip()
    {
        AudioManager.Play("Click");
        if (level != null)
        {
            Destroy(level);
        }
        PlayerPrefs.SetInt("levelsUnlocked", unlockedLevelsNumber + 1);
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        GameObject loadedPrefab = Resources.Load<GameObject>(unlockedLevelsNumber.ToString());
        level = Instantiate(loadedPrefab);
    }
    public void Go()
    {
        if (level != null)
        {
            Destroy(level.gameObject);
        }
        if (int.Parse(inputField.text) <= numerLevel && int.Parse(inputField.text) >= 0)
        {
            unlockedLevelsNumber = int.Parse(inputField.text);
        }
        GameObject loadedPrefab = Resources.Load<GameObject>(unlockedLevelsNumber.ToString());
        level = Instantiate(loadedPrefab);
    }
    public void increase()
    {
        if (level != null)
        {
            Destroy(level.gameObject);
        }
        if(unlockedLevelsNumber < numerLevel)
        {
            PlayerPrefs.SetInt("levelsUnlocked", unlockedLevelsNumber + 1);
            unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        }
        GameObject loadedPrefab = Resources.Load<GameObject>(unlockedLevelsNumber.ToString());
        level = Instantiate(loadedPrefab);
    }
    public void reduce()
    {
        if (level != null)
        {
            Destroy(level.gameObject);
        }
        if (unlockedLevelsNumber > 1) 
        {
            PlayerPrefs.SetInt("levelsUnlocked", unlockedLevelsNumber - 1);
            unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        }
        GameObject loadedPrefab = Resources.Load<GameObject>(unlockedLevelsNumber.ToString());
        level = Instantiate(loadedPrefab);
    }
}
