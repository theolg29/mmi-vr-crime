using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float timer = 300f;
    private bool timerRunning = false;
    private Coroutine timerCoroutine;

    private void Start()
    {
        // Uniquement mettre à jour l'affichage au début, sans démarrer le timer
        UpdateTimerText();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre est spécifiquement XR Origin (XR Rig) ou un de ses enfants
        if ((other.gameObject.name == "XR Origin (XR Rig)" || 
             other.transform.IsChildOf(GameObject.Find("XR Origin (XR Rig)").transform)) && 
            !timerRunning)
        {
            Debug.Log("XR Rig détecté, démarrage du timer");
            StartTimer();
        }
    }

    public void StartTimer()
    {
        if (!timerRunning)
        {
            timerRunning = true;
            timerCoroutine = StartCoroutine(RunTimer());
        }
    }

    private IEnumerator RunTimer()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer -= 1f;
            UpdateTimerText();
        }
        timerRunning = false;
        Debug.Log("Timer terminé");
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("Temps : {0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            Debug.LogError("timerText n'est pas assigné dans l'inspecteur");
        }
    }
}