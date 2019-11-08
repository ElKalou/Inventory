using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IQuantityGiver
{
    void ReceiveAskForQuantity(int quantityMax);
    void ReturnQuantity(int quantityReturned);
    void UpdateQuantity();
}
