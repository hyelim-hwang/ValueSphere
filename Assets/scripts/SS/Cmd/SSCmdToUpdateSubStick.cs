// using SS.AppObject;
// using SSAppObject;
// using UnityEngine;
// using SS.Scenario;
// using X;
// using UnityEngine.UIElements;
// using System.Collections.Generic;

// namespace SS.Cmd {
//     public class SSCmdToUpdateSubStick : XLoggableCmd {
//         //fields
//         Vector3 mPrevPt1 = Vector3.negativeInfinity;
//         Vector3 mCurPt1 = Vector3.negativeInfinity;
//         Vector3 mPrevPt2 = Vector3.negativeInfinity;
//         Vector3 mCurPt2 = Vector3.negativeInfinity;

//         //private constructor
//         private SSCmdToUpdateSubStick(XApp app) : base(app) {}

//         //static method to construct and execute this command
//         public static bool execute(XApp app) {
//             SSCmdToUpdateSubStick cmd =
//                 new SSCmdToUpdateSubStick(app);
//             return cmd.execute();
//         }

//         protected override bool defineCmd() {
//             SSApp ss = (SSApp)this.mApp;
//             SSStickHandleScenario scenario =
//                 (SSStickHandleScenario)SSStickHandleScenario.getSingleton();
//             SSShadowStickMgr stickMgr = ss.getShadowStickMgr();
//             SSStick stick = stickMgr.getShadowStick();
//             SSGridCameraPerson cp = ss.getGridCameraPerson();
//             Debug.LogError(scenario.getManipulatingTouchMarks().Count);
//             Debug.Assert(scenario.getManipulatingTouchMarks().Count >= 3);
//             SSTouchMark tm1 = scenario.getManipulatingTouchMarks()[1];
//             SSTouchMark tm2 = scenario.getManipulatingTouchMarks()[2];
//             if (tm1.getPts().Count == 1 || tm2.getPts().Count == 1) {
//                 return true;
//             }
//             this.mCurPt1 = tm1.getRecentPt(0);
//             this.mCurPt2 = tm2.getRecentPt(0);

//             SSSubStick subStick = stick.getSubStick();
//             Plane pivotPlane = stick.getPlane();

//             //match the substick's size to the original stick.
//             subStick.setEdgeTop(stick.getEdgeTop());
//             subStick.setEdgeBottom(stick.getEdgeBottom());

//             //project the previous screen point to the plane.
//             Ray cur1PtRay = cp.getCamera().ScreenPointToRay(this.mCurPt1);
//             float cur1PtDist = float.NaN;
//             pivotPlane.Raycast(cur1PtRay, out cur1PtDist);
//             Vector3 cur1PtOnPlane = cur1PtRay.GetPoint(cur1PtDist);

//             //project the current screen point to the plane.
//             Ray cur2PtRay = cp.getCamera().ScreenPointToRay(this.mCurPt2);
//             float cur2PtDist = float.NaN;
//             pivotPlane.Raycast(cur2PtRay, out cur2PtDist);
//             Vector3 cur2PtOnPlane = cur2PtRay.GetPoint(cur2PtDist);

//             //calculate the shadow direction.
//             Vector3 subStickShadowDirection = cur2PtOnPlane - cur1PtOnPlane;
//             subStick.getGameObject().transform.position = cur1PtOnPlane;

//             //update the substick widget with changed shadow direction.
//             Vector3 stickShadowDirection =
//                 stick.getShadowTop() - stick.getShadowBottom();
//             if (SSUtil.FindLineIntersection(
//                 stick.getGameObject().transform.position, stickShadowDirection,
//                 cur2PtOnPlane, subStickShadowDirection,
//                 out Vector3 intersection)) {
//                 Debug.Log("Intersection at: " + intersection);
//             } else {
//                 Debug.Log("No intersection.");
//             }
//             SSUtil.FindLineIntersection(
//                 intersection, pivotPlane.normal,
//                 stick.getEdgeTop(), -stickMgr.getLightDirection(),
//                 out Vector3 lightPosition);
//             Vector3 lightVectorAtSubStick =
//                 subStick.getEdgeTop() - lightPosition;
//             subStick.setLightDirection(lightVectorAtSubStick);
//             return true;
//         }

//         protected override XJson createLogData() {
//             XJson data = new XJson();
//             data.addMember("doSomething", this.GetType().Name);
//             return data;
//         }
//     }
// }