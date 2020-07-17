namespace com.faith.ui
{

    using UnityEngine;
    using UnityEngine.UI;

    using com.faith.gameplay.service;

    public class UICrossPlatformController : MonoBehaviour
    {

        #region Custom Variables

        [System.Serializable]
        public struct CrossPlatformAspectRatioFittering
        {
            public AspectRatioFitter referenceOfAspectRationFittering;
            public AspectRatioFitter.AspectMode aspectModeFor_iPhone;
            public AspectRatioFitter.AspectMode aspectModeFor_iPhoneXS;
            public AspectRatioFitter.AspectMode aspectModeFor_iPad;
        }

        #endregion

        #region Public Variables

        public static UICrossPlatformController Instance;

        public UIManagerOfSpriteBundleForCrossPlatform dataContainerOfSpriteBundle;

        public CrossPlatformAspectRatioFittering[] crossPlatformAspectRationFittering;

        #endregion

        #region Mono Behaviour

        private void Awake()
        {
            if (Instance == null)
            {

                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {

#if UNITY_IOS || UNITY_EDITOR
            int t_NumberOfCPARF = crossPlatformAspectRationFittering.Length;
            for (int index = 0; index < t_NumberOfCPARF; index++)
            {

                if (DeviceInfoManager.Instance.IsDevice_iPad())
                {
                    crossPlatformAspectRationFittering[index].referenceOfAspectRationFittering.aspectMode = crossPlatformAspectRationFittering[index].aspectModeFor_iPad;
                }
                else if (DeviceInfoManager.Instance.IsDevice_iPhoneX())
                {
                    crossPlatformAspectRationFittering[index].referenceOfAspectRationFittering.aspectMode = crossPlatformAspectRationFittering[index].aspectModeFor_iPhoneXS;
                }
                else if (DeviceInfoManager.Instance.IsDevice_iPhone())
                {

                    crossPlatformAspectRationFittering[index].referenceOfAspectRationFittering.aspectMode = crossPlatformAspectRationFittering[index].aspectModeFor_iPhone;
                }
            }
#endif

        }

        #endregion

        #region Configuretion

        private bool IsValidSpriteBundleId(int t_SpriteBundleId)
        {

            if (t_SpriteBundleId >= 0 && t_SpriteBundleId < dataContainerOfSpriteBundle.spriteBundle.Length)
            {
                return true;
            }

            Debug.LogError("Invalid SpriteBundleId : " + t_SpriteBundleId);
            return false;
        }

        private Sprite GetSpriteBundle(int t_SpriteBundleId)
        {

            if (DeviceInfoManager.Instance.IsDevice_iPhone())
            {
                return dataContainerOfSpriteBundle.spriteBundle[t_SpriteBundleId].iPhone8;
            }
            else if (DeviceInfoManager.Instance.IsDevice_iPhoneX())
            {
                return dataContainerOfSpriteBundle.spriteBundle[t_SpriteBundleId].iPhoneXSMax;
            }
            else if (DeviceInfoManager.Instance.IsDevice_iPad())
            {
                return dataContainerOfSpriteBundle.spriteBundle[t_SpriteBundleId].iPadPro;
            }

            Debug.LogError("Not an apple device");
            return dataContainerOfSpriteBundle.spriteBundle[t_SpriteBundleId].iPhone8;
        }

        #endregion

        #region Public Callback

        public Sprite GetSprite(string t_SpriteBundleId)
        {

            int t_NumberOfSpriteBundle = dataContainerOfSpriteBundle.spriteBundle.Length;
            for (int bundleIndex = 0; bundleIndex < t_NumberOfSpriteBundle; bundleIndex++)
            {
                if (dataContainerOfSpriteBundle.spriteBundle[bundleIndex].bundleName == t_SpriteBundleId)
                {
                    return GetSpriteBundle(bundleIndex);
                }
            }

            Debug.LogError("Invalid SpriteBundleId : " + t_SpriteBundleId);
            return null;
        }

        public Sprite GetSprite(int t_SpriteBundleId)
        {
            if (IsValidSpriteBundleId(t_SpriteBundleId))
            {

                return GetSpriteBundle(t_SpriteBundleId);
            }

            return null;
        }

        #endregion
    }

}
