using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

enum FloorMat { wood, fabric };
public class PlayerAudioListener : MonoBehaviour
{
    
    private FloorMat currentMat;
    private static PlayerAudioListener inst = null;
    private AudioClip theClip = null;
    float lastPitch = 0;

    void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        MessageDispatch.GetInstance().AddMessageHandler(MessageReceived); // add this instance as a message handler to message dispatch system
    }
    public static PlayerAudioListener GetInstance()
    {
        return inst;
    }
    public void PlayTheSound(AudioClip sound, AudioSource source)
    {
        if (source != null)
        {
            /*
                 if (source.isPlaying)
                 {
                     source.Stop();
                 }
            */
            source.clip = sound;
            source.Play();
            source.pitch = lastPitch;
        }

    }

    public void MessageReceived(string message, AudioSource source)
    {
        switch (message)
        {

            case "Jump":                            
                theClip = Resources.Load<AudioClip>("SoundsAndMusic/PlayerSounds/JumpSounds/JimmyJump");
                lastPitch = source.pitch;
                source.pitch = Random.Range(0.89f, 1.00f);
                PlayTheSound(theClip, source);
                break;
            case "SetWood":
                currentMat = FloorMat.wood;
                break;
            case "SetFabric":
                currentMat = FloorMat.fabric;
                break;
               
            case "Run":
                float seed = Random.Range(0, 1);
                if (currentMat == FloorMat.wood)
                {
                    if (seed <= 0.9f)
                    {
                        theClip = Resources.Load<AudioClip>("SoundsAndMusic/PlayerSounds/Footsteps/WoodFootsteps/WoodLeft 1");
                    }
                    else
                    {
                        theClip = Resources.Load<AudioClip>("SoundsAndMusic/PlayerSounds/Footsteps/WoodFootsteps/WoodRight 1");
                    }
                }
                else if (currentMat == FloorMat.fabric) {
                    if (seed <= 0.9f)
                    {
                        theClip = Resources.Load<AudioClip>("SoundsAndMusic/PlayerSounds/Footsteps/FabricFootsteps/FabricLeft 1");
                    }
                    else
                    {
                        theClip = Resources.Load<AudioClip>("SoundsAndMusic/PlayerSounds/Footsteps/FabricFootsteps/FabricRight 1");
                    }
                }
                lastPitch = source.pitch;
                source.pitch = Random.Range(0.59f, 0.71f);
                PlayTheSound(theClip, source);
                break;
            case "Landed":
                if (currentMat == FloorMat.wood)
                {
                    theClip = Resources.Load<AudioClip>("SoundsAndMusic/PlayerSounds/Footsteps/FabricFootsteps/FabricRight 1");
                }
                else if (currentMat == FloorMat.fabric)
                {
                    theClip = Resources.Load<AudioClip>("SoundsAndMusic/PlayerSounds/Footsteps/FabricFootsteps/FabricRight 1");
                }
                PlayTheSound(theClip, source);
                break;
           
        }
    }

    public void OnAuidioEvent(string eventName)
    {
        throw new System.NotImplementedException();
    }
}

