using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public GameObject user;
    private float currentTime = 0f;
    

    private Stack<GameObject> stack;
    void Start()
    {
        stack = new Stack<GameObject>();
        SpawnNewEnemy(new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 10f)
        {
            currentTime = 0f;
            if (stack.Count > 4)
                Destroy(stack.Pop());

            SpawnNewEnemy(new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)));

           
        }
        currentTime += Time.deltaTime;

    }

    void SpawnNewEnemy(Vector3 coordinates)
    {
        GameObject newEnemy = Instantiate(enemy, coordinates, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().user = user;
        stack.Push(newEnemy);
    }
}
