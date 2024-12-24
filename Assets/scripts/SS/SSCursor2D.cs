using UnityEngine;
using SS.AppObject;

namespace SS {
    public class SSCursor2D : SSAppCircle2D {
        //constants
        public static readonly float RADIUS = 2f;
        public static readonly Color COLOR = Color.clear;
        //public static readonly Color COLOR = Color.red;

        //fields
        private SSApp mSS = null;
        private string mId = string.Empty;
        public string getId() {
            return this.mId;
        }

        // constructor
        public SSCursor2D(SSApp ss, string id, string name, float radius,
            Vector2 pt) : base($"{ name }({ id })/Cursor2D", radius,
            SSCursor2D.COLOR) {
            this.mSS = ss;
            this.mId = id;
            this.mGameObject.transform.position = pt;
        }

        //methods
        public bool hits(SSAppGeom3D appGeom3D) {
            Vector2 ctr = this.mGameObject.transform.position;
            SSPerspCameraPerson cp = this.mSS.getPerspCameraPerson();
            Ray ray = cp.getCamera().ScreenPointToRay(ctr);
            RaycastHit hit;
            Collider collider = appGeom3D.getCollider();
            if (collider.Raycast(ray, out hit, Mathf.Infinity)) {
                SSUtil.createDebugSphere(hit.point);
                return true;
            } else {
                return false;
            }
        }
    }
}