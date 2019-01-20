using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int Score;
    public List<SpawnedObjectType.ObjectType> pickedUpObjects;
    public SpawnedObjectType.ObjectType lastPickedup;
    public int currentMultiplier;
    public List<int> scoreIteration = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        pickedUpObjects = new List<SpawnedObjectType.ObjectType>();
        currentMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<SpawnedObjectType>())
        {
            SpawnedObjectType temp = other.GetComponent<SpawnedObjectType>();
            if(temp.objectType == lastPickedup)
            {
                currentMultiplier++;
            }
            else
            {
                currentMultiplier = 1;
            }
            other.GetComponent<Part>().Score();
            lastPickedup = temp.objectType;
            Score += (int)temp.objectType * currentMultiplier;
            scoreIteration.Add(Score);
            pickedUpObjects.Add(temp.objectType);
            Destroy(temp);
        }
    }

    public int CalculateScores()
    {
        for (int i = 0; i < pickedUpObjects.Count; i++)
        {
            Score += (int)pickedUpObjects[i];
        }
        return Score;
    }
}
