using UnityEngine;
using X;

namespace SS.Cmd {
    public class SSCmdToChangeFOV : XLoggableCmd {
        //fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        //private constructor
        private SSCmdToChangeFOV(XApp app) : base(app) {
            SSApp ss = (SSApp)this.mApp;
            SSTouchMark touchMark = ss.getTouchMarkMgr().getLastDownTouchMark();
            this.mPrevPt = touchMark.getRecentPt(1);
            this.mCurPt = touchMark.getRecentPt(0);
        }

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToChangeFOV cmd = new SSCmdToChangeFOV(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
    SSGridCameraPerson cp = ss.getGridCameraPerson();
    Camera cam = cp.getCamera();
    SSPerspectiveCubeMgr cubeMgr = ss.getSSPerspectiveCubeMgr();
    GameObject cube = cubeMgr.getPerspectiveCube().getCube();

    // 현재 FOV 저장
            float prevFOV = cam.fieldOfView;

            // 터치한 꼭짓점의 화면 좌표
            Vector3 targetVertexScreen = cam.WorldToScreenPoint(-0.5f * Vector3.zero);
            float prevDist = Vector2.Distance(this.mPrevPt, targetVertexScreen);
            float curDist = Vector2.Distance(this.mCurPt, targetVertexScreen);

            // 스케일링 비율 계산 (FOV 변경)
            float scale = curDist / prevDist;
            float newFOV = Mathf.Clamp(prevFOV * scale, 15f, 90f); // FOV 제한 설정

            // 돌리 줌: 카메라 위치 조정
            Vector3 camPos = cam.transform.position;
            float prevDistance = Vector3.Distance(camPos, -0.5f * Vector3.zero);
            float newDistance = (prevDistance * Mathf.Tan(prevFOV * 0.5f * Mathf.Deg2Rad))
                                / Mathf.Tan(newFOV * 0.5f * Mathf.Deg2Rad);

            Vector3 camDir = (camPos - (-0.5f) * Vector3.zero).normalized;
            Vector3 newCamPos = -0.5f * Vector3.zero + camDir * newDistance;

            // FOV 적용 전에 카메라 이동 보정
            cam.transform.position = newCamPos;
            cam.fieldOfView = newFOV;

            return true;
        }

        protected override XJson createLogData()
        {
            XJson data = new XJson();
            data.addMember("changeFOV", this.GetType().Name);
            data.addMember("PrevPt", this.mPrevPt);
            data.addMember("CurPt", this.mCurPt);
            return data;
        }
    }
}
