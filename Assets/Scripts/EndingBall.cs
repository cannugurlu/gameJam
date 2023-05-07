using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBall : MonoBehaviour
{
    [SerializeField] float rotateValue;

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("EndingHole"))
        {
            gameManager.instance.miniGamePhase = false;
            gameManager.instance.endingPhase = true;
        }
    }
}
