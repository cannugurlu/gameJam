using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBall : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("MinigameExit"))
        {
            gameManager.instance.miniGamePhase = false;
            gameManager.instance.endingPhase = true;
        }
    }
}
