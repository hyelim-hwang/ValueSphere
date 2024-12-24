using System.Collections.Generic;
using UnityEngine;
namespace SS.Geom {
    public class SSPolyline3D : SSGeom3D {
        private readonly List<Vector3> mPts = null;
        public List<Vector3> getPts() {
            return this.mPts;
        }

        //constructor
        public SSPolyline3D(List<Vector3> pts) {
            this.mPts = pts;
        }

        public Vector3 calcCentroid() {
            Vector3 centroid = Vector3.zero;
            int num = this.mPts.Count;
            foreach (Vector3 pt in this.mPts) {
                centroid += pt;
            }
            centroid /= (float)num;
            return centroid;
        }

        public float calcMaxDevFrom(Vector3 fromPt) {
            float maxDev = float.NegativeInfinity;
            foreach(Vector3 pt in this.mPts) {
                float d = Vector3.Distance(fromPt, pt);
                if (d > maxDev) {
                    maxDev = d;
                }
            }
            return maxDev;
        }
    }
}


