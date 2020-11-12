using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public bool m_musicEnabled = true;
    public bool m_effectsEnabled = true;

    [Range(0,1)]
    public float m_musicVolume = 1.0f;

    [Range(0,1)]
    public float m_effectsVolume = 1.0f;

    public AudioClip m_clearLineSound;
    public AudioClip m_moveSound;
    public AudioClip m_dropSound;
    public AudioClip m_gameOverSound;
    public AudioClip m_backgroundMusic;
    public AudioClip m_titleMusic;
    public AudioClip m_nextLevelSound;
    public AudioSource m_musicSource;

    // Start is called before the first frame update
    void Start()
    {
      PlayBackgroundMusic(m_titleMusic);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleMusic() {
      m_musicEnabled = !m_musicEnabled;
      UpdateMusicSetting();
    }

    public void ToggleEffects() {
      m_effectsEnabled = !m_effectsEnabled;
    }

    public void PlayBackgroundMusic(AudioClip musicClip) {
      if (!m_musicEnabled || !musicClip || !m_musicSource) {
        return;
      }

      m_musicSource.Stop();
      m_musicSource.clip = musicClip;
      m_musicSource.volume = m_musicVolume;
      m_musicSource.loop = true;
      m_musicSource.Play();
    }

    public void StopBackgroundMusic() {
      m_musicSource.Stop();
    }

    public void StartGameBackgroundMusic() {
      PlayBackgroundMusic(m_backgroundMusic);
    }

    void UpdateMusicSetting() {
      if (m_musicSource.isPlaying != m_musicEnabled) {
        if (m_musicEnabled) {
          PlayBackgroundMusic(m_backgroundMusic);
        } else {
          m_musicSource.Stop();
        }
      }
    }
}
