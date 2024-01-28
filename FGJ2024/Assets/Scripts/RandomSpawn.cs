using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPosition : MonoBehaviour
{
    [SerializeField]
    public GameObject[] gameObjectS;

    public Transform[] spawnPoints;


    void Start()
    {
        StartCoroutine(CreateItem());

    }

    IEnumerator CreateItem()
    {
        yield return new WaitForSeconds(5);
        int randItem = Random.Range(0, gameObjectS.Length);

        Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        Instantiate(gameObjectS[randItem], position, Quaternion.identity, transform);
        StartCoroutine(CreateItem());

    }

}
