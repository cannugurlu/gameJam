using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GemPlacer : MonoBehaviour
{
    Player player;

    [SerializeField] List<Transform> gemPlacers;
    [SerializeField] GameObject gemPrefab;
    [SerializeField] ParticleSystem connectParticle;

    [SerializeField] Vector3 gemsRotation;
    public List<GameObject> collectedGemParts;
    [SerializeField] float placementTime;
    int triggerCounter;

    private void Start() 
    {
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Collectible"))
        {

            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            other.transform.DOMove(gemPlacers[other.GetComponent<Gem>().keyValue].position,0.5f);

            other.transform.parent = this.gameObject.transform;
            other.transform.localScale = other.transform.localScale;
            other.transform.DORotate(other.GetComponent<Gem>().placementLocation,0.5f);

            if(player.selectedObject != null)
            {
                player.selectedObject = null;
            }

            collectedGemParts.Add(other.gameObject);
            // rotation da ayarlanacak

            if(transform.childCount == 6) 
            {
                for (int i = 0; i < collectedGemParts.Count; i++)
                {
                    collectedGemParts[i].transform.DOMoveY(gemPlacers[1].transform.position.y, placementTime);
                }

                Instantiate(connectParticle,gemPlacers[1].position,Quaternion.identity);
                // partcile effect çalışmıyor 
                connectParticle.transform.position = gameObject.transform.position;
                connectParticle.gameObject.SetActive(true);
                connectParticle.Play();


                player.canTeleport = true;


                if(connectParticle)
                    Debug.Log("wtf");

                StartCoroutine("StopParticle");

            }
        }
    }
    

    IEnumerator StopParticle()
    {
        yield return new WaitForSecondsRealtime(2.0f);

        for (int i = 0; i < collectedGemParts.Count; i++)
        {
            collectedGemParts[i].SetActive(false);
        }

        Instantiate(gemPrefab,gemPlacers[1].position,Quaternion.Euler(gemsRotation.x,gemsRotation.y,gemsRotation.z));
        //connectParticle.Stop(true);
    }
}
