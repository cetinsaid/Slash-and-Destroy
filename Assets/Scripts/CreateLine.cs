using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class CreateLine : MonoBehaviour
{
    
    private Camera mainCamera;
    private Vector3 startingPoint;
    private bool isContinue;

    private Vector3 endingPoint;
    private Vector3 currentPoint;
    
    private LineRenderer line;
    [SerializeField] private float minDistBetweenPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrawing();
        }
        else if (isContinue)
        {
            ContinueDrawing();
        }

    }

    void StartDrawing()
    {
        
        isContinue = true;
        startingPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        startingPoint.z = 0;
        line.positionCount++;
        line.SetPosition(0 , startingPoint);
        currentPoint = startingPoint;
    }

    void ContinueDrawing()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (Vector3.Distance(mousePosition, currentPoint ) > minDistBetweenPoints)
        {
            line.positionCount++;
            line.SetPosition(line.positionCount - 1 , mousePosition);
            currentPoint = mousePosition;
        }

    }

    void EndDrawing()
    {
        line.positionCount = 0;
        isContinue = false;
    }
}
