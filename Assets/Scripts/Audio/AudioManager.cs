using AlmenaraGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField] private AudioObject _persistentAudio;

    // Start is called before the first frame update
    void Start()
    {
        MultiAudioSource persistentAudio = MultiAudioManager.PlayAudioObject(_persistentAudio, 0, transform.position);

        //Makes the sound persistent
        persistentAudio.PersistsBetweenScenes = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
