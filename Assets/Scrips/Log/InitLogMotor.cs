using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLogMotor : MonoBehaviour
{
    [SerializeField] GameObject SkillLog;
    LogRotation logRotation;
    LogManager logManager;
    GameObject gameObjectLog;
    CanvasKnifes canvasKnifes;
    int indexLog;
    void Awake()
    {
        logManager = FindObjectOfType<LogManager>();
        indexLog = logManager.GetIndexLog();
        logRotation = FindObjectOfType<LogRotation>();
        canvasKnifes = FindObjectOfType<CanvasKnifes>();
    }
    void Start()
    {
        InitLog(indexLog);
    }

    void Update()
    {

    }
    public void InitLog(int index)
    {
        if (gameObjectLog != null)
        {
            Destroy(gameObjectLog);
            GameObject gameObjSkillLog = Instantiate(SkillLog, gameObjectLog.transform.position, Quaternion.identity);
            StartCoroutine(destroySkillLog(gameObjSkillLog));
        }
        StartCoroutine(InstanLog(index));
    }
    IEnumerator InstanLog(int index)
    {
        yield return new WaitForSeconds(0.5f);
        if (index < logManager.GetCountElementLog())
        {
            gameObjectLog = Instantiate(logManager.GetElementLog(index), new Vector3(0f, 2.5f, 0f), Quaternion.identity);
            gameObjectLog.transform.SetParent(GameObject.Find("LogMotor").transform);
            canvasKnifes.RenderKnifePanle();
        }
        else
        {
            logManager.SetDeffaulIndexLog();
            gameObjectLog = Instantiate(logManager.GetElementLog(0), new Vector3(0f, 2.5f, 0f), Quaternion.identity);
            gameObjectLog.transform.SetParent(GameObject.Find("LogMotor").transform);
            canvasKnifes.RenderKnifePanle();
        }
    }
    IEnumerator destroySkillLog(GameObject gameObjectSkillLog)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObjectSkillLog);
    }
}
