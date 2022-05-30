using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectHealth : MonoBehaviour
{
    public UnityAction onObjectDie;
    private float health;
    [SerializeField] private float maxhealth = 30f;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
    }

    public void DealDamage(Unit enemy, float damage)
    {
        
        if (health <= 0)
        {
            Debug.Log("Kill");
            
            onObjectDie?.Invoke();
            audioManager.getDeath().Play();
            StartCoroutine(Flasher(GetComponent<Renderer>().material.color));
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(Flasher(GetComponent<Renderer>().material.color));
            health -= damage;
        }
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
