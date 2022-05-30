using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mobs : MonoBehaviour
{
    //public GameObject[] unitList;
    // Start is called before the first frame update
    [SerializeField] float searchRadius = 5f;
    [SerializeField] LayerMask targetDetectionLayer;
    [SerializeField] LayerMask targetBlockSeeLayer;
    [SerializeField] UnitStats stats;
    float attackDamage;
    float attackInterval;
    GameObject target;
    NavMeshAgent navMeshAgent;
    Vector3 lastTargetPos;
    float attackTimer;
    AudioManager audioManager;

    void Awake()
    {
        // Audio manager
        audioManager = GetComponent<AudioManager>();

        // Navmesh init
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
		navMeshAgent.updateUpAxis = false;
        lastTargetPos = transform.position;

        // Stat init
        attackDamage = stats.attackValue;
        attackInterval = stats.attackSpeed;
        GetComponent<ObjectHealth>().setMaxHealth(stats.health);

        if(!audioManager.attack || !audioManager.death )
        {
            Debug.Log("Set UNIT AUDIO");
            audioManager.setAttack(stats.audioClip[2]);
            audioManager.setDeath(stats.audioClip[1]);
        }
    }

    void Update()
    {
        // Lista potencjalnych celów
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, searchRadius, Vector2.zero, 0f, targetDetectionLayer);
        
        // Jak nie ma celu, a są potencjalne cele w pobliżu
        if (!target && hits.Length > 0)
        {
            // Czy jest nawiązywany kontakt wzrokowy z którymś z celów
            foreach (var hit in hits)
            {
                if (CanSeeTarget(hit.collider.gameObject))
                {
                    // Widać cel, wybrać i atakować
                    target = hit.collider.gameObject;
                    break;
                }
            }
        }
        // Jest cel
        else if (target)
        {
            // Jak wystarczająco blisko, atakuj
            if (navMeshAgent.remainingDistance <= 0.3f)
            {
                AttackTarget();
            }
            
            if(CanSeeTarget())
            {
                // Jak wciąż widać cel, zapamiętaj jego pozycję
                lastTargetPos = target.transform.position;
            }
            else
            {
                // Jak nie widać celu, utrać cel
                target = null;
            }
            // Idź do ostatrniej pamiętanej pozycji
            navMeshAgent.SetDestination(lastTargetPos);
        }

        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;
    }

    private bool CanSeeTarget(GameObject customTarget = null)
    {
        GameObject tg = (customTarget) ? customTarget : target;
        return !Physics2D.Linecast(transform.position, tg.transform.position, targetBlockSeeLayer);
    }

    private void AttackTarget()
    {
        if (attackTimer > 0f)
        {
            return;
        }

        var targetHelath = target.GetComponent<ObjectHealth>();
        targetHelath.DealDamage(null, attackDamage);

        attackTimer = attackInterval;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}
