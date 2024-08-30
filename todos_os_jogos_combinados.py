import itertools

numeros = [1, 2, 3, 4, 5, 8, 9, 10, 11, 12, 13, 14, 15, 18, 20, 21, 22, 24, 25]  #Nesse caso, exlcuindo 6, 7, 16, 17, 19, 23

combinacoes = list(itertools.combinations(numbers, 15))

with open('combinacoes.txt', 'w') as file:
    file.write(f"Total de combinacoes: {len(combinacoes)}\n")
    for combo in combinacoes:
        file.write(f"{combo}\n")

print("Todos seus jogos foram salvos em combinacoes.txt")
