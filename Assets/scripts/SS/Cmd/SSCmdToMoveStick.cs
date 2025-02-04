using UnityEngine;
using X;
using SS.Scenario;
using SS.AppObject;
using SSAppObject;

namespace SS.Cmd {
    public class SSCmdToMoveStick : XLoggableCmd {
        //fields
        Vector2 mPrevPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt1 = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToMoveStick(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToMoveStick cmd = new SSCmdToMoveStick(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSShadowStickMgr SSMgr = ss.getShadowStickMgr();
            SSStickHandleScenario scenario =
                (SSStickHandleScenario)SSStickHandleScenario.getSingleton();
            SSStick stick = ((SSApp)scenario.getApp()).getShadowStickMgr().
                getShadowStick();
            SSTouchMark tm1 = scenario.getManipulatingTouchMarks()[0];
            if (tm1.getPts().Count == 1) {
                return true;
            }
            this.mCurPt1 = tm1.getRecentPt(0);
            this.mPrevPt1 = tm1.getRecentPt(1);

            //define variables.
            //from screen-world point, make a view direction beam and
            // /get a rayhit.
            Plane plane = ((SSApp)this.mApp).getShadowStickMgr().getPlane();
            Vector3 cameraView = ((SSApp)this.mApp).
                getGridCameraPerson().getView();
            //get prev pt intersection with ground plane.
            Ray prevPtInRay = ((SSApp)this.mApp).
                getGridCameraPerson().getCamera().
                ScreenPointToRay(this.mPrevPt1);
            float prevPtDist = float.NaN;
            plane.Raycast(prevPtInRay, out prevPtDist);
            Vector3 prevPtOnPlane = prevPtInRay.GetPoint(prevPtDist);

            //get cur pt intersection with ground plane.
            Ray curPtInRay = ((SSApp)this.mApp).
                getGridCameraPerson().getCamera().
                ScreenPointToRay(this.mCurPt1);
            float curPtDist = float.NaN;
            plane.Raycast(curPtInRay, out curPtDist);
            Vector3 curPtOnPlane = curPtInRay.GetPoint(curPtDist);

            //Get current stick's pos and rad.
            Vector3 baseOfStick = stick.getBaseOfStick();
            Vector3 prevPos = baseOfStick;

            //Set stick with updated touch input.
            baseOfStick = baseOfStick + (curPtOnPlane - prevPtOnPlane);
            Vector3 stickPos =
                new Vector3(baseOfStick.x, baseOfStick.y, baseOfStick.z);
            stick.setBaseOfStick(stickPos);
            Vector3 curPos = stickPos;
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("moveSphere", this.GetType().Name);
            return data;
        }
    }
}