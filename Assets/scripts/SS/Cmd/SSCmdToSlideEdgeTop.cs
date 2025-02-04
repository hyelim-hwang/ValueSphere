using UnityEngine;
using X;
using SS.Scenario;
using SS.AppObject;
using SSAppObject;
using System.Collections.Generic;

namespace SS.Cmd {
    public class SSCmdToSlideEdgeTop : XLoggableCmd {
        //fields
        Vector2 mPrevPt = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToSlideEdgeTop(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToSlideEdgeTop cmd = new SSCmdToSlideEdgeTop(app);
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
            this.mCurPt = tm1.getRecentPt(0);
            this.mPrevPt = tm1.getRecentPt(1);

            //get screen pt diff and convert it into world coordinate.
            SSGridCameraPerson cp = ss.getGridCameraPerson();

            //create a plane on a pivot, directly facing the camera.
            Plane pivotPlane = new Plane(-cp.getView(), cp.getPivot());

            //project the previous screen point to the plane.
            Ray prevPtRay = cp.getCamera().ScreenPointToRay(this.mPrevPt);
            float prevPtDist = float.NaN;
            pivotPlane.Raycast(prevPtRay, out prevPtDist);
            Vector3 prevPtOnPlane = prevPtRay.GetPoint(prevPtDist);

            //project the current screen point to the plane.
            Ray curPtRay = cp.getCamera().ScreenPointToRay(this.mCurPt);
            float curPtDist = float.NaN;
            pivotPlane.Raycast(curPtRay, out curPtDist);
            Vector3 curPtOnPlane = curPtRay.GetPoint(curPtDist);

            //calculate the position differece between the two points.
            Vector3 offset = curPtOnPlane - prevPtOnPlane;

            //Set stick with updated touch input.
            Vector3 edgeTop = stick.getEdgeTop();
            edgeTop +=  new Vector3(0, offset.y, 0);
            stick.setEdgeTop(edgeTop);
            stick.updateWidgetWithChangedPoints();
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("slideEdgeTop", this.GetType().Name);
            return data;
        }
    }
}