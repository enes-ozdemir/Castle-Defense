using System;
using Sirenix.Utilities.Editor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoldDropManager : MonoBehaviour
{
    public GameObject goldDrop;
    public GameObject goldTarget;

    public static GoldDropManager Instance;

    #region "Singleton"

    private void Awake()
    {
            Instance = this;
    }
    #endregion

    public void CheckLoot(int goldAmount,Vector3 position)
    {
        Debug.Log("Checking Gold");
        var goldDropChange = Random.Range(0, 5);
        if (goldDropChange >= 3)
        {
            Debug.Log("Gold earned "+ goldAmount);
            Instantiate(goldDrop, position, Quaternion.identity);
            goldDrop.GetComponent<Animator>().transform.parent.gameObject.transform.position  = position;
            //Add this to end of the animation later
            GameManager.Money += goldAmount;
        }
   
    }

}

