using UnityEngine;
using System;
using NaughtyAttributes;


namespace ARPGFrame
{
	public class CreateObjectOnMap : MonoBehaviour
	{
		#region Fields
		
		private const string TOOLTIP_SET_PREFAB_MANUALLY = "Если не стоит флажок, выбираем тип префаба (None = все префабы), затем сам префаб. А если стоит - в ручную перетаскиваем префаб в поле префаба";
		private const string TOOLTIP_PREFAB = "Выберите префаб, который хотите разместить в редакторе";
		private const string TOOLTIP_ADDITIONAL_SETTINGS = "Дополнительные настройки будут активны, только если здесь стоит флажок";
		private const string TOOLTIP_PARENT_NAME = "Введите название родительского объекта для префабов, которые хотите разместить. Если оставить пустым, префабы будут размещаться в корень сцены";
		private const string END_OF_PARENT_NAME = "s";
		private const int PREFAB_TYPES_COUNT = 5;

		[Tooltip(TOOLTIP_SET_PREFAB_MANUALLY)]
		[SerializeField] private bool _setPrefabManually;

		[ShowIf(nameof(_setPrefabManually)), Tooltip(TOOLTIP_PREFAB), AllowNesting]
		[SerializeField] private GameObject _Prefab;

		[ShowIf(nameof(ShowOriginal)),  Dropdown(nameof(PrefabTypes))]
		[SerializeField] private PrefabTypes _prefabsType;

		[ShowIf(nameof(ShowOriginal)), Tooltip(TOOLTIP_PREFAB), Dropdown(nameof(Prefabs))]
		[SerializeField] private GameObject _prefab;

		[Tooltip(TOOLTIP_ADDITIONAL_SETTINGS)]
		[SerializeField] private bool _additionalSettings;

		[ShowIf(nameof(_additionalSettings)), Tooltip(TOOLTIP_PARENT_NAME), AllowNesting]
		[SerializeField] private string _parentName;

		private GameObject _parentObject;
		private GameObject _currentPrefab;
		private string _currentParentName;

		#endregion


		#region Properties

		public bool ShowOriginal => !_setPrefabManually;

		#endregion


		#region Methods

		private DropdownList<PrefabTypes> PrefabTypes()
		{
			var prefabTypes = new DropdownList<PrefabTypes>();

			for (int i = 0; i < PREFAB_TYPES_COUNT; i++)
			{
				var t = (PrefabTypes)i;
				prefabTypes.Add(t.ToString(), (PrefabTypes)i);
			}

			return prefabTypes;
		}

		private DropdownList<GameObject> Prefabs()
		{
			var prefabs = FindObjectOfType<Reference>().GetPrefabs();

			switch (_prefabsType)
			{
				case ARPGFrame.PrefabTypes.None:
					break;
				case ARPGFrame.PrefabTypes.Ammunition:
					prefabs = SelectPrefabs<Ammunition>(prefabs);
					break;
				case ARPGFrame.PrefabTypes.Weapon:
					prefabs = SelectPrefabs<Weapon>(prefabs);
					break;
				case ARPGFrame.PrefabTypes.Enemy:
					prefabs = SelectPrefabs<BaseEnemy>(prefabs);
					break;
				case ARPGFrame.PrefabTypes.Pickable:
					prefabs = SelectPrefabs<IPickable>(prefabs);
					break;
				default:
					break;
			}

			return prefabs;
		}

		private DropdownList<GameObject> SelectPrefabs<T>(DropdownList<GameObject> prefabs)
		{
			DropdownList<GameObject> selectedPrefabs = new DropdownList<GameObject>();

			foreach (var prefab in prefabs)
			{
				var obj = (GameObject)prefab.Value;
				if (obj.TryGetComponent<T>(out _))
					selectedPrefabs.Add(prefab.Key, (GameObject)prefab.Value);
			}
			return selectedPrefabs;
		}

		public void InstantiateObj(Vector3 pos)
		{
			SetPrefab();

			if (_currentPrefab != null)
			{
				SetParentName();

				if (_currentParentName.Length != 0)
				{
					SetParentObject();
					Instantiate(_currentPrefab, pos, Quaternion.identity, _parentObject.transform);
				}
				else
					Instantiate(_currentPrefab, pos, Quaternion.identity);
			}
			else
				throw new Exception($"Префаб не выбран {typeof(CreateObjectOnMap)} {gameObject.name}");			
		}

		private void SetPrefab()
		{
			if (_setPrefabManually)
				_currentPrefab = _Prefab;
			else
				_currentPrefab = _prefab;
		}

		private void SetParentName()
		{
			if (_additionalSettings)
				_currentParentName = _parentName;
			else
				_currentParentName = _currentPrefab.name + END_OF_PARENT_NAME;
		}

		private void SetParentObject()
		{
			if (!_parentObject || !_parentObject.name.Equals(_currentParentName))
				_parentObject = new GameObject(_currentParentName);
		}

        #endregion
    }
}
