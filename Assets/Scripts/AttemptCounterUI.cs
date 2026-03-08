using UnityEngine;
using TMPro;
using System.Collections;

public class AttemptCounterUI : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null;

        if (AttemptManager.Instance != null)
        {
            AttemptManager.Instance.RegisterText(GetComponent<TextMeshProUGUI>());
        }
    }
}
