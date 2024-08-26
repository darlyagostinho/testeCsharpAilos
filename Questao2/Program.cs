using Newtonsoft.Json.Linq;


public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        string baseUrl = $@"https://jsonmock.hackerrank.com/api/football_matches";
        string[] teams = { "team1", "team2" };
        HttpClient client = new HttpClient();
        int totalGoals = 0;
        int totalPages = 0;

        try
        {
            string url = $"{baseUrl}?year={year}&{teams[0]}={team}";

            HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();

            string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            JObject json = JObject.Parse(responseBody);

            totalPages = json["total_pages"].ToObject<int>();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Erro na requisição: " + e.Message);
        }

        for (int i = 1; i <= totalPages; i++)
        {
            foreach (var teamParameter in teams)
            {
                try
                {
                    string url = $"{baseUrl}?year={year}&{teamParameter}={team}&page={i}";

                    HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();

                    string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    JObject json = JObject.Parse(responseBody);

                    foreach (var item in json["data"])
                    {
                        int teamGoals = item[$"{teamParameter}goals"].ToObject<int>();
                        totalGoals += teamGoals;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Erro na requisição: " + e.Message);
                }

            }
        }

        return totalGoals;
    }

}