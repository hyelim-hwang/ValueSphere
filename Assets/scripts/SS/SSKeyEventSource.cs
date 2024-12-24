using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace SS {
    public class SSKeyEventSource {
        //constants
        private static readonly List<Key> WATCHING_KEYS =
            new List<Key>() {
                Key.LeftCtrl, // for rotating
                Key.LeftAlt, // for translating
                Key.Enter, // for creating a standing card
                Key.Z, // undo
                Key.Y, // redo
                Key.S, // for saving file
                Key.O, // for opening file
                Key.UpArrow, // for stroke width modification
                Key.DownArrow, // for stroke width modification
                Key.S, // for saving file
                Key.O, // for opening file
                Key.H, //for calling hairdryer file
                Key.E, //for erasing valuestrokes
                Key.C, //for clear all the valuestrokes
                Key.R //for calling robot file
            };

        //fields
        private SSEventListener mEventListener = null;
        public void setEventListener(SSEventListener eventListner) {
            this.mEventListener = eventListner;
        }

        //constructor
        public class SSkeyEventSource {}

        //methods
        public void update() {
            foreach (Key k in SSKeyEventSource.WATCHING_KEYS) {
                if (Keyboard.current[k].wasPressedThisFrame) {
                    this.mEventListener.keyPressed(k);
                }
                if (Keyboard.current[k].wasReleasedThisFrame) {
                    this.mEventListener.keyReleased(k);
                }
            }
        }
    }
}
