using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public VerticalAccelerator fogueteanim;
    public Camera maincamera;
    public Camera maincamera2;
    public PlayerMovement mov;
    public GameEvent LoadingScreenEvent;

    public bool ativo = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fogueteanim.inanim = true;
            maincamera.gameObject.SetActive(false);
            maincamera2.gameObject.SetActive(true);
            mov.move = false;

            if (!ativo) {
                ativo = !ativo;
                StartCoroutine(ActivateAfterDelay(10f));
            }

        }
    }

    private System.Collections.IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        DataManager.ClearPlayerData();
        LoadingScreenEvent.Raise(this, null);
    }
}
