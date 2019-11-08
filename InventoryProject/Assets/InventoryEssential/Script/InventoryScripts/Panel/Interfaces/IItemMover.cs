using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemMover
{
    void SendInventoryFullMessage(int numberOfItemThatCouldntFit);
    void MoveItem(int quantityToMove);
}
