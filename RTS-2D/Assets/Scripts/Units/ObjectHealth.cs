using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectHealth : MonoBehaviour
{
    public UnityAction onObjectDie;
    private float health;
    private float maxhealth;

    public void DealDamage(Unit enemy, float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Kill");
            onObjectDie?.Invoke();
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(Flasher(GetComponent<Renderer>().material.color));
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
            yield return new WaitForSeconds(.05f);
            renderer.material.color = defaultColor;
            yield return new WaitForSeconds(.05f);
        }
    }

}
