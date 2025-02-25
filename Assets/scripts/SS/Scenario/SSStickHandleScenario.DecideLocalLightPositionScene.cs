// using UnityEngine;
// using X;
// using UnityEngine.InputSystem;
// using SS.Cmd;
// using SS.Scenario;
// using SS.AppObject;

// namespace SS.Scenario {
//     public partial class SSStickHandleScenario : XScenario {
//         public class DecideLocalLightPositionScene : SSScene {
//             //singleton pattern
//             private static DecideLocalLightPositionScene mSingleton = null;
//             public static DecideLocalLightPositionScene getSingleton() {
//                 Debug.Assert(
//                     DecideLocalLightPositionScene.mSingleton != null);
//                 return DecideLocalLightPositionScene.mSingleton;
//             }
//             public static DecideLocalLightPositionScene createSingleton(
//                 XScenario scenario) {
//                 Debug.Assert(
//                     DecideLocalLightPositionScene.mSingleton == null);
//                 DecideLocalLightPositionScene.mSingleton =
//                     new DecideLocalLightPositionScene(scenario);
//                 return DecideLocalLightPositionScene.mSingleton;
//             }

//             private DecideLocalLightPositionScene(XScenario scenario) :
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
//                 XCmdToChangeScene.execute(ss,
//                     SSStickHandleScenario.UpdateSubStickScene.
//                     getSingleton(), null);
//             }

//             public override void handleTouchDrag() {
//                 SSApp ss = (SSApp)this.mScenario.getApp();
//             }

//             public override void handleTouchUp() {
//                 SSApp ss = (SSApp)this.mScenario.getApp();
//                 SSStickHandleScenario scenario =
//                     (SSStickHandleScenario)this.mScenario;
//                 SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
//                 if (scenario.getManipulatingTouchMarks().Contains(tm)) {
//                     scenario.getManipulatingTouchMarks().Remove(tm);
//                     XCmdToChangeScene.execute(ss,
//                     SSStickHandleScenario.DecideLocalLightPositionReadyScene.
//                     getSingleton(), null);
//                 }
//             }

//             public override void wrapUp() {}

//             //util function
//         }
//     }
// }