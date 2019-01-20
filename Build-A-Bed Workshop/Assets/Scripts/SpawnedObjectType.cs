using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObjectType : MonoBehaviour
{
    public enum ObjectType
    {
        Sofa = 1,
        SoftBed =2,
        Bone = 3,
        TennisBall = 4,
        Burger = 5,
        Spike = 6,
        Chicken = 10,

    }
    public ObjectType objectType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
