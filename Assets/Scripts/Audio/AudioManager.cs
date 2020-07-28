using UnityEngine;

namespace Audio
{
    [System.Serializable]
    public class AudioManager
    {
        [SerializeField] private AudioClip[] _sounds;
        [SerializeField] private AudioClip _defaultClip;
        [SerializeField] private AudioClip _gameMusic;
        [SerializeField] private AudioClip _menuMusic;

        [SerializeField] private AudioSource _sourceSFX;
        [SerializeField] private AudioSource _sourceMusic;
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

        private AudioClip GetSound(string clipName)                                         //поиск звука в массиве
        {
            for (int i = 0; i < _sounds.Length; i++)
            {
                if (_sounds[i].name == clipName)
                {
                    return _sounds[i];
                }
            }
            return _defaultClip;
        }


        public void PlaySound(string clipName)                                             
        {
            SourceSFX.PlayOneShot(GetSound(clipName), SfxVolume);
        }

        public void StopMusic()
        {
            SourceMusic.Pause();
        }

        public void PlayMusic(bool inGame)                                                    
        {
            if (inGame)
            {
                SourceMusic.clip = _gameMusic;
            }
            else if (!inGame)
            {
                SourceMusic.clip = _menuMusic;
            }
            SourceMusic.volume = MusicVolume;
            SourceMusic.loop = true;
            SourceMusic.Play();
        }

    }
}

