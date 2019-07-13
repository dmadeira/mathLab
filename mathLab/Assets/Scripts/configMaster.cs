﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;


public class configMaster : MonoBehaviour {

    public GameObject audioPanel, prefPanel,truthPanel, equivPanel, infPanel; // paineis
    public GameObject addConfirm;
    public Text linhasTableText; //texto de confirmação de adição!
    public InputField[] inputs; /*guarda as respostas recebidas, 
                                cada posição é um input diferente*/
    private static int tableCounter = 0;
    protected string pathTable = "Assets/NewAdds/tabelaVerdade.txt";
    protected string expr, num, resp, linhasTabela;
    char[] vec1 = new char[2];
    

    void CriaArquivo() {
        if (!File.Exists(pathTable))
        {
            File.WriteAllText(pathTable, "LOG FILE - ADICIONAR NOVA TABELA VERDADE " + tableCounter.ToString() + "\n");    
        }else
        {
            File.AppendAllText(pathTable, "LOG FILE - ADICIONAR NOVA TABELA VERDADE " + tableCounter.ToString() + "\n");
        }
        
    }
    public void openPanel(int panelNum) {
         
         switch (panelNum) {
            case (0):
                audioPanel.SetActive(true);
                truthPanel.SetActive(false);
                equivPanel.SetActive(false);
                infPanel.SetActive(false);
                prefPanel.SetActive(false);
                break;
            case (1):
                prefPanel.SetActive(true);
                audioPanel.SetActive(false);
                break;
            case (2):
                truthPanel.SetActive(true);
                equivPanel.SetActive(false);
                infPanel.SetActive(false);
                break;
            case (3): 
                equivPanel.SetActive(true);
                infPanel.SetActive(false);
                truthPanel.SetActive(false);
                break;
            case (4): 
                infPanel.SetActive(true);
                equivPanel.SetActive(false);
                truthPanel.SetActive(false);
                break;
        }
        
    }
    
    public void closePanel(int panelNum) {
        
            switch (panelNum) {
                case (0):
                    audioPanel.SetActive(false);
                    break;
                case (1):
                    prefPanel.SetActive(false);
                    truthPanel.SetActive(false);
                    equivPanel.SetActive(false);
                    infPanel.SetActive(false);
                    break;
                case (2):
                    truthPanel.SetActive(false);
                    break;
                case (3): 
                    equivPanel.SetActive(false);
                    break;
                case (4): 
                    infPanel.SetActive(false);
                    break;
            }
    }

    public void getTruthTable(int inputNum){

        switch (inputNum){
            case (0): //Expressão
                print(inputs[inputNum].text);
                expr = inputs[inputNum].text;
                break;
            case (1): //N° Var
                print(inputs[inputNum].text);
                num = inputs[inputNum].text;
                geraTabelaExemplo(inputs[inputNum].text);
                break;
            case (2): // Vetor Respostas
                print(inputs[inputNum].text);
                resp = inputs[inputNum].text;
                break;
        }
    }

    public void buttonWrite(){
        CriaArquivo();
        File.AppendAllText(pathTable, "Expr: " + expr + "\n");
        File.AppendAllText(pathTable, "Nº Var: " + num + "\n");
        File.AppendAllText(pathTable, "Linhas Tabela: " + linhasTabela + "\n");
        File.AppendAllText(pathTable, "Respotas: " + resp + "\n");
        inputs[0].text = "";
        inputs[1].text = "";
        inputs[2].text = "";
        addConfirm.SetActive(true);
        StartCoroutine(undisplay()); 
        tableCounter += 1;
    }

    IEnumerator undisplay()
    {
        yield return new WaitForSeconds(1);
        addConfirm.SetActive(false);   
    }

    public void geraTabelaExemplo(string numVar){
        int Entradas = System.Convert.ToInt32(numVar);
        //Debug.Log(intVar.GetType());
        //Algoritmo de multicombinação a partir de random numbers
        double TotalDeLinhas = Math.Pow(2, Convert.ToDouble(Entradas)); // Calcula o total de linhas a serem geradas;
        int[] linha = new int[Entradas]; // Cada elemento da linha é jogado em uma posição do vetor
        int LinhasGeradas = 0;
        string LinhaString = ""; // Do vetor, os elementos são unidos nesta String
        List<string> linhas = new List<string>();
        System.Random randNum = new System.Random();

        while (LinhasGeradas < TotalDeLinhas) //Permanece no loop enquanto não completar todas as combinações.
        {
            for (int i = 0; i < Entradas; i++) // Gera uma combinação
            {
                linha[i] = new int();
                linha[i] = randNum.Next(0, 2); //Gera um num. aleatório entre 0 e 1;
            }

            for (int i = 0; i < Entradas; i++) //Transforma o vetor de combinações em uma única string. (Ex: 0 - 1 - 0 - 1)
            {
                if (i == Entradas - 1) // Se for o ultimo número da combinação, não irá ter traço após ele.
                {
                    if (linha[i] == 1)
                    {
                        LinhaString += 'V';    
                    }else
                    {
                        LinhaString += 'F';
                    }
                }
                else
                {
                    if (linha[i] == 1)
                    {
                        LinhaString += 'V' + " ";
                    }
                    else
                    {
                        LinhaString += 'F' + " ";
                    }
                }
            }

            if (!linhas.Contains(LinhaString)) // Se não tiver esta combinação na lista, adiciona.
            {
                linhas.Add(LinhaString);
                LinhasGeradas++;
            }
            LinhaString = "";
        }
        for (int i = 0; i < TotalDeLinhas; i++) {
            linhasTabela += linhas[i] + "\n";  
        }
        Debug.Log(linhasTabela);
        linhasTableText.text = linhasTabela;
        linhasTabela = "";
    }
}

