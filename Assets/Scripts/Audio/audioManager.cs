using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class audioManager : MonoBehaviour
{
    public static audioManager Instance { get; private set; } // Singleton instance

    [SerializeField] private AudioSource[] BGMAvailable;
    [SerializeField] private GameObject[] SFXAvailable;
    private AudioSource[] loadedAudio;
    [SerializeField] private AudioSource voiceVol;
    public static AudioSource currentlyPlaying;
    private int songID = 20; // for playbgm

    // HOW IT WORKS!!
    // you have to pass in the song name and the speed you'd like it to fade in/out
    // speed defaults to 1, but SPEED MUST ALWAYS BE GREATER THAN 0.1!!!
    // A script calls playBGM, playBGM checks to see if there is a song currently playing.
    // it then finds the audio using the string passed in. it reassigns currentlyPlaying to the new audio.

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep it alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        voiceVol.volume = audioStatics.VoiceVolume * audioStatics.MasterVolume;
        BGMAvailable[0].DOFade(audioStatics.BGMVolume * audioStatics.MasterVolume, 0.5f);
    }
    public void playBGM(string songToPlay, float speed = 1)
    {
        switch (songToPlay.ToUpper()) // change songID based on the input
        {
            case "T1": case "OPENING":
                songID = 0;
                break;
            case "T2": case "DEFEAT":
                songID = 1;
                break;
            case "T3": case "ZARO":
                songID = 2;
                break;
            case "T4": case "MANSION":
                songID = 3;
                break;
            case "T5": case "VEINWOOD":
                songID = 4;
                break;
            case "T6": case "CAVES":
                songID = 5;
                break;
            case "T7": case "ORDER OF TRUTH":
                songID = 6;
                break;
            case "T8": case "BOSS":
                songID = 7;
                break;
            case "T9": case "SIDING WITH THE EXTREME":
                songID = 8;
                break;
            case "T10": case "OVERWORLD":
                songID = 9;
                break;
            case "T11": case "LEORA BOSS":
                songID = 10;
                break;
            case "T12": case "VERITA'S THEME":
                songID = 11;
                break;
            default:
                Debug.LogWarning("The BGM [" + songToPlay + "] could not be found.");
                break;
        }
        if (BGMAvailable[songID] == currentlyPlaying) // if the song is already playing, do nothing
        {
            return;
        }
        else
        {
            stopBGM(speed, true);
            playSongUsingID(songID, speed);
        }
    }


    //stops any bgm that was playing
    public void stopBGM(float speed, bool playingMusicAfter = false)
    {
        if (speed < 0.1f)
        {
            Debug.LogWarning("Cannot fade in/out songs using speed that is < 0.1 seconds!");
            return;
        }
        if (currentlyPlaying)
        {
            if (playingMusicAfter)
            {
                currentlyPlaying.DOFade(0, (speed - 0.05f)).OnComplete(() => { 
                    //this is the only music that stays at the same pitch
                    if(currentlyPlaying != BGMAvailable[1])
                    {
                        currentlyPlaying.pitch = 1f;
                    }
                });
            } else
            {
                currentlyPlaying.DOFade(0, (speed - 0.05f)).OnComplete(() => { currentlyPlaying.Stop(); currentlyPlaying.pitch = 1f; });
            }
        }
    }
    public void stopHeartbeatSFX() // only used for heartbeat
    {
        //SFXAvailable[13].volume = 0f;
        //SFXAvailable[13].Stop();
    }

    //I didn't update this function with the 2.0 options but we seem to like the int playSFX better
    public void playSFX(int id)
    {
        //Because I'm a wee bit silly and did the Id's wrong
        playSFXUsingID(id-1);
    }

    private void playSFXUsingID(int ID)
    {
        //if(ID == 13)
        //{
        //    SFXAvailable[ID].DOFade((audioStatics.SFXVolume * audioStatics.MasterVolume)/3, (60f));
        //    SFXAvailable[ID].Play();
        //    return;
        //}

        //Use ID to check what range we should be checking

        if (SFXAvailable[ID].transform.childCount > 0)
        {
            // If the object has children, gather AudioSources from them
            loadedAudio = GetComponentsInChildren<AudioSource>();
        }
        else
        {
            // If no children, use the AudioSource attached to this object
            AudioSource thisAudio = SFXAvailable[ID].GetComponent<AudioSource>();

            thisAudio.volume = (audioStatics.SFXVolume * audioStatics.MasterVolume);
            thisAudio.Play();
            return;
        }

        //If there's multiple sounds, pull a random one
        int randomIndex = Random.Range(0, loadedAudio.Length);
        loadedAudio[ID].volume = (audioStatics.SFXVolume * audioStatics.MasterVolume);
        loadedAudio[randomIndex].Play();
    }

    private void PlaySongUsingID(int ID, float speed)
    {
        //if (ID == 9)
        //{
        //    BGMAvailable[10].volume = (audioStatics.SFXVolume * audioStatics.MasterVolume);
        //    BGMAvailable[10].Play();
        //    while (!BGMAvailable[10].isPlaying)
        //    {
        //        //play other music
        //    }
        //}
        //else
        //{
            BGMAvailable[ID].Play();
            BGMAvailable[ID].DOFade(audioStatics.BGMVolume * audioStatics.MasterVolume, speed).OnComplete(() =>
            {
                if (currentlyPlaying)
                {
                    currentlyPlaying.Stop();
                }
                currentlyPlaying = BGMAvailable[ID];
            });
        //}
    }

    private void playSongUsingID(int ID, float speed)
    {

        //TODO: MAKE THIS WORK FOR DEFEAT MENU
        if (ID == 2) // if this is the you win screen
        {
            // Play the first audio clip
            AudioSource firstAudioSource = BGMAvailable[10];
            firstAudioSource.volume = audioStatics.BGMVolume * audioStatics.MasterVolume;
            firstAudioSource.pitch = 1;
            firstAudioSource.Play();
            //if (youWinMenu.killedPartyMember) // if a party member has been killed, change pitch
            //{
            //    changePitch(10, 0.9f, 2f);
            //}

            // Schedule the second audio clip to play after the first one finishes
            StartCoroutine(PlayVictory(firstAudioSource, ID, speed));
        }
        else
        {
            // Play the second audio clip directly
            PlayOtherAudio(ID, speed);
        }
    }

    private IEnumerator PlayVictory(AudioSource firstAudioSource, int ID, float speed)
    {
        // Wait until the first audio clip finishes playing
        while (firstAudioSource.isPlaying)
        {
            yield return null;
        }

        // Play the second audio clip
        PlayOtherAudio(ID, speed);
    }

    private void PlayOtherAudio(int ID, float speed)
    {
        //if (youWinMenu.killedPartyMember)
        //{
        //    BGMAvailable[ID].pitch = 0.8f;
        //}
        BGMAvailable[ID].Play();
        BGMAvailable[ID].DOFade(audioStatics.BGMVolume * audioStatics.MasterVolume, speed).OnComplete(() =>
        {
            //if (youWinMenu.killedPartyMember)
            //{
            //    BGMAvailable[10].pitch = 1f;
            //}
            if (currentlyPlaying)
            {
                currentlyPlaying.Stop();
            }
            currentlyPlaying = BGMAvailable[ID];
        });
    }


    public void changePitch(int id, float toValue = 0.8f, float speed = 5f)
    {
        BGMAvailable[id].DOPitch(toValue, speed);
        //Debug.Log("Pitch change done");
    }
}
