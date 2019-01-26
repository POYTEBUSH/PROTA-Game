using UnityEngine;
using UnityEngine.UI;

public class HideText : MonoBehaviour
{
    //public GameObject obj;
    private Text text;

	// Use this for initialization
	void Start ()
    {
       text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Image")
        {
            text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Image")
        {
            text.enabled = false;
        }
    }
}
