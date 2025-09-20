using UnityEngine;

namespace Utils {
    public class RandomAnimation : MonoBehaviour {
        private Animator anim;

        private void Awake() {
            anim = GetComponent<Animator>();
        }

        // Randomizes the start frame of an animation
        private void Start() {
            if (!anim) return;
            
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        }
        
    }
}
