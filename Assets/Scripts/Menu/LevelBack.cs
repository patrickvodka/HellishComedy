using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBack : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKey("return"))
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
        }

    }
    
    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(levelIndex);

    }
        
    }