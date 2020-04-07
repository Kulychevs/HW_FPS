using UnityEngine;
using UnityEditor;


namespace ARPGFrame.Editor
{
    public class CustomHelper
    {
        #region Methods

        [MenuItem("CustomHelper/Set prefabs on map %q")]
        private static void PickObjectCreator()
        {
            var temp = Object.FindObjectOfType<CreateObjectOnMap>();
            if (!temp)
                temp = new GameObject("ObjectSetter").AddComponent<CreateObjectOnMap>();
            Selection.activeGameObject = temp.gameObject;
        }

        #endregion
    }
}
