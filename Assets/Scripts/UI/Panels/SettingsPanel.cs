using GameControl;
using LoadSaveData;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Panels
{
    public class SettingsPanel : MonoBehaviour
    {
        
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;


        private void Start()
        {
            _musicSlider.value = GameController.Instance.SoundParams.MusicVolume / 100;
            print(_musicSlider.value);

            _soundSlider.value = GameController.Instance.SoundParams.SoundVolume / 100;
            print(_soundSlider.value);
        }

        public void SetSoundsVolume(float volume)
        {
            GameController.Instance.AudioManager.SfxVolume = volume;
            GameController.Instance.SoundParams.SoundVolume = volume * 100;

            LoadSaveToJSON.SaveParams(GameController.Instance.SoundParams);  
        }

        public void SetMusicVolume(float volume)
        {
            GameController.Instance.AudioManager.MusicVolume = volume;
            GameController.Instance.SoundParams.MusicVolume = volume * 100;

            LoadSaveToJSON.SaveParams(GameController.Instance.SoundParams);

        }

        public void UpdateOptions()
        {
            _soundSlider.value = GameController.Instance.AudioManager.SfxVolume;
            _musicSlider.value = GameController.Instance.AudioManager.MusicVolume;
        }

    }
}

