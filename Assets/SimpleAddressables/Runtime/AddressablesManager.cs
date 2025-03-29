using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace konsnos.SimpleAddressables
{
    public class AddressablesManager
    {
        private static AddressablesManager _instance;
        public static AddressablesManager Instance => _instance ??= new AddressablesManager();

        private readonly Dictionary<int, object> _operationHandles =
            new();

        public void LoadAsset<T>(AssetReference assetReference, Action<T> onComplete, Action<string> onError) where T: Object
        {
            if (!assetReference.RuntimeKeyIsValid())
            {
                onError?.Invoke("Asset reference is invalid.");
                return;
            }
            var handle = Addressables.LoadAssetAsync<T>(assetReference);
            var operationData = new OperationData<T>
            {
                OnComplete = onComplete,
                OnError = onError
            };
            _operationHandles.Add(handle.GetHashCode(), operationData);
            handle.Completed += OperationCompleted;
        }
        
        private void OperationCompleted<T>(AsyncOperationHandle<T> handle) where T : Object
        {
            if (_operationHandles.Remove(handle.GetHashCode(), out var operationData))
            {
                if (operationData is OperationData<T> data)
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        data.OnComplete?.Invoke(handle.Result);
                    }
                    else
                    {
                        data.OnError?.Invoke($"Failed to load asset: {handle.Status}");
                        Addressables.Release(handle);
                    }
                }
                else
                {
                    Debug.LogError("OperationData is not of the expected type");
                    Addressables.Release(handle);
                }
            }
            else
            {
                Debug.LogError("Failed to find operation");
                Addressables.Release(handle);
            }
        }

        private class OperationData<T> where T : Object
        {
            public Action<T> OnComplete;
            public Action<string> OnError;
        }
    }
}