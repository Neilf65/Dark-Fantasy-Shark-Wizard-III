using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Objective : MonoBehaviour
{

   public GameObject theObjective;
   public GameObject theTrigger;
   public GameObject theText;
   private void OnTriggerEnter(Collider other) 
   {
    if(other.CompareTag("Player"))
        StartCoroutine(missionObj());
   }
   private IEnumerator missionObj()
   {
    theText.GetComponent<Text>().text = "Objective: Go to castle";
    yield return new WaitForSeconds(5.3f);
    theTrigger.SetActive(false);
   }
}
