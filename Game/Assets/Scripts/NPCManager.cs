using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public GameObject NPC;
    public float spawnDelay;
    public int MaxNPCPopulation;

    int currentPopulationCount;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("SpawnNPC", 2, spawnDelay);
	}

    void SpawnNPC()
    {
        if (currentPopulationCount < MaxNPCPopulation)
        {
            currentPopulationCount++;
            Instantiate(NPC, this.transform);
        }
    }
}
