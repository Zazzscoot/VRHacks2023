using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator animator;
    private float currentTime, walkTime, runTime, deathTime;
    private int check = 0;
    public int hp = 100;

    public GameObject user;
    public float walkSpeed = 0.6f;
    public float runSpeed = 1.2f;
    public float viewRadius = 50f;
    public LayerMask userMask;

    private float viewAngle = 20f;
    public float meshResolution = 0.5f;

    public List<AudioClip> walkClips;
    public List<AudioClip> runClips;
    SpawnEnemy spawnEnemyInstance;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        currentTime = runTime = walkTime = 0;
        spawnEnemyInstance = SpawnEnemy.instance;
    }

    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.Rotate(Vector3.zero);

        //Testing walk/run/idle animation
        if (currentTime > 3)
        {
            check = Random.Range(0, 3);
            currentTime = 0;
        }
        currentTime += Time.deltaTime;

        if (hp <= 0)
            check = 3;

        if(DrawFieldOfView())
            return;

        if (Vector3.Distance(user.transform.position, transform.position) < 1)
            GetComponent<NavMeshAgent>().speed = 0;

        switch (check)
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
            case 3:
                Death();
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
        animator.SetFloat("Speed", 0.25f, 0.1f, Time.deltaTime);

        if (walkTime > 0.6f)
        {
            GetComponent<AudioSource>().clip = walkClips[Random.Range(0, walkClips.Count)];
            GetComponent<AudioSource>().Play();
            walkTime = 0;
        }
        walkTime += Time.deltaTime;
    }

    void Run()
    {
        transform.GetComponent<NavMeshAgent>().speed = runSpeed;
        animator.SetFloat("Speed", 0.50f, 0.1f, Time.deltaTime);

        if (runTime > 0.3f)
        {
            GetComponent<AudioSource>().clip = runClips[Random.Range(0, runClips.Count)];
            GetComponent<AudioSource>().Play();
            runTime = 0;
        }
        runTime += Time.deltaTime;
    }

    void Gunfire()
    {
        transform.GetComponent<NavMeshAgent>().speed = 0;
        animator.SetFloat("Speed", 0.75f, 0.1f, Time.deltaTime);
    }

    public void Death()
    {
        transform.GetComponent<NavMeshAgent>().speed = 0;
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
        deathTime += Time.deltaTime;
        if (deathTime > 1.5f)
        {
            spawnEnemyInstance.list.Remove(gameObject);
            Destroy(gameObject);
        }

    }

    public void SetHealth(int hp)
    {
        this.hp = hp;
    }

    public int GetHealth() { return hp; }

    bool DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            //ViewCastInfo newViewCast = ViewCast(angle);
            //viewPoints.Add(newViewCast.point);
            if(DetectPlayer(angle))
                return true;

        }
        return false;
    }

    Vector3 DirFromAngle(float angle, bool value)
    {
        if (!value)
            angle += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }


    bool DetectPlayer(float angle)
    {
        Vector3 dir = DirFromAngle(angle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, userMask)) { 
            Gunfire();
            return true;
        }

        return false;

    }
}
