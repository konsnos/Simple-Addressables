using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;


public class LoadButton : MonoBehaviour
{
    [SerializeField] private AssetReference assetReference;

    public Action<AssetReference> OnClick;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        OnClick?.Invoke(assetReference);
    }
}