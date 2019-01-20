using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private bool go;
    public float spawnTimeGap;
    private float currentSpawnTime;
    public List<ScoreTracker> scoreList = new List<ScoreTracker>();
    public List<Vector3> spawnPositions = new List<Vector3>();
    public List<TextMeshPro> textMeshList = new List<TextMeshPro>();

    public List<GameObject> ScissorDogs = new List<GameObject>();
    public List<GameObject> DropDogs = new List<GameObject>();

    private OBJSpawnScript spawner;
    private int spawnCount;
    bool winnerFound;
    int winner;
    bool countdowntoTransions;
    public float countDownToTransions;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindObjectOfType<OBJSpawnScript>();
        //foreach(ScoreTracker scoreTracker in GameObject.FindObjectsOfType<ScoreTracker>())
        //{
        //    scoreList.Add(scoreTracker);
        //}
        countdowntoTransions = false;
        spawnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(go)
        {
            currentSpawnTime -= Time.deltaTime;
            if(spawner.TimerRunning != false)
            {
                spawner.TimerRunning = false;
            }
            if (currentSpawnTime <= 0 && !winnerFound)
            {

                currentSpawnTime = spawnTimeGap;
                for(int i = 0; i < scoreList.Count; i++)
                {
                    if(spawnCount < scoreList[i].pickedUpObjects.Count)
                    {
                        GameObject gameObject = GetGameObjectByEnum(scoreList[i].pickedUpObjects[spawnCount]);
                        GameObject newGameObject = Instantiate(gameObject, spawnPositions[i], Quaternion.identity);
                        textMeshList[i].text = scoreList[i].scoreIteration[spawnCount].ToString();
                    }
                }

                bool allEnd = true;
                for (int i = 0; i < scoreList.Count; i++)
                {
                    if (spawnCount <= scoreList[i].pickedUpObjects.Count)
                    {
                        allEnd = false;
                        break;
                    }
                }
                if(allEnd)
                {
                    winner = 0;
                    for (int i = 0; i < scoreList.Count; i++)
                    {
                        if (scoreList[winner].scoreIteration.Count > 0)
                        {
                            if (scoreList[i].scoreIteration.Count > 0)
                            {
                                if (scoreList[winner].scoreIteration[scoreList[winner].scoreIteration.Count -1] < scoreList[i].scoreIteration[scoreList[i].scoreIteration.Count - 1])
                                {
                                    winner = i;
                                }
                            }
                        }
                        else
                        {
                            winner++;
                        }
                    }
                    winnerFound = true;
                    if (winner < 4)
                    {
                        GameObject temp = Instantiate(DropDogs[winner], spawnPositions[winner], Quaternion.identity);
                        ScissorDogs[winner].GetComponent<Rigidbody>().velocity = new Vector3(0, 100, 0);
                    }
                    countdowntoTransions = true;
                }
                else
                {
                    spawnCount++;
                }
            }
            if(countdowntoTransions)
            {
                countDownToTransions -= Time.deltaTime;
                if(countDownToTransions <0)
                {
                    SceneManager.LoadScene(0);
                }
            }










        }
    }

    public void StartEndGame()
    {
        go = true;
        Camera.main.transform.position = transform.Find("CameraPosition").transform.position;
        Camera.main.transform.rotation = transform.Find("CameraPosition").transform.rotation;
    }

    private GameObject GetGameObjectByEnum(SpawnedObjectType.ObjectType objectType)
    {
        foreach(GameObject gameObject in spawner.SpawnableObjects)
        {
            if(gameObject.GetComponent<SpawnedObjectType>().objectType == objectType)
            {
                return gameObject;
            }
        }
        return null;
    }

    public bool EndOfGame()
    {
        return go;
    }
}
