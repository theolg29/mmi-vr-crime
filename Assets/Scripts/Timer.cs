using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public AudioClip countdownSound; // Son pour le décompte
    public VideoPlayer cinemaVideo; // Référence au VideoPlayer
    private AudioSource audioSource; // Source audio pour jouer le son
    private float timer = 300f;
    private bool timerRunning = false;
    private Coroutine timerCoroutine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateTimerText();
        GameObject cinemaObject = GameObject.FindGameObjectWithTag("cinema-video");
        if (cinemaObject != null)
        {
            cinemaVideo = cinemaObject.GetComponent<VideoPlayer>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !timerRunning)
        {
            StartCoroutine(StartCountdown());
        }
    }

    private IEnumerator StartCountdown()
    {
        if (audioSource != null && countdownSound != null)
        {
            audioSource.PlayOneShot(countdownSound);
        }

        for (int i = 3; i > 0; i--)
        {
            if (timerText != null)
            {
                timerText.text = $"Début dans\n{i}...";
            }
            yield return new WaitForSeconds(1f);
        }

        StartTimer();
    }

    public void StartTimer()
    {
        if (!timerRunning)
        {
            timerRunning = true;

            // Démarre la vidéo si elle est assignée
            if (cinemaVideo != null)
            {
                cinemaVideo.Play();
            }

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
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("Temps restant :\n{0:00}:{1:00}", minutes, seconds);
        }
    }
}