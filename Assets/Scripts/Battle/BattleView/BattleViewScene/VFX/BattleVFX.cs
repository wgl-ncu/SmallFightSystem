using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleVFX : MonoBehaviour
{
    public List<ParticleSystem> _particleSystems;
    private bool followTarget;
    private Transform tar;
    public void Init(bool followTarget, GameObject target = null)
    {
        this.followTarget = followTarget;
        if (followTarget)
        {
            tar = target.transform;
        }

        foreach (var _particleSystem in _particleSystems)
        {
            var renders = _particleSystem.transform.GetComponentsInChildren<Renderer>();
            foreach (var render in renders)
            {
                render.sortingLayerName = "VFX";
            }
        }
    }

    private void Update()
    {
        if (followTarget)
        {
            if (tar)
            {
                transform.position = tar.position;
            }
            else
            {
                Destory();
            }
            
        }
    }

    public void Play(int index)
    {
        _particleSystems[index].Play();
    }

    public void Destory()
    {
        Destroy(gameObject);
    }
}
