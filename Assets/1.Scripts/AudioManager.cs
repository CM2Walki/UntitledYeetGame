using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip[] Yeet;
    public AudioClip Ingame;
    //public AudioClip EndOfGame;
    public AudioClip CombineSucces;
    public AudioClip CombineFail;
    public AudioClip HitPink;
    public AudioClip HitGreen;

    public AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayYeet()
    {
        audioSource.PlayOneShot(Yeet[Random.Range(0, Yeet.Length - 1)]);
    }

    public void PlayHitPink()
    {
        audioSource.PlayOneShot(HitPink);
    }

    public void PlayHitGreen()
    {
        audioSource.PlayOneShot(HitGreen);
    }

    public void PlayCombineSuccess()
    {
        audioSource.PlayOneShot(CombineSucces);
    }

    public void PlayCombineFail()
    {
        audioSource.PlayOneShot(CombineFail);
    }

    public void PlayIngame()
    {
        audioSource.clip = Ingame;
        audioSource.Play();
    }

    public void PlayEndOfGame()
    {
        //audioSource.clip = EndOfGame;
        //audioSource.Play();
    }
}
