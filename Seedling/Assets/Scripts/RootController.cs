using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float spawnDistance;
    private float horizontalInput;
    private Vector3 startPos;

    public GameObject staticRoot;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * horizontalInput * rotationSpeed * Time.deltaTime);

        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if(Vector3.Distance(startPos, transform.position) >= spawnDistance)
        {
            startPos = transform.position;
            Debug.Log("Spawning new root at old position");
            Instantiate(staticRoot, transform.position, transform.rotation);
        }
    }
}
