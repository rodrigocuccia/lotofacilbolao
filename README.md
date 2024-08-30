# lotofacilbolao
Cálculos para achar jogos anteriores da lotofacil
Para o scripts estamos utilizando o cálculo para um jogo de 19 dezenas

achar_jogos_anteriores.py -> Após definir os números que serao jogados (16 a 20) execute esse script para saber se suas combinações ja saíram alguma vez em sorteios anteriores da Lotofácil
IMPORTANTE: defina o caminho do arquivo 'jogos' no script.
DEFAULT: 6 7 16 17 19 23 estao excluidos (jogo de 19)

ARQUIVO 'jogos': todos os jogos da Lotofácil desde o primeiro em 2003 até hoje

todos_os_jogos_combinados.py -> Gera um arquivo (combinacoes.tx) com todos as combinacoes de jogos definido pelas dezenas setadas
DEFAULT: 6 7 16 17 19 23 estao excluidos (jogo de 19)

combinacoes.sh -> Shell script para calcular o número de combinações possíveis excluindo 6 números de 25
IMPORTANTE: troque o valor de 'n' pela quantidade de dezenas apostada.
DEFAULT: 19







