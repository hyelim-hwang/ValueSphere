using SS.AppObject;
using Unity.VisualScripting;
using UnityEngine;

namespace SS {
    public class SSGridCameraPerson : SSCameraPerson {
        // constants
        public static readonly Color BG_COLOR =
            new Color(1f, 1f, 1f);
        public static readonly float NEAR = 0.01f; // in meter (1 cm)
        public static readonly float FAR = 10.0f; // in meter (10 m)
        public static readonly Vector3 HOME_EYE =
            new Vector3(0f, 0f, -1f);
        public static readonly Vector3 HOME_VIEW =
            new Vector3(0f, 0f, 1f);
        public static readonly Vector3 HOME_PIVOT =
            new Vector3(0f, 0f, 0f);
        public static readonly float FOV = 60f;

        // fields
        private Vector3 mPivot = Vector3.zero;
        public Vector3 getPivot() {
            return this.mPivot;
        }
        public void setPivot(Vector3 pivot) {
            this.mPivot = pivot;
        }

        // constructor
        public SSGridCameraPerson() : base("3DCameraPerson") {
        }

        protected override void defineInternalCameraParameters() {
            this.mCamera.orthographic = false;
            this.mCamera.orthographicSize = 1.0f;
            this.mCamera.clearFlags = CameraClearFlags.Depth;
            this.mCamera.backgroundColor = SSPerspCameraPerson.BG_COLOR;
            int layer = LayerMask.NameToLayer("grid");
            this.mCamera.cullingMask = 1 << layer; //grid layer only
            this.mCamera.nearClipPlane = SSPerspCameraPerson.NEAR;
            this.mCamera.farClipPlane = SSPerspCameraPerson.FAR;
            this.mCamera.fieldOfView = SSGridCameraPerson.FOV;
            this.mCamera.depth = 1.0f;


        }

        protected override void defineExternalCameraParameters() {
            this.setEye(SSGridCameraPerson.HOME_EYE);
            this.setView(SSGridCameraPerson.HOME_VIEW);
            this.setPivot(SSGridCameraPerson.HOME_PIVOT);
            this.mCameraRig.getGameObject().transform.position =
                new Vector3(-4.09f, 1.47f, -1.22f);
            this.mCameraRig.getGameObject().transform.rotation =
                Quaternion.Euler(0f, 75.3f, 0f);
        }

        public float getOrthographicSize() {
            return this.mCamera.orthographicSize;
        }

        public void setOrthographicSize(float size) {
            this.mCamera.orthographicSize = size;
        }
    }
}