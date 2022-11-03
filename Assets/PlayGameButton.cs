using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButton : MonoBehaviour
{
   public void Press()
   {
      SceneManager.LoadScene(2);
   }
}
