using UnityEngine;
using X;
using UnityEngine.InputSystem;

namespace SS {
    public abstract class SSScene : XScene {
        //constructor
        protected SSScene(XScenario scenario) : base(scenario) {}

        //methods
        public abstract void handleKeyDown(Key kc);
        public abstract void handleKeyUp(Key kc);
        public abstract void handlePenDown(Vector2 pt);
        public abstract void handlePenDrag(Vector2 pt);
        public abstract void handlePenUp(Vector2 pt);
        public abstract void handleEraserDown(Vector2 pt);
        public abstract void handleEraserDrag(Vector2 pt);
        public abstract void handleEraserUp(Vector2 pt);
        public abstract void handleTouchDown();
        public abstract void handleTouchDrag();
        public abstract void handleTouchUp();
    }
}
