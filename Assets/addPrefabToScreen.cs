using System.Collections;
using UnityEngine;

public class addPrefabToScene : MonoBehaviour
{
    public GameObject prefab; // Assign your prefab in the inspector
    public float delayInSeconds = 5f; // Time after which the prefab is added to the scene
    public AudioClip audioClip; // Assign your AudioClip in the inspector
    public OVRPassthroughLayer passthroughLayer; // Drag the GameObject with OVRPassthroughLayer component here

    private AudioSource audioSource;

    void Start()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is not assigned.");
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = audioClip;
        audioSource.playOnAwake = false;

        if (passthroughLayer == null)
        {
            Debug.LogError("OVRPassthroughLayer component is not assigned.");
            return;
        }

        Invoke("InstantiatePrefab", delayInSeconds);
    }

    void InstantiatePrefab()
    {
        if (prefab == null) return;

        Instantiate(prefab);

        if (audioClip != null)
        {
            audioSource.Play();
        }

        StartCoroutine(FlashBrightness());
    }

    IEnumerator FlashBrightness()
    {
        for (int i = 0; i < 2; i++) // Number of flashes
        {
            passthroughLayer.SetColorMapControls(contrast: 0, brightness: -1); // Set brightness to -1
            yield return new WaitForSeconds(0.25f); // Wait for 0.25 seconds
            passthroughLayer.SetColorMapControls(contrast: 0, brightness: 0); // Reset brightness to 0
            if (i < 1) // Check to prevent waiting after the last flash
                yield return new WaitForSeconds(0.25f); // Wait for 0.25 seconds before next flash
        }
    }
}
