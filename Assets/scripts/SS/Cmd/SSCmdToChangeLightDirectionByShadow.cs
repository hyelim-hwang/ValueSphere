using System.Collections.Generic;
using SS.AppObject;
using SSAppObject;
using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToChangeLightDirectionByShadow : XLoggableCmd {
        //fields
        //fields
        private Vector3 mPrevPt = Vector3.zero;
        private Vector3 mCurPt = Vector3.zero;

        //private constructor
        private SSCmdToChangeLightDirectionByShadow(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            SSTouchMark touchMark = ss.getTouchMarkMgr().getLastDownTouchMark();
            this.mPrevPt = touchMark.getRecentPt(1);
            this.mCurPt = touchMark.getRecentPt(0);
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToChangeLightDirectionByShadow cmd =
                new SSCmdToChangeLightDirectionByShadow(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSShadowStickMgr stickMgr = ss.getShadowStickMgr();
            SSGridCameraPerson cp = ss.getGridCameraPerson();
            //calculate the ray colliding point on the gound plane.
            Ray prevPtRay = cp.getCamera().ScreenPointToRay(this.mPrevPt);
            float prevPtDist = float.NaN;
            Plane plane = stickMgr.getPlane();
            plane.Raycast(prevPtRay, out prevPtDist);
            Vector3 prevPtOnPlane = prevPtRay.GetPoint(prevPtDist);
            Ray curPtRay = cp.getCamera().ScreenPointToRay(this.mCurPt);
            float curPtDist = float.NaN;
            plane.Raycast(curPtRay, out curPtDist);
            Vector3 curPtOnPlane = curPtRay.GetPoint(curPtDist);
            Vector3 lightDirDiff = curPtOnPlane - prevPtOnPlane;

            //need to be in global position.
            Vector3 updatedShadowTop = stickMgr.getShadowStick().getShadowTop()
                + lightDirDiff;
            Vector3 edgeTop = stickMgr.getShadowStick().getEdgeTop();
            Vector3 reversedLightDir = (edgeTop - updatedShadowTop).normalized;
            Vector3 updatedLightPos = reversedLightDir * 1000f;
            stickMgr.setLightPosition(updatedLightPos);

            return true;

        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("doSomething", this.GetType().Name);
            return data;
        }
    }
}