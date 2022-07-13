using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController instance = null;
    public static AudioController Instance => instance;

    public AudioSource audio;
    public List<AudioClip> ClipList;
    
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
    
    
    public void SpawnAudio(int index)
    {
        audio.PlayOneShot(ClipList[index]);
    }
}
