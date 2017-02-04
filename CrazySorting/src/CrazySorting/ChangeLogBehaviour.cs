using UnityEngine;
using UnityEngine.UI;

namespace CrazySorting
{

    class ChangeLogBehaviour : MonoBehaviour
    {
        public Text ChangeLogUIText;
        public TextAsset ChangeLog;

        void Awake()
        {
            var text = ChangeLog.text;

            if (text.Length == 0 || PlayerPrefs.GetInt("ChangeLogLength") == text.Length)
            {
                Dismiss();
                return;
            }

            ChangeLogUIText.text = ChangeLog.text;

            PlayerPrefs.SetInt("ChangeLogLength", text.Length);

        }

        public void Dismiss()
        {
            DestroyImmediate(this.gameObject);
        }
    }
}
