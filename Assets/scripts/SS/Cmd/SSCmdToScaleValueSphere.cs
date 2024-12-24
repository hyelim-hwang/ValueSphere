using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using X;
using System.Text;
using SS.Scenario;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToScaleValueSphere : XLoggableCmd {
        //fields
        Vector2 mPrevPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mPrevPt2 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt2 = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToScaleValueSphere(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToScaleValueSphere cmd = new SSCmdToScaleValueSphere(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSValueStrokeMgr VSMgr = ss.getValueStrokeMgr();
            SSSphereHandleScenario scenario =
                (SSSphereHandleScenario)SSSphereHandleScenario.getSingleton();
            SSValueSphere vs= ((SSApp)scenario.getApp()).getValueSphereMgr().
                getValueSphere();
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

            //define variables.
            Vector2 prevPtsAvg = (this.mPrevPt1 + this.mPrevPt2) / 2;
            Vector2 CurPtsAvg = (this.mCurPt1 + this.mCurPt2) / 2;
            Vector3 prevPtsAvgInWorld = ((SSApp)this.mApp).
                getPerspCameraPerson().getCamera().ScreenToWorldPoint(
                prevPtsAvg);
            Vector3 CurPtsAvgInWorld = ((SSApp)this.mApp).
                getPerspCameraPerson().getCamera().ScreenToWorldPoint(
                CurPtsAvg);
            Vector3 curPtDist =
                new Vector3((this.mCurPt1 - this.mCurPt2).x,
                (this.mCurPt1 - this.mCurPt2).y, 0);
            Vector3 prevPtDist =
                new Vector3((this.mPrevPt1 - this.mPrevPt2).x,
                (this.mPrevPt1 - this.mPrevPt2).y, 0);

            float prevPtsDist = (this.mPrevPt1 - this.mPrevPt2).magnitude;
            float CurPtsDist = (this.mCurPt1 - this.mCurPt2).magnitude;
            float scale = CurPtsDist / prevPtsDist;

            //Get current sphere's pos and rad.
            Vector3 sphereCenter = vs.getSphere().transform.position;
            float sphereRadius = vs.getRadius();

            //Set sphere with updated touch input.
            sphereRadius *= scale;
            vs.setRadius(sphereRadius);
            sphereCenter =
            sphereCenter + (CurPtsAvgInWorld - prevPtsAvgInWorld);
            Vector3 spherePos =
                new Vector3(sphereCenter.x, sphereCenter.y, 2.0f);
            vs.setPos(spherePos);
            Vector3 curUpDir = Vector3.Cross(Vector3.forward, curPtDist);
            Vector3 prevUpDir = Vector3.Cross(Vector3.forward, prevPtDist);
            Quaternion rot = Quaternion.FromToRotation(prevUpDir, curUpDir);

            Quaternion newRot = rot * vs.getRot();
            vs.setRot(newRot);
            VSMgr.updateValueCoordinate();
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("scaleValueSphere", this.GetType().Name);
            return data;
        }
    }
}