using UnityEngine;
using SS.Geom;
using SS.AppObject;
using System.Collections.Generic;
using System.Linq;

namespace SSAppObject {
    public class SSEquator : SSAppGeom3D {
        // constants
        public static readonly int DEFAULT_NUM_CURVE_SEGS = 20;
        public static readonly float DEFAULT_WIDTH = 0.005f;
        public static readonly Color DEFAULT_COLOR = Color.red;
        public static readonly float CROSS_DEFAULT_WIDTH = 0.005f;

        // fields
        private float mWidth = SSEquator.DEFAULT_WIDTH;
        public float getWidth() {
            return this.mWidth;
        }
        public void setWidth(float width) {
            this.mWidth = width;
            this.refreshRenderer();
        }
        private Color mColor = SSEquator.DEFAULT_COLOR;
        public Color getColor() {
            return this.mColor;
        }
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }
        private SSAppPolyline3D mDiameterLine1 = null;
        public SSAppPolyline3D getDiameterLine1() {
            return this.mDiameterLine1;
        }
        private SSAppPolyline3D mDiameterLine2 = null;
        public SSAppPolyline3D getDiameterLine2() {
            return this.mDiameterLine2;
        }

        private SSValueSphere mValueSphere = null;

        // constructor
        public SSEquator(string name, float width, Color color,
        SSValueSphere vs) : base($"{ name }/Equator3D") {
            this.mGeom = new SSCircle3D(vs.getRadius(), vs.getSphere().
                transform.position, vs.getRot());
            this.mValueSphere = vs;
            List<Vector3> dummyPts = new List<Vector3>();
            dummyPts.Add(Vector3.zero);
            dummyPts.Add(Vector3.zero);
            this.mDiameterLine1 = new SSAppPolyline3D("DiameterLine", dummyPts,
                SSEquator.CROSS_DEFAULT_WIDTH, SSEquator.DEFAULT_COLOR);
            this.mDiameterLine2 = new SSAppPolyline3D("DiameterLine", dummyPts,
                SSEquator.CROSS_DEFAULT_WIDTH, SSEquator.DEFAULT_COLOR);
            //this.mWidth = width;
            this.setWidth(SSEquator.DEFAULT_WIDTH);
            this.mColor = color;
            this.refreshAtGeomChange();
        }

        // methods
        protected override void addComponents() {
            this.mGameObject.AddComponent<LineRenderer>();
        }

        protected override void refreshRenderer() {
            this.mGeom = new SSCircle3D(this.mValueSphere.getRadius() / 1.98f,
                this.mValueSphere.getGameObject().transform.position,
                this.mValueSphere.getRot());

            //update equator.
            List<Vector3> pts = ((SSCircle3D)this.mGeom).calcPts(50);
            LineRenderer lr = this.mGameObject.GetComponent<LineRenderer>();
            lr.useWorldSpace = false;
            lr.alignment = LineAlignment.View; // lines face the camera.
            lr.positionCount = pts.Count;
            lr.SetPositions(pts.ToArray());
            lr.startWidth = this.mWidth;
            lr.endWidth = this.mWidth;
            lr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            lr.material.color = this.mColor;

            //update crosshair.
            List<Vector3> horizontalPts = new List<Vector3> { pts[0], pts[25] };
            List<Vector3> inwardPts = new List<Vector3> { pts[12], pts[37] };
            this.mDiameterLine1.setPts(horizontalPts);
            this.mDiameterLine2.setPts(inwardPts);
        }

        protected override void refreshCollider() {
        }
    }
}