using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneControlScript : MonoBehaviour
{
    public float MoveSpeed;
    public int CraneNumber;
    public Transform CraneClaw;
    public Vector3 maxDistance;
    public Vector3 minDistance;
    public float clawUpDistance;
    public float clawDownDistance;
    public float dropSpeed;
    public float maxCountdownTillReturn;
    public float dropTimer;

    private Vector3 homePosition; 
    private float currentDistance;
    private bool grab;
    private bool canGrab;
    private bool returning;
    private LineRenderer lineRenderer;
    private Rigidbody CraneRigid;
    private float countdownTillReturn;
    private GameObject playerTracker;
    private float currentDropTimer;
    private AudioManager AudioManager;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        CraneRigid = GetComponent<Rigidbody>();
        currentDistance = clawUpDistance;
        canGrab = true;
        homePosition = transform.position;
        playerTracker = transform.parent.transform.Find("PlayerTracker").gameObject;
        AudioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }


    void Update()
    {
        lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        lineRenderer.SetPosition(0, new Vector3(CraneClaw.position.x, CraneClaw.position.y, CraneClaw.position.z));
        if(!returning)
        {
            if (transform.position.x > maxDistance.x)
            {
                transform.position = new Vector3(maxDistance.x, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < minDistance.x)
            {
                transform.position = new Vector3(minDistance.x, transform.position.y, transform.position.z);
            }
            if (transform.position.z > maxDistance.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxDistance.z);
            }
            else if (transform.position.z < minDistance.z)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minDistance.z);
            }
        }

        if(grab)
        {
            currentDistance = Mathf.Clamp(currentDistance + Time.deltaTime * dropSpeed, clawUpDistance, clawDownDistance);
            CraneRigid.velocity = Vector3.zero;
            currentDropTimer = dropTimer;
        }
        else
        {
            currentDistance = Mathf.Clamp(currentDistance - Time.deltaTime * dropSpeed, clawUpDistance, clawDownDistance);
        }
        if(currentDistance >= clawDownDistance && returning == false)
        {
            returning = true;
            countdownTillReturn = maxCountdownTillReturn;
        }
        if(countdownTillReturn > 0 && returning)
        {
            countdownTillReturn -= Time.deltaTime;
        }
        if(countdownTillReturn <= 0 && returning)
        {
            grab = false;
        }
        if(grab == false && returning)
        {
            if((homePosition - transform.position).magnitude < 0.1 && currentDistance <= clawUpDistance && currentDropTimer <= 0)
            {
                returning = false;
                canGrab = true;
            }
            else if((homePosition - transform.position).magnitude < 0.1 && currentDistance <= clawUpDistance)
            {
                currentDropTimer -= Time.deltaTime;
                CraneRigid.velocity = Vector3.zero;
            }
            else if(currentDistance != clawUpDistance)
            {
                CraneRigid.velocity = Vector3.zero;
            }
            else
            {
                Vector3 velocity = (homePosition - transform.position);
                if (velocity.magnitude > 1)
                {
                    velocity.Normalize();
                }
                CraneRigid.velocity = velocity * MoveSpeed;
                //Vector3.MoveTowards(transform.position, homePosition, Time.deltaTime * MoveSpeed);
            }
        }
        PlayerTracker();
    }

    public void MoveHorizontal(float Input)
    {
        if(!returning)
        {
            CraneRigid.velocity = new Vector3(MoveSpeed * Input, CraneRigid.velocity.y, CraneRigid.velocity.z);
        }

    }

    public void MoveVertical(float Input)
    {
        if (!returning)
        {
            CraneRigid.velocity = new Vector3(CraneRigid.velocity.x, CraneRigid.velocity.y, MoveSpeed * -Input);
        }
    }

    public void Grab()
    {
        if(canGrab)
        {
            grab = true;
            AudioManager.PlaySound("Charge");
            canGrab = false;
        }
    }

    public float GetCurrentAnchorDistance()
    {
        return currentDistance;
    }

    public void SetGrab(bool value)
    {
        grab = value;
    }

    public bool GetGrab()
    {
        return grab;
    }
    
    public bool GetPickUp()
    {
        if((currentDistance == clawDownDistance || returning) && currentDropTimer >=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PlayerTracker()
    {

        foreach (RaycastHit floorCast in Physics.RaycastAll(transform.position, -transform.up, 1000))
        {
            if (floorCast.collider.tag == "Floor")
            {
                playerTracker.GetComponent<SpriteRenderer>().enabled = true;
                playerTracker.transform.rotation = Quaternion.Euler(0, 1, 0);
                playerTracker.transform.position = floorCast.point;
                playerTracker.transform.rotation = Quaternion.FromToRotation(playerTracker.transform.forward, floorCast.normal);
                playerTracker.transform.position += playerTracker.transform.forward * 0.02f;
                break;
            }
            else
            {
                playerTracker.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

    }

    public void Return()
    {
        returning = true;
    }

    public Vector3 GetHome()
    {
        return homePosition;
    }
}
