using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnDatas;

    int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if (!GameManager.Instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min( Mathf.FloorToInt( GameManager.Instance.gameTime / 10f) , spawnDatas.Length - 1);
        if(timer > spawnDatas[level].spawnTime)
        {
            timer = 0;
            spawn();
        }
    }
    void spawn()
    {
        GameObject enermy =  GameManager.Instance.pool.Get(0);
        enermy.transform.position = spawnPoint[Random.Range(1 , spawnPoint.Length)].position;
        enermy.GetComponent<Enermy>().Init(spawnDatas[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}