using Mirror;

namespace _Project.Scripts
{
    public class LevelController: NetworkBehaviour
    {
        public static LevelController Instance { get; private set; }

        private void Awake()
        {
            CreateInstance();
        }

        private void CreateInstance()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if(Instance == this)
            { 
                Destroy(gameObject); 
            }
            
            DontDestroyOnLoad(gameObject);
        }
    }
}