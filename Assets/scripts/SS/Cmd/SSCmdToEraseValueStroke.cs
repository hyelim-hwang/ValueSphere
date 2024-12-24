using X;
using UnityEngine;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToEraseValueStroke: XLoggableCmd {
        //fields

        //private constructor
        private SSCmdToEraseValueStroke(XApp app) : base(app) { }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToEraseValueStroke cmd = new SSCmdToEraseValueStroke(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            Camera cam = ss.getPerspCameraPerson().getCamera();
            Vector2 lastPt = ss.getPenMarkMgr().getLastPenMark().getLastPt();
            SSValueStrokeMgr mgr = ss.getValueStrokeMgr();
            Vector3 worldPt = cam.ScreenToWorldPoint(lastPt);
            SSValueStroke strokeToRemove = null;

            foreach (SSValueStroke vs in mgr.getValueStrokes()) {
                if (CheckCollision(lastPt, vs)) {
                    strokeToRemove = vs;
                    break;
                }
            }

            //erase only collided strokes
            if (strokeToRemove != null) {
                mgr.getValueStrokes().Remove(strokeToRemove);
                return true;
            }
            return true;
        }

        //util methods
        bool CheckCollision(Vector2 lastPt, SSValueStroke vs) {
            //collision check btw vs Collider2D's position and lastPt
            EdgeCollider2D collider =
                vs.getGameObject().GetComponent<EdgeCollider2D>();
            if (collider != null && collider.OverlapPoint(lastPt)) {
                Debug.LogWarning("erase stroke");
                vs.destroyGameObject();
                return true;
            } else {
                return false;
            }
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("eraseValueStroke", this.GetType().Name);
            return data;
        }
    }
}