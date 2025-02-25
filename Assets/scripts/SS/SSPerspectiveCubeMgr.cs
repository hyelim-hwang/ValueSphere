using UnityEngine;
using SS.AppObject;
using SSAppObject;
using Unity.VisualScripting;

namespace SS {
    public class SSPerspectiveCubeMgr {
        // constants
        public static readonly Color CUBE_EDGE_COLOR = Color.black;
        public static readonly float CUBE_EDGE_WIDTH = 0.05f;
        public static readonly Color VANISHING_LINE_COLOR = Color.gray;
        public static readonly float VANISHING_LINE_WIDTH = 0.03f;

        //fields
        private SSApp mSS = null;
        private SSPerspectiveCube mPerspectiveCube = null;
        public SSPerspectiveCube getPerspectiveCube() {
            return this.mPerspectiveCube;
        }
        public void setPerspectiveCube(SSPerspectiveCube cube) {
            this.mPerspectiveCube = cube;
        }
        public Vector3 mPOVControllerPos = Vector3.zero;
        public Vector3 mFOVControllerPos = Vector3.zero;

        //constructor
        public SSPerspectiveCubeMgr(SSApp ss) {
            this.mSS = ss;
            this.mPerspectiveCube = new SSPerspectiveCube();
            this.mPOVControllerPos = new Vector3(-0.5f, +0.5f, -0.5f);
            this.mFOVControllerPos = new Vector3(-0.5f, -0.5f, -0.5f);
        }

        //util functions

        public void makeGridTransparent() {
            SSApp ss = this.mSS;
            SSGrid grid = ss.getSSPerspectiveCubeMgr().getPerspectiveCube().
                getGrid();
                grid.getGameObject().SetActive(false);
        }

        public void makeGridShow() {
            SSApp ss = this.mSS;
            SSGrid grid = ss.getSSPerspectiveCubeMgr().getPerspectiveCube().
                getGrid();
            grid.getGameObject().SetActive(true);
        }

        public void makeCubeTransparent() {
            SSApp ss = this.mSS;
            GameObject cube = ss.getSSPerspectiveCubeMgr().getPerspectiveCube().
                getCube();
            cube.SetActive(false);
            foreach (Transform child in
                this.getPerspectiveCube().getGameObject().transform) {
                child.gameObject.SetActive(false);
            }
            //turn on the grid
            SSGrid grid = this.getPerspectiveCube().getGrid();
            grid.getGameObject().SetActive(true);
            Debug.LogError(grid == null);
            foreach (SSAppPolyline3D child in grid.getChildren()) {
                child.getGameObject().SetActive(true);
            }
        }

        public void makeCubeShow() {
            SSApp ss = this.mSS;
            GameObject cube = ss.getSSPerspectiveCubeMgr().getPerspectiveCube().
                getCube();
            SSUtil.changeRenderMode(cube.GetComponent<Renderer>().material,
                SSUtil.BlendMode.Opaque);
            cube.SetActive(true);
        }

        public string cubeCollideChecker(Vector2 touchedScreenPt) {
            //perspective cube's world coordinate vertices.
            this.tagHandleByCamPos();
            //make a ray from the grid camera.
            Camera cam = this.mSS.getGridCameraPerson().getCamera();
            Vector2 POVControllerPosInScreen =
                cam.WorldToScreenPoint(this.mPOVControllerPos);
            Vector2 FOVControllerPosInScreen =
                cam.WorldToScreenPoint(this.mFOVControllerPos);
            float distanceFromPointToRotationController =
                SSUtil.DistanceFromPointToLine(POVControllerPosInScreen,
                FOVControllerPosInScreen, touchedScreenPt);
            if (Vector2.Distance(touchedScreenPt, POVControllerPosInScreen) < 30f) {
                Debug.Log("Controlling POV");
                return "POV";
            } else if (
                Vector2.Distance(touchedScreenPt, FOVControllerPosInScreen) < 30f) {
                Debug.Log("Controlling FOV");
                return "FOV";
            } else if (
                distanceFromPointToRotationController < 30f) {
                Debug.Log("Rotation");
                return "Rotation";
            } else {
                return "nothing";
            }
        }

        public void tagHandleByCamPos() {
            Camera cam = this.mSS.getGridCameraPerson().getCamera();
            float camYPos = cam.transform.position.y;
            if(camYPos < 0) {
                this.mPOVControllerPos = new Vector3(-0.5f, -0.5f, -0.5f);
                this.mFOVControllerPos = new Vector3(-0.5f, +0.5f, -0.5f);
            }
        }


    }
}
