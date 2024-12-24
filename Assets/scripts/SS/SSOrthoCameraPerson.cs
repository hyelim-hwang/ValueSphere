using UnityEngine;

namespace SS {
    public class SSOrthoCameraPerson : SSCameraPerson {
        public static readonly float NEAR = 0.1f; // in meter(10cm)
        public static readonly float FAR = 2f;
        public static readonly float SCREEN_CAMERA_DIST = 1f;

        //fields
        private float mScreenWidth = float.NaN;
        private float mScreenHeight = float.NaN;

        //constructor
        public SSOrthoCameraPerson() : base("OrthoCameraPerson") {}

        protected override void defineInternalCameraParameters() {
            this.mCamera.orthographic = true;
            this.mCamera.orthographicSize = 1.0f;
            this.mCamera.depth = 1f; //rendering order
            this.mCamera.clearFlags = CameraClearFlags.Depth; //depth buffer
            this.mCamera.cullingMask = 32; //100000. UI layer only

            this.mCamera.nearClipPlane = SSOrthoCameraPerson.NEAR;
            this.mCamera.farClipPlane = SSOrthoCameraPerson.FAR;

            // for VR compatibility
            this.mCamera.stereoTargetEye = StereoTargetEyeMask.None;
        }

        protected override void defineExternalCameraParameters() {
            this.update();
        }

        public void update() {
            if (Screen.width != this.mScreenWidth || Screen.height !=
                this.mScreenHeight) {
                //update the screen size
                this.mScreenWidth = Screen.width;
                this.mScreenHeight = Screen.height;

                //update the screen camera
                this.mCameraRig.getGameObject().transform.position =
                    new Vector3(this.mScreenWidth / 2f,
                    this.mScreenHeight / 2f,
                    -SSOrthoCameraPerson.SCREEN_CAMERA_DIST);
                this.mCamera.orthographicSize = this.mScreenHeight / 2f;
            }
        }
    }
}