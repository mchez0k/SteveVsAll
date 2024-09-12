using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip[] attackClips;
    [SerializeField] private AudioClip[] hurtClips;
    [SerializeField] private AudioClip[] deathClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnAttack()
    {
        if (attackClips != null && audioSource != null && attackClips.Length > 0)
        {
            audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
            audioSource.Play();
        }
    }

    public void OnHurt()
    {
        if (hurtClips != null && audioSource != null && hurtClips.Length > 0)
        {
            audioSource.clip = hurtClips[Random.Range(0, hurtClips.Length)];
            audioSource.Play();
        }
    }

    public void OnDeath()
    {
        if (deathClips != null && audioSource != null && deathClips.Length > 0)
        {
            audioSource.clip = deathClips[Random.Range(0, deathClips.Length)];
            audioSource.Play();
        }
    }
}
