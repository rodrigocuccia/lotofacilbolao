#!/bin/bash

# Function to calculate factorial
factorial() {
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

# Calculate n! / (k! * (n-k)!)
combinations=$(echo "scale=0; $(factorial $n) / ($(factorial $k) * $(factorial $(($n - $k))))" | bc)

echo "Total number of combinations: $combinations"

