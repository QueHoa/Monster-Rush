using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public AnimationCurve curve;
    public Image loadingBar;
    public Text loadingText;
    public CanvasGroup alpha;
    private float time;
    private int number;
    private void OnEnable()
    {
        alpha.alpha = 0;
        DOTween.To(() => alpha.alpha, x => alpha.alpha = x, 1, 0.5f);
        loadingBar.fillAmount = 0;
        number = 0;
        time = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(alpha.alpha == 1)
        {
            time += 0.3f * Time.deltaTime;
        }
        float a = curve.Evaluate(time);
        loadingBar.fillAmount = a;
        number = Mathf.RoundToInt(a * 100);
        loadingText.text = number.ToString() + "%";
        if (loadingBar.fillAmount >= 0.995f)
        {
            alpha.alpha -= 2 * Time.deltaTime;
            if(alpha.alpha <= 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
