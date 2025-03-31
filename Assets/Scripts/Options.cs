using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
using UnityEngine.UI;
using TMPro;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Options : MonoBehaviour
{
    //for changing text speed
    [SerializeField] private DialogueSystem ds; //Needs to be grabbed
    //[SerializeField] private Button button;
    //[SerializeField] private TextMeshProUGUI buttonTXT;
    [SerializeField] private audioManager am; //Needs to be grabbed
    //[SerializeField] private PlayerController playerController;
    [SerializeField] private AudioSource voiceVol; //Needs to be grabbed
    [SerializeField] private Slider[] optionsSliders;
    [SerializeField] private GameObject DeleteSaveMenu;

    void Start()
    {

        ds = FindObjectOfType<DialogueSystem>(); //Should only have one in scene
        am = FindObjectOfType<audioManager>();

        optionsSliders[0].value = audioStatics.MasterVolume;
        optionsSliders[1].value = audioStatics.BGMVolume;
        optionsSliders[2].value = audioStatics.SFXVolume;
        optionsSliders[3].value = audioStatics.VoiceVolume;
        optionsSliders[4].value = audioStatics.TextSpeedMultiplier;
        //Add back when its relevant
        //voiceVol.volume = audioStatics.VoiceVolume * audioStatics.MasterVolume;
        //buttonTXT.text = audioStatics.interractButton.ToUpper();
    }
    public void onMasterSliderChanged(float value)
    {
        audioStatics.MasterVolume = value;
        if (audioManager.currentlyPlaying)
        {
            audioManager.currentlyPlaying.volume = audioStatics.MasterVolume * audioStatics.BGMVolume;
        }
    }
    public void onBGMSliderChanged(float value)
    {
        audioStatics.BGMVolume = value;
        if (audioManager.currentlyPlaying)
        {
            audioManager.currentlyPlaying.volume = audioStatics.MasterVolume * audioStatics.BGMVolume;
        }
    }
    public void onSFXSliderChanged(float value)
    {
        audioStatics.SFXVolume = value;
    }
    public void onVoiceSliderChanged(float value)
    {
        audioStatics.VoiceVolume = value;
        voiceVol.volume = audioStatics.VoiceVolume * audioStatics.MasterVolume;
    }
    public void onTextSpeedSliderChanged(float value)
    {
        try
        {
            ds.setTextSpeed(value);
        } catch
        {
            Debug.Log("For some reason, I cannot set the speed");
        }
        audioStatics.TextSpeedMultiplier = value;
    }
    public void deleteSaveData()
    {
        //playerController.DeleteSave();
        DeleteSaveMenu.SetActive(true);
    }
}

