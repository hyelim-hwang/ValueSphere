using System.Collections.Generic;
using SS.Geom;
using UnityEngine;

namespace SS.AppObject {
    public class SSAppPolyline2D : SSAppGeom2D {
        private float mWidth = float.NaN;

        public void setWidth(float width) {
            this.mWidth = width;
            this.refreshRenderer();
        }
        private Color mColor = Color.red;
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshRenderer();
        }

        public void setPts(List<Vector2> pts) {
            this.mGeom = new SSPolyline2D(pts);
            this.refreshAtGeomChange();
        }

        public SSAppPolyline2D(string name, List<Vector2> pts, float width,
            Color color) : base($"{name}/Polyline2D") {
            this.mWidth = width;
            this.mColor = color;
            this.mGeom = new SSPolyline2D(pts);
            this.refreshAtGeomChange();
        }
        protected override void addComponents() {
            this.mGameObject.AddComponent<LineRenderer>();
            this.mGameObject.AddComponent<EdgeCollider2D>();
        }

        protected override void refreshCollider() {
            SSPolyline2D polyline = (SSPolyline2D)this.mGeom;
            EdgeCollider2D ec = this.mGameObject.GetComponent<EdgeCollider2D>();
            ec.points = polyline.getPts().ToArray();
        }

        protected override void refreshRenderer() {
            SSPolyline2D polyline = (SSPolyline2D)this.mGeom;
            List<Vector2> pt2Ds = polyline.getPts();
            List<Vector3> pt3Ds = new List<Vector3>();
            for (int i = 0; i < pt2Ds.Count; i++) {
                Vector2 pt2D = pt2Ds[i];
                Vector3 pt3D = new Vector3(pt2D.x, pt2D.y, 0f);
                pt3Ds.Add(pt3D);
            }

            LineRenderer lr = this.mGameObject.GetComponent<LineRenderer>();
            lr.useWorldSpace = false;
            lr.alignment = LineAlignment.View; //lines face the camera
            lr.positionCount = pt3Ds.Count;
            lr.SetPositions(pt3Ds.ToArray());
            lr.startWidth = this.mWidth;
            lr.endWidth = this.mWidth;
            lr.material = new Material(Shader.Find("UI/Unlit/Transparent"));
            lr.material.color = this.mColor;
        }
    }
}