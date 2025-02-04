using UnityEngine;
using X;
using SS.Scenario;
using SS.AppObject;
using SSAppObject;

namespace SS.Cmd {
    public class SSCmdToConfirmPlane : XLoggableCmd {
        //fields
        Vector2 mPrevPt1 = SSUtil.VECTOR2_NAN;
        Vector2 mCurPt1 = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToConfirmPlane(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToConfirmPlane cmd = new SSCmdToConfirmPlane(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            //envision the grid and hide the cube.
            SSPerspectiveCubeMgr cubeMgr = ss.getSSPerspectiveCubeMgr();
            cubeMgr.makeGridShow();
            cubeMgr.makeCubeTransparent();
            return true;

        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("confirmPlane", this.GetType().Name);
            return data;
        }
    }
}