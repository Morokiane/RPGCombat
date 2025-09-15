using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Utils {
    public class AreaExit : MonoBehaviour {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private string sceneTransitionName;
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                StartCoroutine(HoldForFade());
                Controllers.LevelController._instance.SetTransitionName(sceneTransitionName);
                Utils.Fade._instance.FadeToBlack();
            }
        }

        private IEnumerator HoldForFade() {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}