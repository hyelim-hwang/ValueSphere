using System.Collections.Generic;
using SS.AppObject;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace SS {
    public class SSPerspectiveGrid : SSAppNoGeom3D {
        //constants
        private static readonly float LENGTH = 100f;
        private static readonly float WIDTH = 0.01f;
        private static readonly Color COLOR = new Color(0.75f, 0.75f, 0.75f);
        private static readonly int NUM_X_GRID_LINES = 11;
        private static readonly int NUM_Z_GRID_LINES = 11;
        public static readonly Quaternion GRID_ROTATION =
            Quaternion.Euler(0, 0f, 0f);
            public static readonly Vector3 GRID_SCALE =
            new Vector3(0.7f, 0.7f, 0.7f);

        //field
        private Quaternion mRot = Quaternion.identity;
        public void setRot(Quaternion rot) {
            this.mRot = rot;
            rotateGrid();
        }
        private Vector3 mScale = Vector3.zero;
        public void setScale(Vector3 scale) {
            this.mScale = scale;
            scaleGrid();
        }

        //constructor
        public SSPerspectiveGrid() : base("Grid") {
            this.mRot = Quaternion.identity;
            this.mScale = Vector3.one;
            List<Vector3> pts1 = new List<Vector3>();
            Vector3 floorInfinityRight = new Vector3(100f, 0f, 0f);
            Vector3 floorInfinityLeft = new Vector3(0f, 0f, 100f);
            Vector3 POVController = new Vector3(-0.5f, 0.5f, -0.5f);
            Vector3 FOVController = new Vector3(-0.5f, -0.5f, -0.5f);
            pts1.Add(FOVController);
            pts1.Add(floorInfinityRight);
            SSAppPolyline3D line =
                new SSAppPolyline3D("perspectiveGridLine1", pts1,
                SSPerspectiveGrid.WIDTH, SSPerspectiveGrid.COLOR);
            this.addChild(line);

            //add grid to grid layer(3)
            this.getGameObject().layer = 3;
            foreach (Transform child in this.getGameObject().transform) {
                child.GameObject().layer = 3;
            }
        }

        //methods
        public void rotateGrid() {
            this.getGameObject().transform.rotation = this.mRot;
        }
        public void scaleGrid() {
            this.getGameObject().transform.localScale = this.mScale;
        }
    }
}


