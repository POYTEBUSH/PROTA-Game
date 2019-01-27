using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour {
    [Range(0,1)]
    public float PlayerAttractionRate;
    public float Payout, MoveSpeed;
    public Vector3 CurrentTarget;
    bool LeftRight;

    public bool AttractedToPlayer = false;
    private Vector3 OffMapTarget;

    private void Awake()
    {
        //False is left
        LeftRight = (Random.value > 0.5f);
        transform.position = new Vector3(LeftRight ? 12 : -12, 0, Random.Range(-5, 5));
        OffMapTarget = new Vector3(LeftRight ? -12 : 12, 0, Random.Range(-5, 5));
        AttractedToPlayer = Random.value < PlayerAttractionRate;
        MoveSpeed += Random.value;
    }
	
	// Update is called once per frame
	void Update () {
        if (AttractedToPlayer)
        {
            CurrentTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        }
        else
        {
            CurrentTarget = OffMapTarget;
        }
        
        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, CurrentTarget, step);
        
        if (Vector3.Distance(transform.position, CurrentTarget) < 0.001f && !AttractedToPlayer)
        {
            ChatLogger.SendChatMessage("Arrived at Location", Color.yellow);
            Destroy(gameObject);
        }
        else if (Vector3.Distance(transform.position, CurrentTarget) < 1f && AttractedToPlayer)
        {
            StartCoroutine(WatchPlayer());
            ChatLogger.SendChatMessage("Someone started watching", Color.green);
            AttractedToPlayer = false;
        }
    }

    IEnumerator WatchPlayer()
    {
        yield return new WaitForSeconds(Random.Range(5,10));
        ChatLogger.SendChatMessage("Should have been payed", Color.cyan);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Money += Mathf.FloorToInt(Random.Range(0, 1));
    }
}
