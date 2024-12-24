using System.Collections.Generic;
using UnityEngine;
using SS.Geom;

namespace SS.AppObject {
    public class SSValueStroke : SSAppGeom2D {
        //fields
        private float mWidth = float.NaN;
        public void setWidth(float width) {
            this.mWidth = width;
            this.refreshAtGeomChange();
        }
        public float getWidth() {
            return this.mWidth;
        }
        private Color mColor = Color.red;
        public void setColor(Color color) {
            this.mColor = color;
            this.refreshAtGeomChange();
        }
        public Color getColor() {
            return this.mColor;
        }
        public void setPts(List<Vector2> pts) {
            this.mGeom = new SSPolyline2D(pts);
            this.refreshAtGeomChange();
        }
        private Vector2 mValueCoordinate = SSUtil.VECTOR2_NAN;
        public void setValueCoordinate(Vector2 coordinate) {
            this.mValueCoordinate = coordinate;
            //this.setValueFromCoordinate(cp);
        }
        public Vector2 getValueCoordinate() {
            return this.mValueCoordinate;
        }
        private Vector3 mSphereCenter = SSUtil.VECTOR3_NAN;
        public void setSphereCenter(Vector3 pt) {
            this.mSphereCenter = pt;
            //this.setValueFromCoordinate(cp);
        }
        public Vector3 getSphereCenter() {
            return this.mSphereCenter;
        }
        private float mSphereRadius = float.NaN;
        public void setSphereRadius(float r) {
            this.mSphereRadius = r;
            //this.setValueFromCoordinate(cp);
        }
        public float getSphereRadius() {
            return this.mSphereRadius;
        }
        //for color calibration
        int maxIteration = 10000;
        private string mId = string.Empty;
        public string getId() {
            return this.mId;
        }

        //constructor
        public SSValueStroke(string id, Vector2 coordinate, List<Vector2> pts,
            float width, Color color, Vector3 sphereCenter,
            float sphereRadious) : base($"ValueStroke({id})") {
            this.mId = id;
            this.mValueCoordinate = coordinate;
            this.mWidth = width;
            this.mColor = color;
            this.mGeom = new SSPolyline2D(pts);
            //save sphere center and radius.
            this.mSphereCenter = sphereCenter;
            this.mSphereRadius = sphereRadious;
            //app.getValueSphereMgr().
            //getValueSphere().getSphere().transform.position;
            // this.mSphereRadius = app.getValueSphereMgr().
            //     getValueSphere().getRadius() / 2;
            this.refreshAtGeomChange();
        }

        //methods
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

        public void setValueFromCoordinate(Texture2D screenshot,
            SSCameraPerson cp) {
            Vector2 valueCoord = this.mValueCoordinate;
            Color bla =
                screenshot.GetPixel((int)valueCoord.x, (int)valueCoord.y);
            cp.getCameraRig().destroyGameObject();
            //color calibration
            float RValue = bla.r;
            float GValue = bla.g;
            float BValue = bla.b;
            //Debug.LogWarning("setted with:" + bla);
            bool isInGreyScale = (RValue - GValue) < 0.3;
            // while (!isInGreyScale && (maxIteration > 0)) {
            //     int newCoordX = (int)valueCoord.x + 10;
            //     int newCoordY = (int)valueCoord.y + 10;
            //     bla = screenshot.GetPixel(newCoordX, newCoordY);
            //     RValue = bla.r;
            //     GValue = bla.g;
            //     BValue = bla.b;
            //     isInGreyScale = (RValue - GValue) < 0.3;
            //     // RValue = bla.r;
            //     //Debug.LogWarning("color modified to" + bla);
            //     maxIteration--;
            // }
            this.setColor(bla);
        }
    }
}
