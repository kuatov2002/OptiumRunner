using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject obstaclePrefab;
    public GameObject gameInstallerPrefab;
    public GameObject uiPrefab;
    
    private void Awake()
    {
        if (GameObject.FindObjectOfType<EntityFactory>() == null)
        {
            GameObject factoryObj = new GameObject("EntityFactory");
            EntityFactory factory = factoryObj.AddComponent<EntityFactory>();
            factory.playerPrefab = playerPrefab;
            factory.obstaclePrefab = obstaclePrefab;
        }
        
        // Create essential game objects if they don't exist
        if (GameObject.FindObjectOfType<GameInstaller>() == null)
        {
            GameObject installerObj = Instantiate(gameInstallerPrefab);
            installerObj.name = "GameInstaller";
        }
        
        if (GameObject.FindObjectOfType<GameUI>() == null && uiPrefab != null)
        {
            Instantiate(uiPrefab);
        }
    }
}