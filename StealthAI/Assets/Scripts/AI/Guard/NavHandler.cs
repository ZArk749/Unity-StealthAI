using UnityEngine;
using UnityEngine.AI;

/* small class made to make navmeshcomponents more usable.
 * it includes a debug method that activates click-to-move, plus some human-readable methods
 * that rotate, move and stop the navMeshAgent. In addition, some calc methods are present
 */

public class NavHandler : MonoBehaviour
{
   

    public Camera cam;

    [SerializeField]
    [Tooltip("if true navmesh move on point works")]
    private bool debug = true;

    public NavMeshAgent agent;
    private float rotationSpeed = 10f;
    

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.stoppingDistance = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
            ClickNav();
    }


    public void MoveTowards(Vector3 target) {
        if (agent.destination != target) {//may want to replace this with HasReachedDestination()
            agent.SetDestination(target);
            agent.isStopped = false;
        }

    }

    public void RotateTowards(Vector3 target) {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    //Returns path distance in floating point
    public float CalculatePathLength(Vector3 target) {
        NavMeshPath path = new NavMeshPath();

        if (agent.enabled)
            agent.CalculatePath(target, path);

        Vector3[] waypoints = new Vector3[path.corners.Length + 2];
        waypoints[waypoints.Length - 1] = target;
        waypoints[0] = transform.position;

        for (int i = 0; i < path.corners.Length; i++) {
            waypoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < waypoints.Length - 1; i++) {
            pathLength += Vector3.Distance(waypoints[i], waypoints[i + 1]);
        }

        return pathLength;
    }

    public bool HasReachedDestination() {
        return (agent.remainingDistance <= agent.stoppingDistance);
    }

    public void Stop() {
        agent.isStopped = true;
    }
    public void Go() {
        agent.isStopped = false;
    }

    public void SetAgentSpeed(float speed) {
        agent.speed = speed;
    }

    public void DeleteDestination() {
        agent.destination = gameObject.transform.position;
    }

    //DEBUG
    private void ClickNav() {

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
