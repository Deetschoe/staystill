using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInViewChecker : MonoBehaviour
{
    public Camera ovrCamera; // Assign your OVR Camera here
    public AudioClip ghostSound; // Assign the audio clip for the ghost here
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        foreach (GameObject ghost in ghosts)
        {
            Vector3 directionToGhost = ghost.transform.position - ovrCamera.transform.position;
            float angle = Vector3.Angle(ovrCamera.transform.forward, directionToGhost);

            if (angle < (ovrCamera.fieldOfView / 2))
            {
                if (!audioSource.isPlaying)
                {
                    Debug.Log("Ghost in view: " + ghost.name);
                    audioSource.PlayOneShot(ghostSound);
                }
            }
        }
    }
}
