using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        DOTween.Clear(true);
        StartCoroutine(LoadScene(2));
    }

    IEnumerator LoadScene(int sceneInt)
    {
        yield return new WaitForSecondsRealtime(gameManager.instance.sceneLoadDelayTime);
        SceneManager.LoadScene(sceneInt);
    }
}
