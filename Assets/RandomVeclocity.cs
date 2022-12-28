using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVeclocity : MonoBehaviour
{

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2,2),Random.Range(1,5));
    }


    void Update()
    {

    }
}
