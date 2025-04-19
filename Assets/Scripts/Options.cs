using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
using UnityEngine.UI;
using TMPro;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

        if (voiceVol == null)
        {
            voiceVol = GameObject.FindGameObjectWithTag("CharVoice").GetComponent<AudioSource>();
        }
        //buttonTXT.text = audioStatics.interractButton.ToUpper();
    }

    private void OnEnable()
    {
        if (voiceVol == null)
        {
            voiceVol = GameObject.FindGameObjectWithTag("CharVoice").GetComponent<AudioSource>();
            voiceVol.volume = audioStatics.VoiceVolume * audioStatics.MasterVolume;
        }
        else
        {
            voiceVol.volume = audioStatics.VoiceVolume * audioStatics.MasterVolume;
        }
        optionsSliders[0].value = audioStatics.MasterVolume;
        optionsSliders[1].value = audioStatics.BGMVolume;
        optionsSliders[2].value = audioStatics.SFXVolume;
        optionsSliders[3].value = audioStatics.VoiceVolume;
        optionsSliders[4].value = audioStatics.TextSpeedMultiplier;
    }

    public void onMasterSliderChanged()
    {
        audioStatics.MasterVolume = optionsSliders[0].value;
        if (audioManager.currentlyPlaying)
        {
            audioManager.currentlyPlaying.volume = audioStatics.MasterVolume * audioStatics.BGMVolume;
        }
    }
    public void onBGMSliderChanged()
    {
        audioStatics.BGMVolume = optionsSliders[1].value;
        if (audioManager.currentlyPlaying)
        {
            audioManager.currentlyPlaying.volume = audioStatics.MasterVolume * audioStatics.BGMVolume;
        }
    }
    public void onSFXSliderChanged()
    {
        audioStatics.SFXVolume = optionsSliders[2].value;
    }
    public void onVoiceSliderChanged()
    {
        audioStatics.VoiceVolume = optionsSliders[3].value;
        //voiceVol.volume = audioStatics.VoiceVolume * audioStatics.MasterVolume;
    }
    public void onTextSpeedSliderChanged()
    {
        try
        {
            ds.setTextSpeed(optionsSliders[4].value);
        } catch
        {
            Debug.Log("For some reason, I cannot set the speed");
        }
        audioStatics.TextSpeedMultiplier = optionsSliders[4].value;
    }
    public void deleteSaveData()
    {
        //playerController.DeleteSave();
        DeleteSaveMenu.SetActive(true);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", audioStatics.MasterVolume);
        PlayerPrefs.SetFloat("BGMVolume", audioStatics.BGMVolume);
        PlayerPrefs.SetFloat("SFXVolume", audioStatics.SFXVolume);
        PlayerPrefs.SetFloat("VoiceVolume", audioStatics.VoiceVolume);
        PlayerPrefs.SetFloat("TextSpeedMultiplier", audioStatics.TextSpeedMultiplier);

        PlayerPrefs.Save(); // Actually writes to disk
    }
}

