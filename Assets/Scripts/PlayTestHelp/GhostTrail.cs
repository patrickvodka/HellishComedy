using System;
using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using UnityEditor;

public class GhostTrail : MonoBehaviour
{
    private Movement move;
    public float cloneSpawnTimer;
    private GameObject ghostClone;
    private GameObject ghostStacks;
    private bool canSpawn=true;
    public bool destroyAllCopy;
    public bool invisibleGhost;

    private void Awake()
    {
        move = GetComponent<Movement>();
        ghostStacks = GameObject.FindWithTag("FallenStacks");
        ghostClone = transform.Find("FallenCopy").gameObject;
    }

    public void Update()
    {
        if (invisibleGhost)
        {
            foreach (Transform child in ghostStacks.transform) {
                child.gameObject.SetActive(false);
            } 
            
        }
        else
        {
            foreach (Transform child in ghostStacks.transform) {
                child.gameObject.SetActive(true);
            } 
        }
        if (destroyAllCopy)
        {
            foreach (Transform child in ghostStacks.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
        if (move.GhostTrail)
        {
            if(canSpawn)
            StartCoroutine(SpawnClone());
        }
    }

    private IEnumerator SpawnClone()
    {
        canSpawn = false;
        Instantiate(ghostClone,transform.position, quaternion.Euler(0, 0, 0),ghostStacks.transform);
        yield return new WaitForSeconds(cloneSpawnTimer);
        canSpawn = true;

    }
}
