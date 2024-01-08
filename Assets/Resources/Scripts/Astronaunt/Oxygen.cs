using Assets.Scripts;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    [SerializeField] private float oxygenMax = 120f;
    [SerializeField] private Text oxygenHUD;
    [SerializeField] private AudioClip breathing;
    [SerializeField] private AudioClip oxygen;
    [SerializeField] private AudioClip gasping;
    [SerializeField] private AudioClip balloon;
    private float timer;
    private bool isNonOxygenArea = false;
    private bool isOxygenUp = false;
    private bool isOverOxygen = false;
    private AudioSource astronauntAudioSource;

    private Health astronauntHealth;
    private float damageTimer = 2f;
    private Moving playerMoving;
    void Start()
    {
        timer = oxygenMax;
        oxygenHUD.text = oxygenMax.ToString(CultureInfo.InvariantCulture);
        astronauntAudioSource = GetComponent<AudioSource>();
        astronauntHealth = GetComponent<Health>();
        playerMoving = GetComponent<Moving>();
    }

    void Update()
    {
        if(astronauntHealth.GetLiveStatus() || playerMoving.GetFreezeStatus()) return;

        oxygenHUD.text = (Mathf.Round(timer * 100f) / 100f).ToString(CultureInfo.InvariantCulture);
        if (!astronauntAudioSource.isPlaying && isNonOxygenArea)
        {
            astronauntAudioSource.clip = breathing;
            astronauntAudioSource.Play();
        }
        if (timer <= 0)
        {
            isOverOxygen = true;
            timer = 0;
        }

        if (isNonOxygenArea && !isOverOxygen)
        {
            timer -= Time.deltaTime;
        }
        else if (isOxygenUp)
        {
            isOverOxygen = false;
            timer += 15 * Time.deltaTime;
            if(timer > oxygenMax)
                timer = oxygenMax;
        }

        if(isOverOxygen)
            damageTimer -= Time.deltaTime;
        if (!isOverOxygen || !(damageTimer <= 0)) return;
        astronauntAudioSource.Stop();
        astronauntAudioSource.PlayOneShot(gasping);
        astronauntHealth.TakeDamage(10);
        damageTimer = 2f;

    }

    public void AddOxygen(int oxygen)
    {
        timer += oxygen;
        if(timer > oxygenMax)
            timer = oxygenMax;
        oxygenHUD.text = (Mathf.Round(timer * 100f) / 100f).ToString(CultureInfo.InvariantCulture);
        astronauntAudioSource.PlayOneShot(balloon);

        isOverOxygen = false;
        damageTimer = 2f;
    }

    public void RemoveOxygen(float oxygen)
    {
        timer -= oxygen;
    }

    public void EndGame()
    {
        astronauntAudioSource.Stop();
        timer = 120f;
    }

    public bool isGasping()
    {
        return isOverOxygen;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("OxygenStation"))
        {
            astronauntAudioSource.clip = oxygen;
            astronauntAudioSource.Play();
            isNonOxygenArea = false;
            isOxygenUp = true;
        }

        if (other.gameObject.tag == "NonOxygen" && !isOxygenUp)
        {
            isNonOxygenArea = true;
            astronauntAudioSource.clip = breathing;
            astronauntAudioSource.Play();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "NonOxygen" && !isOxygenUp)
        {
            isNonOxygenArea = true;
            astronauntAudioSource.clip = breathing;
            if(!astronauntAudioSource.isPlaying)
                astronauntAudioSource.Play();
        }

        if (other.gameObject.name.Contains("OxygenStation"))
        {
            isNonOxygenArea = false;
            isOxygenUp = true;
            astronauntAudioSource.clip = oxygen;
            if (!astronauntAudioSource.isPlaying)
                astronauntAudioSource.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("OxygenStation"))
        {
            isOxygenUp = false;
            astronauntAudioSource.Stop();
        }

        if (other.gameObject.tag == "NonOxygen")
        {
            isNonOxygenArea = false;
            astronauntAudioSource.Stop();
        }

    }
}
