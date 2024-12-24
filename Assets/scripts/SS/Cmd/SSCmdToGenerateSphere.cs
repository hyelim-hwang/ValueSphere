using UnityEngine;
using X;
using SS.Scenario;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToGenerateSphere : XLoggableCmd {
        //fields
        Vector2 mPrevPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mPrevPt2 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt2 = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToGenerateSphere(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToGenerateSphere cmd = new SSCmdToGenerateSphere(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSSphereHandleScenario scenario =
                (SSSphereHandleScenario)SSSphereHandleScenario.getSingleton();
            SSValueSphere vs =
            ((SSApp)scenario.getApp()).getValueSphereMgr().getValueSphere();
            SSTouchMark tm1 = scenario.getManipulatingTouchMarks()[0];
            SSTouchMark tm2 = scenario.getManipulatingTouchMarks()[1];
            this.mCurPt1 = tm1.getRecentPt(0);
            this.mCurPt2 = tm2.getRecentPt(0);
            Vector3 curPtInWorld1 = ((SSApp)this.mApp).
                getPerspCameraPerson().getCamera().ScreenToWorldPoint(
                this.mCurPt1);
            Vector3 curPtInWorld2 = ((SSApp)this.mApp).
                getPerspCameraPerson().getCamera().ScreenToWorldPoint(
                this.mCurPt2);
            Vector3 sphereCenter = (curPtInWorld1 + curPtInWorld2) / 2;
            float sphereRadius = (curPtInWorld1 - sphereCenter).magnitude;
            vs.setRadius(sphereRadius);

            Vector3 spherePos =
                new Vector3(sphereCenter.x, sphereCenter.y, 2);

            vs.setPos(spherePos);
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("generateSphere", this.GetType().Name);
            return data;
        }
    }
}