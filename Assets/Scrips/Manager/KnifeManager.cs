using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour
{

    [SerializeField] List<KnifeConfig> knifeCofig;
    [SerializeField] float minGravityKnife;
    [SerializeField] float maxGravityKnife;

    public KnifeConfig GetKnifeConfigElement(int index)
    {
        if (index < knifeCofig.Count)
        {
            return knifeCofig[index];

        }
        else
        {
            return knifeCofig[0];
        }
    }
    public int GetCountKnifeConfig()
    {
        return knifeCofig.Count;
    }
    public float GetMinGravityKnife()
    {
        return minGravityKnife;
    }
    public float GetMaxGravityKnife()
    {
        return maxGravityKnife;
    }
}
