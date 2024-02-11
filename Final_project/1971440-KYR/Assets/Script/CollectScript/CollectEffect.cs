using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectEffect : MonoBehaviour
{
    public GameObject EffectPrefab;
    private GameObject effect;
    public void PlayEffect()
    {
        //effect = Instantiate(EffectPrefab, transform.localPosition, transform.localRotation);
        effect = Instantiate(EffectPrefab, gameObject.transform.position, Quaternion.identity);
        
        //effect.transform.SetParent(gameObject.transform, true);
        Destroy(effect, 1);
    }
}
