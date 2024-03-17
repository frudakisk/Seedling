using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachCamera : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float offset;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private float speed;
    [SerializeField] private float startTime;
    [SerializeField] private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos, endPos);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPos, endPos, fracJourney);

        if(GameManager.gameOn)
        {
            transform.position = new Vector3(0f, (player.position.y + offset), -10f);
        }
    }
}
