using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Audio;

public class AVmanager : MonoBehaviour
{
    public Sound[] sounds;

    public static AVmanager instance;

    [Range(0f,1f)]
    public float Totalvolume;
    // Start is called before the first frame update
    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //PlayerPrefs.DeleteKey("Audio");

        DontDestroyOnLoad(gameObject);

        Totalvolume =  PlayerPrefs.GetFloat("Audio",1f); //必須對應KEY值默認為1
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip  = s.clip;

            s.source.pitch = s.ptich;
            s.source.loop = s.loop;

            s.source.volume = Totalvolume;
        }

    }
    
    void Start()
    {
        foreach(Sound s in sounds)
        {
            s.source.volume = Totalvolume;
            //print(s.source.volume);
        }
    }

    // Update is called once per frame
    void Update()
    {

        foreach(Sound s in sounds)
        {
            s.source.volume = Totalvolume;

            PlayerPrefs.SetFloat("Audio",s.source.volume); //存儲

            //print(s.source.volume);
        }

        
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Stop();
    }
}
