using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public GameObject NPC;
    public float minSecondsBetweenSpawn;
    public float maxSecondsBetweenSpawn;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(SpawnNPC());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator SpawnNPC()
    {
        yield return new WaitForSeconds(Random.Range(minSecondsBetweenSpawn, maxSecondsBetweenSpawn));

        Instantiate(NPC);

        StartCoroutine(SpawnNPC());
    }
}
