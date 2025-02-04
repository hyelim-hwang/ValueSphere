using System.Collections.Generic;
using UnityEngine;

namespace SS.Geom {
    public class SSTrapezoid : SSGeom3D {
        //field
        private readonly Vector3 mEdgeTop = Vector3.zero;
        public Vector3 getEdgeTop() {
            return this.mEdgeTop;
        }
        private readonly Vector3 mEdgeBottom = Vector3.zero;
        public Vector3 getEdgeBottom() {
            return this.mEdgeBottom;
        }
        private readonly Vector3 mShadowTop = Vector3.zero;
        public Vector3 getShadowTop() {
            return this.mShadowTop;
        }
        private readonly Vector3 mShadowBottom = Vector3.zero;
        public Vector3 getShadowBottom() {
            return this.mShadowBottom;
        }

        public SSTrapezoid(Vector3 edgeTop, Vector3 edgeBottom,
            Vector3 shadowTop, Vector3 ShadowBottom) {
            this.mEdgeTop = edgeTop;
            this.mEdgeBottom = edgeBottom;
            this.mShadowTop = shadowTop;
            this.mShadowBottom = ShadowBottom;
        }

        //methods

        public Mesh calcMesh() {
            Mesh mesh = new Mesh();
            List<Vector3> pts = new List<Vector3>();
            pts.Add(this.mEdgeTop);
            pts.Add(this.mEdgeBottom);
            pts.Add(this.mShadowBottom);
            pts.Add(this.mShadowTop);
            Vector3[] vs = new Vector3[pts.Count];
            for (int i = 0; i < pts.Count; i++) {
                vs[i] = (Vector3) pts[i];
            }
            mesh.vertices = vs;
            mesh.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
            return mesh;
        }
    }
}