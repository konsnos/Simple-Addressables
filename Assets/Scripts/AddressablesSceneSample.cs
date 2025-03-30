using konsnos.SimpleAddressables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddressablesSceneSample : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        AddressablesManager.Instance.LoadScene("SampleScene", LoadSceneMode.Single, null, null);
    }
}
