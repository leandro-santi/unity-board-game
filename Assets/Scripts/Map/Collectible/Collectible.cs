using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected ParticleSystem particleEffect;

    protected void PlayEffect() // Future implementation :))
    {
        particleEffect.Play();

        audioSource.Play();
    }
}