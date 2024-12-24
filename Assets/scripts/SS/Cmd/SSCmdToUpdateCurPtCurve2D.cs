using UnityEngine;
using X;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToUpdateCurPtCurve2D : XLoggableCmd {
        //fields
        private Vector2 mPt = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToUpdateCurPtCurve2D(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            this.mPt = ss.getPenMarkMgr().getLastPenMark().getLastPt();
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToUpdateCurPtCurve2D cmd = new SSCmdToUpdateCurPtCurve2D(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSValueStroke curPtCurve2D =
            ss.getValueStrokeMgr().getCurValueStroke();
            curPtCurve2D.setPts(ss.getPenMarkMgr().getLastPenMark().getPts());
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("updateCurPtCurve2D", this.GetType().Name);
            data.addMember("point", this.mPt);
            return data;
        }
    }
}