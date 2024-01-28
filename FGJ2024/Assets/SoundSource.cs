using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    public static SoundSource main { get; private set; }
    [SerializeField]
    public AudioSource beerSource;
    [SerializeField]
    public AudioSource shitSource;
    [SerializeField]
    public AudioSource friesSource;
    [SerializeField]
    public AudioSource FollowerSource;

    private void Awake()
    {
        main = this;
    }

    public void hitBeer()
    {
        beerSource.Play();
    }
    public void hitFries()
    {
        friesSource.Play();
    }

    public void hitShit()
    {
        shitSource.Play();
    }
    public void hitFollower()
    {
        FollowerSource.Play();
    }
}
