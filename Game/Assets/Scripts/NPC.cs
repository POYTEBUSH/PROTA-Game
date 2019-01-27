using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string PlayerName;

    Vector3 startPos = new Vector3(-10.0f, 0.07f, -9.5f);
    Vector3 velocity = new Vector3(1.0f, 0.0f, 0.0f);
    Vector3 otherVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 otherPos = new Vector3();

    public Quaternion initialRotation;

    bool facingLeft, staring, paying = false;

    public float minimumTimeToStay, minimumTimeToPay, minimumToPayPennies, maximumToPayPennies;
    
    Rigidbody rb;

    Animator anim;

    Transform playerTransform;

    // Use this for initialization
    void Start ()
    {
        facingLeft = (Random.value > 0.5f);

        velocity.x *= (Random.Range(1.0f, 2.0f));

        startPos.z = Random.Range(-10.0f, -6.0f);

        if (facingLeft)
        {
            startPos.x *= -1.0f;
            velocity.x *= -1.0f;
            transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        }

        initialRotation = transform.localRotation;

        transform.localPosition = startPos;

        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();

        playerTransform = GameObject.Find("PlayerModel").transform;
    }

    // Update is called once per frame
    void Update ()
    {
        rb.velocity = velocity;

        if (transform.localPosition.x * transform.localPosition.x > 110)
            Destroy(transform.gameObject);

        if (velocity.x == 0.0f)
        {
            otherPos = playerTransform.position;
            otherPos.y = transform.position.y;
            rb.angularVelocity = Vector3.zero;
            transform.LookAt(otherPos);
        }
        //if (paying)
        //{
        //    StartCoroutine(PayHomeless());
        //}
        //if (staring)
        //{
        //    otherPos = GameObject.Find("Capsule").transform.position;
        //    otherPos.y = transform.position.y;
        //    transform.LookAt(otherPos);

        //    if (paying)
        //    {
        //        StartCoroutine(PayHomeless());
        //    }            
        //}

        if (!paying)
            StartCoroutine(PayHomeless());
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("NPC"))
        {
            otherVelocity = collision.transform.GetComponent<Rigidbody>().velocity;
            if (!staring)
                HandleNPCCollision(collision);
            else
                velocity = Vector3.zero;
        }
        else if (collision.transform.CompareTag("Player"))
        {
            //otherPos = collision.transform.position;
            //otherPos.y = transform.position.y;
            //transform.LookAt(collision.transform);
            HandlePlayerCollision();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("NPC"))
        {
            velocity.z = 0.0f;
        }
        transform.localRotation = initialRotation;
        rb.angularVelocity = Vector3.zero;
    }

    private void HandleNPCCollision(Collision collision)
    {
        if (velocity.x == 0.0f)
        {
            anim.SetBool("staring", false);
            velocity.x = otherVelocity.x;
            initialRotation = collision.transform.GetComponent<NPC>().initialRotation;
            transform.rotation = initialRotation;
            if (velocity.x < 0.0f)
                facingLeft = true;
            else
                facingLeft = false;
        }
        else if (facingLeft)
        {
            if (otherVelocity.x >= 0.0f)
                velocity.z = -1.0f;
            else if (otherVelocity.sqrMagnitude > velocity.sqrMagnitude)
                velocity.x = otherVelocity.x;
        }
        else if (transform.localPosition.z < -6.0f)
        {
            if (otherVelocity.x <= 0.0f)
                velocity.z = 1.0f;
            else if (otherVelocity.sqrMagnitude > velocity.sqrMagnitude)
                velocity.x = otherVelocity.x;
        }
    }

    private void HandlePlayerCollision()
    {
        int reaction = Mathf.CeilToInt(Random.Range(0.01f, 2.99f));

        switch (reaction)
        {
            case 0:
                velocity.x *= -2.0f;
                transform.rotation = initialRotation;
                transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
                initialRotation = transform.rotation;
                if (facingLeft)
                    facingLeft = false;
                else
                    facingLeft = true;
                break;
            case 1:
                velocity.x *= 2.0f;
                break;
            default:
                anim.SetBool("staring", true);
                StartCoroutine(StareAtHomeless());
                break;
        }
    }

    IEnumerator StareAtHomeless()
    {
        velocity.x = 0.0f;
        staring = true;

        yield return new WaitForSeconds(Random.Range(minimumTimeToStay, minimumTimeToStay + 10));
        staring = false;
    }

    IEnumerator PayHomeless()
    {
        paying = true;

        yield return new WaitForSeconds(Random.Range(minimumTimeToPay, minimumTimeToPay + 3));

        ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        //// CODE TO GIVE MONEY STORED IN minimumToPay TO HOMELESS MAN ////
        ///////////////////////////////////////////////////////////////////
        ///// LEAVE IF TO ENSURE NPC IS STILL WATCHING HOMELESS MAN! //////
        ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        if (staring)
        {
            float moneyToAdd = (Mathf.FloorToInt(Random.Range(minimumToPayPennies, maximumToPayPennies)) / 100.0f);

            CultureInfo gb = CultureInfo.GetCultureInfo("en-GB");

            ChatLogger.SendChatMessage(PlayerName + " has given you " + moneyToAdd.ToString("c2", gb), Color.cyan);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().playerData.Money += moneyToAdd;
            //GameObject.Find("Money Text").GetComponent<Text>().text = (int.Parse(GameObject.Find("MoneyCounter").GetComponent<Text>().text) + Mathf.FloorToInt(Random.Range(minimumToPay, minimumToPay + 5))).ToString();
        }

        paying = false;
    }
}

public enum NPCReactions
{
    Repulsed,
    Busy,
    Interested
}