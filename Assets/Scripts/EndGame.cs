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

    [SerializeField] Transform doorsEndGamePosition;

    [SerializeField] GameObject plain;

    [SerializeField] Material plainMat;

    [SerializeField] GameObject leftDoor;

    [SerializeField] GameObject rightDoor;


    float pitch=0.0f;
    float yaw = 0.0f;
    
    private void Start() 
    {

        player = FindObjectOfType<Player>();

    }

    void Update() 
    {

        miniGame.transform.position = new Vector3(-0.469999999f,5.375f,458.880005f);

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
            leftDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position,doorsEndGamePosition.position,15.0f);
            rightDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position,doorsEndGamePosition.position,15.0f);
            // sonlanma durumuna göre silinecek
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameManager.instance.gameHasEnded = true;
            gameManager.instance.miniGamePhase = true;

            plain.GetComponent<MeshRenderer>().material = plainMat;
        }    
    }
}
