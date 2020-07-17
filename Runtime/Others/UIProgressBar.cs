namespace com.faith.ui {
    
    using System.Collections;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using UnityEngine;

    public class UIProgressBar : MonoBehaviour {
        #region Public Variables

        public Camera mainCamera;
        public Animator animatorReference;
        public Image progressbar;

        [Space (5.0f)]
        public Gradient defaultColor = new Gradient () {
            colorKeys = new GradientColorKey[] { new GradientColorKey (Color.white, 0f), new GradientColorKey (Color.white, 1f) },
            alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey (1f, 0f), new GradientAlphaKey (1f, 1f) }
        };
        public Gradient defaultColorWhenIncreasing = new Gradient () {
            colorKeys = new GradientColorKey[] { new GradientColorKey (Color.green, 0f), new GradientColorKey (Color.green, 1f) },
            alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey (1f, 0f), new GradientAlphaKey (1f, 1f) }
        };
        public Gradient defaultColorWhenDecreasing = new Gradient () {
            colorKeys = new GradientColorKey[] { new GradientColorKey (Color.red, 0f), new GradientColorKey (Color.red, 1f) },
            alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey (1f, 0f), new GradientAlphaKey (1f, 1f) }
        };
        [Range (0f, 1f)]
        public float defaultSpeedForProgressbar = 0.1f;

        #endregion

        #region Private Variables

        private float m_ErrorRateOnLerp = 0.01f;

        private bool m_IsProgressbarControllerRunning;
        private float m_CurrentSpeedForProgressbar;
        private float m_TargetedValueOfProgressbar;
        private float m_CurrentValueOfProgressbar;
        
        private Transform m_TransformReference;
        private Transform m_TransformReferenceOfMainCamera;
        private Transform m_TransformReferenceOfFollowingObject;
        private Vector3 m_Offset;

        private Gradient m_Color;
        private Gradient m_ColorWhenIncreasing;
        private Gradient m_ColorWhenDecreasing;

        private UnityAction OnProgressbarReachItsDestination;
        private UnityAction<float> OnUpgradingProgressbar;
        #endregion

        #region Mono Behaviour

        private void Awake () {

            m_TargetedValueOfProgressbar = 0f;
            
            m_TransformReference = transform;

            if(mainCamera == null)
                mainCamera = Camera.main;

            m_TransformReferenceOfMainCamera = mainCamera.transform;

            if(animatorReference == null)
                Debug.LogError("Progressbar 'Animator' is not assigned");
        }

        #endregion

        #region Configuretion

        private IEnumerator ControllerForProgressbar () {

            float t_CycleLength = 0.033f;
            WaitForSecondsRealtime t_CycleDelay = new WaitForSecondsRealtime (t_CycleLength);
            while (m_IsProgressbarControllerRunning) {

                if(m_TransformReferenceOfFollowingObject != null){

                    Vector3 t_ModifiedPosition      = m_TransformReferenceOfFollowingObject.position + m_Offset;
                    m_TransformReference.position   = t_ModifiedPosition;

                    Quaternion t_ModifiedRotation = Quaternion.LookRotation(m_TransformReferenceOfMainCamera.position - m_TransformReference.position);
                    m_TransformReference.rotation = t_ModifiedRotation;
                }

                m_CurrentValueOfProgressbar = Mathf.Lerp (
                    m_CurrentValueOfProgressbar,
                    m_TargetedValueOfProgressbar,
                    m_CurrentSpeedForProgressbar);

                if (Mathf.Abs (m_TargetedValueOfProgressbar - m_CurrentValueOfProgressbar) <= (m_TargetedValueOfProgressbar * m_ErrorRateOnLerp)) {
                    m_CurrentValueOfProgressbar = m_TargetedValueOfProgressbar;
                    OnProgressbarReachItsDestination?.Invoke ();

                    if(m_CurrentValueOfProgressbar == 1)
                        animatorReference.SetTrigger("DISAPPEAR");

                    m_IsProgressbarControllerRunning = false;
                }

                if (m_CurrentValueOfProgressbar > m_TargetedValueOfProgressbar)
                    progressbar.color = m_ColorWhenDecreasing.Evaluate (m_CurrentValueOfProgressbar);
                else if (m_CurrentValueOfProgressbar < m_TargetedValueOfProgressbar)
                    progressbar.color = m_ColorWhenIncreasing.Evaluate (m_CurrentValueOfProgressbar);
                else
                    progressbar.color = m_Color.Evaluate (m_CurrentValueOfProgressbar);

                progressbar.fillAmount = m_CurrentValueOfProgressbar;

                OnUpgradingProgressbar?.Invoke (m_CurrentValueOfProgressbar);

                yield return t_CycleDelay;
            }

            StopCoroutine (ControllerForProgressbar ());
        }

        #endregion

        #region Public Callback

        public void SetInitialValue(float t_InitialValue = 0){

            progressbar.fillAmount = t_InitialValue;
            m_CurrentValueOfProgressbar = t_InitialValue;
            m_TargetedValueOfProgressbar = t_InitialValue;
        }

        public void ShowProgressbar () {

            animatorReference.SetTrigger("APPEAR");
        }

        public void HideProgressbar(){

            animatorReference.SetTrigger("DISAPPEAR");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_ProgressbarValue"> Value must be within [0,1] </param>
        public void UpdateProgressBar (
            float t_ProgressbarValue,
            UnityAction OnProgressbarReachItsDestination = null,
            UnityAction<float> OnUpgradingProgressbar = null) {

            UpdateProgressBar (
                null,
                Vector3.zero,
                t_ProgressbarValue,
                0,
                null,
                null,
                null,
                OnProgressbarReachItsDestination,
                OnUpgradingProgressbar
            );
        }

        public void UpdateProgressBarWithFollowingObject(
            Transform t_FollowingObject,
            Vector3 t_Offset,
            float t_ProgressbarValue,
            UnityAction OnProgressbarReachItsDestination = null,
            UnityAction<float> OnUpgradingProgressbar = null
        ){

            UpdateProgressBar(
                t_FollowingObject,
                t_Offset,
                t_ProgressbarValue,
                0,
                null,
                null,
                null,
                OnProgressbarReachItsDestination,
                OnUpgradingProgressbar
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_ProgressbarValue"> Value must be within [0,1] </param>
        public void UpdateProgressBar (
            Transform t_FollowingObject,
            Vector3 t_Offset,
            float t_ProgressbarValue,
            float t_VelocityOfProgressbar = 0,
            Gradient t_Color = null,
            Gradient t_ColorWhenIncreasing = null,
            Gradient t_ColorWhenDecreasing = null,
            UnityAction OnProgressbarReachItsDestination = null,
            UnityAction<float> OnUpgradingProgressbar = null) {
            
            m_TransformReferenceOfFollowingObject = t_FollowingObject;

            m_Offset = t_Offset;

            t_ProgressbarValue = Mathf.Clamp01 (t_ProgressbarValue);

            m_TargetedValueOfProgressbar = t_ProgressbarValue;

            if (t_VelocityOfProgressbar == 0)
                m_CurrentSpeedForProgressbar = defaultSpeedForProgressbar;
            else
                m_CurrentSpeedForProgressbar = t_VelocityOfProgressbar;

            if (t_Color == null)
                m_Color = defaultColor;
            else
                m_Color = t_Color;

            if (t_ColorWhenIncreasing == null)
                m_ColorWhenIncreasing = defaultColorWhenIncreasing;
            else
                m_ColorWhenDecreasing = t_ColorWhenIncreasing;

            if (t_ColorWhenDecreasing == null)
                m_ColorWhenDecreasing = defaultColorWhenDecreasing;
            else
                m_ColorWhenDecreasing = t_ColorWhenDecreasing;

            this.OnUpgradingProgressbar = OnUpgradingProgressbar;
            this.OnProgressbarReachItsDestination = OnProgressbarReachItsDestination;

            if (!m_IsProgressbarControllerRunning) {

                m_IsProgressbarControllerRunning = true;
                StartCoroutine (ControllerForProgressbar ());
            }else{

                animatorReference.SetTrigger("UPDATE");
            }
        }

        #endregion
    }

}