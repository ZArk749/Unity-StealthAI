using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("idk")]
    private Transform waypoints = null;

    NavHandler nav;

    Vector3[] guardPoints;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavHandler>();
        
        /* guardPoints structure:
         * 1 - Recharge position
         * 2 - Task Position
         * 3+ - other guardpoints
         */

        guardPoints = new Vector3[waypoints.childCount];
        for (int i = 0; i < guardPoints.Length; i++) {
            guardPoints[i] = waypoints.GetChild(i).position;
            guardPoints[i] = new Vector3(guardPoints[i].x, transform.position.y, guardPoints[i].z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        nav.Go();
        nav.MoveTowards(guardPoints[1]);
    }
}
