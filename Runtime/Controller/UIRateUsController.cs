namespace com.faith.ui
{
    using UnityEngine;
    using UnityEngine.UI;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

    public class UIRateUsController : MonoBehaviour
    {

        #region Publoc Variables

        public static UIRateUsController Instance;

        [Header("URL")]
        public string playStoreURL;
        public string appStoreURL;

        [Space(5.0f)]
        [Header("Reference")]
        public Button cancelButton;
        public Button rateUsButtonReference;
        public Animator rateUsAnimatorReference;

        public int[] askForReviewAtLevels;

        #endregion

        #region Private Variables

        private string IS_USER_REVIEW_ASKED_AT_LEVEL_X = "IS_USER_REVIEW_ASKED_AT_LEVEL_";

        #endregion

        #region Mono Behaviour

        void Start()
        {

            Instance = this;


            if (cancelButton != null) {

                cancelButton.onClick.AddListener(delegate
                {
                    rateUsAnimatorReference.SetTrigger("DISAPPEAR");
                });
            }

            if (rateUsButtonReference != null) {

                rateUsButtonReference.onClick.AddListener(delegate
                {

                    rateUsAnimatorReference.SetTrigger("DISAPPEAR");

                    string t_ValidURL;

#if UNITY_ANDROID

                t_ValidURL = playStoreURL;

#elif UNITY_IOS

                    t_ValidURL = appStoreURL;

#else

            t_ValidURL = "www.faithstudio.org";

#endif

                    Application.OpenURL(t_ValidURL);
                });
            }

            
        }

        #endregion

        #region Configuretion

        private bool IsAllowToAskForReview(int t_CurrentLevel) {

            bool t_Result = false;
            int t_NumberOfReviewPoints = askForReviewAtLevels.Length;
            for (int reviewIndex = 0; reviewIndex < t_NumberOfReviewPoints; reviewIndex++) {

                string t_PreferenceKey = IS_USER_REVIEW_ASKED_AT_LEVEL_X + askForReviewAtLevels[reviewIndex].ToString();
                if (t_CurrentLevel >= askForReviewAtLevels[reviewIndex] && PlayerPrefs.GetInt(t_PreferenceKey, 0) == 0) {

                    PlayerPrefs.SetInt(t_PreferenceKey, 1);
                    t_Result = true;
                    break;
                }
            }

            return t_Result;
        }

        #endregion

        #region Public Callback

        public void AskUserForReview(int t_CurrentLevel)
        {

            if (IsAllowToAskForReview(t_CurrentLevel))
            {

#if UNITY_IOS
                Device.RequestStoreReview();
#else
                Debug.Log("Failed To request user review : Not iOS Platform");
#endif
            }
        }

        public void AskForCustomUserReview(int t_CurrentLevel) {

            if (IsAllowToAskForReview(t_CurrentLevel)) {

                rateUsAnimatorReference.SetTrigger("APPEAR");
            }
        }

        #endregion

    }
}


