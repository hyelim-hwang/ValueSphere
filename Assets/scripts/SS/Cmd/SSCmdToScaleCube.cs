using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using System.Text;
using SS.Scenario;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToScaleCube : XLoggableCmd {
        //fields
        Vector2 mPrevPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mPrevPt2 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt2 = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToScaleCube(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToScaleCube cmd = new SSCmdToScaleCube(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSSperspectiveDecideScenario scenario =
                (SSSperspectiveDecideScenario)SSSperspectiveDecideScenario.getSingleton();
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

            Camera cam = ((SSApp)this.mApp).getGridCameraPerson().getCamera();
            //define variables.
            Vector2 prevPtsAvg = (this.mPrevPt1 + this.mPrevPt2) / 2;
            Vector2 CurPtsAvg = (this.mCurPt1 + this.mCurPt2) / 2;
            Vector3 prevPtsAvgInWorld = cam.ScreenToWorldPoint(prevPtsAvg);
            Vector3 CurPtsAvgInWorld = cam.ScreenToWorldPoint(CurPtsAvg);
            Vector3 curPtDist =
                new Vector3((this.mCurPt1 - this.mCurPt2).x,
                (this.mCurPt1 - this.mCurPt2).y, 0);
            Vector3 prevPtDist =
                new Vector3((this.mPrevPt1 - this.mPrevPt2).x,
                (this.mPrevPt1 - this.mPrevPt2).y, 0);

            float prevPtsDist = (this.mPrevPt1 - this.mPrevPt2).magnitude;
            float CurPtsDist = (this.mCurPt1 - this.mCurPt2).magnitude;
            float scale = CurPtsDist / prevPtsDist;

            //Set camera with updated touch input.
            float FOV = cam.fieldOfView;
            FOV *= scale;
            cam.fieldOfView = FOV;

            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("scaleValueSphere", this.GetType().Name);
            return data;
        }
    }
}