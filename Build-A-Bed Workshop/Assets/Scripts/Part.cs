using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    Transform plate;
    OBJSpawnScript spawner;
    private AudioManager AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        plate = GameObject.FindObjectOfType<Plate>().transform;
        spawner = GameObject.FindObjectOfType<OBJSpawnScript>();
        AudioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    public void Rotate(float angle)
    {
        transform.RotateAround(plate.position, Vector3.up, angle * Time.deltaTime);
    }

    public void Score()
    {
        foreach(GameObject spawnedObject in spawner.SpawnedObjects)
        {
            if(spawnedObject == gameObject)
            {
                spawner.SpawnedObjects.Remove(gameObject);
                break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameObject.FindObjectOfType<EndGame>().EndOfGame())
        {
            AudioManager.PlaySound("Squeek");
        }
    }
}
