using System.Linq; //Debugging
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDebugger : MonoBehaviour
{
    void Update() {
        /* if (Input.GetMouseButtonDown(0)) {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            Debug.Log("=== CLICK RAYCAST RESULTS ===");
            foreach (var result in results) {
                Debug.Log($"Hit: {result.gameObject.name} (Layer: {result.gameObject.layer})");
            }
        } */

        if (Input.GetMouseButtonDown(0)) {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            Debug.Log("=== DETAILED CLICK RAYCAST RESULTS ===");
            for (int i = 0; i < results.Count; i++) {
                var result = results[i];
                Debug.Log($"#{i}: {result.gameObject.name} - Components: {string.Join(", ", result.gameObject.GetComponents<Component>().Select(c => c.GetType().Name))}");
            }
        }

    }//Update

}//class