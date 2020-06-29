using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicPageScript : MonoBehaviour
{

    private float rotationSpeed = .5f;
    private bool isPaused;
    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = gameManager.GetComponent<PauseScript>().isPaused;
        if (!isPaused)
        {
            transform.Rotate(new Vector3(0, 1, 0), rotationSpeed);
        }
    }
}
