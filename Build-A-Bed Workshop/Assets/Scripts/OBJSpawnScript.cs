using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJSpawnScript : MonoBehaviour
{
    public GameObject TestSpawnOBJ;
    public GameObject SpawnLocationOBJ;
    public GameObject ObjectToSpawn;
    public Vector3 SpawnLocation;
    public float SpawnRadius;
    public int SpawnLimit;
    public float Timer;
    public float TimerTarget;
    public bool TimerUp;
    public bool Spawnobject;
    public bool TimerRunning;
    public bool SpawnRandom;
    private AudioManager AudioManager;


    public List<GameObject> SpawnableObjects = new List<GameObject>();
    public List<GameObject> SpawnedObjects = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        AudioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RunTimer();

        if (SpawnedObjects.Count >= SpawnLimit)
        {
            Spawnobject = false;
        }
        else
        {
            Spawnobject = true;
        }

        if ((TimerUp) && (SpawnRandom))
        {
            Vector2 Temp = Random.insideUnitCircle * SpawnRadius;
            SpawnLocation = new Vector3 (Temp.x , 0, Temp.y);
            if (Spawnobject)
            {
                //Instantiate(TestSpawnOBJ, (SpawnLocation + transform.position), Quaternion.identity);
                GameObject NewlySpwanedOBJ = Instantiate(pickRandomObject(), (SpawnLocation + transform.position), Quaternion.identity);
                SpawnedObjects.Add(NewlySpwanedOBJ);
                AudioManager.PlaySound("Pop");
                Spawnobject = false;
            }


            TimerUp = false;
        }
        else if ((TimerUp) && (!SpawnRandom))
        {
            Vector2 Temp = Random.insideUnitCircle * SpawnRadius;
            SpawnLocation = new Vector3(Temp.x, 0, Temp.y);
            if (Spawnobject)
            {
                //Instantiate(TestSpawnOBJ, (SpawnLocation + transform.position), Quaternion.identity);
                GameObject NewlySpwanedOBJ = Instantiate(PickObjectByPercentage(), (SpawnLocation + transform.position), Quaternion.identity);
                SpawnedObjects.Add(NewlySpwanedOBJ);
                AudioManager.PlaySound("Pop");
                Spawnobject = false;
            }

            TimerUp = false;
        }
    }

    GameObject pickRandomObject()
    {
        // up logic that checks stuff. 



        return SpawnableObjects[Random.Range(0, SpawnableObjects.Count)];
    }

    public void RunTimer()
    {
        if (TimerRunning)
        {
            if(Timer < TimerTarget)
            {
                Timer += Time.deltaTime;
            }
            else if (Timer >= TimerTarget)
            {
                TimerUp = true;
                TimerTarget = Random.Range(2, 4);
                Timer = 0;
            }
        }
    }

    GameObject PickObjectByPercentage()
    {
        int SpawnChance;
        SpawnChance = Random.Range(0, 100);

        if (CheckIfInRange(SpawnChance, 0, 20))
        {
            ObjectToSpawn = SpawnableObjects[0]; // SleekSofa
        }
        else if (CheckIfInRange(SpawnChance, 21, 40))
        {
            ObjectToSpawn = SpawnableObjects[1]; // SoftBed
        }
        else if (CheckIfInRange(SpawnChance, 41, 55))
        {
            ObjectToSpawn = SpawnableObjects[2]; // Bone
        }
        else if (CheckIfInRange(SpawnChance, 56, 70))
        {
            ObjectToSpawn = SpawnableObjects[3]; // Tennnis Ball
        }
        else if (CheckIfInRange(SpawnChance, 71, 85))
        {
            ObjectToSpawn = SpawnableObjects[4]; // Burger
        }
        else if (CheckIfInRange(SpawnChance, 86, 95))
        {
            ObjectToSpawn = SpawnableObjects[5]; // Spike Ball
        }
        else if (CheckIfInRange(SpawnChance, 96, 100))
        {
            ObjectToSpawn = SpawnableObjects[6]; // Chicken
        }

        return ObjectToSpawn;
    }

    public bool CheckIfInRange(int NumberToCheck, int LowerNumber, int HigherNumber)
    {
        if ((NumberToCheck >= LowerNumber) && (NumberToCheck <= HigherNumber))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


