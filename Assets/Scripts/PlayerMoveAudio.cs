using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAudio : MonoBehaviour
{
    AudioSource audioSource;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public void MoveSound()
    {
        audioSource.Play();
    }
}
