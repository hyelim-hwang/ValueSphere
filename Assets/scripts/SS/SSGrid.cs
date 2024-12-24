using System.Collections.Generic;
using SS.AppObject;
using UnityEngine;

namespace SS {
    public class SSGrid : SSAppNoGeom3D {
        private static readonly float LENGTH = 4f;
        private static readonly float WIDTH = 0.01f;
        private static readonly Color COLOR = new Color(0.75f, 0.75f, 0.75f);

        private static readonly int NUM_X_GRID_LINES = 5;
        private static readonly int NUM_Z_GRID_LINES = 5;

        public SSGrid() : base("Grid") {
            for (int i = 0; i < SSGrid.NUM_X_GRID_LINES; i++) {
                List<Vector3> pts = new List<Vector3>();
                pts.Add(new Vector3(-SSGrid.LENGTH / 2f, 0f, (float)i - 2f));
                pts.Add(new Vector3(+SSGrid.LENGTH / 2f, 0f, (float)i - 2f));

                SSAppPolyline3D line =
                    new SSAppPolyline3D("XGridLine", pts, SSGrid.WIDTH,
                    SSGrid.COLOR);
                this.addChild(line);
            }

            for (int i = 0; i < SSGrid.NUM_Z_GRID_LINES; i++) {
                List<Vector3> pts = new List<Vector3>();
                pts.Add(new Vector3((float)i - 2f, 0f, -SSGrid.LENGTH / 2f));
                pts.Add(new Vector3((float)i - 2f, 0f, +SSGrid.LENGTH / 2f));

                SSAppPolyline3D line =
                    new SSAppPolyline3D("ZGridLine", pts, SSGrid.WIDTH,
                    SSGrid.COLOR);
                this.addChild(line);
            }
        }
    }
}


