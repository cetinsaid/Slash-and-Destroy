using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class TrailBlade : MonoBehaviour
{
    private TrailRenderer trailBlade;
    BoxCollider collider;
    [SerializeField]private float minDistance;
    private bool isContinue;
    private GameManager gameManager;

    private Vector3 prevPoint;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        trailBlade = GetComponent<TrailRenderer>();
        collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!gameManager.gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                collider.enabled = true;
                trailBlade.Clear();
                trailBlade.enabled = true;
                StartBlade();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                collider.enabled = false;
                trailBlade.enabled = false;
                EndBlade();
            }
            else if (isContinue)
            {
                Continue();
            }
        }

        
    }

    void StartBlade()
    {
        
        isContinue = true;
        Vector3 curr = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curr.z = 0;
        prevPoint = curr;
        


    }

    void Continue()
    {
        Vector3 curr = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        curr.z = 0;
        if (Vector3.Distance(curr, prevPoint) > minDistance)
        {
            transform.position = curr;
        }
    }

    void EndBlade()
    {
        
        isContinue = false;
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Target>() && !gameManager.gameOver)
        {
            other.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
    
}

