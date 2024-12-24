using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToDollyCamera : XLoggableCmd {
        //fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        //private constructor
        private SSCmdToDollyCamera(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            SSPenMark penMark = ss.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = penMark.getRecentPt(1);
            this.mCurPt = penMark.getRecentPt(0);
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToDollyCamera cmd = new SSCmdToDollyCamera(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSPerspCameraPerson cp = ss.getPerspCameraPerson();

            //create a plane on a pivot, directly facing the camera.
            Plane pivotPlane = new Plane(-cp.getView(), cp.getPivot());

            //project the previous screen point to the plane.
            Ray prevPtRay = cp.getCamera().ScreenPointToRay(this.mPrevPt);
            float prevPtDist = float.NaN;
            pivotPlane.Raycast(prevPtRay, out prevPtDist);
            Vector3 prevPtOnPlane = prevPtRay.GetPoint(prevPtDist);

            //project the current screen point to the plane.
            Ray curPtRay = cp.getCamera().ScreenPointToRay(this.mCurPt);
            float curPtDist = float.NaN;
            pivotPlane.Raycast(curPtRay, out curPtDist);
            Vector3 curPtOnPlane = curPtRay.GetPoint(curPtDist);

            //calculate the position differece between the two points.
            Vector3 offset = curPtOnPlane - prevPtOnPlane;

            //update the position of the camera.
            cp.setEye(cp.getEye() - offset);

            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("dollyCamera", this.GetType().Name);
            data.addMember("PrevPt", this.mPrevPt);
            data.addMember("curPt", this.mCurPt);
            //comment out the type that cannot be converted to json type rn
            //data.addMember("selectedStandingCard", this.mSelectedStandingCard);

            return data;
        }
    }
}