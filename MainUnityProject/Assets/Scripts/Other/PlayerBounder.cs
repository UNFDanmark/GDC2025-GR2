using System;
using UnityEngine;

public class PlayerBounder : MonoBehaviour {
    [SerializeField] Transform[] players;
    [SerializeField] Transform up;
    [SerializeField] Transform down;
    [SerializeField] Transform left;
    [SerializeField] Transform right;
    void Update() {
        foreach (var _player in players) {
            var _prevPos = _player.position;
            
            if (_player.position.x > right.position.x) _prevPos.x = right.position.x;
            if (_player.position.x < left.position.x) _prevPos.x = left.position.x;
            if (_player.position.z > up.position.z) _prevPos.z = up.position.z;
            if (_player.position.z < down.position.z) _prevPos.z = down.position.z;
            
            _player.position = _prevPos;
        }
    }
}
