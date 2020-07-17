
namespace com.faith.ui {

    using UnityEngine;
    using UnityEngine.Events;

    public class UIToggleButton : MonoBehaviour
    {

        public UnityEvent OnFirstPressed;
        public UnityEvent OnSecondPressed;

        private bool m_OnFirstPressed = true;
        private bool m_OnInteractionEnabled = true;

        public void ToggleInteractionConfig()
        {

            m_OnInteractionEnabled = !m_OnInteractionEnabled;
        }

        public void ToggleButton()
        {

            if (m_OnInteractionEnabled)
            {

                if (m_OnFirstPressed)
                {
                    OnFirstPressed.Invoke();
                }
                else
                {
                    OnSecondPressed.Invoke();
                }

                m_OnFirstPressed = !m_OnFirstPressed;
            }
        }
    }
}

