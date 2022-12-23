using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotation : MonoBehaviour
{
    [System.Serializable]
    private class RotaitonElement
    {
        public float Speed;
        public float Duration;
    }

    [SerializeField]
    RotaitonElement[] rotaionPattern;
    [SerializeField] Transform LogGroup;

    KnifeManager knifeManager;
    LogManager logManager;
    InitKnife initKnife;
    float TimeCountDown;
    int indexRd;
    void Awake()
    {
        knifeManager = FindObjectOfType<KnifeManager>();
        logManager = FindObjectOfType<LogManager>();
        initKnife = FindObjectOfType<InitKnife>();
    }
    void Start()
    {
        indexRd = Random.Range(0, rotaionPattern.Length);
    }
    void Update()
    {
        CheckSkillKnife();

        TimeCountDown += Time.deltaTime;
        if (TimeCountDown <= rotaionPattern[indexRd].Duration)
        {
            float Speed = rotaionPattern[indexRd].Speed;
            transform.Rotate(new Vector3(0f, 0f, Speed) * Time.deltaTime);
        }
        else
        {
            indexRd = Random.Range(0, rotaionPattern.Length);
            TimeCountDown = 0;
        }
    }
    // hiệu ứng tung dao khi chuyển màn
    void CheckSkillKnife()
    {
        if (LogGroup.childCount == knifeManager.GetKnifeConfigElement(logManager.GetIndexLog()).GetCountKnife())
        {
            for (int i = 0; i < LogGroup.childCount; i++)
            {
                Rigidbody2D rb2D;
                BoxCollider2D BoxClid2D;
                GameObject gameObjectKnife = LogGroup.GetChild(i).gameObject;
                gameObjectKnife.tag = "Untagged";
                gameObjectKnife.transform.SetParent(GameObject.Find("KnifeTemp").transform);
                rb2D = gameObjectKnife.GetComponent<Rigidbody2D>();
                BoxClid2D = gameObjectKnife.GetComponent<BoxCollider2D>();
                rb2D.velocity = new Vector2(Random.Range(-5, 5f), Random.Range(1f, 10f));
                rb2D.bodyType = RigidbodyType2D.Dynamic;
                rb2D.gravityScale = 1;
                BoxClid2D.isTrigger = true;
                initKnife.DestroyKnifeTemp();
                // StartCoroutine(delayDestroyKnife());
            }
        }
    }




}
