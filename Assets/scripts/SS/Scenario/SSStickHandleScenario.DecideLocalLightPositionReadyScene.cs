// using UnityEngine;
// using X;
// using UnityEngine.InputSystem;
// using SS.Cmd;
// using SS.Scenario;
// using SS.AppObject;
// using SSAppObject;

// namespace SS.Scenario {
//     public partial class SSStickHandleScenario : XScenario {
//         public class DecideLocalLightPositionReadyScene : SSScene {
//             //singleton pattern
//             private static DecideLocalLightPositionReadyScene mSingleton = null;
//             public static DecideLocalLightPositionReadyScene getSingleton() {
//                 Debug.Assert(
//                     DecideLocalLightPositionReadyScene.mSingleton != null);
//                 return DecideLocalLightPositionReadyScene.mSingleton;
//             }
//             public static DecideLocalLightPositionReadyScene createSingleton(
//                 XScenario scenario) {
//                 Debug.Assert(
//                     DecideLocalLightPositionReadyScene.mSingleton == null);
//                 DecideLocalLightPositionReadyScene.mSingleton =
//                     new DecideLocalLightPositionReadyScene(scenario);
//                 return DecideLocalLightPositionReadyScene.mSingleton;
//             }

//             private DecideLocalLightPositionReadyScene(XScenario scenario) :
//                 base(scenario) {}

//             //event handling methods
//             public override void getReady() {
//                 SSApp ss = (SSApp)this.mScenario.getApp();
//                 SSShadowStickMgr SSMgr= ss.getShadowStickMgr();
//                 SSStickHandleScenario scenario =
//                     (SSStickHandleScenario)this.mScenario;
//                 SSCameraPerson cam = ss.getGridCameraPerson();
//                 if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
//                     SSTouchMark tm =
//                     ss.getTouchMarkMgr().getLastDownTouchMark();
//                     scenario.getManipulatingTouchMarks().Add(tm);
//                 }
//             }

//             public override void handleKeyDown(Key kc) {}

//             public override void handleKeyUp(Key kc) {}

//             public override void handlePenDown(Vector2 pt) {
//             }

//             public override void handlePenDrag(Vector2 pt) {}

//             public override void handlePenUp(Vector2 pt) {}

//             public override void handleEraserDown(Vector2 pt) {
//                 //throw new System.NotImplementedException();
//             }

//             public override void handleEraserDrag(Vector2 pt) {
//                 //throw new System.NotImplementedException();
//             }

//             public override void handleEraserUp(Vector2 pt) {
//                 //throw new System.NotImplementedException();
//             }

//             public override void handleTouchDown() {
//                 SSApp ss = (SSApp)this.mScenario.getApp();
//                 SSShadowStickMgr stickMgr = ss.getShadowStickMgr();
//                 //create sub stick.
//                 SSCmdToCreateSubStick.execute(ss);
//                 XCmdToChangeScene.execute(ss,
//                     SSStickHandleScenario.DecideLocalLightPositionScene.
//                     getSingleton(), null);
//             }

//             public override void handleTouchDrag() {
//             }

//             public override void handleTouchUp() {
//                 SSApp ss = (SSApp)this.mScenario.getApp();
//                 SSStickHandleScenario scenario =
//                     (SSStickHandleScenario)this.mScenario;
//                 SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
//                 if (scenario.getManipulatingTouchMarks().Contains(tm)) {
//                     scenario.getManipulatingTouchMarks().Remove(tm);
//                     XCmdToChangeScene.execute(ss,
//                     SSDefaultScenario.ReadyScene.getSingleton(), null);
//                 }
//             }

//             public override void wrapUp() {}

//             //util function
//         }
//     }
// }