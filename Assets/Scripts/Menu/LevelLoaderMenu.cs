using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public bool NextLevel = false;


    void Update()
    {
        if (Input.GetKey("return"))
        {
            Application.Quit();
        }

        if (NextLevel)
        {
            LoadNextLevel();
        }
    }
    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);

    }
        
    }