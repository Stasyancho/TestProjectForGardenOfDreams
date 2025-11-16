using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int count;
    [SerializeField] private float radius;

    private void Start()
    {
        GenerateEnemies();
    }

    public void GenerateEnemies()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemy, new Vector2(Random.Range(-10,10), Random.Range(-10,10)).normalized * radius, Quaternion.identity);
        }
    }
}
