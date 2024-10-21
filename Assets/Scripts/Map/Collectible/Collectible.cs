using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected ParticleSystem particleEffect;

    protected void PlayEffect()
    {
        particleEffect.Play();

        audioSource.Play();
    }
}