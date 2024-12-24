using System;
using System.Collections.Generic;
using SS.AppObject;
using SS.Geom;
using UnityEngine;

namespace SS.File {
    [Serializable]
    public class SSSerializableAppPolyline3D {
        // fields
        public List<SSSerializableVector3> pts = null;
        public SSSerializableColor color = null;
        public float width = float.NaN;

        // constructor
        public SSSerializableAppPolyline3D(SSAppPolyline3D ptCurve3D) {
            this.pts = new List<SSSerializableVector3>();
            SSPolyline3D polyline = (SSPolyline3D) ptCurve3D.getGeom();
            foreach (Vector3 pt in polyline.getPts()) {
                SSSerializableVector3 sPt = new SSSerializableVector3(pt);
                this.pts.Add(sPt);
            }
            this.color = new SSSerializableColor(ptCurve3D.getColor());
            this.width = ptCurve3D.getWidth();
        }

        // methods
        public SSAppPolyline3D toAppPolyline3D() {
            List<Vector3> pts = new List<Vector3>();
            foreach (SSSerializableVector3 sPt in this.pts) {
                Vector3 pt = sPt.toVector3();
                pts.Add(pt);
            }
            Color color = this.color.toColor();
            float width = this.width;

            return new SSAppPolyline3D("PtCurve3D", pts, width, color);
        }
    }
}
