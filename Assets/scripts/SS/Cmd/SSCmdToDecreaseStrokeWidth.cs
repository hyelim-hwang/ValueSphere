using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToDecreaseStrokeWidth: XLoggableCmd {
        //fields
        private Vector2 mPt = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToDecreaseStrokeWidth(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            this.mPt = ss.getPenMarkMgr().getLastPenMark().getLastPt();
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToDecreaseStrokeWidth cmd = new SSCmdToDecreaseStrokeWidth(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            float width = ss.getValueStrokeMgr().getStrokeWidth();
            ss.getValueStrokeMgr().setStrokeWidth(width / 1.2f);
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("decreaseStrokeWidth", this.GetType().Name);
            data.addMember("point", this.mPt);
            return data;
        }
    }
}