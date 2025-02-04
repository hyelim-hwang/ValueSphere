using System;
using System.Collections.Generic;
using SS.AppObject;
using Unity.VisualScripting;
using UnityEngine;

namespace SS {
    public class SSPerspectiveCube : SSAppNoGeom3D {
        //fields
        private GameObject mCube = null;
        public GameObject getCube() {
            return this.mCube;
        }
        public void setCube(GameObject cube) {
            this.mCube = cube;
        }
        private SSGrid mGrid = null;
        public SSGrid getGrid() {
            return this.mGrid;
        }
        public void setGrid(SSGrid grid) {
            this.mGrid = grid;
        }

        //constructor
        public SSPerspectiveCube() : base("Perspective Cube") {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.setCube(cube);
            SSGrid grid = new SSGrid();
            grid.setRot(SSGrid.GRID_ROTATION);
            grid.setScale(SSGrid.GRID_SCALE);
            this.setGrid(grid);
            this.mCube.transform.SetParent(this.mGameObject.transform);
            this.addChild(this.mGrid);
            foreach (Transform child in this.getGameObject().transform) {
                child.GameObject().layer = 3;
            }
        }
    }
}


