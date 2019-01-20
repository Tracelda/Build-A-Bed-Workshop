using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneClawScript : MonoBehaviour
{
    public CraneControlScript CraneMover;

    private Joint joint;
    private Rigidbody rb;
    private AudioManager AudioManager;
    private Transform grabPoint;
    bool canPickUp;

    GameObject PickedUpObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<Joint>();
        joint.connectedAnchor = CraneMover.transform.position;
        AudioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
        grabPoint = transform.Find("GrabPoint");
    }

    // Update is called once per frame
    void Update()
    {
        joint.connectedAnchor = CraneMover.transform.position;
        if (rb.velocity.magnitude == 0)
        {
            rb.velocity = new Vector3(0, -0.01f, 0);
        }
        joint.anchor = Vector3.up * CraneMover.GetCurrentAnchorDistance();
    }

    private void OnTriggerStay(Collider other)
    {
        if(CraneMover.GetPickUp())
        {
            if (other.GetComponent<Part>())
            {
                if (PickedUpObject == other.gameObject || PickedUpObject == null)
                {
                    if(other.gameObject.layer != LayerMask.NameToLayer("IgnoreBowlBounds"))
                    {
                        other.gameObject.layer = LayerMask.NameToLayer("IgnoreBowlBounds");
                    }
                    other.GetComponent<Rigidbody>().velocity = rb.velocity;
                    other.transform.position = Vector3.MoveTowards(other.transform.position, grabPoint.position, Time.deltaTime * 10);
                    PickedUpObject = other.gameObject;
                    other.GetComponent<Rigidbody>().freezeRotation = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == PickedUpObject)
        {
            PickedUpObject = null;
            //other.gameObject.layer = LayerMask.NameToLayer("Default");
            other.GetComponent<Rigidbody>().freezeRotation = false;
            AudioManager.PlayRandomSoundFromRange(3,6);
        }
    }
}
