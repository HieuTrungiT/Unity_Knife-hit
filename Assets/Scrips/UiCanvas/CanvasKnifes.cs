using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CanvasKnifes : MonoBehaviour
{
    [SerializeField] GameObject PanelKnife;
    [SerializeField] GameObject PanelEndGame;
    [SerializeField] TextMeshProUGUI ScoreTMP;
    [SerializeField] TextMeshProUGUI ScoreBgrkTMP;
    [SerializeField] GameObject ImgCanvasRotation;
    KnifeManager knifeManager;
    LogManager logManager;
    StateManager stateManager;
    // int lenghtKnife = 0;
    void Awake()
    {
        stateManager = FindObjectOfType<StateManager>();
        knifeManager = FindObjectOfType<KnifeManager>();
        logManager = FindObjectOfType<LogManager>();
        // RenderKnifePanle();
    }
    void Update()
    {
        ImgCanvasRotation.transform.Rotate(new Vector3(0f, 0f, 50f) * Time.deltaTime);
        if (stateManager.GetLife() <= 0)
        {
            StartCoroutine(delayShowPanel());
        }
        ScoreBgrkTMP.text = stateManager.GetScore().ToString("0");
    }
    IEnumerator delayShowPanel()
    {
        yield return new WaitForSeconds(1);
        PanelEndGame.SetActive(true);
        ScoreTMP.text = stateManager.GetScore().ToString("0");
    }
    public void RenderKnifePanle()
    {

        int lenghtKnife = knifeManager.GetKnifeConfigElement(logManager.GetIndexLog()).GetCountKnife();
        Debug.Log(lenghtKnife);
        for (int i = 0; i < lenghtKnife; i++)
        {
            var myObject = Instantiate(PanelKnife, transform.position, Quaternion.Euler(0f, 0f, -130f));
            myObject.transform.SetParent(GameObject.Find("PanelKnifes").transform, true);
            myObject.transform.localScale = new Vector2(0.5f, 1.7f);
        }
    }
    public void SetDestroyKnifePanle()
    {
        if (transform.GetChild(0) != null)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

}
