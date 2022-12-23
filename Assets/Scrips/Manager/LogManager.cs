using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    [SerializeField] int IndexLog = 2;
    [SerializeField] List<GameObject> LogElement;

    public GameObject GetElementLog(int index)
    {
        return LogElement[index];
    }
    public int GetCountElementLog()
    {
        return LogElement.Count;
    }

    public int GetIndexLog()
    {
        return IndexLog;
    }
    public void SetIndexLog()
    {
        if (IndexLog < LogElement.Count)
        {

            IndexLog += 1;
        }
        else
        {
            IndexLog = 0;
        }
    }
    public void SetDeffaulIndexLog()
    {
        IndexLog = 0;
    }

}
