using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    
    public static AudioManager instance;
    // public GameObject player;
    GameObject player;
    
    void Awake() {
        
        if(instance == null){
            instance = this;
        } else{
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
            s.source.pitch = s.pitch;
        }
        
        Play("MainMusic");

    }

    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            print("No sound named " + name + " , make sure you've spelt the name correctly.");
            return;
        }
        if(!s.source.isPlaying){
            s.source.Play();
        }
    }

    public void LoopSound (string name, bool isLoop){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            print("No sound named " + name + " , make sure you've spelt the name correctly.");
            return;
        }
        if(!s.source.isPlaying){
            s.source.loop = isLoop;
        }else{
            s.source.loop = isLoop;
        }
    }

    void Update() {
        // var playerScript = player.gameObject.GetComponent<PlayerMovement>();
        // var playerRigid = player.gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        var playerScript = player.gameObject.GetComponent<PlayerMovement>();
        var playerRigid = player.gameObject.GetComponent<Rigidbody>();

        // Only play footsep sound when player is walking, not in the air, and is moving across the x axis.
        if(playerScript.IsWalking && !playerScript.IsInAir && (playerRigid.velocity.x > 0.5f || playerRigid.velocity.x < -0.5f)){
            LoopSound("FootSteps", true);
            Play("FootSteps");
        } else{
            LoopSound("FootSteps", false);
        }

    }
}
