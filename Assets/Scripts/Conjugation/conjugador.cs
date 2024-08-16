using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Conjugador
{
    private static Dictionary<string, Palavra> PalavrasConjugaveis = new Dictionary<string, Palavra>
    {
        { 
            "novo", new Palavra(new List<string> { "novo", "nova", "nove" })
        },
        { 
            "Novo", new Palavra(new List<string> { "Novo", "Nova", "Nove" })
        },
        { 
            "novato", new Palavra(new List<string> { "novato", "novata", "novate" })
        },
        { 
            "Novato", new Palavra(new List<string> { "Novato", "Novata", "Novate" })
        },
        {
            "vindo", new Palavra(new List<string> { "vindo", "vinda", "vinde" })
        }
    };

    private static List<string> palavrasAtual = new List<string>();
    private static List<string> pontuaçãoAtual = new List<string>();//No sentido gramatico

    public static string format(string frase){
        SeparateWordsAndPunctuation(frase);
        ConjugateWordsFromList();
        string result = JoinWordsAndPunctuation();
        return result;
    }

    static void CleanLists(){
        palavrasAtual.Clear();
        pontuaçãoAtual.Clear();
    }

    static void SeparateWordsAndPunctuation(string input)
    {
        // Regex para encontrar palavras e pontuação
        string pattern = @"(\w+|[^\w\s])";
        MatchCollection matches = Regex.Matches(input, pattern);

        foreach (Match match in matches)
        {
            if (Regex.IsMatch(match.Value, @"\w+")) // Se for uma palavra
                palavrasAtual.Add(match.Value);
            else
                pontuaçãoAtual.Add(match.Value);
        }

        // Exibir resultados no console
        Debug.Log("Palavras: " + string.Join(", ", palavrasAtual));
        Debug.Log("Pontuação: " + string.Join(", ", pontuaçãoAtual));
    }

    static void ConjugateWordsFromList(){
        for(int index = 0; index < palavrasAtual.Count; index++)
            if(PalavrasConjugaveis.ContainsKey(palavrasAtual[index]))
                palavrasAtual[index] = PalavrasConjugaveis[palavrasAtual[index]].ToString();
    }

static string JoinWordsAndPunctuation()
{
    System.Text.StringBuilder resultado = new System.Text.StringBuilder();
    
    int maxLength = Math.Max(palavrasAtual.Count, pontuaçãoAtual.Count);
    
    for (int i = 0; i < maxLength; i++)
    {
        if (i < palavrasAtual.Count)
        {
            resultado.Append(palavrasAtual[i]);
        }
        
        if (i < pontuaçãoAtual.Count)
        {
            resultado.Append(pontuaçãoAtual[i]);
        }
    }
    
    return resultado.ToString();
}

}

public class Palavra{
    private Dictionary<Gênero, string> _conjugações;
    public Dictionary<Gênero, string> Conjugações {get => _conjugações;}
    
    public override string ToString() => _conjugações[DataManager.playerData.getGender()];

    public Palavra(Dictionary<Gênero, string> conjugações) => _conjugações = conjugações;
    public Palavra(List<string> conjugações){
        Gênero[] gêneros = (Gênero[])Enum.GetValues(typeof(Gênero));

        _conjugações = new Dictionary<Gênero, string>();
        for(int index = 0; index < gêneros.Length; index++)
            _conjugações.Add(gêneros[index], conjugações[index]);
    }
}

public enum Gênero {
    Homem,
    Mulher,
    Neutro
}
