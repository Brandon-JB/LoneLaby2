using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class audioStatics
{
    public static float MasterVolume = 0.5f;
    public static float BGMVolume = 0.5f;
    public static float SFXVolume = 1f;
    public static float VoiceVolume = 0.6f;
    public static string interractButton = "space";
    public static KeyCode keycodeInterractButton = KeyCode.Space;
    public static float TextSpeedMultiplier = 1f;

    static audioStatics() // static constructor runs once when the class is first accessed
    {
        LoadSettings();
    }

    public static void LoadSettings()
    {
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        BGMVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        VoiceVolume = PlayerPrefs.GetFloat("VoiceVolume", 0.6f);
        TextSpeedMultiplier = PlayerPrefs.GetFloat("TextSpeedMultiplier", 1f);
    }

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefs.SetFloat("BGMVolume", BGMVolume);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        PlayerPrefs.SetFloat("VoiceVolume", VoiceVolume);
        PlayerPrefs.SetFloat("TextSpeedMultiplier", TextSpeedMultiplier);
        PlayerPrefs.Save();
    }
}
