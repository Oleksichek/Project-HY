using UnityEngine;
using UnityEngine.AI;

public class ClickMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent RobotAgent;
    [SerializeField] private RobotCleanerController RobotController;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && RobotController.TrashTarget == null)
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                RobotAgent.destination = hit.point;
            }
        }
    }
}
