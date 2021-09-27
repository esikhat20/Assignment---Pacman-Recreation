using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    public AudioSource backgroundSource;
    public AudioSource ghostSource;
    
    public AudioClip backgroundClip;
    public AudioClip ghostClip;

    // Start is called before the first frame update
    void Start()
    {
        //plays bgm intro until the end, then plays the ghost audio
        backgroundSource.PlayScheduled(AudioSettings.dspTime);
        double clipLength = backgroundSource.clip.samples / backgroundSource.clip.frequency;
        ghostSource.PlayScheduled(AudioSettings.dspTime + clipLength);            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
