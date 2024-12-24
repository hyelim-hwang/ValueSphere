using System.Collections.Generic;
using UnityEngine;

namespace SS.Geom {
    public class SSCircle2D : SSGeom2D {
        //field
        private readonly float mRadius = float.NaN;
        public float getRadius() {
            return this.mRadius;
        }
        private readonly Vector2 mPos = Vector2.zero;
        public Vector3 getPos() {
            return this.mPos;
        }
        private readonly Quaternion mRot = Quaternion.identity;
        public Quaternion getRot() {
            return this.mRot;
        }

        //constructor
        public SSCircle2D(float radius, Vector2 pos, Quaternion rot) {
            this.mRadius = radius;
            this.mPos = pos;
            this.mRot = rot;
        }

        //methods
        public List<Vector2> calcPts(int sideNum) {
            float dTheta = 2f * Mathf.PI / (float)sideNum;
            List<Vector2> pts = new List<Vector2>();
            for (int i = 0; i < sideNum + 1; i++) {
                Vector2 pt = this.mPos + new Vector2(
                    this.mRadius * Mathf.Cos((float)i * dTheta),
                    this.mRadius * Mathf.Sin((float)i * dTheta));
                pts.Add(pt);
            }
            return pts;
        }

        public List<Vector2> calcPts(int sideNum, float radius) {
            float dTheta = 2f * Mathf.PI / (float)sideNum;
            List<Vector2> pts = new List<Vector2>();
            for (int i = 0; i < sideNum + 1; i++) {
                Vector2 pt = this.mPos + new Vector2(
                    radius * Mathf.Cos((float)i * dTheta),
                    radius * Mathf.Sin((float)i * dTheta));
                pts.Add(pt);
            }
            return pts;
        }

        public Mesh calcMesh(int sideNum) {
            //the second to last vertex is the starting point.
            //the last vertex is the center.
            List<Vector3> vs = new List<Vector3>();
            foreach (Vector2 pt2D in this.calcPts(sideNum)) {
                vs.Add((Vector3)pt2D);
            }
            vs.Add((Vector3)this.mPos);

            int[] ts = new int[3 * sideNum];
            for (int i = 0; i < sideNum; i++) {
                int j = 3 * i;
                ts[j] = i;
                ts[j + 1] = i + 1;
                ts[j + 2] = sideNum + 1;

            }

            Mesh mesh = new Mesh();
            mesh.vertices = vs.ToArray();
            mesh.triangles = ts;
            return mesh;
        }
    }
}