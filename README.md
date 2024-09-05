# Lotacalculadora

Lotocalculadora.exe calcula seus jogos e chances para a Lotofacil da Caixa

## Instalacao

Download [LotocalculadoraSetup.exe](https://www.mediafire.com/file/gcjtls7jhdhgf3r/LotocalculadoraSetup.exe/file) para instalar o programa.

Avançe até o sucesso da instalaçao.

Localizaçao: C:\ProgramFile(x86)\Lotocalculadora\Lotocalculadora.exe

## Uso

Aposta Única: Selecione de 15 a 20 números e clique em Calcular

Multiplas Apostas: Selecione de 15 a 20 números e clique em Add Aposta. Após adicionar a quantidade necessária de jogos clique em Calcular

Reset: Limpa os números selecionados e as apostas salvas no modo Multiplas Apostas


Escolha sua porcentagem para ganhar: Insira um valor de 1 a 100 para saber quantos jogos serao necessários para se obter a porcentagem desejada. Valores de jogos incluídos.

## Banco de dados

O arquivo jogos.txt contém todos os jogos já sorteados pela Caixa desde 2003 até 23/08/2024

## Cálculo das combinaçoes

C(n,k)= k!(n−k)! / n!


Exemplo com um jogo de 20 números:

C(20,15)= 15!×5! / 20! = 15504 combinaçoes de 15 números.

## Cálculo de probabilidades

Probabilidade de ganhar: 
Chance = C(quantidade de numeros selecionados,15) / C(25,15)

| Numeros Selecionados | Combinaçoes | Total Combinaçoes (C(25, 15)) | Probabilidade (1 em x) |
|:-------------------:|:---------------------:|:----------------:|:-----------------:|
| 15                  | C(15,15)=1            | C(25,15)=3268760 | 1 em 3268760      |
| 16              | C(16,15)=16                | C(25,15)=3268760           | 1 em 204297            |
| 17              | C(17,15)=136                | C(25,15)=3268760           | 1 em 24049        |
| 18              | C(18,15)=816                | C(25,15)=3268760           | 1 em 4003            |
| 19              | C(19,15)=3876                | C(25,15)=3268760           | 1 em 843          |
| 20              | C(20,15)=15504              | C(25,15)=3268760          | 1 em 211

IMPORTANTE: o código leva em consideraçao os jogos que ja sairam como fator de reduçao de chance. Caso o jogo já tenha saído anteriormente, a porcentagem de ganho diminui.

Cálculo:

Chance Original = 1 / Total de Combinaçoes

Combinaçao válida = Total de Combinaçoes - Jogos que Sairam

Chance Ajustada = 1 / Combinaçao válida

Porcentagem de Decréscimo = (Jogos que Sairam / Total de Combinaçoes) * 100


## Requerimentos

Windows 7, 8, 8.1, 10 e 11.
```
## Open Source
Pull request sao bem vindos. Crie issues para qualquer bug encontrado.

## License
Free
