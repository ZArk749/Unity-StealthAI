﻿using UnityEngine;
using UnityEngine.AI;

public class navTest : MonoBehaviour
{

    public Camera cam;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                //Move agent
                agent.SetDestination(hit.point);
            }
        }
    }
}
