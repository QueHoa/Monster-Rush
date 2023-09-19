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

    public GameObject home;
    public GameObject loading;
    public GameObject[] rank;
    public Button backHome;
    public Button skipLevel;
    public Button iconAds;
    public Button reload;
    public Text textLevel;

    private int unlockedLevelsNumber;

    // Start is called before the first frame update
    void Start()
    {
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        numberPlaying = unlockedLevelsNumber;
    }
    private void OnEnable()
    {
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
        textLevel.text = "level " + numberPlaying.ToString();
    }
    public void BackHome()
    {
        StartCoroutine(loadingBackHome());
    }
    IEnumerator loadingBackHome()
    {
        loading.SetActive(true);
        Transform Level = transform.Find(numberPlaying.ToString() + "(Clone)");
        if (Level != null)
        {
            Destroy(Level.gameObject);
        }
        yield return new WaitForSeconds(0.7f);
        home.SetActive(true);
        gameObject.SetActive(false);
    }
    public void SwapWeapon()
    {
        swap = true;
    }
    public void Skip()
    {
        if (numberPlaying == unlockedLevelsNumber)
        {
            PlayerPrefs.SetInt("levelsUnlocked", unlockedLevelsNumber + 1);
        }
        Transform Level = transform.Find(numberPlaying.ToString() + "(Clone)");
        if (Level != null)
        {
            Destroy(Level.gameObject);
        }
        numberPlaying++;
        GameObject loadedPrefab = Resources.Load<GameObject>(numberPlaying.ToString());
        GameObject level = Instantiate(loadedPrefab, gameObject.transform);
        level.transform.SetParent(gameObject.transform, false);
    }
}
