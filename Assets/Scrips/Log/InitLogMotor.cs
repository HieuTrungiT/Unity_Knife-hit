using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLogMotor : MonoBehaviour
{
    [SerializeField] GameObject SkillLog;
    LogRotation logRotation;
    LogManager logManager;
    GameObject gameObjectLog;
    InitKnife initKnife;
    KnifeManager knifeManager;
    CanvasKnifes canvasKnifes;
    int indexLog;
    void Awake()
    {
        knifeManager = FindObjectOfType<KnifeManager>();
        initKnife = FindObjectOfType<InitKnife>();
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
            KnifeConfig knifeConfig;
            knifeConfig = knifeManager.GetKnifeConfigElement(logManager.GetIndexLog());

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
        gameObjectLog.GetComponent<LogRotation>().CheckSkillKnife();
        Destroy(gameObjectLog);
        // yield return new WaitForSeconds(0.5f);


        yield return new WaitForSeconds(2f);
        Destroy(gameObjectSkillLog);
    }
}
