using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSSphereHandleScenario : XScenario {
        public class HandleHighlightScene : SSScene {
            //singleton pattern
            private static HandleHighlightScene mSingleton = null;
            public static HandleHighlightScene getSingleton() {
                Debug.Assert(HandleHighlightScene.mSingleton != null);
                return HandleHighlightScene.mSingleton;
            }
            public static HandleHighlightScene createSingleton(
                XScenario scenario) {
                Debug.Assert(HandleHighlightScene.mSingleton == null);
                HandleHighlightScene.mSingleton =
                    new HandleHighlightScene(scenario);
                return HandleHighlightScene.mSingleton;
            }

            private HandleHighlightScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSSphereHandleScenario scenario =
                (SSSphereHandleScenario)SSSphereHandleScenario.getSingleton();
                SSApp ss = (SSApp)scenario.getApp();
                foreach (
                    SSCursor2D tc in ss.getCursorMgr().getTouchCursors()) {
                    tc.getGameObject().SetActive(false);
                }
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {}

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
                SSCmdToHandlePassiveHighlight.execute(ss);
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSphereHandleScenario scenario =
                    (SSSphereHandleScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                        SSDefaultScenario.ReadyScene.getSingleton(),
                        null);
                }
            }

            public override void wrapUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                foreach (
                    SSCursor2D tc in ss.getCursorMgr().getTouchCursors()) {
                    tc.getGameObject().SetActive(false);
                }
            }
        }
    }
}