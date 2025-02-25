using System.Collections.Generic;
using SS.AppObject;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace SS {
    public class SSGrid : SSAppNoGeom3D {
        //constants
        private static readonly float LENGTH = 4f;
        private static readonly float WIDTH = 0.01f;
        private static readonly Color COLOR = new Color(0.75f, 0.75f, 0.75f);
        private static readonly int NUM_X_GRID_LINES = 5;
        private static readonly int NUM_Z_GRID_LINES = 5;
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
        public SSGrid() : base("Grid") {
            this.mRot = Quaternion.identity;
            this.mScale = Vector3.one;
            for (int i = 0; i < SSGrid.NUM_X_GRID_LINES; i++) {
                List<Vector3> pts = new List<Vector3>();
                pts.Add(new Vector3(-SSGrid.LENGTH / 2f, 0f,
                    (float)i - (SSGrid.LENGTH / 2)));
                pts.Add(new Vector3(+SSGrid.LENGTH / 2f, 0f,
                    (float)i - (SSGrid.LENGTH / 2)));

                SSAppPolyline3D line =
                    new SSAppPolyline3D("XGridLine", pts, SSGrid.WIDTH,
                    SSGrid.COLOR);
                this.addChild(line);
            }

            for (int i = 0; i < SSGrid.NUM_Z_GRID_LINES; i++) {
                List<Vector3> pts = new List<Vector3>();
                pts.Add(new Vector3((float)i - (SSGrid.LENGTH / 2), 0f,
                    -SSGrid.LENGTH / 2f));
                pts.Add(new Vector3((float)i - (SSGrid.LENGTH / 2), 0f,
                    +SSGrid.LENGTH / 2f));

                SSAppPolyline3D line =
                    new SSAppPolyline3D("ZGridLine", pts, SSGrid.WIDTH,
                    SSGrid.COLOR);
                this.addChild(line);
            }
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


