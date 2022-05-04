using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Settings", fileName = "GameSettings")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Wave Settings")]
    public float timeBetweenWaves = 5f;
    public float enemySpawnTime = 3f;

    [Header("Other Settings")]
    public float maxGardenHealth = 100f;
   
}
