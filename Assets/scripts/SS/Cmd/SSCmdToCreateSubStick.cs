// using SS.AppObject;
// using SSAppObject;
// using UnityEngine;
// using SS.Scenario;
// using X;
// using UnityEngine.UIElements;

// namespace SS.Cmd {
//     public class SSCmdToCreateSubStick : XLoggableCmd {
//         //fields
//         Vector3 mPrevPt1 = Vector3.negativeInfinity;
//         Vector3 mCurPt1 = Vector3.negativeInfinity;
//         Vector3 mPrevPt2 = Vector3.negativeInfinity;
//         Vector3 mCurPt2 = Vector3.negativeInfinity;

//         //private constructor
//         private SSCmdToCreateSubStick(XApp app) : base(app) {}

//         //static method to construct and execute this command
//         public static bool execute(XApp app) {
//             SSCmdToCreateSubStick cmd =
//                 new SSCmdToCreateSubStick(app);
//             return cmd.execute();
//         }

//         protected override bool defineCmd() {
//             SSApp ss = (SSApp)this.mApp;
//             SSStickHandleScenario scenario =
//                 (SSStickHandleScenario)SSStickHandleScenario.getSingleton();
//             SSShadowStickMgr stickMgr = ss.getShadowStickMgr();
//             SSStick stick = stickMgr.getShadowStick();
//             SSGridCameraPerson cp = ss.getGridCameraPerson();
//             // Debug.LogError(scenario.getManipulatingTouchMarks().Count);
//             // Debug.Assert(scenario.getManipulatingTouchMarks().Count >= 3);
//             // SSTouchMark tm1 = scenario.getManipulatingTouchMarks()[0];
//             // SSTouchMark tm2 = scenario.getManipulatingTouchMarks()[1];
//             // if (tm1.getPts().Count == 1 || tm2.getPts().Count == 1) {
//             //     return true;
//             // }
//             // this.mCurPt1 = tm1.getRecentPt(0);
//             // this.mPrevPt1 = tm1.getRecentPt(1);
//             // this.mCurPt2 = tm2.getRecentPt(0);
//             // this.mPrevPt2 = tm2.getRecentPt(1);

//             SSSubStick subStick =
//                 new SSSubStick("subStick", stick.getPlane(),
//                 stickMgr.getLightDirection());
//             stick.setSubStick(subStick);


//             return true;
//         }

//         protected override XJson createLogData() {
//             XJson data = new XJson();
//             data.addMember("doSomething", this.GetType().Name);
//             return data;
//         }
//     }
// }