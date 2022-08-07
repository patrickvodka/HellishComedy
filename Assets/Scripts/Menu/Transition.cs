using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    
    public void LoadLevelTuto()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
       yield return new WaitForSeconds(0);
    }

}

