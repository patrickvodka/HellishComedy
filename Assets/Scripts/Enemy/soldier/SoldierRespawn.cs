using System.Collections;
using UnityEngine;

public class SoldierRespawn : MonoBehaviour
{
    public float spawnTimer;
    [HideInInspector]
    public bool soldierDied;
    [HideInInspector]
    public GameObject _soldierObj;

    void Update()
    {
        if (soldierDied)
        {
            soldierDied = false;
            StartCoroutine(RespawnTimer());
        }
    }

    IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(spawnTimer);
        _soldierObj = transform.GetChild(0).gameObject;
        _soldierObj.SetActive(true);
    }
}
