using System;
using System.Collections.Generic;
using SS.AppObject;
using SS.Geom;
using UnityEngine;

namespace SS.File {
    [Serializable]
    public class SSSerializableValueStroke {
        // fields
        public string id = string.Empty;
        public float width = float.NaN;
        public SSSerializableColor color = null;
        public List<SSSerializableVector2> pts = null;
        public SSSerializableVector2 valueCoordinate = null;
        public SSSerializableVector3 sphereCenter = null;
        public float sphereRadius = float.NaN;
        public SSPolyline2D geom = null;

        // constructor
        public SSSerializableValueStroke(SSValueStroke vs) {
            this.id = vs.getId();
            this.width = vs.getWidth();
            this.color = new SSSerializableColor(vs.getColor());
            this.pts = new List<SSSerializableVector2>();
            SSPolyline2D polyline = (SSPolyline2D) vs.getGeom();
            foreach (Vector2 pt in polyline.getPts()) {
                SSSerializableVector2 sPt = new SSSerializableVector2(pt);
                this.pts.Add(sPt);
            }
            this.color = new SSSerializableColor(vs.getColor());
            this.valueCoordinate = new SSSerializableVector2(
                vs.getValueCoordinate());
            this.sphereCenter = new SSSerializableVector3(vs.getSphereCenter());
            this.sphereRadius = vs.getSphereRadius();
        }

        // methods
        public SSValueStroke toValueStroke() {
            string id = this.id;
            Vector2 coordinate = this.valueCoordinate.toVector2();
            List<Vector2> pts = new List<Vector2>();
            foreach (SSSerializableVector2 sPt in this.pts) {
                Vector2 pt = sPt.toVector2();
                pts.Add(pt);
            }
            Color color = this.color.toColor();
            float width = this.width;
            float sphereRadius = this.sphereRadius;
            Vector3 sphereCenter = this.sphereCenter.toVector3();

            return new SSValueStroke(id, coordinate, pts, width,
            color, sphereCenter, sphereRadius);
        }
    }
}
