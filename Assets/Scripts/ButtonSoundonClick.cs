using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip hoverSound;
    public AudioSource audioSource;
    public float cooldown = 0.2f;

    private float lastSoundTime = -1000f;
    private bool isHovering = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isHovering)
        {
            isHovering = true;
            PlayHoverSound();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    private void PlayHoverSound()
    {
        if (Time.time - lastSoundTime > cooldown)
        {
            if (hoverSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hoverSound);
                lastSoundTime = Time.time;
            }
        }
    }
}
