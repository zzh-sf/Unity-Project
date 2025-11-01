using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public plant currentPlant;
    private void OnMouseDown() {
    HandManage.instance.OnCellClick(this);
    }
    public bool AddPlant(plant newPlant) {
        if(currentPlant == null) {
            currentPlant = newPlant;
            currentPlant.transform.position = transform.position;
            return true;
        }
        return false;
    }
}