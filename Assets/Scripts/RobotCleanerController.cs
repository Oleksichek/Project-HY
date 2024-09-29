using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RobotCleanerController : MonoBehaviour
{
    public GameObject TrashTarget;

    private List<GameObject> _trachList;
    private NavMeshAgent _agent;

    public bool _onTheWay;

    void Start()
    {
        _trachList = GameObject.FindGameObjectsWithTag("Trash").ToList();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (TrashTarget == null)
        {
            if (_trachList.Count(x => Vector3.Distance(transform.position, x.transform.position) <= 2f) > 0)
                TrashTarget = _trachList.First(x => Vector3.Distance(transform.position, x.transform.position) <= 2f);
        }
        else if (!_onTheWay)
        {
            _onTheWay = true;
            StartCoroutine(GoToTarget());
        }
    }

    private IEnumerator GoToTarget()
    {
        _agent.destination = TrashTarget.transform.position;

        yield return new WaitUntil(() => Vector3.Distance(transform.position, TrashTarget.transform.position) <= 0.5f || TrashTarget == null);

        _agent.destination = transform.position;
        _trachList.Remove(TrashTarget);
        Destroy(TrashTarget);
        TrashTarget = null;
        _onTheWay = false;
    }
}
