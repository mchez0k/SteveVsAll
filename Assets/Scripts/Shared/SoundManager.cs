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

    private bool AudioValidate(AudioClip[] clips)
    {
        return clips != null && audioSource != null && audioSource.enabled && clips.Length > 0;
    }

    public void OnAttack()
    {
        if (!AudioValidate(attackClips)) return;
        audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
        audioSource.Play();
    }

    public void OnHurt()
    {
        if (!AudioValidate(hurtClips)) return;
        audioSource.clip = hurtClips[Random.Range(0, hurtClips.Length)];
        audioSource.Play();
    }

    public void OnDeath()
    {
        if (!AudioValidate(deathClips)) return;
        audioSource.clip = deathClips[Random.Range(0, deathClips.Length)];
        audioSource.Play();
    }
}
