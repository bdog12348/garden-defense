using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public enum Type { Beetle, Raccoon}

    [SerializeField]
    Type type;

    public Type GetEnemyType()
    {
        return type;
    }
}
