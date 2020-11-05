using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    //Make the Start() method a coroutine
    IEnumerator Start()
    {
        //Reference component
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Turn the sprite red overtime to indicate it is about to damage the player.
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
            yield return null;
        }

        //If this is not destroyed by the player, then deal damage to player.
        GameplaySceneManager.instance.LoseHealth();
        Destroy(gameObject);
    }
}
