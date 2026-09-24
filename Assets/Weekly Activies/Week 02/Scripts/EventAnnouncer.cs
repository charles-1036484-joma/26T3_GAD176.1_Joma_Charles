using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventAnnouncer : MonoBehaviour
{
   #region Notes
   /*
   Use 'static' to make the variable attached the class 
   and not to any specific instance 
   of the script on any object
   */
   #endregion
   public static UnityEvent onSpaceBarPressed = new UnityEvent();
}
