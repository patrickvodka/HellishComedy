using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    
    void Update()
    {
         if (Input.GetKey("escape"))
        {
            BackLevel();
        }
    }
    void BackLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(levelIndex);
    }
}
