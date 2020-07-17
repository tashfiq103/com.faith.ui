namespace com.faith.ui
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "SpriteAssetBundle", menuName = "Info Container/Create SpriteAssetBundle")]
    public class UIManagerOfSpriteBundleForCrossPlatform : ScriptableObject
    {
        #region Custom Variables

        [System.Serializable]
        public struct SpriteBundle
        {
            public string bundleName;

            [Space(5.0f)]
            public Sprite iPhone8;
            public Sprite iPadPro;
            public Sprite iPhoneXSMax;
        }

        #endregion

        #region Public Variables

        public SpriteBundle[] spriteBundle;

        #endregion
    }
}

