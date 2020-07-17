namespace com.faith.ui {

    using UnityEngine;

    public class UIPositionTracking : MonoBehaviour
    {

        public bool isContinuesTrackOfPosition;
        public RectTransform UIPosition;
        public Vector3 displacement;

        private Transform m_TransformReference;

        void Start()
        {

            m_TransformReference = transform;

            Reposition();
        }

        private void Update()
        {
            if (isContinuesTrackOfPosition)
            {

                Reposition();
            }
        }

        private void Reposition()
        {
            m_TransformReference.position = new Vector3(
                UIPosition.position.x + displacement.x,
                UIPosition.position.y + displacement.y,
                UIPosition.position.z + displacement.z
            );

        }

    }
}


