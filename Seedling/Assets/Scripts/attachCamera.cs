using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachCamera : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, (player.position.y + offset), -10f);
    }
}
