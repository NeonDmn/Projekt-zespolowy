using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource attack;
    public AudioSource death;
    public AudioSource build;

    // Start is called before the first frame update
    void Awake()
    {
        attack = gameObject.AddComponent<AudioSource>();
        death = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAttack(AudioClip  clip)
    {
        attack.clip = clip;
    }

    public void setDeath(AudioClip clip)
    {
        death.clip = clip;
    }

    public void setBuild(AudioClip clip)
    {
        build = gameObject.AddComponent<AudioSource>();
        build.clip = clip;
    }

    public AudioSource getAttack()
    {
        return attack;
    }

    public AudioSource getDeath()
    {
        return death;
    }

    public AudioSource getBuild()
    {
        return build;
    }
}
