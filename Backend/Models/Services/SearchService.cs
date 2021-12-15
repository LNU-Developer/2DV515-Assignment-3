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
        public SearchService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<List<Score>> FindKSearchResult(string searchedWord, int k)
        {
            var hashIds = await GetHashIdsForWords(searchedWord);
            var pages = _unitOfWork.Pages.GetAllPagesWithWordsQuery();

            var metrics = new Metric(pages.Count());
            var scores = new List<Score>();

            int x = 0;
            foreach (var page in pages)
            {
                metrics.Content[x] = GetFrequencyMetric(page, hashIds);
                x++;
            }

            int i = 0;
            Normalize(metrics.Content, false);
            foreach (var page in pages)
            {
                scores.Add(new Score
                {
                    Page = page,
                    Content = metrics.Content[i],
                    Location = metrics.Location[i]
                });
                i++;
            }
            return scores.OrderByDescending(o => o.Content).Take(k).ToList();
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
            {
                if (query.Contains(word.WordHashMapId))
                    score++;
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
                double max = scores.Max();
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