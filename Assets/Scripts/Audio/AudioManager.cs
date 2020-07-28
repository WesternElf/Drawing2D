using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [System.Serializable]
    public class AudioManager
    {
        [SerializeField] private AudioClip[] _sounds;
        [SerializeField] private AudioClip _defaultClip;
        [SerializeField] private AudioClip _gameMusic;
        [SerializeField] private AudioMixer _mixer;

        private AudioSource _sourceSFX;
        private AudioSource _sourceMusic;
        private float _musicVolume = 1f;                                     
        private float _sfxVolume = 1f;

        public AudioSource SourceMusic { get => _sourceMusic; private set => _sourceMusic = value; }
        public AudioSource SourceSFX { get => _sourceSFX; set => _sourceSFX = value; }

        public float MusicVolume
        {
            get
            {
                return _musicVolume;
            }
            set
            {
                _musicVolume = value;
                SourceMusic.volume = _musicVolume;
            }
        }

        public float SfxVolume
        {
            get
            {
                return _sfxVolume;
            }
            set
            {
                _sfxVolume = value;
                SourceSFX.volume = _sfxVolume;
            }
        }

        public void PlayMusic(bool playState)                                                    
        {
            if (playState)
            {
                SourceMusic.clip = _gameMusic;
            }

            SourceMusic.volume = MusicVolume;
            SourceMusic.loop = true;
            SourceMusic.Play();
        }

    }
}

