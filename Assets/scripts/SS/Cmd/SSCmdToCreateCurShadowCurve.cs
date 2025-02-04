using System.Collections.Generic;
using UnityEngine;
using X;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToCreateCurShadowCurve : XLoggableCmd {
        //fields
        private Vector2 mPt = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToCreateCurShadowCurve(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            this.mPt = ss.getPenMarkMgr().getLastPenMark().getLastPt();
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToCreateCurShadowCurve cmd =
                new SSCmdToCreateCurShadowCurve(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            List<Vector2> pts = new List<Vector2>();
            pts.Add(this.mPt);
            SSAppPolyline2D shadowCurve2D =
                new SSAppPolyline2D("ShadowCurve2D", pts,
                SSShadowCurveMgr.PT_CURVE_WIDTH,
                SSShadowCurveMgr.PT_CURVE_COLOR);
            // shadowCurve2D.getGameObject().layer = 6;
            ss.getShadowCurveMgr().setCurShadowCurve(shadowCurve2D);
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("createCurShadowCurve", this.GetType().Name);
            return data;
        }
    }
}