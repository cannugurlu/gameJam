using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    
    public GameObject futureMaze;

    public GameObject AncientMaze;

    public GameObject endhud;

    public GameObject teleportText;
    
    public static gameManager instance { get; set; }

    public Transform firstpos1, firstpos2;

    public bool gameHasEnded;

    public bool miniGamePhase;

    public bool endingPhase;

    public float sceneLoadDelayTime;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}
