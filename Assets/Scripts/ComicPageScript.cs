using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicPageScript : MonoBehaviour
{
    private float rotationSpeed = .5f;
    public GameObject pauseObject;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Here is where I would raise a counter for pages collected or somethin, but we dont have one just yet.
            //I would also trigger whatever victory "yay!" thing here the player has.
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = pauseObject.GetComponent<PauseScript>().isPaused;
        if (!isPaused)
        {
            transform.Rotate(new Vector3(0, 1, 0), rotationSpeed);
        }
    }
}
