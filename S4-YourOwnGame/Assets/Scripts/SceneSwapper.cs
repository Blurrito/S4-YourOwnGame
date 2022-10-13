using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    [SerializeField] int nextScene;

    private void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GoToNextScene();
        }
    }
}
