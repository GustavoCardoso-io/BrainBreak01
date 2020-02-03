using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private Sprite muteUImage = null;
    [SerializeField]
    private Sprite soundImage = null;
    [SerializeField]
    public bool isPlayingMusic = true;
    [SerializeField]
    private AudioSource[] audiosInGame = null;
    private int statusMusic = 0;
    [SerializeField]
    private Button musicButton = null;
    private void Start()
    {
        statusMusic  = PlayerPrefs.GetInt("musicStatus");
        SetMusicStatus();
        SetIconMusic();
        Debug.Log(statusMusic);
    }
    public void ButtonMusicClick()
    {
        if (statusMusic == 1)
        {
            statusMusic = 0;
            PlayerPrefs.SetInt("musicStatus", statusMusic);
            SetIconMusic();
        }
        else
        {
            statusMusic = 1;
            SetIconMusic();
            PlayerPrefs.SetInt("musicStatus", statusMusic);
        }
        SetMusicStatus();
    }

    private void SetMusicStatus()
    {
        for (int i = 0; i < audiosInGame.Length; i++)
        {
            if (statusMusic == 1)
            {
                audiosInGame[i].mute = true;
            }
            else
            {
                audiosInGame[i].mute = false;
            }
        }
    }
    private void SetIconMusic()
    {
        if (statusMusic == 0)
        {
            musicButton.image.sprite = muteUImage;
        }
        else
        {
            musicButton.image.sprite = soundImage;
        }
    }

}
