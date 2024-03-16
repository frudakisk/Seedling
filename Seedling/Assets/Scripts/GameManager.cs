using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> rockObjects;
    public int numRocks;
    public float xRange;
    public float yHeight;

    public static bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        SpawnRocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRocks()
    {
        for (int i = 0; i < numRocks; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-xRange, xRange), Random.Range(0, yHeight), 0);
            int randomRock = Random.Range(0, rockObjects.Count);
            GameObject rock = Instantiate(rockObjects[randomRock], randPos, Quaternion.identity);
            float randomRotation = Random.Range(0, 360f);
            rock.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
            float randomeScale = Random.Range(0.5f, 3f);
            rock.transform.localScale = new Vector3(randomeScale, randomeScale, randomeScale);
        }
    }
}
