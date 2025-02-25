using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;
using SS.Scenario;
using SS.AppObject;

namespace SS.Scenario {
    public partial class SSStickHandleScenario : XScenario {
        public class ChangeLocalLightPositionScene : SSScene {
            //singleton pattern
            private static ChangeLocalLightPositionScene mSingleton = null;
            public static ChangeLocalLightPositionScene getSingleton() {
                Debug.Assert(
                    ChangeLocalLightPositionScene.mSingleton != null);
                return ChangeLocalLightPositionScene.mSingleton;
            }
            public static ChangeLocalLightPositionScene createSingleton(
                XScenario scenario) {
                Debug.Assert(
                    ChangeLocalLightPositionScene.mSingleton == null);
                ChangeLocalLightPositionScene.mSingleton =
                    new ChangeLocalLightPositionScene(scenario);
                return ChangeLocalLightPositionScene.mSingleton;
            }

            private ChangeLocalLightPositionScene(XScenario scenario) :
                base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSShadowStickMgr SSMgr= ss.getShadowStickMgr();
                SSStickHandleScenario scenario =
                    (SSStickHandleScenario)this.mScenario;
                SSCameraPerson cam = ss.getGridCameraPerson();
                if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
                    SSTouchMark tm =
                        ss.getTouchMarkMgr().getLastDownTouchMark();
                    scenario.getManipulatingTouchMarks().Add(tm);
                }
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {}

            public override void handlePenUp(Vector2 pt) {}

            public override void handleEraserDown(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserDrag(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserUp(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDown() {

            }

            public override void handleTouchDrag() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                if (ss.getTouchMarkMgr().getLastDownTouchMark().getPts().Count >
                    2) {
                    SSCmdToChangeLocalLightPosition.execute(ss);
                }
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSShadowStickMgr stickMgr = ss.getShadowStickMgr();
                SSStickHandleScenario scenario =
                    (SSStickHandleScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    //remove currently manipulating stick from the manipulating
                    //stick list.
                    stickMgr.getManipulatingSticks().RemoveAt(
                        stickMgr.getManipulatingSticks().Count - 1);
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                        SSStickHandleScenario.
                        ChangeSunlightPositionScene.getSingleton(), null);
                }
            }

            public override void wrapUp() {}

            //util function
        }
    }
}