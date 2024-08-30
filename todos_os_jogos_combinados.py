import itertools

numeros = [1, 2, 3, 4, 5, 8, 9, 10, 11, 12, 13, 14, 15, 18, 20, 21, 22, 24, 25]  #Nesse caso, exlcuindo 6, 7, 16, 17, 19, 23

combinations = list(itertools.combinations(numbers, 15))

with open('combinations_output.txt', 'w') as file:
    file.write(f"Total combinations: {len(combinations)}\n")
    for combo in combinations:
        file.write(f"{combo}\n")

print("Todos seus jogos foram salvos em combinacoes.txt")
