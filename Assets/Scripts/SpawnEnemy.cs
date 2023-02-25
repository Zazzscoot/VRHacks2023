using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public GameObject user;
    private float currentTime = 0f;
    public static SpawnEnemy instance;
    public List<GameObject> list;

    public float meshResolution;
    private float viewAngle = 365f;
    private float viewRadius = 50f;
    public LayerMask obstacleMask;

    public MeshFilter meshFilter;
    public Mesh mesh;
    private bool readyToSpawn;
    public float spawnTime;

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dist, float _angle)
        {
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle; 
        }
    }

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else 
            instance = this;
    }

    void Start()
    {
        //To view FOV
        /*mesh = new Mesh();
        mesh.name = "View mesh";
        meshFilter.mesh = mesh;*/

        list = new List<GameObject>();
        
        SpawnNewEnemy(new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)));
        
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        DrawFieldOfView();
        if (currentTime > spawnTime)
        {
            readyToSpawn = true;
            currentTime = 0f;
        }
        currentTime += Time.deltaTime;
    }

    void SpawnNewEnemy(Vector3 coordinates)
    {
        GameObject newEnemy = Instantiate(enemy, coordinates, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().user = user;
        list.Add(newEnemy);
        Debug.Log(list.Count);
        readyToSpawn = false;
    }

    Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * Mathf.PI / 180f;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for(int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            //ViewCastInfo newViewCast = ViewCast(angle);
            //viewPoints.Add(newViewCast.point);
            if (readyToSpawn)
                GetCoordinatesForSpawn(angle);
            
        }


        //Used to view FOV
        /*int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2)*3];

        vertices[0] = Vector3.zero;

        for(int i = 0; i < vertexCount-1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();*/
    }

    Vector3 DirFromAngle(float angle, bool value)
    {
        if (!value)
            angle += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    //For Viewing FOV
    /*ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        else
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);

    }*/

    void GetCoordinatesForSpawn(float angle)
    {
        Vector3 dir = DirFromAngle(angle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            Vector3 position = hit.collider.gameObject.transform.position + dir * 3f;
            SpawnNewEnemy(position);
        }
    }
}
