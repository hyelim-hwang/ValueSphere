using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToChangePOV : XLoggableCmd {
        //fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        //private constructor
        private SSCmdToChangePOV(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            SSTouchMark touchMark = ss.getTouchMarkMgr().getLastDownTouchMark();
            this.mPrevPt = touchMark.getRecentPt(1);
            this.mCurPt = touchMark.getRecentPt(0);
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToChangePOV cmd = new SSCmdToChangePOV(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSGridCameraPerson cp = ss.getGridCameraPerson();
            Camera cam = cp.getCamera();

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

            //get the distance btw cam and object.
            Ray distPtRay = cp.getCamera().ScreenPointToRay(cp.getEye());
            float distPtDist = float.NaN;
            pivotPlane.Raycast(distPtRay, out distPtDist);

            // 피벗(기준점)과 카메라 간의 거리 (Depth)
    float depth = Vector3.Distance(cam.transform.position, cp.getPivot());

    // 터치 지점의 뷰포트 좌표 (0~1 범위)
    Vector2 prevViewportPos = cam.ScreenToViewportPoint(this.mPrevPt);
    Vector2 curViewportPos = cam.ScreenToViewportPoint(this.mCurPt);

    // Viewport에서의 Y축 이동량 (정규화된 0~1 값)
    float viewportDeltaY = curViewportPos.y - prevViewportPos.y;

    // Viewport 이동량을 월드 이동량으로 변환
    float worldMoveY = viewportDeltaY * depth * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad) * 2f;

    // Y축 이동량 적용
    Vector3 yDiff = new Vector3(0, worldMoveY, 0);

    // 카메라 이동 적용
    cp.setEye(cp.getEye() - yDiff);

            return true;
        }

        protected override XJson createLogData()
        {
            XJson data = new XJson();
            data.addMember("ChangePOV", this.GetType().Name);
            data.addMember("PrevPt", this.mPrevPt);
            data.addMember("CurPt", this.mCurPt);
            return data;
        }
    }
}
