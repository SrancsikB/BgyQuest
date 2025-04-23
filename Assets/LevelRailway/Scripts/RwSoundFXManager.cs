using UnityEngine;

public class RwSoundFXManager : MonoBehaviour
{
    public static RwSoundFXManager Instance;

    [SerializeField] AudioSource soundFXObject;
    [SerializeField] AudioClip trainStartClip;
    [SerializeField] AudioClip trainRunningClip;
    [SerializeField] AudioClip trainStopCoinClip;
    [SerializeField] AudioClip trainStopEmptyClip;
    [SerializeField] float soundFXVolume;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    public void PlayTrainStart(Transform parent)
    {
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = trainStartClip;
        audioSource.volume = soundFXVolume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
        PlayTrainRunning(parent);
    }

    void PlayTrainRunning(Transform parent)
    {
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = trainRunningClip;
        audioSource.volume = soundFXVolume;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayTrainStop(Transform parent)
    {
        GameObject soundFX = parent.GetComponentInChildren<AudioSource>().gameObject;
        if (soundFX != null)
        {
            Destroy(soundFX);
        }
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = trainStopCoinClip;
        audioSource.volume = soundFXVolume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlayTrainStopEmpty(Transform parent)
    {
        GameObject soundFX = parent.GetComponentInChildren<AudioSource>().gameObject;
        if (soundFX != null)
        {
            Destroy(soundFX);
        }
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = trainStopEmptyClip;
        audioSource.volume = soundFXVolume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
        

    }

}
