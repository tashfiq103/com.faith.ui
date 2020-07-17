namespace com.faith.ui {

    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using System.Collections;
    using UnityEngine.SceneManagement;

    public class UISceneLoadController : MonoBehaviour
    {

        [Header("Config	:	LoadScene (With Progress Bar")]
        public Image progressionBar;

        [Space(5f)]
        [Header("Config	:	LoadScene (Without Progress Bar")]
        [Range(0f, 5f)]
        public float defaultDelay;
        public UnityEvent OnStartEvent;

        void Start()
        {
            OnStartEvent.Invoke();
        }

        #region Public Callback	:	LoadScene (Without Progress Bar)

        public void LoadThisScene()
        {

            string m_CurrentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(m_CurrentScene, LoadSceneMode.Single);
        }

        public void LoadScene(string m_SceneName)
        {

            SceneManager.LoadScene(m_SceneName, LoadSceneMode.Single);
        }

        public void LoadSceneWithDelay(string m_SceneName)
        {

            StartCoroutine(Delay(m_SceneName, defaultDelay));
        }

        public void LoadSceneWithDelay(string m_SceneName, float m_Delay)
        {
            StartCoroutine(Delay(m_SceneName, m_Delay));
        }

        private IEnumerator Delay(string m_SceneName, float m_Delay)
        {

            yield return new WaitForSeconds(m_Delay);
            LoadScene(m_SceneName);
        }

        #endregion

        //--------------------------------------------------
        #region Public Callback	:	LoadScene (With Progression Bar)

        public void LoadSceneWithProgressBar(string t_SceneName)
        {

            StartCoroutine(ControllerForLoadSceneWithProgressbar(t_SceneName));
        }

        #endregion

        //--------------------------------------------------
        #region Configuretion	:	LoadScene (With Progression Bar)

        private const float LOAD_READY_PERCENTAGE = 0.9f;

        private IEnumerator ControllerForLoadSceneWithProgressbar(string t_SceneName)
        {

            yield return new WaitForSeconds(defaultDelay);

            WaitForSeconds t_CycleDelay = new WaitForSeconds(0.1f);

            AsyncOperation t_AsyncOperationForLoadScene = SceneManager.LoadSceneAsync(t_SceneName);
            t_AsyncOperationForLoadScene.allowSceneActivation = false;

            float t_LoadingProgression;

            while (!t_AsyncOperationForLoadScene.isDone)
            {

                t_LoadingProgression = t_AsyncOperationForLoadScene.progress;

                if (progressionBar != null)
                {
                    progressionBar.fillAmount = t_LoadingProgression / LOAD_READY_PERCENTAGE;
                }

                if (t_LoadingProgression >= LOAD_READY_PERCENTAGE)
                {
                    break;
                }

                yield return t_CycleDelay;
            }

            t_AsyncOperationForLoadScene.allowSceneActivation = true;
            StopCoroutine(ControllerForLoadSceneWithProgressbar(""));
        }

        #endregion
    }

}

