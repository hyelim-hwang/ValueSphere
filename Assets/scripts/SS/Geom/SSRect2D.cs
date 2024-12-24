using System.Collections.Generic;
using UnityEngine;
using SS.Geom;

namespace SS {
    public class SSRect2D : SSGeom2D {
        // fields
        private readonly float mWidth = float.NaN;
        public float getWidth() {
            return this.mWidth;
        }
        private readonly float mHeight = float.NaN;
        public float getHeight() {
            return this.mHeight;
        }
        private readonly Vector2 mPos = Vector3.zero;
        public Vector2 getPos() {
            return this.mPos;
        }
        private readonly Quaternion mRot = Quaternion.identity;
        public Quaternion getRot() {
            return this.mRot;
        }

        // constructor
        public SSRect2D(float width, float height, Vector2 pos,
            Quaternion rot) {
            this.mWidth = width;
            this.mHeight = height;
            this.mPos = pos;
            this.mRot = rot;
        }

        // methods
        public Vector2 calcWidthDir() {
            return this.mRot * Vector2.right;
        }
        public Vector2 calcHeightDir() {
            return this.mRot * Vector2.up;
        }

        public List<Vector2> calcPts() {
            Vector2 wDir = this.calcWidthDir();
            Vector2 hDir = this.calcHeightDir();
            List<Vector2> pts = new List<Vector2>();

            pts.Add(this.mPos + wDir * (-this.mWidth) /
                2f + hDir * (+this.mHeight) / 2f);
            pts.Add(this.mPos + wDir * (-this.mWidth) /
                2f + hDir * (-this.mHeight) / 2f);
            pts.Add(this.mPos + wDir * (+this.mWidth) /
                2f + hDir * (-this.mHeight) / 2f);
            pts.Add(this.mPos + wDir * (+this.mWidth) /
                2f + hDir * (+this.mHeight) / 2f);

            return pts;
        }

        public Mesh calcMesh() {
            Mesh mesh = new Mesh();
            List<Vector2> pts = this.calcPts();
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