using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public GameObject Target;
    private NavMeshAgent agent;
    private Animator m_Animator;



    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        m_Animator = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        agent.destination = Target.transform.position;
        m_Animator.speed = agent.velocity.magnitude;
        if (agent.speed > 2f)
        {
            m_Animator.SetBool("isRunning", true);
            m_Animator.SetBool("isWalking", false);

        }
        else if (agent.speed <= 2f)
        {
            m_Animator.SetBool("isRunning", false);
            m_Animator.SetBool("isWalking", true);

        }
        else
        {
            m_Animator.SetBool("isRunning", false);
            m_Animator.SetBool("isWalking", false);
        }

    }
    private void OnTriggerStay(Collider other)
    {
       
    }
}