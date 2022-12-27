using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InitKnife : MonoBehaviour
{
    [SerializeField] Transform KnifeTemp;
    [SerializeField] Slider mSlider;
    [Range(1, 300)]
    [SerializeField] int frameRate;
    [SerializeField] GameObject Knife;
    KnifeManager knifeManager;
    LogManager logManager;
    StateManager stateManager;
    GameObject gameObjectKnife;

    InitLogMotor initLogMotor;
    int indexKnife = 0;
    bool checkKnife = true;
    int indexKnifeConfigElement = 0;
    bool clicking = false;
    bool stateKnife = true;
    float gravityScale = -1;
    float minGravity;
    float maxGravity;
    bool checkReloadGravity = false;
    void Awake()
    {
        knifeManager = FindObjectOfType<KnifeManager>();
        logManager = FindObjectOfType<LogManager>();
        stateManager = FindObjectOfType<StateManager>();
        initLogMotor = FindObjectOfType<InitLogMotor>();

    }
    void Start()
    {
        InitKnifes(indexKnifeConfigElement);

        minGravity = knifeManager.GetMinGravityKnife();
        maxGravity = knifeManager.GetMaxGravityKnife();
        mSlider.minValue = Mathf.Abs(minGravity);
        mSlider.maxValue = Mathf.Abs(maxGravity);
        gravityScale = minGravity;
    }


    void Update()
    {
        Application.targetFrameRate = frameRate;
        indexKnifeConfigElement = logManager.GetIndexLog();

        if (stateManager.GetLife() > 0)
        {

            OnTapOrClick();
            if (clicking && stateKnife)
            {
                if (!checkReloadGravity)
                {
                    if (gravityScale >= maxGravity)
                    {
                        gravityScale += Time.deltaTime * -50;
                    }
                    else
                    {
                        gravityScale = maxGravity;
                        checkReloadGravity = true;
                    }
                }
                else
                {
                    if (gravityScale <= minGravity)
                    {
                        gravityScale += Time.deltaTime * 50;
                    }
                    else
                    {
                        gravityScale = minGravity;
                        checkReloadGravity = false;
                    }
                }
                mSlider.value = Mathf.Abs(gravityScale);
                if (gameObjectKnife != null)
                {
                    gameObjectKnife.GetComponent<knife>().SetPowerEffect(gravityScale / 2);
                    if (stateKnife)
                    {
                        gameObjectKnife.transform.position = new Vector2(0f, -3.53398f + gravityScale / 100);
                    }
                }

            }

            if (indexKnife == 0 && checkKnife)
            {
                checkKnife = false;
                StartCoroutine(delayInstanKnife());
            }
        }
    }
    IEnumerator delayInstanKnife()
    {
        yield return new WaitForSeconds(1f);
        // indexKnife = 1;
        InitKnifes(logManager.GetIndexLog());
    }
    public int GetIndexKnife()
    {
        return indexKnife;
    }
    void OnTapOrClick()
    {


        if (Input.GetMouseButtonDown(0))
        {
            gravityScale = minGravity;
            clicking = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {

            float numberGrvt = gravityScale / 1000;

            if (gameObjectKnife != null)
            {
                gameObjectKnife.GetComponent<BoxCollider2D>().offset = new Vector2(0f, (numberGrvt * 3));
                gameObjectKnife.GetComponent<knife>().PlaySFXShoot();
                gameObjectKnife.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
                gravityScale = minGravity;
                clicking = false;
                stateKnife = false;
            }

        }

    }
    public void InitKnifes(int index)
    {

        KnifeConfig knifeConfig;
        knifeConfig = knifeManager.GetKnifeConfigElement(index);

        if (indexKnife < knifeConfig.GetCountKnife())
        {
            Debug.Log("sinh sản");
            GameObject knife = knifeConfig.GetKnifeElement(indexKnife);
            gameObjectKnife = Instantiate(knife, new Vector3(0f, -3.5f, 0f), Quaternion.identity);
            gameObjectKnife.GetComponent<knife>().StartEffect();
            gameObjectKnife.transform.SetParent(GameObject.Find("Knifes").transform);
            indexKnife += 1;

        }
        else if (stateManager.GetLife() > 0 && indexKnife == knifeConfig.GetCountKnife())
        {
            Debug.Log("sinh sản vòng mới");
            StartCoroutine(delayInitLog());
        }



    }

    public void DestroyKnifeTemp()
    {
        StartCoroutine(delayDestroyKnife());
    }

    IEnumerator delayDestroyKnife()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < KnifeTemp.childCount; i++)
        {
            Destroy(KnifeTemp.GetChild(i).gameObject);
        }
    }
    IEnumerator delayInitLog()
    {




        yield return new WaitForSeconds(0.6f);
        checkKnife = true;
        indexKnife = 0;
    }

    public void StateKnife()
    {
        stateKnife = true;
    }

}
