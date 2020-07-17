namespace com.faith.ui
{

    using UnityEngine;
    using UnityEngine.UI;

    public class UISettingsController : MonoBehaviour
    {

        #region Public Variables

        public Animator animatorReference;
        public Animator animatorReferenceForSoundFX;
        public Animator animatorReferenceForMusicFX;
        public Animator animatorReferenceForHapticFeedback;

        public Button settingButtonReference;

        #endregion

        #region Private Variables

        private bool m_ShowingSettingsPanel = false;

        #endregion

        #region Mono Behaviour

        private void Awake()
        {
            settingButtonReference.onClick.AddListener(delegate
            {

                //PlaySound : For Button Pressed
                //AudioController.Instance.PlaySoundFXForButtonSelect();
                animatorReference.SetTrigger("PRESSED");

                if (!m_ShowingSettingsPanel)
                {
                    animatorReferenceForMusicFX.SetTrigger("APPEAR");
                    animatorReferenceForSoundFX.SetTrigger("APPEAR");
                    animatorReferenceForHapticFeedback.SetTrigger("APPEAR");
                }
                else
                {

                    animatorReferenceForMusicFX.SetTrigger("DISAPPEAR");
                    animatorReferenceForSoundFX.SetTrigger("DISAPPEAR");
                    animatorReferenceForHapticFeedback.SetTrigger("DISAPPEAR");
                }

                m_ShowingSettingsPanel = !m_ShowingSettingsPanel;

            });

        }

        #endregion

        #region Public Callback

        public void AppearSettingsButton()
        {

            animatorReference.SetTrigger("APPEAR");
        }

        public void DisappearSettingsButton()
        {

            animatorReference.SetTrigger("DISAPPEAR");
        }

        public void ShowSettings()
        {

            if (!m_ShowingSettingsPanel)
            {

                m_ShowingSettingsPanel = true;
                animatorReferenceForMusicFX.SetTrigger("APPEAR");
                animatorReferenceForSoundFX.SetTrigger("APPEAR");
                animatorReferenceForHapticFeedback.SetTrigger("APPEAR");
            }
        }

        public void HideSettings()
        {

            if (m_ShowingSettingsPanel)
            {

                m_ShowingSettingsPanel = false;
                animatorReferenceForMusicFX.SetTrigger("DISAPPEAR");
                animatorReferenceForSoundFX.SetTrigger("DISAPPEAR");
                animatorReferenceForHapticFeedback.SetTrigger("DISAPPEAR");
            }
        }

        #endregion

    }
}


