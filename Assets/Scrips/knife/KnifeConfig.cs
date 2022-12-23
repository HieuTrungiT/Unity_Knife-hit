using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Knife Config", fileName = "New Knife Config")]
public class KnifeConfig : ScriptableObject
{
    [SerializeField] List<GameObject> KnifePrefab;
    public int GetCountKnife()
    {
        return KnifePrefab.Count;
    }

    public GameObject GetKnifeElement(int index)
    {
        return KnifePrefab[index];
    }

}
