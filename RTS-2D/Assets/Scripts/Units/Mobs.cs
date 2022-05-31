using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mobs : Unit
{
    //public GameObject[] unitList;
    // Start is called before the first frame update
    [SerializeField] float searchRadius = 5f;
    [SerializeField] float chaseRadius = 7f;
    [SerializeField] LayerMask targetDetectionLayer;
    [SerializeField] LayerMask targetBlockSeeLayer;
    [SerializeField] Vector3 station;
    Vector3 lastTargetPos;

    new void Update()
    {
        base.Update();

        if (GetDistanceFromStation(transform.position) > chaseRadius)
        {
            GotoAndSwitchToIdle(station);
            return;
        }

        // Lista potencjalnych celów
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, searchRadius, Vector2.zero, 0f, targetDetectionLayer);
        
        // Jak nie ma celu, a są potencjalne cele w pobliżu
        if (!currentTask.isWorking() && hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                if (GetDistanceFromStation(hit.collider.transform.position) < chaseRadius) {
                    SwitchTask(new AttackTask(this, hit.collider.GetComponent<ObjectHealth>()));
                    break;
                }
            }
        }
        // Jest cel
        // else if (target)
        // {
        //     // Jak wystarczająco blisko, atakuj
        //     if (navMeshAgent.remainingDistance <= 0.3f)
        //     {
        //         AttackTarget();
        //     }
            
        //     if(CanSeeTarget())
        //     {
        //         // Jak wciąż widać cel, zapamiętaj jego pozycję
        //         lastTargetPos = target.transform.position;
        //     }
        //     else
        //     {
        //         // Jak nie widać celu, utrać cel
        //         target = null;
        //     }
        //     // Idź do ostatrniej pamiętanej pozycji
        //     navMeshAgent.SetDestination(lastTargetPos);
        // }
        // if (attackTimer > 0f)
        //     attackTimer -= Time.deltaTime;
        // //attackTimer += Time.deltaTime; chyba powinno byc tak
    }

    private float GetDistanceFromStation(Vector3 position)
    {
        return (position - station).magnitude;
    }

    // private bool CanSeeTarget(GameObject customTarget = null)
    // {
    //     GameObject tg = (customTarget) ? customTarget : target;
    //     return !Physics2D.Linecast(transform.position, tg.transform.position, targetBlockSeeLayer);
    // }

    // private void AttackTarget()
    // {
    //     if (attackTimer > 0f)
    //         attackTimer -= Time.deltaTime;
    //     //if (attackTimer >= stats.attackSpeed) //chyba powinno byc tak
    //     //{
    //     Debug.Log("PIG ATTACK");
    //     var targetHelath = target.GetComponent<ObjectHealth>();
    //         targetHelath.DealDamage(null, attackDamage);
    //         audioManager.getAttack().Play();
    //         attackTimer = attackInterval;
    //         //attackTimer = 0; chyba powinno byc tak
    //     //}
    // }

    private void OnDrawGizmosSelected() {
        // Zasięg zauważenia wroga
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
        // Zasięg ataku
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
        // Zasięg pościgu
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(station, chaseRadius);
    }
}
