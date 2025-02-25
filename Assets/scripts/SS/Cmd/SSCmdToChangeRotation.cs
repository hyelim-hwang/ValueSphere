using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToChangeRotation : XLoggableCmd {
        //fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        //private constructor
        private SSCmdToChangeRotation(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            SSTouchMark touchMark = ss.getTouchMarkMgr().getLastDownTouchMark();
            this.mPrevPt = touchMark.getRecentPt(1);
            this.mCurPt = touchMark.getRecentPt(0);
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToChangeRotation cmd = new SSCmdToChangeRotation(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSGridCameraPerson cp = ss.getGridCameraPerson();

            float dx = this.mCurPt.x - this.mPrevPt.x;
            float dy = this.mCurPt.y - this.mPrevPt.y;
            float dAzimuth = 180f * dx / Screen.width;
            float dZenith = 180f * dy / Screen.height;

            Quaternion qa = Quaternion.AngleAxis(dAzimuth, Vector3.up);
            Quaternion qz = Quaternion.AngleAxis(-dZenith, cp.getRight());
            GameObject cube =
                ss.getSSPerspectiveCubeMgr().getPerspectiveCube().getCube();
            Vector3 cubePos = cube.transform.position;
            Vector3 pivotToEye = cp.getEye() - cubePos;
            Vector3 nextEye = cubePos + qa * pivotToEye;
            Vector3 nextView = qa * cp.getView();

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
