using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public float rotationSpeed;
    private List<Part> collidingParts = new List<Part>();

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
        foreach(Part part in collidingParts)
        {
            part.Rotate(rotationSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Part>())
        {
            collidingParts.Add(collision.gameObject.GetComponent<Part>());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Part>())
        {
            collidingParts.Remove(collision.gameObject.GetComponent<Part>());
        }
    }
}
