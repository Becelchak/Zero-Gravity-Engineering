using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> playlist;
    private AudioSource audioSource;
    private int currentNumber = -1;
    private int maxNumber = 0;
    private int minNumber = 0;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!audioSource.isPlaying && currentNumber != -1)
        {
            audioSource.PlayOneShot(playlist[currentNumber]);

            currentNumber = currentNumber + 1 > maxNumber ? minNumber : currentNumber + 1;
        }
    }

    public void SetParameterCounter(int counterMin, int counterMax)
    {
        audioSource.Stop();

        maxNumber = counterMax;
        minNumber = counterMin;

        currentNumber = counterMin;
    }
}
