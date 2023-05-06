using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikedWalls : MonoBehaviour
{
    [SerializeField] bool sideWall, upWall;
    
    Vector3 targetVector;
    private void OnTriggerEnter(Collider other)     
    {
        if(other.CompareTag("Player"))
        {
            StartWallMovement();
            
            targetVector = other.transform.position;
        }
    }

    private void StartWallMovement()
    {
        if(sideWall)
        {
            transform.DOMoveX(transform.position.x,targetVector.x);
        }

        if(upWall)
        {
            transform.DOMoveZ(transform.position.z,targetVector.z);
        }
    }
}
