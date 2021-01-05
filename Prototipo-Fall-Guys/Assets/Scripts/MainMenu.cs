using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace menus
{
    public class MainMenu : MonoBehaviour
    {
        private AsyncOperation cena;
        private int SegurancaClick; // garante que o jogador não sobrecarrege uma função
        public GameObject fadeIn;

        private void Awake()
        {
            SegurancaClick = 0;
            DontDestroyOnLoad(gameObject);
        }

        private IEnumerator TempoMinimo() // Espera um tempo na cena de load antes de mandar para a cena destino e destroi o game object
        {
            yield return new WaitForSeconds(1.0f);
            cena.allowSceneActivation = true;
            Destroy(gameObject);
        }

        private IEnumerator TempoAnimacao(string cena) // Rotina que chama a cena de load depois do Fade In
        {
            fadeIn.GetComponent<Animator>().SetBool("Fade In", true);
            yield return new WaitForSeconds(fadeIn.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.5f);
            CarregarCena(cena);
        }

        private void CarregarCena(string destino) // Manda para cena de Load e carrega a cena destino
        {
            cena = SceneManager.LoadSceneAsync("CenaLoad");
            cena = SceneManager.LoadSceneAsync(destino);
            cena.allowSceneActivation = false;
            StartCoroutine("TempoMinimo");
        }

        public void MenuInicial() // Menu Principal
        {
            if (SegurancaClick == 0)
            {
                StartCoroutine("TempoAnimacao", "MainMenu");
                SegurancaClick++;
            }
        }

        public void Iniciar() // Novo Jogo
        {
            if(SegurancaClick == 0)
            {
                StartCoroutine("TempoAnimacao", "fase");
                SegurancaClick++;
            }
        }

        public void Multiplayer()
        {

        }

        public void Controles()
        {

        }

        public void Sair()
        {
            Application.Quit();
        }
    }
}