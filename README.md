# Simple Addressables

A simple package to handle Addressables.

### Usage
```
void Load()
{
    AddressablesManager.Instance.LoadAsset<GameObject>(assetReference, LoadedAsset, LoadError);
}

private void LoadError(string message)
{
    Debug.LogError($"Load error: {message}");
}

private void LoadedAsset(GameObject newGameObject)
{
    Debug.Log("Loaded asset");

    _currentGameObject = Instantiate(newGameObject, Vector3.zero, Quaternion.identity);
}
```


### Installation

1. Open the Package Manager from Window > Package Manager.
2. Click the "+" button > Add package from git URL.
3. Enter the following URL:

```
https://github.com/konsnos/Simple-Addressables.git?path=Assets/SimpleAddressables
```

Alternatively, open Packages/manifest.json and add the following to the dependencies block:

```json
{
    "dependencies": {
        "com.konsnos.simpleaddressables": "https://github.com/konsnos/Simple-Addressables.git?path=Assets/SimpleAddressables"
    }
}
```