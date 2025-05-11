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
    [SerializeField] float soundFXVolumeNotSelected;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    public void PlayTrainStart(Transform parent, bool isSelected)
    {
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = trainStartClip;
        audioSource.volume = SetSoundFXVolume(isSelected);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
        PlayTrainRunning(parent, isSelected);
    }

    void PlayTrainRunning(Transform parent, bool isSelected)
    {
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = trainRunningClip;
        audioSource.volume = SetSoundFXVolume(isSelected);
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayTrainStop(Transform parent, bool isSelected)
    {
        GameObject soundFX = parent.GetComponentInChildren<AudioSource>().gameObject;
        if (soundFX != null)
        {
            Destroy(soundFX);
        }
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = trainStopCoinClip;
        audioSource.volume = SetSoundFXVolume(isSelected);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlayTrainStopEmpty(Transform parent, bool isSelected)
    {
        GameObject soundFX = parent.GetComponentInChildren<AudioSource>().gameObject;
        if (soundFX != null)
        {
            Destroy(soundFX);
        }
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = trainStopEmptyClip;
        audioSource.volume = SetSoundFXVolume(isSelected);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
        

    }

    private float SetSoundFXVolume(bool isSelected)
    {
        if (isSelected)
        {
            return soundFXVolume;
        }
        else
        {
            return soundFXVolumeNotSelected;
        }
    }

}
