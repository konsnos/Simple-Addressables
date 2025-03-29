using konsnos.SimpleAddressables;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablesSample : MonoBehaviour
{
    [SerializeField] private LoadButton[] loadButtons;
    private GameObject _currentGameObject;

    private void Start()
    {
        foreach (var loadButton in loadButtons)
        {
            loadButton.OnClick += ClickedButton;
        }
    }

    private void ClickedButton(AssetReference assetReference)
    {
        AddressablesManager.Instance.LoadAsset<GameObject>(assetReference, LoadedAsset, LoadError);
    }

    private void LoadError(string message)
    {
        Debug.LogError($"Load error: {message}");
        RemoveCurrentGameObject();
    }

    private void LoadedAsset(GameObject newGameObject)
    {
        Debug.Log("Loaded asset");
        RemoveCurrentGameObject();

        _currentGameObject = Instantiate(newGameObject, Vector3.zero, Quaternion.identity);
    }

    private void RemoveCurrentGameObject()
    {
        if (_currentGameObject != null)
        {
            Destroy(_currentGameObject);
        }
    }
}
