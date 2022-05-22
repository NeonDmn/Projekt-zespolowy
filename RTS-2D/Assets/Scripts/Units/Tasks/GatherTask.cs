using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherTask : UnitTask
{
    Worker worker;
    private Resource resource;
    private bool timerStarted = false;
    private float timeLeft;

    public GatherTask(Unit parent, Resource resource) : base(parent)
    {
        this.resource = resource;
    }
    public override void OnTaskStart()
    {
        working = true;
        owner.enteredResourceRange += OnEnteredResource;
        //owner.leftResourceRange += OnLeftResource;
        owner.enteredTownHall += DumpResources;

        if (!(worker = owner as Worker))
        {
            Debug.LogError("Jednostka" + owner.gameObject + "musi być obiektem typu Worker by wykonywać GatherTask");
            owner.SwitchTask(new IdleTask(owner));
        }

        timeLeft = resource.GetGatherTime();
        GotoResourceIfSpaceAvailible();
    }
    public override void OnTaskEnd()
    {
        working = false;
        owner.enteredResourceRange -= OnEnteredResource;
        //owner.leftResourceRange -= OnLeftResource;
        owner.enteredTownHall -= DumpResources;
    }

    public override void Tick()
    {
        if (timerStarted)
        {
            if (timeLeft > 0.0f)
            {
                // Zbieranie
                timeLeft -= Time.deltaTime;
            }
            else
            {
                // Sukces
                GatherSuccess();
            }
        }
    }

    private bool IsSpaceAvailible()
    {
        return (
            GameManager.instance.GetTownHallObject(
                owner.gameObject.GetComponent<PlayerTeam>().team
                ).resources.GetResourceFreeCount(resource.GetResourceType()) > 0);
    }

    private void OnEnteredResource(Resource res)
    {
        // Jak jest to resource ktorego szukamy
        if (res.gameObject.Equals(resource.gameObject))
        {
            Debug.Log(owner + " zaczyna zbierac " + resource.GetResourceType());
            GatherStart();
        }
    }

    private void GatherStart()
    {
        timeLeft = resource.GetGatherTime();
        timerStarted = true;
    }

    private void GatherSuccess()
    {
        timerStarted = false;
        worker.AddToBackpack(resource.GetResourceType(), resource.GetResourceYield());
        worker.PrintBackpackContent();
        GotoTownHall();
    }

    private void GotoTownHall()
    {
        //TODO: Zmiania Team
        TownHall th = GameManager.instance.GetTownHallObject(PlayerTeam.Team.Friendly);
        owner.Goto(th.transform.position);
    }

    private void GotoResourceIfSpaceAvailible()
    {
        if (!IsSpaceAvailible())
        {
            // TownHall full of resource
            Debug.LogWarning("Brak miejsca dla " + resource.GetResourceType() + " w Ratuszu!");
            owner.SwitchTask(new IdleTask(owner));
        }
        else
        {
            // Free space for resource availible
            owner.Goto(resource.transform.position);
        }
    }

    private void DumpResources()
    {
        foreach (var it in worker.GetBackpackContent())
        {
            if (it.Value < 1) continue;

            int added = GameManager.instance.GetTownHallObject(
                owner.gameObject.GetComponent<PlayerTeam>().team
                ).resources.Add(it.Key, it.Value);
            Debug.Log("Do ratusza oddano " + added + " " + it.Key + "!");
        }

        worker.ClearBackpack();
        GotoResourceIfSpaceAvailible();
    }
}
