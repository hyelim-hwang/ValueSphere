using UnityEngine;

namespace SS {
    public class SSPerspCameraPerson : SSCameraPerson {
        // constants
        public static readonly Color BG_COLOR =
            new Color(1f, 1f, 1f);
        public static readonly float NEAR = 0.01f; // in meter (1 cm)
        public static readonly float FAR = 100.0f; // in meter (100 m)
        public static readonly Vector3 HOME_EYE =
            new Vector3(0f, 0f, -1f);
        public static readonly Vector3 HOME_VIEW =
            new Vector3(0f, 0f, 1f);
        public static readonly Vector3 HOME_PIVOT =
            new Vector3(0.0f, 0.0f, 0.0f);

        // fields
        private Vector3 mPivot = Vector3.zero;
        public Vector3 getPivot() {
            return this.mPivot;
        }
        public void setPivot(Vector3 pivot) {
            this.mPivot = pivot;
        }

        // constructor
        public SSPerspCameraPerson() : base("3DCameraPerson") {}

        protected override void defineInternalCameraParameters() {
            this.mCamera.orthographic = true;
            this.mCamera.orthographicSize = 1.0f;
            this.mCamera.clearFlags = CameraClearFlags.Color;
            this.mCamera.backgroundColor = SSPerspCameraPerson.BG_COLOR;
            this.mCamera.cullingMask = 1; // default layer only

            this.mCamera.nearClipPlane = SSPerspCameraPerson.NEAR;
            this.mCamera.farClipPlane = SSPerspCameraPerson.FAR;
        }

        protected override void defineExternalCameraParameters() {
            this.setEye(SSPerspCameraPerson.HOME_EYE);
            this.setView(SSPerspCameraPerson.HOME_VIEW);
            this.setPivot(SSPerspCameraPerson.HOME_PIVOT);
        }

        public float getOrthographicSize() {
            return this.mCamera.orthographicSize;
        }

        public void setOrthographicSize(float size) {
            this.mCamera.orthographicSize = size;
        }
    }
}