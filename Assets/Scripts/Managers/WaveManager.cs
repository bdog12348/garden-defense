using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Public Classes
    [System.Serializable]
    public class WaveData
    {
        public int amount;
        public string enemyName;
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
    #endregion

    #region Private Fields
    // Used to not have to use Resource.Load everytime
    Dictionary<string, TextAsset> allWaveTexts = new Dictionary<string, TextAsset>();
    Dictionary<string, GameObject> allEnemyGos = new Dictionary<string, GameObject>();

    WaveDataList currentWaveData;
    GameManager gameManager;

    float spawnTimer = float.MaxValue;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();

        Preload();
        LoadWave(allWaveTexts["wave1"]);
    }

    private void Update()
    {
        if (spawnTimer >= gameManager.GameSettings.timeBetweenWaves)
        {
            string nextEnemy = GetNextEnemy();
            if (nextEnemy.Equals(""))
            {
                //Ran out of enemies, you win ig
                //TODO: Change later to load in next wave json
                gameManager.WinGame();
            }else
            {
                SpawnEnemy(GetEnemyFromName(nextEnemy));
            }
            spawnTimer = 0f;
        }else
        {
            spawnTimer += Time.deltaTime;
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

    void PreloadWaves()
    {
        TextAsset[] textList = Resources.LoadAll<TextAsset>(pathToWavesFolder);
        foreach (TextAsset textAsset in textList)
        {
            allWaveTexts.Add(textAsset.name, textAsset);
        }
    }

    void PreloadEnemies()
    {
        GameObject[] enemyGameObjectList = Resources.LoadAll<GameObject>(pathToEnemyFolder);
        foreach (GameObject enemyGameObject in enemyGameObjectList)
        {
            allEnemyGos.Add(enemyGameObject.name, enemyGameObject);
        }
    }

    void SpawnEnemy(GameObject enemyObject)
    {
        GameObject spawnedEnemy = Instantiate(enemyObject, spawnPointParent.GetChild(Random.Range(0, spawnPointParent.childCount)));
        spawnedEnemy.transform.SetParent(transform, true);

        spawnedEnemy.GetComponent<NavigationScript>().GetTarget();
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

    string GetNextEnemy()
    {
        if (currentWaveData != null)
        {
            for(int i = 0; i < currentWaveData.waveData.Length; i++)
            {
                if (currentWaveData.waveData[i].amount > 0)
                {
                    currentWaveData.waveData[i].amount--;
                    return currentWaveData.waveData[i].enemyName;
                }
            }
        }

        return "";
    }
}
