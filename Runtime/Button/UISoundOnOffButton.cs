namespace com.faith.ui
{
    using UnityEngine;
    using UnityEngine.UI;

    using com.faith.gameplay.service;

    public class UISoundOnOffButton : MonoBehaviour
    {

        #region Public Variables

        public Sprite spriteForSoundEnabled;
        public Sprite spriteForSoundDisabled;

        public Animator buttonAnimator;
        public Image imageReference;
        public Button buttonReference;

        #endregion

        #region Private Variables

        private bool m_IsSoundMuted;

        #endregion

        #region Mono Behaviour

        private void Awake()
        {
            buttonReference.onClick.AddListener(delegate
            {
                //PlaySound : For Button Pressed
                //AudioController.Instance.PlaySoundFXForButtonSelect();

                buttonAnimator.SetTrigger("PRESSED");
                ToggleSoundEnabledAndDisabled();
            });
        }

        private void Start()
        {
            if (!AudioManager.Instance.IsMusicEnabled() || !AudioManager.Instance.IsSoundFXEnabled())
            {
                m_IsSoundMuted = true;
                AudioManager.Instance.DisableMusic();
                AudioManager.Instance.DisableSoundFX();
            }
            else
                m_IsSoundMuted = false;

            MakeVisualChangeOfSoundStatus();
        }

        #endregion

        #region Configuretion

        private void MakeVisualChangeOfSoundStatus()
        {

            if (m_IsSoundMuted)
            {
                imageReference.sprite = spriteForSoundDisabled;
            }
            else
            {
                imageReference.sprite = spriteForSoundEnabled;
            }
        }

        #endregion

        #region Public Callback

        public void ToggleSoundEnabledAndDisabled()
        {

            m_IsSoundMuted = !m_IsSoundMuted;
            MakeVisualChangeOfSoundStatus();
            if (m_IsSoundMuted)
            {
                AudioManager.Instance.DisableMusic();
                AudioManager.Instance.DisableSoundFX();
            }
            else
            {
                AudioManager.Instance.EnableMusic();
                AudioManager.Instance.EnableSoundFX();

                //PlaySound : For The CurrentBackground
                //AudioController.Instance.PlayBackgroundMusicForMainMenu();
            }
        }

        #endregion

    }
}


