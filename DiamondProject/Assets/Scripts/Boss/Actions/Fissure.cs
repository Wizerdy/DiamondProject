using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fissure : BossAction, IAction
{
    [SerializeField] float _fissureSpeed = 1f;
    [SerializeField] float _sizeToReach = 1f;
    
    [Header("For Prog: ")]
    [SerializeField] Shockwave showkWave = null;
    [SerializeField] Shelter shelter = null;
    [SerializeField] BossBodyReference _body;
    public override void StartAction()
    {
        _boss.Instance.ChangeState(GetState());
        StartCoroutine(Abime());
    }
    IEnumerator Abime()
    {
        Shockwave newShockWave = Instantiate(showkWave.gameObject, _body.Instance.transform.position, Quaternion.identity).GetComponent<Shockwave>();
        while (newShockWave.transform.localScale.x < _sizeToReach)
        {
            newShockWave.transform.localScale = Vector3.one * (newShockWave.transform.localScale.x + _fissureSpeed * Time.deltaTime);
            yield return null;
        }
        NextState();
    }

    public override Boss.State GetState()
    {
        return Boss.State.FISSURE;
    }
}
