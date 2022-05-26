using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/UnitStats")]
public class UnitStats : ScriptableObject
{
    public float attackValue;
    public float attackRange;
    public float attackSpeed;
    public float health;
    public AudioClip[] audioClip;

}
