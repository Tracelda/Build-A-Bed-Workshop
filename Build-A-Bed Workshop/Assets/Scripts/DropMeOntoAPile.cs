using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMeOntoAPile : MonoBehaviour
{
    public GameObject controlDog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Win()
    {
        
    }
}
