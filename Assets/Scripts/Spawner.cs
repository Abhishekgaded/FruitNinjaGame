using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{    
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f; 
    public float maxForce = 22f;

    //Life Span of the fruit after the spawning
    public float maxLifetime = 5f;

    public GameObject[] fruitPrefabs;
    private Collider SpawnArea;

    public GameObject BombPrefab;

    [Range(0f,1f)]
    public float bombChance = 0.05f;


private void Update() {
    Debug.Log("Changes made to the code editor");
}

    private void Awake()
    {
        SpawnArea = GetComponent<Collider>();

    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn ()
    {
        yield return new WaitForSeconds(2f);
        while (enabled)
        {
            //Spawning the random fruit from the fruit's prefab
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            if(Random.value < bombChance)
            {
               prefab =  BombPrefab;
            }

            

            Vector3 position = new Vector3();
            //Setting the boundary for each and every axis;
            position.x = Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.x);
            position.y = Random.Range(SpawnArea.bounds.min.y, SpawnArea.bounds.max.y);

            position.z = Random.Range(SpawnArea.bounds.min.z, SpawnArea.bounds.max.z);
            // position.z = Random.Range(SpawnArea.bounds.min.z, SpawnArea.bounds.max.z);

            

            //Using Euler angle and Quaternion rotation rotating the z axis;
            Quaternion rotation = Quaternion.Euler(0f,0f,Random.Range(minAngle,maxAngle));

            GameObject fruit = Instantiate(prefab,position,rotation);
            Destroy(fruit,maxLifetime);

            float force = Random.Range(minForce,maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force,ForceMode.Impulse);


            yield return new WaitForSeconds(Random.Range(minSpawnDelay,maxSpawnDelay));
        }

    }
}
