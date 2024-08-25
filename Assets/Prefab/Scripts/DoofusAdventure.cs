using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoofusAdventure : MonoBehaviour
{
    public GameObject doofus;          // Reference to Doofus (the player character)
    public GameObject pulpitPrefab;    // Reference to the pulpit prefab
    public int score = 0;              // Player's score
    public float movementSpeed = 5f;   // Speed of Doofus
    public float pulpitLifetime = 3f;  // Lifetime of each pulpit

    private Vector3 lastPulpitPosition; // Last pulpit position
    private GameObject currentPulpit;   // Current pulpit Doofus is on

    void Start()
    {
        // Spawn the initial pulpit
        SpawnPulpit(Vector3.zero);
        lastPulpitPosition = Vector3.zero;
    }

    void Update()
    {
        MoveDoofus();
        CheckForFall();
    }

    void MoveDoofus()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0f, v) * movementSpeed * Time.deltaTime;
        doofus.transform.Translate(movement);

        // Check if Doofus has moved to a new pulpit
        if(Vector3.Distance(doofus.transform.position, lastPulpitPosition) > 1f)
        {
            score++;
            Debug.Log("Score: " + score);
            lastPulpitPosition = doofus.transform.position;
            SpawnPulpit(GetRandomAdjacentPosition());
        }
    }

    void SpawnPulpit(Vector3 position)
    {
        // Destroy previous pulpit after its lifetime
        if(currentPulpit != null)
            Destroy(currentPulpit, pulpitLifetime);

        // Create a new pulpit
        currentPulpit = Instantiate(pulpitPrefab, position, Quaternion.identity);
    }

    Vector3 GetRandomAdjacentPosition()
    {
        // Randomly choose a direction (up, down, left, right)
        int direction = Random.Range(0, 4);
        switch(direction)
        {
            case 0: return lastPulpitPosition + Vector3.forward * 1.5f;
            case 1: return lastPulpitPosition + Vector3.back * 1.5f;
            case 2: return lastPulpitPosition + Vector3.left * 1.5f;
            case 3: return lastPulpitPosition + Vector3.right * 1.5f;
            default: return lastPulpitPosition;
        }
    }

    void CheckForFall()
    {
        if(doofus.transform.position.y < -5f)
        {
            Debug.Log("Game Over! Final Score: " + score);
            // Restart or show Game Over screen
        }
    }
}
