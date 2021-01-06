using System.Collections;
using UnityEngine;
using Wyt.CharacterStats;

public class StatusPanel : MonoBehaviour
{
    [SerializeField] StatDisplay[] _statDisplays;
    [SerializeField] string[] _statNames;

    private CharacterStat[] _stats;

    private void OnValidate()
    {
        _statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatName();
    }

    public void SetStats(params CharacterStat[] characterStats)
    {
        _stats = characterStats;

        if(_stats.Length > _statDisplays.Length)
        {
            Debug.LogError("Not Enough Stat Display");
            return;
        }

        for (int i = 0; i < _statDisplays.Length; i++)
        {
            _statDisplays[i].gameObject.SetActive(i < _stats.Length);
        }
    }

    public void UpdateStatVlues()
    {
        for (int i = 0; i < _stats.Length; i++)
        {
            _statDisplays[i].ValueText.SetText(_stats[i].Value.ToString());
        }
    }

    public void UpdateStatName()
    {
        for (int i = 0; i < _statNames.Length; i++)
        {
            _statDisplays[i].NameText.SetText(_statNames[i]);
        }
    }
}
