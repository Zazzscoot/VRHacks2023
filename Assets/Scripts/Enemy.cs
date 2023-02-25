using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator animator;
    private float currentTime;
    private int check = 0;

    public GameObject user;
    public float walkSpeed = 1.2f;
    public float runSpeed = 2.4f;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Tracks and rotates to user's position
        RotateToUser();


        //Testing walk/run/idle animation
        if (currentTime > 3)
        {
            check = Random.Range(0, 3);
            currentTime = 0;
        }

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

        currentTime += Time.deltaTime;
    }

    void Idle()
    {
        animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }

    void Walk()
    {
        transform.position += transform.forward * walkSpeed * Time.deltaTime;
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    void Run()
    {
        transform.position += transform.forward * runSpeed * Time.deltaTime;
        animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }

    void RotateToUser()
    {
        Vector3 userVector = user.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, userVector);
        transform.Rotate(0, angle, 0);
    }
}
