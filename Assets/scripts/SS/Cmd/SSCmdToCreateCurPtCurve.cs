using System.Collections.Generic;
using UnityEngine;
using X;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToCreateCurPtCurve2D : XLoggableCmd {
        //fields
        private Vector2 mPt = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToCreateCurPtCurve2D(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            this.mPt = ss.getPenMarkMgr().getLastPenMark().getLastPt();
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToCreateCurPtCurve2D cmd = new SSCmdToCreateCurPtCurve2D(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            List<Vector2> pts = new List<Vector2>();
            pts.Add(this.mPt);
            SSValueStroke ptCurve2D =
                new SSValueStroke(SSUtil.createId(), this.mPt, pts,
                ss.getValueStrokeMgr().getStrokeWidth(),
                ss.getValueStrokeMgr().getCurColor(),
                ss.getValueSphereMgr().getValueSphere().getSphere().
                transform.position,
                ss.getValueSphereMgr().getValueSphere().getRadius() / 2);
            ss.getValueStrokeMgr().setCurValueStroke(ptCurve2D);
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("createCurPtCurve", this.GetType().Name);
            return data;
        }
    }
}