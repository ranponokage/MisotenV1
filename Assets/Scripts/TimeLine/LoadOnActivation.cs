using UnityEngine;
using Michsky.LSS;

public class LoadOnActivation : MonoBehaviour
{
    [SerializeField] LoadingScreenManager _lsm;


    void OnEnable()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        _lsm.LoadScene("PlayerSelect");

    }
}
