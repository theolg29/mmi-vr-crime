using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [Header("Prefab à instancier")]
    public GameObject trash; // L'objet à spawn

    [Header("Points de spawn")]
    public Transform[] spawnPoints; // Liste des positions possibles pour spawn

    [Header("Intervalle de spawn")]
    public float spawnInterval = 2f; // Temps entre chaque spawn

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Lancer le spawner à intervalles réguliers
        InvokeRepeating(nameof(SpawnTrash), spawnInterval, spawnInterval);
    }

    public void SpawnTrash()
    {
        if (spawnPoints.Length == 0 || trash == null)
        {
            Debug.LogWarning("Aucun point de spawn ou prefab assigné au Spawner.");
            return;
        }

        // Choisir un point de spawn aléatoire
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instancier l'objet au point de spawn
        Instantiate(trash, spawnPoint.position, spawnPoint.rotation);
    }
}
