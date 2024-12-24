using UnityEngine;

namespace SS {
    public class SSLightSourceMgr {
        // constants

        //fields
        private SSApp mSS = null;
        private GameObject mLightGameObject = null;
        private Quaternion mDirection = Quaternion.identity;
        public Quaternion getDirection() {
            return this.mDirection;
        }
        public void setDirection(Vector3 lightPos) {
            SSValueSphereMgr valueSphereMgr = this.mSS.getValueSphereMgr();
            Vector3 sphereCenter =
                valueSphereMgr.getValueSphere().getSphere().transform.position;
            Quaternion rotation =
                Quaternion.LookRotation(sphereCenter - lightPos);
            this.mDirection = rotation;
            refreshLightCondition();
        }
        private Vector3 mLightSourcePos = SSUtil.VECTOR3_NAN;
        public Vector3 getLightSourcePos() {
            return this.mLightSourcePos;
        }
        public void setLightSourcePos(Vector3 pos) {
            this.mLightSourcePos = pos;
            refreshLightCondition();
        }
        private Color mColor = Color.white;
        public Color GetColor() {
            return this.mColor;
        }
        public void setColor(Color c) {
            this.mColor = c;
        }

        //constructor
        public SSLightSourceMgr(SSApp ss) {
            this.mSS = ss;
            // Make a game object
            this.mLightGameObject = new GameObject("The Light");

            // Add the light component
            Light lightComp = this.mLightGameObject.AddComponent<Light>();

            // Set color and position
            lightComp.color = Color.white;
            lightComp.type = LightType.Directional;
            lightComp.intensity = 1f;

            // Set the position (or any transform property)
            this.mLightGameObject.transform.position =
                new Vector3(-4.36f, 7.31f, 7.54f);
            SSValueSphereMgr valueSphereMgr = ss.getValueSphereMgr();
            Vector3 sphereCenter =
                valueSphereMgr.getValueSphere().getSphere().transform.position;
            Quaternion rotation =
                Quaternion.LookRotation(
                sphereCenter - new Vector3(-4.36f, 7.31f, 7.54f));
            this.mDirection = rotation;
            this.mColor = Color.white;
            this.mLightSourcePos = new Vector3(-4.36f, 7.31f, 7.54f);
        }

        //methods
        public void refreshLightCondition() {
            this.mLightGameObject.transform.position = this.mLightSourcePos;
            this.mLightGameObject.transform.rotation = this.mDirection;
        }
    }
}
