using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneHit.Framework;

public class Confetti : MonoBehaviour
{
    public static Confetti Instance;
    private void Awake()
    {
        Instance = this;
    }
    public ParticleSystem particle;

    public void Fire()
    {
        AudioManager.Play(AudioNames.Confetti);
        particle.Play();
    }
}
