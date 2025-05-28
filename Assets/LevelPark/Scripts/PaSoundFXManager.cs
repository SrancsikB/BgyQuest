using UnityEngine;

public class PaSoundFXManager : MonoBehaviour
{
    public static PaSoundFXManager Instance;

    [SerializeField] AudioSource soundFXObject;
    [SerializeField] AudioClip catFright;
    [SerializeField] AudioClip catSleep;
    [SerializeField] AudioClip dogBark;
    
    [SerializeField] float soundFXVolume;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    public void PlayCatFright(Transform parent)
    {
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = catFright;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
        
    }

    public void PlayCatSleep(Transform parent)
    {
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = catSleep;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlayDogBark(Transform parent)
    {
        AudioSource audioSource = Instantiate(soundFXObject, parent);
        audioSource.clip = dogBark;
        audioSource.loop = true;
        audioSource.Play();
        float clipLength = audioSource.clip.length * 3;
        Destroy(audioSource.gameObject, clipLength);

    }

    //void PlayTrainRunning(Transform parent, bool isSelected)
    //{
    //    AudioSource audioSource = Instantiate(soundFXObject, parent);
    //    audioSource.clip = trainRunningClip;
    //    audioSource.volume = SetSoundFXVolume(isSelected);
    //    audioSource.loop = true;
    //    audioSource.Play();
    //}



}
