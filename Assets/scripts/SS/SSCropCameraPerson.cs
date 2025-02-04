using SS.AppObject;
using Unity.VisualScripting;
using UnityEngine;

namespace SS {
    public class SSCropCameraPerson : SSCameraPerson {
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
        private SSApp mApp = null;
        private Vector3 mPivot = Vector3.zero;
        public Vector3 getPivot() {
            return this.mPivot;
        }
        public void setPivot(Vector3 pivot) {
            this.mPivot = pivot;
        }

        // constructor
        public SSCropCameraPerson(SSApp app) : base("3DCameraPerson") {
            this.mApp = app;
            //this.cropPerspCamImage();
        }

        protected override void defineInternalCameraParameters() {
            this.mCamera.orthographic = false;
            this.mCamera.orthographicSize = 1.0f;
            this.mCamera.clearFlags = CameraClearFlags.SolidColor;
            this.mCamera.backgroundColor = SSPerspCameraPerson.BG_COLOR;
            int layer = LayerMask.NameToLayer("grid");
            this.mCamera.cullingMask = 1 << layer; //grid layer only
            this.mCamera.nearClipPlane = SSPerspCameraPerson.NEAR;
            this.mCamera.farClipPlane = SSPerspCameraPerson.FAR;
        }

        protected override void defineExternalCameraParameters() {
            this.setEye(SSCropCameraPerson.HOME_EYE);
            this.setView(SSCropCameraPerson.HOME_VIEW);
            this.setPivot(SSCropCameraPerson.HOME_PIVOT);
            this.mCamera.fieldOfView = SSCropCameraPerson.FOV;
            this.mCameraRig.getGameObject().transform.position =
                new Vector3(-3.27999997f, 1.10000002f, 0.610000014f);
            this.mCameraRig.getGameObject().transform.rotation =
                Quaternion.Euler(0f, 110.889992f, 0);
        }

        public float getOrthographicSize() {
            return this.mCamera.orthographicSize;
        }

        public void setOrthographicSize(float size) {
            this.mCamera.orthographicSize = size;
        }

        public void cropPerspCamImage() {
            SSApp SS = (SSApp)this.mApp;
            Vector3 eye = this.getEye();
            Camera gridCam = SS.getGridCameraPerson().getCamera();

            float gpw = 1.5f;
            float gph = 1;
            Vector3 gpo = Vector3.zero;
            Vector3 gpz = Vector3.forward;
            Vector3 gpy = Vector3.up;
            Vector3 gpx = Vector3.right;

            Vector3 eyeToGlassPanelCenter = gpo - eye;
            Vector3 eyeToFullImageCenter =
                Vector3.Dot(eyeToGlassPanelCenter, gpz) * gpz;
            Vector3 fullImageCenter = eye + eyeToFullImageCenter;

            // calculate distance from full image center to each edge of the
            // glass panel to determine the view frustum of persp cam
            Vector3 offset = gpo - fullImageCenter;
            float l = Vector3.Dot(offset, gpx) - 0.5f * gpw;
            float r = Vector3.Dot(offset, gpx) + 0.5f * gpw;
            float b = Vector3.Dot(offset, gpy) - 0.5f * gph;
            float t = Vector3.Dot(offset, gpy) + 0.5f * gph;

            gridCam.projectionMatrix = this.calcCroppedProjectionMat(l, r, b, t,
                eyeToFullImageCenter.magnitude, gridCam.farClipPlane);
            // SS.getCropCameraPerson().copyPerspCameraParameters(
            //     SS.getPerspCameraPerson());
        }

        private Matrix4x4 calcCroppedProjectionMat(float left, float right,
            float bottom, float top, float near, float far) {

            float x = 2.0F * near / (right - left);
            float y = 2.0F * near / (top - bottom);
            float a = (right + left) / (right - left);
            float b = (top + bottom) / (top - bottom);
            float c = -(far + near) / (far - near);
            float d = -(2.0F * far * near) / (far - near);
            float e = -1.0F;
            Matrix4x4 m = new Matrix4x4();
            m[0, 0] = x;
            m[0, 1] = 0;
            m[0, 2] = a;
            m[0, 3] = 0;
            m[1, 0] = 0;
            m[1, 1] = y;
            m[1, 2] = b;
            m[1, 3] = 0;
            m[2, 0] = 0;
            m[2, 1] = 0;
            m[2, 2] = c;
            m[2, 3] = d;
            m[3, 0] = 0;
            m[3, 1] = 0;
            m[3, 2] = e;
            m[3, 3] = 0;
            return m;
        }
    }
}