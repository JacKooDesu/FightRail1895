using UnityEngine;

namespace JacDev.Testing
{
    public class TestTutorial : MonoBehaviour
    {
        private void Start()
        {

        }
        public void Test()
        {
            Tutorial.TutorialManager.Singleton.Tutorial(0);
        }

    }
}