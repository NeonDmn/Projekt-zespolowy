using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectHealth : MonoBehaviour
{
    public UnityAction<ObjectHealth> onObjectDie;
    private float health;
    [SerializeField] private float maxhealth = 30f;
    private AudioManager audioManager;

    bool dead;
    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
    }

    public void DealDamage(Unit enemy, float damage)
    {
        if (dead) return;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Flasher(GetComponent<Renderer>().material.color));
            health -= damage;
        }
    }

    private void Die()
    {
        dead = true;
        audioManager.getDeath().Play();
        StartCoroutine(Flasher(GetComponent<Renderer>().material.color));
        onObjectDie?.Invoke(this);
        Destroy(this.gameObject);
    }

    public void setMaxHealth(float maxhealth)
    {
        this.maxhealth = maxhealth;
        health = maxhealth;
    }

    IEnumerator Flasher(Color defaultColor)
    {
        var renderer = GetComponent<Renderer>();
        for (int i = 0; i < 2; i++)
        {
            renderer.material.color = Color.red;
            yield return new WaitForSeconds(.1f);
            renderer.material.color = defaultColor;
            yield return new WaitForSeconds(.1f);
        }
    }

}
