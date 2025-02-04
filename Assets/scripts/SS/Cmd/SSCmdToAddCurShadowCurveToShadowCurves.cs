using UnityEngine;
using X;
using SS.AppObject;
using SS.Geom;

namespace SS.Cmd {
    public class SSCmdToAddCurShadowCurveToShadowCurve : XLoggableCmd {
        //fields
        private Vector2 mPt = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToAddCurShadowCurveToShadowCurve(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            this.mPt = ss.getPenMarkMgr().getLastPenMark().getLastPt();
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToAddCurShadowCurveToShadowCurve cmd = new
                SSCmdToAddCurShadowCurveToShadowCurve(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSAppPolyline2D curShadowCurve =
                ss.getShadowCurveMgr().getCurShadowCurve();
            SSPolyline2D shadowCurve = (SSPolyline2D)curShadowCurve.getGeom();
            if (shadowCurve.getPts().Count >= 2) {
                ss.getShadowCurveMgr().getShadowCurves().Add(curShadowCurve);
                ss.getShadowCurveMgr().setCurShadowCurve(null);
                return true;
            } else {
                curShadowCurve.destroyGameObject();
                ss.getShadowCurveMgr().setCurShadowCurve(null);
                return false;
            }
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("addCurShadowCurveToShadowCurves",
                this.GetType().Name);
            return data;
        }
    }
}