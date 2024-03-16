using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHoleBehaviour : MonoBehaviour
{
    public GameObject waterPocketFull;
    public GameObject waterPocketEmpty;
    public bool hasWater;
    private bool switched;

    // Start is called before the first frame update
    void Start()
    {
        hasWater = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasWater && !switched)
        {
            switched = true;
            waterPocketFull.SetActive(!waterPocketFull.activeSelf);
            waterPocketEmpty.SetActive(!waterPocketEmpty.activeSelf);
        }
    }
}
