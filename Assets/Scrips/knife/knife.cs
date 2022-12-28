using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    [SerializeField] GameObject SkillApple;
    [SerializeField] ParticleSystem effectKnife;
    [SerializeField] ParticleSystem effectCollisionLog;
    [SerializeField] ParticleSystem effectCollisionApple;
    [SerializeField] ParticleSystem effectCollisionKnife;
    [SerializeField] ParticleSystem effectTriggerLog;
    [SerializeField] int point = 10;
    [SerializeField] AudioClip SFXShooting;
    [SerializeField] AudioClip SFXPowerUp;
    [SerializeField] AudioClip SFXCollisionKnife;
    [SerializeField] AudioClip SFXCollisionLog;
    [SerializeField] AudioClip SFXCollisionLogMax;
    private ParticleSystem.MainModule pMain;
    AudioSource audioSFX;
    InitKnife initKnife;
    LogManager logManager;
    StateManager stateManager;
    CanvasKnifes canvasKnifes;
    Rigidbody2D Rb2D;
    bool isOneCollision = false;

    void Awake()
    {
        audioSFX = GetComponent<AudioSource>();
        Rb2D = GetComponent<Rigidbody2D>();
        stateManager = FindObjectOfType<StateManager>();
        initKnife = FindObjectOfType<InitKnife>();
        logManager = FindObjectOfType<LogManager>();
        canvasKnifes = FindObjectOfType<CanvasKnifes>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {


        if (!isOneCollision && stateManager.GetLife() > 0)
        {
            if (other.gameObject.tag == "Log")
            {
                effectCollisionLog.Play();
                effectKnife.Stop();
                Rb2D.velocity = new Vector2(0f, 0f);
                Rb2D.bodyType = RigidbodyType2D.Kinematic;
                this.gameObject.transform.SetParent(other.gameObject.transform);
                this.gameObject.GetComponent<Collider2D>().offset = new Vector2(this.gameObject.GetComponent<Collider2D>().offset.x, -0.05f);
                canvasKnifes.SetDestroyKnifePanle();
                initKnife.SetStateKnife(true);
                audioSFX.clip = SFXCollisionLog;
                audioSFX.Play();
                if (initKnife.GetIndexKnife() > 0)
                {
                    initKnife.InitKnifes(logManager.GetIndexLog());
                }
            }
            else if (other.gameObject.tag == "Knife")
            {
                Rb2D.velocity = new Vector2(15f, 20f);
                Rb2D.bodyType = RigidbodyType2D.Dynamic;
                Rb2D.gravityScale = 1f;
                stateManager.SetLoseLife();
                effectKnife.Stop();
                effectCollisionKnife.Play();
                audioSFX.clip = SFXCollisionKnife;
                audioSFX.Play();
            }
            isOneCollision = true;
        }
    }
    public void StartEffect()
    {
        effectKnife.Play();
    }


    public void SetPowerEffect(float power)
    {
        pMain = effectKnife.main;
        pMain.startSize = Mathf.Abs(power);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        // Trigger Apple
        if (!isOneCollision && stateManager.GetLife() > 0)
        {
            if (other.tag == "Apple_point")
            {

                effectCollisionApple.Play();
                GameObject gameObjectSkillApple = Instantiate(SkillApple, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                gameObjectSkillApple.GetComponent<AudioSource>().Play();
                stateManager.SetScore(point);
                StartCoroutine(delayDestroySkillApple(gameObjectSkillApple));
                audioSFX.Play();
            }
            else if (other.tag == "Log")
            {
                audioSFX.clip = SFXCollisionLogMax;
                audioSFX.Play();
                effectTriggerLog.Play();

            }
        }
    }
    IEnumerator delayDestroySkillApple(GameObject gameObj)
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObj);
    }
    public void PlaySFXShoot()
    {
        audioSFX.clip = SFXShooting;
        audioSFX.Play();
    }
}
