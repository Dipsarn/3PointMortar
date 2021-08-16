using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FrontpageController : MonoBehaviour {

    public Text highscoreNameText;
    public Text highscoreValueText;
    public GameObject lamp1;
    public GameObject lamp2;
    public ParticleSystem dustPS;
    public AudioSource mortarSoundAS;
    private float eventTimer;
    private float eventTriggerTime = 5f;
    private Quaternion targetRotation;
	// Use this for initialization
	void Start () {
        highscoreNameText.text = PlayerPrefs.GetString("HighscoreName");
        highscoreValueText.text = PlayerPrefs.GetInt("Highscore").ToString();
        LoadMainView();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }
        eventTimer += Time.deltaTime;
        if (eventTimer > eventTriggerTime)
        {
            TriggerRandomEvent();
            eventTimer = 0f;
            eventTriggerTime = Random.Range(5f, 12f);
        }
    }

    private void TriggerRandomEvent()
    {
        mortarSoundAS.Play();
        dustPS.Play();
        StartCoroutine(ShakeLamps(0.2f));
        StartCoroutine(ShakeCamera(0.2f,0.3f, 0.02f));
    }

    IEnumerator ShakeLamps(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 vector = new Vector3(0.2f, 1, 0.2f);
        lamp1.GetComponent<Rigidbody>().AddForce(vector * 3f, ForceMode.Impulse);
        lamp2.GetComponent<Rigidbody>().AddForce(vector * 3f, ForceMode.Impulse);
    }

    IEnumerator ShakeCamera(float delay, float duration, float magnitude)
    {
        yield return new WaitForSeconds(delay);
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            
            transform.localPosition = new Vector3(x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;

    }


    public void LoadControlsView()
    {
        targetRotation = Quaternion.Euler(12, -90, 0);
    }

    public void LoadHighscoreView()
    {
        targetRotation = Quaternion.Euler(12, 90, 0);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMainView()
    {
        targetRotation = Quaternion.Euler(12, 0, 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
