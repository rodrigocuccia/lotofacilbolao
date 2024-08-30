import itertools

#Defina os 16, 17, 18, 19 ou 20 numeros que vc vai apostar
#Excluindo 6 7 16 17 19 23
numeros = [1, 2, 3, 4, 5, 8, 9, 10, 11, 12, 13, 14, 15, 18, 20, 21, 22, 24, 25]

#Gerando todas as combinacoes de 15 numeros da variavel numeros acima
combinations = set(itertools.combinations(numeros, 15))

#Funcao para ler o arquivo 'jogos' (contento todos os jogos da lotofacil desde 2003) e converter cada linha em uma variavel
def read_jogos_file(file_path):
    drawn_sets = set()
    with open(file_path, 'r') as file:
        for line in file:
            #Convertendo a linha ...
            drawn_set = tuple(sorted(map(int, line.split())))
            drawn_sets.add(drawn_set)
    return drawn_sets

#Carregando os resultados do arquivo 'jogos'
jogos_file_path = '/home/rpines/loteria/jogos'
drawn_sets = read_jogos_file(jogos_file_path)

#Achando possiveis combinacoes que sairam anteriormente
matches = combinations.intersection(drawn_sets)

#Plotando os resultados
if matches:
    print("Combinacao anterior achada:")
    for match in matches:
        print(match)
else:
    print("Nenhuma combinacao anterior achada.")
