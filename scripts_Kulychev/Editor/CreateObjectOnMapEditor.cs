using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace ARPGFrame.Editor
{
	[CustomEditor(typeof(CreateObjectOnMap))]
	public class CreateObjectOnMapEditor : UnityEditor.Editor
	{
		#region Fields

		private CreateObjectOnMap _testTarget;

		#endregion


		#region UnityMethods

		private void OnEnable()
		{
			_testTarget = (CreateObjectOnMap)target;
		}


		private void OnSceneGUI()
		{
			if (Event.current.button == 0 && Event.current.type == EventType.MouseDown)
			{
				Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x,
					SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y));

				if (Physics.Raycast(ray, out var hit))
				{
					_testTarget.InstantiateObj(hit.point);
					SetObjectDirty(_testTarget.gameObject);
				}
			}
			Selection.activeGameObject = _testTarget.gameObject;
		}

		#endregion


		#region Methods

		public void SetObjectDirty(GameObject obj)
		{
			if (!Application.isPlaying)
			{
				EditorUtility.SetDirty(obj);
				EditorSceneManager.MarkSceneDirty(obj.scene);
			}
		}

		#endregion
	}
}
