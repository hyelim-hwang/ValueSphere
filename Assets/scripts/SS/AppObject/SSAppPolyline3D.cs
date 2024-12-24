

using System.Collections.Generic;
using SS.Geom;
using UnityEngine;

namespace SS.AppObject {
    public class SSAppPolyline3D : SSAppGeom3D {
        private float mWidth = float.NaN;

        public void setWidth(float width) {
            this.mWidth = width;
            this.refreshRenderer();
        }
        public float getWidth() {
            return this.mWidth;
        }
        private Color mColor = Color.red;
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();

        }
        public Color getColor() {
            return this.mColor;
        }

        public void setPts(List<Vector3> pts) {
            this.mGeom = new SSPolyline3D(pts);
            this.refreshAtGeomChange();

        }

        public SSAppPolyline3D (string name, List<Vector3> pts,
        float width, Color color) : base($"{name}/Polyline3D") {
            this.mWidth = width;
            this.mColor = color;
            this.mGeom = new SSPolyline3D(pts);
            this.refreshAtGeomChange();
        }
        protected override void addComponents() {
            this.mGameObject.AddComponent<LineRenderer>();
            this.mGameObject.AddComponent<SphereCollider>();
        }

        protected override void refreshCollider() {
            SSPolyline3D polyline = (SSPolyline3D)this.mGeom;
            Vector3 ctr = polyline.calcCentroid();
            float r = polyline.calcMaxDevFrom(ctr);
            SphereCollider sc = this.mGameObject.GetComponent<SphereCollider>();
            sc.center = ctr;
            sc.radius = r;
        }

        protected override void refreshRenderer() {
            SSPolyline3D polyline = (SSPolyline3D)this.mGeom;
            LineRenderer lr = this.mGameObject.GetComponent<LineRenderer>();
            lr.useWorldSpace = false;
            lr.alignment = LineAlignment.View;
            lr.positionCount = polyline.getPts().Count;
            lr.SetPositions(polyline.getPts().ToArray());
            lr.startWidth = this.mWidth;
            lr.endWidth = this.mWidth;
            lr.material = new Material(Shader.Find("Unlit/Color"));
            lr.material.color = this.mColor;
        }
    }
}