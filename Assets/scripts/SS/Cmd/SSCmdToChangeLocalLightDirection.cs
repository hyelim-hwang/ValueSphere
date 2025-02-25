using System.Collections.Generic;
using SS.AppObject;
using SSAppObject;
using UnityEngine;
using X;
using SS.Scenario;

namespace SS.Cmd {
    public class SSCmdToChangeLocalLightPosition : XLoggableCmd {
        //fields
        //fields
        private Vector3 mPrevPt1 = Vector3.zero;
        private Vector3 mCurPt1 = Vector3.zero;
        private Vector3 mPrevPt2 = Vector3.zero;
        private Vector3 mCurPt2 = Vector3.zero;

        //private constructor
        private SSCmdToChangeLocalLightPosition(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
           SSStickHandleScenario scenario =
                    (SSStickHandleScenario)ss.getScenarioMgr().getCurScene().getScenario();
            SSTouchMark touchMark1 = scenario.getManipulatingTouchMarks()[0];
            SSTouchMark touchMark2 = scenario.getManipulatingTouchMarks()[1];
            this.mPrevPt1 = touchMark1.getRecentPt(1);
            this.mCurPt1 = touchMark1.getRecentPt(0);
            this.mPrevPt2 = touchMark2.getRecentPt(1);
            this.mCurPt2 = touchMark2.getRecentPt(0);
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToChangeLocalLightPosition cmd =
                new SSCmdToChangeLocalLightPosition(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSShadowStickMgr stickMgr = ss.getShadowStickMgr();
            SSGridCameraPerson cp = ss.getGridCameraPerson();
            SSStick pivotStick =
                stickMgr.getSticks()[stickMgr.getManipulatingSticks()[0]];
            SSStick movingStick =
                stickMgr.getSticks()[stickMgr.getManipulatingSticks()[1]];
            // Debug.LogError("this.mPrevPt1" + this.mPrevPt1);
            // Debug.LogError("this.mCurPt1" + this.mCurPt1);
            // Debug.LogError("this.mPrevPt2" + this.mPrevPt2);
            // Debug.LogError("this.mCurPt2" +this.mCurPt2);
            // Debug.LogError(stickMgr.getManipulatingSticks().Count);
            // Debug.LogError("manistick1" + pivotStick.getGameObject().transform.position);
            // Debug.LogError("manistick2" + movingStick.getGameObject().transform.position);


            //calculate the ray colliding point on the gound plane.
            Ray prevPtRay1 = cp.getCamera().ScreenPointToRay(this.mPrevPt1);
            float prevPtDist1 = float.NaN;
            Plane plane = stickMgr.getPlane();
            plane.Raycast(prevPtRay1, out prevPtDist1);
            Vector3 prevPtOnPlane1 = prevPtRay1.GetPoint(prevPtDist1);
            Ray curPtRay1 = cp.getCamera().ScreenPointToRay(this.mCurPt1);
            float curPtDist = float.NaN;
            plane.Raycast(curPtRay1, out curPtDist);
            Vector3 curPtOnPlane1 = curPtRay1.GetPoint(curPtDist);
            Vector3 lightDirDiff1 = curPtOnPlane1 - prevPtOnPlane1;

            Ray prevPtRay2 = cp.getCamera().ScreenPointToRay(this.mPrevPt2);
            float prevPtDist2 = float.NaN;
            plane.Raycast(prevPtRay2, out prevPtDist2);
            Vector3 prevPtOnPlane2 = prevPtRay2.GetPoint(prevPtDist2);
            Ray curPtRay2 = cp.getCamera().ScreenPointToRay(this.mCurPt2);
            float curPtDist2 = float.NaN;
            plane.Raycast(curPtRay2, out curPtDist2);
            Vector3 curPtOnPlane2 = curPtRay2.GetPoint(curPtDist2);
            Vector3 lightDirDiff2 = curPtOnPlane2 - prevPtOnPlane2;

            //move each sticks by ray colliding point.
            Vector3 updatedShadowTop1 = pivotStick.getShadowTop()
                + lightDirDiff1 +
                pivotStick.getGameObject().transform.position;
            Vector3 updatedShadowTop2 = movingStick.getShadowTop()
                + lightDirDiff2 +
                movingStick.getGameObject().transform.position;;

            //calcultate the light position hint 1.
            //each point needs to be global point?
            Debug.LogError("lightDirDiff1" + lightDirDiff1);
            Debug.LogError("lightDirDiff2" + lightDirDiff2);
            Vector3 intersection =
                SSShadowStickMgr.calculateLocalLightPosByConstructedSticks(
                pivotStick, movingStick, updatedShadowTop1, updatedShadowTop2);
            stickMgr.setLightPosition(intersection);



            //calculate the light position reversely.

            return true;

        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("doSomething", this.GetType().Name);
            return data;
        }
    }
}