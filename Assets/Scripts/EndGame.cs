using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    Player player;

    [Header("MiniGame")]
    [SerializeField] Transform playersMiniGamePosition;
    [SerializeField] Vector3 playersMiniGameRotation;

    [SerializeField] GameObject miniGame;

    [Header("Mouse")]

    [SerializeField] float pitchMax;
    [SerializeField] float pitchMin;
    [SerializeField] float yawMax;
    [SerializeField] float yawMin;
    [SerializeField] float mouseControllerX;
    [SerializeField] float mouseControllerZ;


    [Header("Ending")]

    [SerializeField] Transform playersEndGamePosition;

    float pitch=0.0f;
    float yaw = 0.0f;
    
    private void Start() 
    {

        player = FindObjectOfType<Player>();

    }

    void Update() 
    {
        if(gameManager.instance.miniGamePhase)
        {
            player.transform.DOMove(playersMiniGamePosition.position,1.5f);
            player.transform.DORotate(playersMiniGameRotation,1.5f);
            yaw += mouseControllerX * Input.GetAxis("Mouse Y") ;
            pitch -= mouseControllerZ * Input.GetAxis("Mouse X");

            pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

            yaw = Mathf.Clamp(yaw,yawMin,yawMax);

            transform.eulerAngles = new Vector3(yaw, 0, pitch);
        }

        else if(gameManager.instance.endingPhase)
        {
            player.transform.DOMove(playersEndGamePosition.position,1.5f).OnComplete(() =>
            {
                player.transform.DORotate(playersMiniGameRotation,1.5f).
                    SetEase( Ease.InOutSine ).SetLoops( -1, LoopType.Yoyo);
            });
                

            // sonlanma durumuna göre silinecek
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameManager.instance.gameHasEnded = true;
            gameManager.instance.miniGamePhase = true;
        }    
    }
}
