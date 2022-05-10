using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangLeaf : BaseAttack {
    [SerializeField] Boomerang _boomerangPrefab;
    [SerializeField] float _firstSpeed;
    [SerializeField] float _secondspeed;
    [SerializeField] Vector3 _bottomLine;
    [SerializeField] Vector3 _topLine;
    [SerializeField] int leafsNumber;
    [SerializeField] int leafsDamages;
    [SerializeField] float spaceBetweenLeafs;
    [SerializeField] int ghostLeaf;
    [SerializeField] List<Boomerang> leafs;



    protected override IEnumerator IExecute() {
        if (ghostLeaf >= leafsNumber) {
            yield break;
        }
        List<int> ghosts = new List<int>();
        for (int i = 0; i < leafsNumber; i++) {
            ghosts.Add(i);
        }
        for (int i = 0; i < leafsNumber - ghostLeaf; i++) {
            int r = Random.Range(0,ghosts.Count);
            ghosts.RemoveAt(r);
        }
        for (int i = 0; i < leafsNumber; i++) {
            bool ghost = false;
            for (int j = 0; j < ghosts.Count; j++) {
                if (ghosts[j] == i) {
                    ghost = true;
                }
            }
            if (ghost) {
                continue;
            }
            Vector3 firstPosition = _bottomLine - Vector3.right * spaceBetweenLeafs * (leafsNumber - 1) / 2;
            Boomerang newBoomerangLeaf = Instantiate(_boomerangPrefab, firstPosition + Vector3.right * spaceBetweenLeafs * i, Quaternion.identity).GetComponent<Boomerang>();
            newBoomerangLeaf.SetDirection(Vector3.up).
                SetDestination(_topLine).
                SetFirstSpeed(_firstSpeed).
                SetSecondSpeed(_secondspeed).
                SetDamage(leafsDamages);
            leafs.Add(newBoomerangLeaf);
            newBoomerangLeaf.OnDeath += RemoveLeafs;
        }
        while (leafs.Count != 0) {
            yield return new WaitForSeconds(0.1f);
        }

        void RemoveLeafs(Boomerang boom){
            leafs.Remove(boom);
        }
    }
}
