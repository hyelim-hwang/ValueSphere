using System.Collections.Generic;
using SS.Geom;
using UnityEngine;

namespace SS.AppObject {
    public class SSAppTrapezoid3D : SSAppGeom3D {
        // fields
        private List<Vector3> mPts = new List<Vector3>();
        public List<Vector3> getPts() {
            return this.mPts;
        }
        public void setPts(List<Vector3> pts) {
            this.mPts = pts;
            this.mGeom = new SSTrapezoid(pts[0], pts[1], pts[2], pts[3]);
            this.refreshAtGeomChange();
        }
        private Color mColor = Color.red; // easily noticable color
        public Color getColor() { return this.mColor; }
        public void setColor(Color c) {
            this.mColor = c;
            this.refreshRenderer();
        }
        public void setPosition(Vector2 pos) {
            this.getGameObject().transform.position = pos;
        }
        public Vector2 getPosition() {
            return this.getGameObject().transform.position;
        }

        // constructor
        public SSAppTrapezoid3D(string name, Vector3 edgeTop, Vector3 edgeBottom,
            Vector3 shadowTop, Vector3 shadowBottom, Color color) :
            base($"{ name }/Trapezoid3D") {
            this.mPts.Add(edgeTop);
            this.mPts.Add(edgeBottom);
            this.mPts.Add(shadowTop);
            this.mPts.Add(shadowBottom);
            this.mGeom = new SSTrapezoid(edgeTop, edgeBottom, shadowTop,
                shadowBottom);
            this.mColor = color;
            this.refreshAtGeomChange();
        }

        protected override void addComponents() {
            this.mGameObject.AddComponent<MeshFilter>();
            this.mGameObject.AddComponent<MeshRenderer>();
            // this.mGameObject.AddComponent<Rigidbody2D>();
            // this.mGameObject.AddComponent<BoxCollider2D>();

            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            Material mat = new Material(Shader.Find("UI/Unlit/Transparent"));
            mr.material = mat;
        }

        protected override void refreshRenderer() {
            SSTrapezoid rect = (SSTrapezoid) this.mGeom;
            MeshFilter mf = this.mGameObject.GetComponent<MeshFilter>();
            mf.mesh = rect.calcMesh();
            MeshRenderer mr = this.mGameObject.GetComponent<MeshRenderer>();
            mr.material.color = this.mColor;
            Material faceMat = this.getGameObject().
                GetComponent<MeshRenderer>().material;
            faceMat.SetFloat("_Mode", 3);
            faceMat.SetInt("_SrcBlend",
                (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            faceMat.SetInt("_DstBlend",
                (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            faceMat.SetInt("_ZWrite", 0);
            faceMat.DisableKeyword("_ALPHATEST_ON");
            faceMat.EnableKeyword("_ALPHABLEND_ON");
            faceMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            faceMat.renderQueue = 3000;
            // Alpha 값 적용
            Color color = faceMat.color;
            color.a =  0.5f;
            faceMat.color = color;
        }

        protected override void refreshCollider() {
            SSTrapezoid rect = (SSTrapezoid) this.mGeom;
            // BoxCollider2D bc = this.mGameObject.GetComponent<BoxCollider2D>();
            // bc.size = new Vector2(rect.getWidth(), rect.getHeight());
        }
    }
}