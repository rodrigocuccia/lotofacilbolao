#!/bin/bash

# Funcao pra calcular fatorial
fatorial() {
    n=$1
    fact=1
    while [ $n -gt 1 ]; do
        fact=$((fact * n))
        n=$((n - 1))
    done
    echo $fact
}

n=19
k=15

# Calculando n! / (k! * (n-k)!)
combinacaoes=$(echo "scale=0; $(fatorial $n) / ($(fatorial $k) * $(fatorial $(($n - $k))))" | bc)

echo "Numero total de combinacoes: $combinacoes"

