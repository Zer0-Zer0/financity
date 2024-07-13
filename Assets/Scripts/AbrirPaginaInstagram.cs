using UnityEngine;

public class AbrirPaginaInstagram : MonoBehaviour
{
    public string urlInstagram;

    public void AbrirPagina()
    {
        Application.OpenURL(urlInstagram);
    }
}
