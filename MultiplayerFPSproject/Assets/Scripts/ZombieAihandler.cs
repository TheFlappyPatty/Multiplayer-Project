using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAihandler : MonoBehaviour
{
    private NavMeshAgent Ai;
    private bool ThroughBlockade;
    // Start is called before the first frame update
    void Start()
    {
        Ai = gameObject.GetComponent<NavMeshAgent>();
        ThroughBlockade = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ThroughBlockade)
        {
        }
        Ai.destination = GameObject.FindWithTag("Player").transform.position;
    }
}
