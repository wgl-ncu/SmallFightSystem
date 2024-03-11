using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Status", menuName = "DataCreate/Level Status/Data")]
public class LevelData_SO : ScriptableObject
{
    public int id;
    public List<int> enemyIds;
    public int sceneId;
}
