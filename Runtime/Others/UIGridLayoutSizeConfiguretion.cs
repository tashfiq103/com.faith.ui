namespace com.faith.ui
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UIGridLayoutSizeConfiguretion : MonoBehaviour
    {
        #region Custom Variables

        public struct DeviceInfo
        {

            public float aspectRatio;

            [Space(10.0f)]
            [Range(0.0f, 1.0f)]
            public float cellHeight;
            [Range(0.0f, 1.0f)]
            public float cellWidth;

            [Space(5.0f)]
            public float cellSpacing;
        }

        #endregion

        //--------------------
        #region Public Variables

        public bool enableResize;

        [Space(10.0f)]
        [Range(0.0f, 1.0f)]
        public float defaultCellHeight;
        [Range(0.0f, 1.0f)]
        public float defaultCellWidth;
        [Range(0.0f, 1.0f)]
        public float defaultCellSpacing;

        [Space(10.0f)]
        public DeviceInfo[] dynamicGridSize;

        #endregion

        //--------------------
        #region Private Variables

        private float m_CellHeight;
        private float m_CellWidth;
        private float m_Spacing;

        private RectTransform m_RectTransformReference;
        private GridLayoutGroup m_GridLayoutGroupReference;

        #endregion

        //--------------------
        #region

        private void Awake()
        {
            m_RectTransformReference = gameObject.GetComponent<RectTransform>();
            m_GridLayoutGroupReference = gameObject.GetComponent<GridLayoutGroup>();

            float t_ScreeHeight = Screen.height;
            float t_ScreenWidth = Screen.width;


            if (enableResize)
            {
                //StartCoroutine(ControllerForResizingGrid());
            }
        }

        #endregion

        #region Configuretion

        private void AssignedGridLayoutSize()
        {

        }

        #endregion
    }

}

