using System.Collections.Generic;
using UnityEngine;

public class HeroParticle : MonoBehaviour
{
    public static float height, width;
    public HeroDoll[] hero;
    private void Awake()
    {
        height = Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }
    private void OnEnable()
    {
        AdjustBound();
        SpawnHeroDoll();
    }
    private void SpawnHeroDoll()
    {
        for (int i = 0; i < hero.Length; i++)
        {
            if (PlayerPrefs.GetInt(hero[i].card.name) == hero[i].card.numberAds)
            {
                hero[i].Init();
                hero[i].transform.SetPositionAndRotation(RandomPosition(), RandomQuaternion());
            }
        }
    }
    public EdgeCollider2D bound;
    public void AdjustBound()
    {
        List<Vector2> points = new List<Vector2>
        {
            new Vector2(-width, -height),
            new Vector2(-width, height),
            new Vector2(width, height),
            new Vector2(width, -height),
            new Vector2(-width, -height)
        };

        bound.SetPoints(points);
    }
    #region static method
    public static Quaternion RandomQuaternion()
    {
        return Quaternion.Euler(0, 0, Random.Range(0, 360f));
    }
    public static int rangeForce = 3;
    public static Vector2 RandomForce()
    {
        return Vector2.up * Random.Range(-rangeForce, rangeForce) + Vector2.right * Random.Range(-rangeForce, rangeForce);
    }
    public static Vector3 RandomPosition()
    {
        return Vector3.right * Random.Range(-width + 0.5f, width - 0.5f) + Vector3.up * Random.Range(-height + 0.5f, height - 0.5f);
    }
    #endregion
}