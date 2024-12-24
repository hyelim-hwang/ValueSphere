using UnityEngine;
using X;
using SS.Scenario;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToMoveSphere : XLoggableCmd {
        //fields
        Vector2 mPrevPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt1 = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToMoveSphere(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToMoveSphere cmd = new SSCmdToMoveSphere(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSValueStrokeMgr VSMgr = ss.getValueStrokeMgr();
            SSSphereHandleScenario scenario =
                (SSSphereHandleScenario)SSSphereHandleScenario.getSingleton();
            SSValueSphere vs = ((SSApp)scenario.getApp()).getValueSphereMgr().
                getValueSphere();
            SSTouchMark tm1 = scenario.getManipulatingTouchMarks()[0];
            if (tm1.getPts().Count == 1) {
                return true;
            }
            this.mCurPt1 = tm1.getRecentPt(0);
            this.mPrevPt1 = tm1.getRecentPt(1);

            //define variables.
            Vector3 prevPtInWorld = ((SSApp)this.mApp).
                getPerspCameraPerson().getCamera().ScreenToWorldPoint(
                this.mPrevPt1);
            Vector3 CurPtInWorld = ((SSApp)this.mApp).
                getPerspCameraPerson().getCamera().ScreenToWorldPoint(
                this.mCurPt1);

            //Get current sphere's pos and rad.
            Vector3 sphereCenter = vs.getSphere().transform.position;
            Vector3 prevPos = sphereCenter;

            //Set sphere with updated touch input.
            sphereCenter = sphereCenter + (CurPtInWorld - prevPtInWorld);
            Vector3 spherePos =
                new Vector3(sphereCenter.x, sphereCenter.y, 2.0f);
            vs.setPos(spherePos);
            Vector3 curPos = spherePos;

            //update stroke's valueCoordinate.
            VSMgr.updateValueCoordinate();
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("moveSphere", this.GetType().Name);
            return data;
        }
    }
}