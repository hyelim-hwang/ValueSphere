using System;
using System.Collections.Generic;
using SS.AppObject;

namespace SS.File {
    [Serializable]
    public class SSSerializableSavedData {
        // fields
        public string savedTime = string.Empty;
        public SSSerializableVector3 eye = null;
        public SSSerializableVector3 view = null;
        public float fov = float.NaN;
        // public SSSerializableValueSphere valueSphere = null;
        public List<SSSerializableValueStroke> valueStrokes = null;

        // constructor
        public SSSerializableSavedData(SSSavedData sd) {
            this.savedTime = sd.getSavedTime().ToString("o"); // ISO 8601 format
            // see https://docs.microsoft.com/en-us/dotnet/standard/base-types/
            // standard-date-and-time-format-strings
            this.eye = new SSSerializableVector3(sd.getEye());
            this.view = new SSSerializableVector3(sd.getView());
            // this.fov = sd.getFov();
            // this.valueSphere = new SSSerializableValueSphere(sd.getValueSphere());
            this.valueStrokes = new List<SSSerializableValueStroke>();
            foreach (SSValueStroke valueStroke in sd.getValueStrokes()) {
                SSSerializableValueStroke sValueStroke =
                    new SSSerializableValueStroke(valueStroke);
                this.valueStrokes.Add(sValueStroke);
            }
        }

        // methods
        public SSSavedData toSavedData() {
            // SSValueSphere valueSphere = this.valueSphere.toValueSphere();
            List<SSValueStroke> valueStrokes = new List<SSValueStroke>();
            foreach(SSSerializableValueStroke serialValueStroke in
                this.valueStrokes) {
                SSValueStroke vst = serialValueStroke.toValueStroke();
                valueStrokes.Add(vst);
            }
            return new SSSavedData(DateTime.Parse(this.savedTime), this.eye.
                toVector3(), this.view.toVector3(), valueStrokes);
        }
    }
}
