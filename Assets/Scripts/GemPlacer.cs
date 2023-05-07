using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GemPlacer : MonoBehaviour
{
    [SerializeField] List<Transform> gemPlacers;

    [SerializeField] GameObject gemPrefab;
    [SerializeField] ParticleSystem connectParticle;
    List<GameObject> collectedGemParts;
    [SerializeField] float placementTime;
    int triggerCounter;
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Colllectible"))
        {

            other.transform.position = gemPlacers[other.GetComponent<Gem>().keyValue].position;

            other.transform.parent = this.gameObject.transform;

            collectedGemParts.Add(other.gameObject);
            triggerCounter++;

            // rotation da ayarlanacak

            if(triggerCounter == 3)
            {
                for (int i = 0; i < collectedGemParts.Count; i++)
                {
                    collectedGemParts[i].transform.DOMoveY(collectedGemParts[1].transform.position.y, placementTime);
                }

                Instantiate(gemPrefab,gemPlacers[1].position,Quaternion.Euler(0,0,0));

                connectParticle.transform.position = gemPrefab.transform.position;

                Invoke("StopParticle",1.5f);
                
            }
        }
    }


    void StopParticle()
    {
        connectParticle.Stop();
    }
}
