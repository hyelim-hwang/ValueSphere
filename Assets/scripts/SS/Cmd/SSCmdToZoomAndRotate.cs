using UnityEngine;
using X;
using SS.Scenario;

namespace SS.Cmd {
    public class SSCmdToZoomAndRotate : XLoggableCmd {
        //fields
        Vector2 mPrevPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mPrevPt2 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt2 = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToZoomAndRotate(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToZoomAndRotate cmd = new SSCmdToZoomAndRotate(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            //get the manipulating touchmarks first.
            SSNavigateScenario scenario =
                (SSNavigateScenario)SSNavigateScenario.getSingleton();
            Debug.Assert(scenario.getManipulatingTouchMarks().Count >= 2);
            SSTouchMark tm1 = scenario.getManipulatingTouchMarks()[0];
            SSTouchMark tm2 = scenario.getManipulatingTouchMarks()[1];
            if (tm1.getPts().Count == 1 || tm2.getPts().Count == 1) {
                return true;
            }
            this.mCurPt1 = tm1.getRecentPt(0);
            this.mPrevPt1 = tm1.getRecentPt(1);
            this.mCurPt2 = tm2.getRecentPt(0);
            this.mPrevPt2 = tm2.getRecentPt(1);

            SSCameraPerson cameraPerson = ((SSApp)this.mApp).getOrthoCameraPerson();
            Camera cam = cameraPerson.getCamera();
            Vector3 prevCamPos =
                cameraPerson.getCameraRig().getGameObject().transform.position;

            Vector3 curDir =
                new Vector3((this.mCurPt1 - this.mCurPt2).x,
                (this.mCurPt1 - this.mCurPt2).y, 0);
            Vector3 prevDir =
                new Vector3((this.mPrevPt1 - this.mPrevPt2).x,
                (this.mPrevPt1 - this.mPrevPt2).y, 0);

            // calculate the rotation of the camera.
            Quaternion rot = Quaternion.FromToRotation(-prevDir, -curDir);
            Quaternion newRot = Quaternion.Inverse(rot) *
                cameraPerson.getCameraRig().getGameObject().transform.rotation;
            cameraPerson.getCameraRig().getGameObject().transform.rotation =
                newRot;

            // calculate the zoom of the camera.
            float prevCamOrthoSize = cam.orthographicSize;
            float prevRodLength = (prevDir).magnitude;
            float curRodLength = (curDir).magnitude;
            float scale = curRodLength / prevRodLength;
            float newOrthoSize = prevCamOrthoSize / scale;
            cam.orthographicSize = newOrthoSize;

            // calculate the translation of the camera.
            Vector3 pivotInWorld = cam.ScreenToWorldPoint(this.mPrevPt1);
            Vector3 curPivotInWorld = cam.ScreenToWorldPoint(this.mCurPt1);
            pivotInWorld.z = prevCamPos.z;
            Vector3 curCamPos = pivotInWorld -
                Quaternion.Inverse(rot) * ((1f / scale) *
                (curPivotInWorld - prevCamPos));
            cameraPerson.getCameraRig().getGameObject().transform.position =
                curCamPos;
            Debug.Log(curCamPos - prevCamPos);
            Debug.Log(pivotInWorld);
            Debug.Log(scale);

            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("zoomAndRotate", this.GetType().Name);
            return data;
        }
    }
}