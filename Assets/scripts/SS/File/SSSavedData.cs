using System;
using System.Collections.Generic;
using SS.AppObject;
using UnityEngine;

namespace SS {
    public class SSSavedData {
        // fields
        private DateTime mSavedTime;
        public DateTime getSavedTime() {
            return this.mSavedTime;
        }
        private Vector3 mEye = SSUtil.VECTOR3_NAN;
        public Vector3 getEye() {
            return this.mEye;
        }
        private Vector3 mView = SSUtil.VECTOR3_NAN;
        public Vector3 getView() {
            return this.mView;
        }

        private List<SSValueStroke> mValueStrokes = null;
        public List<SSValueStroke> getValueStrokes() {
            return this.mValueStrokes;
        }

        // constructor
        public SSSavedData(DateTime savedTime, Vector3 eye, Vector3 view,
        List<SSValueStroke> valueStrokes) {
            this.mSavedTime = savedTime;
            this.mEye = eye;
            this.mView = view;
            this.mValueStrokes = valueStrokes;
        }
    }
}

