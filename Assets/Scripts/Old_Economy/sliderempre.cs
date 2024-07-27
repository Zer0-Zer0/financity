using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class sliderempre : MonoBehaviour
{
    public Slider slider;
    public TMP_Text texto;
    private bool ativo = true;

    public Carteira carteira;
    public Dialogo dialogo;

    public TMP_Dropdown dropdown;
    public int minemprestimo = 2;
    public int maxemprestimo = 150;

    public int juros = 8;

    private float dinheiro;

    public string[] mensagensafirmação;
    public string[] mensagensnegação;

    private string[] cancelar;

    public bancocobrar banco;

    //public AnimaObjeto celular;

    private string metodo;

    public bool feito = false;

    public float value;

    /*
    o min e o max futuramente será decidido pelo que o jogador tem e pelo que ele ganhas nas fases com base na dificuldade
    e tals, mas como não tem fase vai ficar desse jeito mesmo até ter
    */

    void Start()
    {
        slider.gameObject.SetActive(false);
        texto.gameObject.SetActive(false);
        dropdown.gameObject.SetActive(false);
    }

    public void Atualizartxt()
    {
        value = slider.value;

        if (dropdown.value == 0)
        {
            texto.text = "+R$ " + slider.value.ToString("F2") + " Juros de R$ " + slider.value * juros / 100f;
        } else if (dropdown.value == 1)
        {
            texto.text = "+R$ " + slider.value.ToString("F2") + " Juros P/dia R$ " + slider.value * juros / 100f * 3;
        } else if (dropdown.value == 2)
        {
            texto.text = "+R$ " + slider.value.ToString("F2") + " Juros final R$ " + (slider.value * Mathf.Pow(1 + juros/100f, 3)).ToString("F2");

        }
        
    }

    public void toggleslide()
    {

        if (ativo)
        {
            if (carteira.Saldo < 0)
            {
                dinheiro = carteira.Saldo * -1;
            }
            else
            {
                dinheiro = carteira.Saldo;
            }

            ativo = !ativo;
            slider.gameObject.SetActive(true);
            texto.gameObject.SetActive(true);
            dropdown.gameObject.SetActive(true);
            slider.minValue = dinheiro * minemprestimo / 100f; // valor minimo de empréstimo é 2% 
            slider.maxValue = dinheiro * maxemprestimo / 100f; // valor máximo de empréstimo é 150%
        }
        else
        {
            ativo = !ativo;
            slider.gameObject.SetActive(false);
            texto.gameObject.SetActive(false);
            dropdown.gameObject.SetActive(false);
        }

    }

    public void aceitar()
    {
        if (carteira.PedirEmprestimo(slider.value))
        {
            metodo = dropdown.options[dropdown.value].text;
            banco.cobrar(metodo);
            dialogo.InicializarDialogo(mensagensafirmação);
            slider.gameObject.SetActive(false);
            texto.gameObject.SetActive(false);
            dropdown.gameObject.SetActive(false);
            ativo = !ativo;
            feito = true;
        }
        else
        {
            dialogo.InicializarDialogo(mensagensnegação);
            slider.gameObject.SetActive(false);
            texto.gameObject.SetActive(false);
            dropdown.gameObject.SetActive(false);
            ativo = !ativo;
        }

    }


    public void negar()
    {
        cancelar = new string[] { "Poxa, pena que não pude ajudar dessa vez", "Mas sei que você ainda irá precisar de mim!" };
        dialogo.InicializarDialogo(cancelar);
        slider.gameObject.SetActive(false);
        texto.gameObject.SetActive(false);
        dropdown.gameObject.SetActive(false);
        ativo = !ativo;
    }
}

