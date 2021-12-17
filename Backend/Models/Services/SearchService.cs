using System.Threading.Tasks;
using Backend.Models.Database;
using Backend.Models.Repositories;
using System.Linq;
using Backend.Models.Search;
using System;
using System.Collections.Generic;
namespace Backend.Models.Services
{
    public class SearchService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SearchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<List<Score>> FindKSearchResult(string searchedWord, int k)
        {
            Console.WriteLine("Fetch data");
            Console.WriteLine("- Get Ids");
            var hashIds = await GetHashIdsForWords(searchedWord);
            Console.WriteLine("- Get pages");
            var pages = await _unitOfWork.Pages.GetAllPagesWithWords();


            var metrics = new Metric(pages.Count);

            Console.WriteLine("Loop data");
            int x = 0;
            foreach (var page in pages)
            {
                metrics.Content[x] = GetFrequencyMetric(page, hashIds);
                metrics.Location[x] = GetLocationMetric(page, hashIds);
                x++;
            }

            Console.WriteLine("Normalize");
            Normalize(metrics.Content, false);
            Normalize(metrics.Location, true);

            Console.WriteLine("Create scores");
            var scores = new List<Score>();
            int i = 0;
            foreach (var page in pages)
            {
                scores.Add(new Score(page, metrics.Content[i], metrics.Location[i]));
                i++;
            }
            return scores.OrderByDescending(o => o.TotalScore).Take(k).ToList();
        }

        private async Task<int[]> GetHashIdsForWords(string searchedWord)
        {
            var splitWords = searchedWord.Split();
            var hashIds = new int[splitWords.Count()];

            int y = 0;
            foreach (var word in splitWords)
            {
                hashIds[y] = await GetIdForWord(word);
                y++;
            }
            return hashIds;
        }

        private double GetFrequencyMetric(Page page, int[] query)
        {
            double score = 0;
            foreach (var word in page.PageWords)
                if (query.Contains(word.WordHashMapId))
                    score++;
            return score;
        }

        private double GetLocationMetric(Page page, int[] query)
        {
            double score = 0;
            foreach (var q in query)
            {
                bool found = false;
                foreach (var word in page.PageWords.OrderBy(x => x.Order)) //The order that things get inserted into the databse is not decided by the order of the insert, as such I needed a new field to hold the ordervalue
                {
                    if (word.WordHashMapId == q)
                    {
                        score += word.Order;
                        found = true;
                        break;
                    }
                    if (!found) score += 1000000;
                }
            }
            return score;
        }

        private void Normalize(double[] scores, bool smallIsBetter)
        {
            if (smallIsBetter)
            {
                double min = scores.Min();
                for (int i = 0; i < scores.Length; i++)
                    scores[i] = min / Math.Max(scores[i], 0.00001);
            }
            else
            {
                double max = Math.Max(scores.Max(), 0.00001);
                for (int i = 0; i < scores.Length; i++)
                    scores[i] = scores[i] / max;
            }
        }
        private async Task<int> GetIdForWord(string word)
        {
            var w = await _unitOfWork.WordMaps.GetWithFilterAsync(x => x.Word == word);
            if (w != null)
                return w.WordMapId;
            else
                return 0;
        }
    }
}