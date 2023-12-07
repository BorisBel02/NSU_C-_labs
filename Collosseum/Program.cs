
using Colloseum.Model;



int iterations = 1000000;

double wins = 0;

Experiment experiment = new();

for (int i = 0; i <= iterations; ++i)
{
    if (experiment.Run())
    {
        ++wins;
    }
}

Console.WriteLine("p = " + (wins/iterations));
