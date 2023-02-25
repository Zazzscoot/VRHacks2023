using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator animator;
    private float currentTime;
    private int check = 0;

    public GameObject user;
    public float walkSpeed = 0.6f;
    public float runSpeed = 1.2f;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //Testing walk/run/idle animation
        if (currentTime > 3)
        {
            check = Random.Range(0, 3);
            currentTime = 0;
        }
        currentTime += Time.deltaTime;


        if (Vector3.Distance(user.transform.position, transform.position) < 1)
            return;

        switch(check)
        {
            case 0:
                Idle();
                break;
            case 1:
                Walk();
                break;
            case 2:
                Run();
                break;
            default:
                Idle();
                break;
        }

        transform.GetComponent<NavMeshAgent>().SetDestination(user.transform.position);
    }

    void Idle()
    {
        transform.GetComponent<NavMeshAgent>().speed = 0;
        animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
        
    }

    void Walk()
    {
        transform.GetComponent<NavMeshAgent>().speed = walkSpeed;
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    void Run()
    {
        transform.GetComponent<NavMeshAgent>().speed = runSpeed;
        animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }
}
