using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToChangePosition : XLoggableCmd {
        //fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        //private constructor
        private SSCmdToChangePosition(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            SSTouchMark touchMark = ss.getTouchMarkMgr().getLastDownTouchMark();
            this.mPrevPt = touchMark.getRecentPt(1);
            this.mCurPt = touchMark.getRecentPt(0);
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToChangePosition cmd = new SSCmdToChangePosition(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            Camera cam = ss.getGridCameraPerson().getCamera();
            Vector2 posDiff = this.mCurPt - this.mPrevPt;
            Matrix4x4 matrix = cam.projectionMatrix;
            matrix.m02 -= posDiff.x / Screen.width; // 반전 적용
            matrix.m12 -= posDiff.y / Screen.height; // 반전 적용
            cam.projectionMatrix = matrix;

            return true;
        }

        protected override XJson createLogData()
        {
            XJson data = new XJson();
            data.addMember("ChangePosition", this.GetType().Name);
            data.addMember("PrevPt", this.mPrevPt);
            data.addMember("CurPt", this.mCurPt);
            return data;
        }
    }
}
