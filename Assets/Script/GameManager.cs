using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("# Game Constrol")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 300, 400, 500 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
            gameTime = maxGameTime;
        
    }

    public void GetExp()
    {
        exp++;
        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
