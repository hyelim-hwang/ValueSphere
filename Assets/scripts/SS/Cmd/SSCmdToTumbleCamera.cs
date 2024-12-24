using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToTumbleCamera : XLoggableCmd {
        //fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        //private constructor
        private SSCmdToTumbleCamera(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            SSPenMark penMark = ss.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = penMark.getRecentPt(1);
            this.mCurPt = penMark.getRecentPt(0);
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToTumbleCamera cmd = new SSCmdToTumbleCamera(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSPerspCameraPerson cp = ss.getPerspCameraPerson();

            float dx = this.mCurPt.x - this.mPrevPt.x;
            float dy = this.mCurPt.y - this.mPrevPt.y;
            float dAzimuth = 180f * dx / Screen.width;
            float dZenith = 180f * dy / Screen.height;

            Quaternion qa = Quaternion.AngleAxis(dAzimuth, Vector3.up);
            Quaternion qz = Quaternion.AngleAxis(-dZenith, cp.getRight());

            Vector3 pivotToEye = cp.getEye() - cp.getPivot();
            Vector3 nextEye = cp.getPivot() + qa * qz * pivotToEye;
            Vector3 nextView = qa * qz * cp.getView();

            cp.setEye(nextEye);
            cp.setView(nextView);

            return true;
        }

        protected override XJson createLogData()
        {
            XJson data = new XJson();
            data.addMember("tumbleCamera", this.GetType().Name);
            data.addMember("PrevPt", this.mPrevPt);
            data.addMember("CurPt", this.mCurPt);
            return data;
        }
    }
}
