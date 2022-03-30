using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    //call instance for audio
    public AudioClip footstep;
    public AudioClip shoot;
    public AudioClip explode;
    public AudioClip dead;
    private AudioSource src;

    private static CharacterAudio _instance;
    public static CharacterAudio Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void Footstep()
    {
        src.PlayOneShot(footstep);
    }
    public void Explode()
    {
        src.PlayOneShot(explode);
    }

    public void Shoot()
    {
        src.PlayOneShot(shoot);
    }

    public void Dead()
    {
        src.PlayOneShot(dead);
    }
}
