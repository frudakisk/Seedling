using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> rockObjects;
    public GameObject waterPocket;
    public int numRocks;
    public float xRange;
    public float yHeight;

    private WaterHoleBehaviour[] waterHoles;
    public int numWaterHoles;

    public static bool gameOver;
    public static bool gameOn;
    private bool alreadyOn;

    // Start is called before the first frame update
    void Start()
    {
        //spawn water pockets first
        SpawnWaterHoles();
        waterHoles = FindAllActiveWaterPockets();
        SpawnRocks();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && !alreadyOn)
        {
            alreadyOn = true;
            gameOn = true;
        }
    }

    private void SpawnWaterHoles()
    {
        for(int i = 0; i < numWaterHoles; i++)
        {
            Vector3 randPos = new Vector3(Random.Range(-xRange, xRange), Random.Range(0, yHeight), 0);
            Instantiate(waterPocket, randPos, Quaternion.identity);
        }
    }

    private void SpawnRocks()
    {
        for (int i = 0; i < numRocks; i++)
        {
            //rock position is dependent on the water pocket positions
            Vector3 randPos;
            do
            {
                randPos = new Vector3(Random.Range(-xRange, xRange), Random.Range(0, yHeight), 0);
            } while (isNearWater(randPos));
            
            int randomRock = Random.Range(0, rockObjects.Count);
            GameObject rock = Instantiate(rockObjects[randomRock], randPos, Quaternion.identity);
            float randomRotation = Random.Range(0, 360f);
            rock.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
            float randomeScale = Random.Range(0.5f, 3f);
            rock.transform.localScale = new Vector3(randomeScale, randomeScale, randomeScale);
        }
    }

    private bool isNearWater(Vector3 pos)
    {
        //need to find all water pockets that are active in the scene
        Debug.Log($"Num of water holes: {waterHoles.Length}");
        foreach(WaterHoleBehaviour obj in waterHoles)
        {
            Debug.Log(Vector3.Distance(obj.transform.position, pos));
            if(Vector3.Distance(obj.transform.position, pos) < 3.0f)
            {
                //Too close to a waterhole
                Debug.Log("Too Close. Create a new distance");
                return true;
            }
        }
        Debug.Log("Far enough from all water holes");
        return false;

    }

    private WaterHoleBehaviour[] FindAllActiveWaterPockets()
    {
        return FindObjectsOfType<WaterHoleBehaviour>();
    }
}
