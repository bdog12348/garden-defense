using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Public Classes
    [System.Serializable]
    public class WaveData
    {
        public int amount;
        public string enemyName;
        public float secondsBetweenSpawns;
    }

    [System.Serializable]
    public class WaveDataList
    {
        public WaveData[] waveData;
    }
    #endregion

    #region Serialized Fields
    [SerializeField] string pathToEnemyFolder = "";
    [SerializeField] string pathToWavesFolder = "";
    [SerializeField] Transform spawnPointParent;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] GameObject playerGameObject;
    [SerializeField] float distanceToPlaySound = 5f;
    #endregion

    #region Private Fields
    // Used to not have to use Resource.Load everytime
    Dictionary<string, TextAsset> allWaveTexts = new Dictionary<string, TextAsset>();
    Dictionary<string, GameObject> allEnemyGos = new Dictionary<string, GameObject>();
    
    //Components
    WaveDataList currentWaveData;
    GameManager gameManager;

    //Wave stuff
    float spawnTimer = float.MaxValue;
    float spawnTimeToWait;
    float waveTimer = 0;
    string wavePrefix = "wave";
    int waveNumber = 0;
    int enemiesAlive = 0;
    bool spawning = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        spawnTimeToWait = gameManager.GameSettings.enemySpawnTime;

        Preload();
        StartLoadNextWave();
    }

    private void Update()
    {
        if (spawning)
        {
            //Returns false when we run out of enemies
            if (!SpawnWave())
            {
                spawning = false;
            }
        }
        else
        {
            if (enemiesAlive <= 0)
            {
                if (waveNumber == allWaveTexts.Keys.Count)
                {
                    //No more waves, you win the game
                    gameManager.WinGame();
                }
                else
                {
                    StartLoadNextWave();
                }
            }
        }
    }

    void LoadWave(TextAsset waveData)
    {
        currentWaveData = JsonUtility.FromJson<WaveDataList>(waveData.text);
    }

    void Preload()
    {
        PreloadWaves();
        PreloadEnemies();
    }

    /// <summary>
    /// Loads all wave jsons into <see cref="allWaveTexts"/>
    /// </summary>
    void PreloadWaves()
    {
        TextAsset[] textList = Resources.LoadAll<TextAsset>(pathToWavesFolder);
        foreach (TextAsset textAsset in textList)
        {
            allWaveTexts.Add(textAsset.name, textAsset);
        }
    }

    /// <summary>
    /// Loads all enemy types into <see cref="allEnemyGos"/>
    /// </summary>
    void PreloadEnemies()
    {
        GameObject[] enemyGameObjectList = Resources.LoadAll<GameObject>(pathToEnemyFolder);
        foreach (GameObject enemyGameObject in enemyGameObjectList)
        {
            allEnemyGos.Add(enemyGameObject.name, enemyGameObject);
        }
    }

    /// <summary>
    /// Given a <paramref name="enemyObject"/> spawns it in at a random spawn point and assigns their target
    /// </summary>
    /// <param name="enemyObject">Enemy GameObject to spawn in</param>
    void SpawnEnemy(GameObject enemyObject)
    {
        Transform randomPosition = spawnPointParent.GetChild(Random.Range(0, spawnPointParent.childCount));
        GameObject spawnedEnemy = Instantiate(enemyObject, randomPosition);
        enemiesAlive++;
        spawnedEnemy.transform.SetParent(transform, true);
        spawnedEnemy.GetComponent<HealthScript>().Died.AddListener(EnemyDied);
        float distanceToPlayer = Vector3.Distance(randomPosition.position, playerGameObject.transform.position);
        Debug.Log(distanceToPlayer);
        if (distanceToPlayer >= distanceToPlaySound)
            AudioSystem.Instance.PlaySound(AudioSystem.Sound.EnemyFarSpawn);
    }

    /// <summary>
    /// Handles spawning wave, returns false when there are no more enemies in wave
    /// </summary>
    /// <returns>False when there are no more enemies, true otherwise</returns>
    bool SpawnWave()
    {
        if (spawnTimer >= spawnTimeToWait)
        {
            WaveData nextEnemy = GetNextEnemy();

            if (nextEnemy == null)
            {
                return false;
            }
            else
            {
                string nextEnemyName = nextEnemy.enemyName;
                SpawnEnemy(GetEnemyFromName(nextEnemyName));
                spawnTimeToWait = nextEnemy.secondsBetweenSpawns;
            }
            spawnTimer = 0f;
        }
        else
        {
            spawnTimer += Time.deltaTime;
        }

        return true;
    }

    GameObject GetEnemyFromName(string enemyName)
    {
        if (allEnemyGos.TryGetValue(enemyName, out GameObject enemy))
        {
            return enemy;
        } else
        {
            Debug.Log($"Enemy {enemyName} does not exist in dictionary");
            return null;
        }
    }

    WaveData GetNextEnemy()
    {
        if (currentWaveData != null)
        {
            for(int i = 0; i < currentWaveData.waveData.Length; i++)
            {
                if (currentWaveData.waveData[i].amount > 0)
                {
                    currentWaveData.waveData[i].amount--;
                    return currentWaveData.waveData[i];
                }
            }
        }

        return null;
    }

    void EnemyDied()
    {
        enemiesAlive--;
    }

    /// <summary>
    /// Waits for time between seconds found in GameSettings and then spawns in next wave
    /// </summary>
    void StartLoadNextWave()
    {
        if (waveTimer >= gameManager.GameSettings.timeBetweenWaves)
        {
            waveNumber++;
            Debug.Log($"Loading new wave: {waveNumber}");
            waveText.text = waveNumber.ToString();
            LoadWave(allWaveTexts[wavePrefix + waveNumber.ToString()]);
            spawning = true;
            enemiesAlive = 0;
        }
        else
        {
            waveTimer += Time.deltaTime;
        }
    }

}
