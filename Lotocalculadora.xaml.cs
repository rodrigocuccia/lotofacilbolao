using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LotteryApp
{
    public partial class MainWindow : Window
    {
        private List<int> selectedNumbers;
        private List<HashSet<int>> pastDraws;
        private List<List<int>> multipleBets;

        public MainWindow()
        {
            InitializeComponent();
            selectedNumbers = new List<int>();
            multipleBets = new List<List<int>>();

            // Load past draws from the file
            LoadPastDraws("C:\\Users\\Sycomp\\Documents\\lotofacil-dotnet\\Lotocalculadora\\jogos.txt");

            // Create and add buttons to the grid
            for (int i = 1; i <= 25; i++)
            {
                Button button = new Button
                {
                    Content = i.ToString(),
                    Background = System.Windows.Media.Brushes.Purple,
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Width = 60,
                    Height = 60,
                    Margin = new Thickness(5)
                };

                button.Click += NumberButton_Click;
                NumberGrid.Children.Add(button);
            }
        }

        private void LoadPastDraws(string filePath)
        {
            pastDraws = new List<HashSet<int>>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var numbers = line.Split(new[] { '\t', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(num => int.Parse(num.Trim()))
                                      .ToHashSet();
                    pastDraws.Add(numbers);
                }
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                int number = Convert.ToInt32(button.Content);
                if (selectedNumbers.Contains(number))
                {
                    selectedNumbers.Remove(number);
                    button.Background = System.Windows.Media.Brushes.Purple; // Reset to original color
                }
                else
                {
                    if (selectedNumbers.Count < 20) // Limit to 20 numbers
                    {
                        selectedNumbers.Add(number);
                        button.Background = System.Windows.Media.Brushes.Gray; // Change color to indicate selection
                    }
                }

                SelectedNumbersLabel.Content = $"Numeros Selecionados: {selectedNumbers.Count}";

                // Enable or disable the Add Bet button based on the number of selected numbers
                if (selectedNumbers.Count >= 15 && selectedNumbers.Count <= 20 && BetOptions.SelectedIndex == 1) // 1 = "Multiple Bets"
                {
                    AddBetButton.IsEnabled = true;
                }
                else
                {
                    AddBetButton.IsEnabled = false;
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedNumbers.Count >= 15)
            {
                multipleBets.Add(new List<int>(selectedNumbers));
                MessageBox.Show($"Aposta {multipleBets.Count} adicionada com {selectedNumbers.Count} numeros.");
                ResetSelection();
            }
            else
            {
                MessageBox.Show("Selecione pelo menos 15 numeros.");
            }
        }

        private void OddsButton_Click(object sender, RoutedEventArgs e)
        {
            if (multipleBets.Count == 0 && selectedNumbers.Count < 15)
            {
                MessageBox.Show("Selecione pelo menos 15 numeros ou adicione pelo menos 1 aposta para calcular.");
                return;
            }

            double totalOdds = 0;
            List<HashSet<int>> allBets = new List<HashSet<int>>();

            if (multipleBets.Count > 0)
            {
                foreach (var bet in multipleBets)
                {
                    allBets.Add(new HashSet<int>(bet));
                }
            }
            else
            {
                allBets.Add(new HashSet<int>(selectedNumbers));
            }

            StringBuilder resultMessage = new StringBuilder();
            List<string> drawnCombinationsForClipboard = new List<string>();

            foreach (var bet in allBets)
            {
                var drawnCombinations = HasCombinationBeenDrawnBefore(bet.ToList());

                resultMessage.AppendLine($"Seu jogo: {string.Join(", ", bet)}");

                if (drawnCombinations.Any())
                {
                    resultMessage.AppendLine("Os jogos abaixo ja foram sorteados anteriormente:");
                    foreach (var combination in drawnCombinations)
                    {
                        string combinationString = string.Join(", ", combination);
                        resultMessage.AppendLine(combinationString);
                        drawnCombinationsForClipboard.Add(combinationString);
                    }
                }
                else
                {
                    resultMessage.AppendLine("Dessa combinacao nenhum jogo foi sorteado anteriormente.");
                }

                double totalBalls = 25;
                double drawnBalls = 15;
                double totalSelections = bet.Count;

                double odds = CalculateOdds(totalBalls, drawnBalls, totalSelections);
                totalOdds += odds;

                resultMessage.AppendLine(); // Add a newline between different bets
            }

            double winningOdds = 1 / totalOdds;
            double percentage = totalOdds * 100;

            resultMessage.AppendLine($"Sua chance combinada de ganhar: 1 em {Math.Round(winningOdds):N0}");
            resultMessage.AppendLine($"A procentagem: {percentage:F8}%");

            // Show the aggregated result in a single message box
            MessageBox.Show(resultMessage.ToString());

            // If any drawn combinations were found, offer to copy them to the clipboard
            if (drawnCombinationsForClipboard.Any())
            {
                string clipboardText = string.Join(Environment.NewLine, drawnCombinationsForClipboard);
                Clipboard.SetText(clipboardText);
                MessageBox.Show("Os jogos que ja sairam foram copiados para a area de transferencia.");
            }
        }


        private List<HashSet<int>> HasCombinationBeenDrawnBefore(List<int> userSelection)
        {
            var userSet = new HashSet<int>(userSelection);
            var drawnCombinations = new List<HashSet<int>>();

            if (userSelection.Count > 15)
            {
                var combinations = GetCombinations(userSelection, 15);
                foreach (var combination in combinations)
                {
                    foreach (var draw in pastDraws)
                    {
                        if (draw.SetEquals(combination))
                        {
                            drawnCombinations.Add(combination);
                        }
                    }
                }
            }
            else
            {
                foreach (var draw in pastDraws)
                {
                    if (draw.SetEquals(userSet))
                    {
                        drawnCombinations.Add(userSet);
                        break;
                    }
                }
            }

            return drawnCombinations;
        }

        private IEnumerable<HashSet<int>> GetCombinations(List<int> numbers, int length)
        {
            int[] indices = Enumerable.Range(0, length).ToArray();
            HashSet<int> combination = new HashSet<int>();

            while (true)
            {
                combination = new HashSet<int>(indices.Select(i => numbers[i]));

                yield return combination;

                int i;
                for (i = length - 1; i >= 0 && indices[i] == numbers.Count - length + i; i--) ;

                if (i < 0) yield break;

                indices[i]++;
                for (int j = i + 1; j < length; j++) indices[j] = indices[j - 1] + 1;
            }
        }

        private double CalculateOdds(double totalBalls, double drawnBalls, double totalSelections)
        {
            double totalCombinations = Combinations(totalBalls, drawnBalls);
            double winningCombinations = Combinations(totalSelections, drawnBalls);
            double odds = winningCombinations / totalCombinations;

            return odds;
        }

        private double Combinations(double n, double k)
        {
            if (k > n)
                return 0;
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }

        private double Factorial(double number)
        {
            if (number <= 1)
                return 1;
            return number * Factorial(number - 1);
        }

        private void ResetSelection()
        {
            selectedNumbers.Clear();
            foreach (var child in NumberGrid.Children)
            {
                if (child is Button button)
                {
                    button.Background = System.Windows.Media.Brushes.Purple;
                }
            }
            SelectedNumbersLabel.Content = "Numeros selecionados: 0";
            AddBetButton.IsEnabled = false;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the selected numbers from the grid
            ResetSelection();

            // Check if the user is in "Multiple Bets" mode
            if (BetOptions.SelectedIndex == 1) // 1 = "Multiple Bets"
            {
                // Clear the saved bets
                multipleBets.Clear();
                MessageBox.Show("Todas as apostas salvas foram excluidas.");
            }
            else
            {
                MessageBox.Show("Numeros selecionados foram resetados.");
            }
        }

        private void PercentageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(PercentageTextBox.Text, out int percentage) && percentage >= 1 && percentage <= 100)
            {
                CalculateGamesButton.IsEnabled = true;
            }
            else
            {
                CalculateGamesButton.IsEnabled = false;
            }
        }

 private void CalculateGamesButton_Click(object sender, RoutedEventArgs e)
{
    if (int.TryParse(PercentageTextBox.Text, out int percentage) && percentage >= 1 && percentage <= 100)
    {
        double targetOdds = percentage / 100.0; // Convert percentage to odds

        // Calculate total combinations for different game types
        double[] oddsPerGame = new double[6];
        oddsPerGame[0] = CalculateOdds(25, 15, 15); // 15 numbers
        oddsPerGame[1] = CalculateOdds(25, 15, 16); // 16 numbers
        oddsPerGame[2] = CalculateOdds(25, 15, 17); // 17 numbers
        oddsPerGame[3] = CalculateOdds(25, 15, 18); // 18 numbers
        oddsPerGame[4] = CalculateOdds(25, 15, 19); // 19 numbers
        oddsPerGame[5] = CalculateOdds(25, 15, 20); // 20 numbers

        double[] costsPerGame = { 3.00, 48.00, 408.00, 2448.00, 11628.00, 46512.00 };
        int[] numbersInGame = { 15, 16, 17, 18, 19, 20 };

        for (int i = 5; i >= 0; i--)
        {
            double requiredGames = Math.Ceiling(targetOdds / oddsPerGame[i]);
            double totalCost = requiredGames * costsPerGame[i];

            if (requiredGames > 0 && requiredGames < 1e6) // Limit games to prevent excessive results
            {
                ResultTextBox.Text = $"Para atingir {percentage}% de chande de ganhar, voce precisa jogar aproximadamente {requiredGames:N0} jogos de {numbersInGame[i]} numeros.\n" +
                                       $"O custo total sera de R$ {totalCost:N2}.";
                return;
            }
        }

        ResultTextBox.Text = "A porcentagem esta muito alta para atingir um numero razoavel de jogos.";
    }
    else
    {
        MessageBox.Show("Entre uma porcentagem valida (1-100).");
    }
}

        private double CalculateTotalCombinations()
        {
            double totalBalls = 25;
            double drawnBalls = 15;
            double totalSelections = selectedNumbers.Count;

            double totalCombinations = Combinations(totalBalls, drawnBalls);
            double winningCombinations = Combinations(totalSelections, drawnBalls);

            return winningCombinations / totalCombinations;
        }

        private double CalculateTotalCost(double requiredGames)
        {
            double costPerGame = 0;

            switch (selectedNumbers.Count)
            {
                case 15:
                    costPerGame = 3.00;
                    break;
                case 16:
                    costPerGame = 48.00;
                    break;
                case 17:
                    costPerGame = 408.00;
                    break;
                case 18:
                    costPerGame = 2448.00;
                    break;
                case 19:
                    costPerGame = 11628.00;
                    break;
                case 20:
                    costPerGame = 46512.00;
                    break;
            }

            return costPerGame * requiredGames;
        }


    }
}
