using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.Geom;
using SS.Scenario;
using SSAppObject;
using UnityEngine.UIElements;

namespace SS.AppObject {
    public class SSValueSphere : SSAppGeom3D {
        //constants
        public static readonly float POLE_LENGTH_FACTOR = 0.7f;

        //fields
        private Color mColor = Color.white;
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        public Color getColor() {
            return this.mColor;
        }
        //sphere
        private GameObject mSphere = null;
        public void setSphere(GameObject sphere) {
            this.mSphere = sphere;
            this.refreshRenderer();
        }
        public GameObject getSphere() {
            return this.mSphere;
        }
        private float mRadius = 0.0f;
        public float getRadius() {
            return this.mRadius;
        }
        public void setRadius(float r) {
            this.mRadius = r;
            this.refreshAtGeomChange();
        }
        private SSEquator mEquator = null;
        public SSEquator getEquator() {
            return this.mEquator;
        }

        //pole
        private SSAppPolyline3D mPole = null;
        public SSAppPolyline3D getPole() {
            return this.mPole;
        }

        //equator
        public void setEquator(float radius, Vector3 pos, Quaternion rot) {
            this.mEquator.setGeom(new SSCircle3D(radius, pos, rot));
            this.mEquator.refreshAtGeomChange();
        }
        public void setEquator(SSEquator equator) {
            this.mEquator = equator;
            this.mEquator.refreshAtGeomChange();
        }
        public void setEquator(SSCircle3D circle3D) {
            this.mEquator.setGeom(circle3D);
            this.mEquator.refreshAtGeomChange();
        }

        private Quaternion mRot = Quaternion.identity;
        public void setRot(Quaternion rot) {
            this.mRot = rot;
            this.refreshRenderer();
        }
        public Quaternion getRot() {
            return this.mRot;
        }

        //constructor
        public SSValueSphere(string name, float radius, Color color) :
            base($"{name}/ValueSphere") {
            GameObject sphere3D =
            GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere3D.GetComponent<Renderer>().material.SetOverrideTag(
                "RenderType", "Transparent");
            sphere3D.transform.position = new Vector3(0, 0, 0);
            sphere3D.transform.localScale = new Vector3(0, 0, 0);
            sphere3D.transform.parent = this.mGameObject.transform;
            List<Vector3> dummyPts = new List<Vector3>();
            dummyPts.Add(Vector3.zero);
            dummyPts.Add(Vector3.zero);
            this.mColor = color;
            this.mSphere = sphere3D;
            this.mEquator = new SSEquator("eqator", 0.005f, Color.red, this);
            this.mPole = new SSAppPolyline3D("pole", dummyPts,
                SSEquator.DEFAULT_WIDTH, SSEquator.DEFAULT_COLOR);
            this.mRot = Quaternion.Euler(90, 0, 0);
            this.refreshAtGeomChange();
        }

        //methods
        protected override void addComponents() {
            this.mGameObject.AddComponent<SphereCollider>();
            this.mGameObject.AddComponent<MeshRenderer>();
            this.mGameObject.AddComponent<MeshFilter>();
        }

        protected override void refreshCollider() {
            SphereCollider cc = this.mSphere.GetComponent<SphereCollider>();
            cc.radius = this.mRadius;
        }

        protected override void refreshRenderer() {
            MeshFilter mf = this.mSphere.GetComponent<MeshFilter>();
            MeshRenderer mr = this.mSphere.GetComponent<MeshRenderer>();
            Vector3 scaleChange =
            new Vector3(this.mRadius, this.mRadius, this.mRadius);
            this.mSphere.transform.localScale = scaleChange;
            this.mEquator.refreshAtGeomChange();
            this.updatePole();
        }

        public void setPos(Vector3 pos) {
            this.getGameObject().transform.position = pos;
            this.refreshAtGeomChange();
        }

        public void updatePole() {
            Vector3 sphereCenter = this.mSphere.transform.position;
            this.mGeom = new SSCircle3D(this.getRadius() / 1.98f,
                this.getGameObject().transform.position, this.getRot());
            List<Vector3> eqPts = ((SSCircle3D)this.mGeom).calcPts(50);
            Vector3 orthoUp =
                Vector3.Cross((eqPts[0] - sphereCenter).normalized,
                (eqPts[12] - sphereCenter).normalized);
            List<Vector3> polePts =
                new List<Vector3> {
                    sphereCenter - orthoUp * SSValueSphere.POLE_LENGTH_FACTOR *
                        this.mRadius,
                    sphereCenter + orthoUp * SSValueSphere.POLE_LENGTH_FACTOR *
                        this.mRadius};
            this.mPole.setPts(polePts);
        }
    }
}