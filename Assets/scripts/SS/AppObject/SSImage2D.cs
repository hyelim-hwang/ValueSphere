using UnityEngine;
using UnityEngine.UI;

namespace SS.AppObject {
    public class SSImage2D : SSAppRect2D {
        // fields
        private string mFolderName = string.Empty;
        public string getFolderName() {
            return mFolderName;
        }
        private string mSubFolderName = string.Empty;
        private string mFilename = string.Empty;
        public string getFilename() {
            return this.mFilename;
        }

        private float mAlpha = 1f;
        public float getAlpha() { return this.mAlpha; }
        public void setAlpha(float alpha) {
            Color color = this.mGameObject.GetComponent<RawImage>().color;
            color.a = Mathf.Clamp01(alpha); // Ensure opacity is between 0 and 1
            this.mGameObject.GetComponent<RawImage>().color = color;
            this.mAlpha = alpha;
        }

        // constructor
        public SSImage2D(Texture2D texture, Vector2 size, Vector2 center) :
            base("Image", size.x, size.y, Color.clear) {

            // init canvas
            this.mGameObject.AddComponent<Canvas>();

            // load img file
            this.mGameObject.AddComponent<RawImage>().texture = texture;

            this.mGameObject.GetComponent<RawImage>().material =
                new Material(Shader.Find("UI/Unlit/Transparent"));

            // Texture2D source = (Texture2D) this.mGameObject.
            //     GetComponent<RawImage>().texture;

            this.setSize(size);
            this.setPosition(center);

            // // init
            // this.mGameObject.SetActive(true);
        }

        // for images in the Resource folder
        public SSImage2D(string folderName, string fileName, Vector2 size,
            Vector2 center) : base(fileName, size.x, size.y, Color.clear) {

            this.mFolderName = folderName;
            this.mFilename = fileName;

            // init canvas
            this.mGameObject.AddComponent<Canvas>();

            // load img file
            Texture texture = Resources.
                Load<Texture>(folderName + "/" + fileName);
            if (texture == null) {
                Debug.Log("Not loaded");
            }
            this.mGameObject.AddComponent<RawImage>().texture = texture;

            this.mGameObject.GetComponent<RawImage>().material =
                new Material(Shader.Find("UI/Unlit/Transparent"));

            // Texture2D source = (Texture2D) this.mGameObject.
            //     GetComponent<RawImage>().texture;

            this.setSize(size);
            this.setPosition(center);

            // init
            this.mGameObject.SetActive(true);
        }

        public SSImage2D(string folderName, string subFolderName,
            string fileName) : base("Image", 0f, 0f, Color.clear) {

            this.mFolderName = folderName;
            this.mSubFolderName = subFolderName;
            this.mFilename = fileName;

            // init canvas
            this.mGameObject.AddComponent<Canvas>();

            // load img file
            this.mGameObject.AddComponent<RawImage>().texture =
                Resources.Load<Texture>(folderName + "/" + subFolderName + "/"
                    + fileName);

            this.mGameObject.GetComponent<RawImage>().material =
                new Material(Shader.Find("UI/Unlit/Transparent"));

            Texture2D source = (Texture2D) this.mGameObject.
                GetComponent<RawImage>().texture;

            // init
            this.mGameObject.SetActive(true);
        }

        // methods
        public new void setSize(float width, float height) {
            this.mGameObject.GetComponent<RawImage>().rectTransform.sizeDelta =
                new Vector2(width, height);
            base.setSize(width, height);
        }

        public void setSize(Vector2 size) {
            this.mGameObject.GetComponent<RawImage>().rectTransform.sizeDelta =
                size;
            base.setSize(size.x, size.y);
        }

        // public void setPosition(Vector2 pos) {
        //     this.getGameObject().transform.position = pos;
        // }

        public new void setRenderQueue(int i) {
            this.mGameObject.GetComponent<RawImage>().material.renderQueue = i;
        }
    }
}