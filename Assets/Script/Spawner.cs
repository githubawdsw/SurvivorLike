using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    float timer;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 0.5f)
        {
            spawn();
            timer = 0;
        }
    }
    void spawn()
    {
        GameObject enermy =  GameManager.Instance.pool.Get(Random.Range(0, 2));
        enermy.transform.position = spawnPoint[Random.Range(1 , spawnPoint.Length)].position;

    }
}
