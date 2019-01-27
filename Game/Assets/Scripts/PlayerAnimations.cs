using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Dance();
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Busk();
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Violin();
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            BeatBox();
        }

        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            Beg();
        }

        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            Band();
        }
    }


    public void Busk()
    {
        animator.SetTrigger("Busk");
        FindObjectOfType<SoundsManager>().Play("Busk");
    }

    public void Dance()
    {
        animator.SetTrigger("Dance");
        FindObjectOfType<SoundsManager>().Play("Dance");
    }

    public void Violin()
    {
        FindObjectOfType<SoundsManager>().Play("Violin");
    }

    public void BeatBox()
    {
        FindObjectOfType<SoundsManager>().Play("BeatBox");
    }

    public void Beg()
    {
        FindObjectOfType<SoundsManager>().Play("Beg");
    }

    public void Band()
    {
        FindObjectOfType<SoundsManager>().Play("Band");
    }
}
