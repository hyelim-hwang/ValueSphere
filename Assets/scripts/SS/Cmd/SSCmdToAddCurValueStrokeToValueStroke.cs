using UnityEngine;
using X;
using SS.AppObject;
using SS.Geom;

namespace SS.Cmd {
    public class SSCmdToAddCurValueStrokeToValueStroke : XLoggableCmd {
        //fields
        private Vector2 mPt = SSUtil.VECTOR2_NAN;

        //private constructor
        private SSCmdToAddCurValueStrokeToValueStroke(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            this.mPt = ss.getPenMarkMgr().getLastPenMark().getLastPt();
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToAddCurValueStrokeToValueStroke cmd = new
                SSCmdToAddCurValueStrokeToValueStroke(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSValueStroke curValueStroke =
            ss.getValueStrokeMgr().getCurValueStroke();
            SSPolyline2D polyline = (SSPolyline2D)curValueStroke.getGeom();
            if (polyline.getPts().Count >= 2) {
                ss.getValueStrokeMgr().getValueStrokes().Add(curValueStroke);
                ss.getValueStrokeMgr().setCurValueStroke(null);
                return true;
            } else {
                curValueStroke.destroyGameObject();
                ss.getValueStrokeMgr().setCurValueStroke(null);
                return false;
            }
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("addCurValueStrokeToValueStroke",
                this.GetType().Name);
            return data;
        }
    }
}