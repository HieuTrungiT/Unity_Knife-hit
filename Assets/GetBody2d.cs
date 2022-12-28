using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBody2d : MonoBehaviour
{
    [SerializeField] GameObject AppleSkill1;
    [SerializeField] GameObject AppleSkill2;
    void Start()
    {
        AppleSkill1.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2, 2), Random.Range(1, 5));
        AppleSkill2.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2, 2), Random.Range(1, 5));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
