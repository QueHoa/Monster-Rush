using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text textCoin;

    public GameObject coinPrefab;
    public Transform target;
    public int maxCoin;
    Queue<GameObject> coinQueue = new Queue<GameObject>();
    [Range(0.5f, 0.9f)]
    public float minAnimDuration;
    [Range(0.9f, 2f)]
    public float maxAnimDuration;

    private Vector3 targetPos;
    private int c = 0;
    private void Awake()
    {
        targetPos = target.position;

        PrepareCoins();
    }
    public int Coins
    {
        get { return c; }
        set
        {
            c = value;
            textCoin.text = Coins.ToString();
        }
    }
    void PrepareCoins()
    {
        GameObject coins;
        coins = Instantiate(coinPrefab);
        coins.transform.parent = transform;
        coins.SetActive(false);
        coinQueue.Enqueue(coins);
    }
    public void AddCoins(int amount)
    {
        Coins += amount;
    }
}
