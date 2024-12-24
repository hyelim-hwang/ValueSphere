using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;
using SS;

namespace SS.Scenario {
    public partial class SSEraseScenario : XScenario {
        public class EraseScene : SSScene {
            //singleton pattern
            private static EraseScene mSingleton = null;
            public static EraseScene getSingleton() {
                Debug.Assert(EraseScene.mSingleton != null);
                return EraseScene.mSingleton;
            }
            public static EraseScene createSingleton(XScenario scenario) {
                Debug.Assert(EraseScene.mSingleton == null);
                EraseScene.mSingleton = new EraseScene(scenario);
                return EraseScene.mSingleton;
            }

            private EraseScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {}

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                switch(kc) {
                    case Key.E:
                        XCmdToChangeScene.execute(ss, this.mReturnScene, null);
                        break;
                }
            }

            public override void handlePenDown(Vector2 pt) {}

            public override void handlePenDrag(Vector2 pt) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToEraseValueStroke.execute(ss);
            }

            public override void handlePenUp(Vector2 pt) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToTakeSnapshot.execute(ss);
            }

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
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDrag() {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchUp() {
                //throw new System.NotImplementedException();
            }

            public override void wrapUp() {}
        }
    }
}