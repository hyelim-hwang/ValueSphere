using UnityEngine;
using X;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToUpdateCurShadowCurve : XLoggableCmd {
        //fields
        private Vector2 mPt = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToUpdateCurShadowCurve(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            this.mPt = ss.getPenMarkMgr().getLastPenMark().getLastPt();
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToUpdateCurShadowCurve cmd =
                new SSCmdToUpdateCurShadowCurve(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSAppPolyline2D curShadowCurve =
            ss.getShadowCurveMgr().getCurShadowCurve();
            curShadowCurve.setPts(ss.getPenMarkMgr().getLastPenMark().getPts());
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("updateCurShadowCurve", this.GetType().Name);
            data.addMember("point", this.mPt);
            return data;
        }
    }
}