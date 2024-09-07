using UnityEngine;

[System.Serializable]
public class DropItem
{
    public GameObject itemPrefab;
    public float dropRate;
    public int minDropAmount;
    public int maxDropAmount;
}