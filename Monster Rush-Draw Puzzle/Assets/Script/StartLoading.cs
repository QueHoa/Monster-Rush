using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLoading : MonoBehaviour
{
    public GameObject home;
    public AnimationCurve curve;
    public GameObject logo;
    public Image loadingBar;
    public Text loadingText;
    public CanvasGroup alpha;
    private float time;
    private int number;
    private void OnEnable()
    {
        loadingBar.fillAmount = 0;
        number = 0;
        time = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(time > 1.6f)
        {
            logo.SetActive(true);
        }
        time += 0.7f * Time.deltaTime;
        float a = curve.Evaluate(time);
        loadingBar.fillAmount = a;
        number = Mathf.RoundToInt(loadingBar.fillAmount * 100);
        loadingText.text = number.ToString() + "%";
        if (loadingBar.fillAmount >= 0.995f)
        {
            home.SetActive(true);
            alpha.alpha -= 1.2f * Time.deltaTime;
            if (alpha.alpha <= 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
