using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInViewChecker : MonoBehaviour
{
    public Camera ovrCamera; // Assign your OVR Camera here
    public AudioClip lookingAudio; // Assign the looking audio clip here
    public AudioClip[] tauntingAudios; // Assign the array of taunting audio clips here
    private AudioSource audioSource;
    private Coroutine tauntingCoroutine;
    private bool isLookingAtGhost = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        bool foundGhostInView = false;

        foreach (GameObject ghost in ghosts)
        {
            Vector3 directionToGhost = ghost.transform.position - ovrCamera.transform.position;
            float angle = Vector3.Angle(ovrCamera.transform.forward, directionToGhost);

            if (angle < (ovrCamera.fieldOfView / 2))
            {
                foundGhostInView = true;
                if (!isLookingAtGhost)
                {
                    isLookingAtGhost = true;
                    PlayLookingAudio();
                }
                break; // Found a ghost in view, no need to check others
            }
        }

        if (!foundGhostInView && isLookingAtGhost)
        {
            isLookingAtGhost = false;
            StartTauntingAudio();
        }
    }

    void PlayLookingAudio()
    {
        if (tauntingCoroutine != null)
        {
            StopCoroutine(tauntingCoroutine);
            tauntingCoroutine = null;
        }
        audioSource.clip = lookingAudio;
        audioSource.Play();
    }

    void StartTauntingAudio()
    {
        if (tauntingCoroutine == null)
        {
            tauntingCoroutine = StartCoroutine(TauntingAudioCoroutine());
        }
    }

    IEnumerator TauntingAudioCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 21));
            if (!isLookingAtGhost)
            {
                audioSource.clip = tauntingAudios[Random.Range(0, tauntingAudios.Length)];
                audioSource.Play();
            }
        }
    }
}
