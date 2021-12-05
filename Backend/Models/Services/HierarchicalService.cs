using Backend.Models.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Models.Clustering;
using Backend.Models.Database;
using System;
using System.Linq;
namespace Backend.Models.Services
{
    public class HierarchicalService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HierarchicalService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<List<Cluster>> FindHierarchicalCluster()
        {
            var blogs = await _unitOfWork.Blogs.GetAllBlogsWithData();
            var clusters = CreateInitialClusters(blogs);
            int count = 1;

            while (clusters.Count > 1)
            {
                Console.WriteLine($"I {count}\t L: {clusters.Count}");
                Iterate(clusters);
                count++;
            }
            return clusters;
        }

        private List<Cluster> CreateInitialClusters(List<Blog> blogs)
        {
            var clusters = new List<Cluster>();
            foreach (var b in blogs)
            {
                Cluster cluster = new Cluster();
                cluster.Blog = b;
                clusters.Add(cluster);
            }
            return clusters;
        }

        private void Iterate(List<Cluster> clusters)
        {
            //Find closest
            double closest = Double.MaxValue;
            var A = new Cluster();
            var B = new Cluster();
            foreach (var cA in clusters)
                foreach (var cB in clusters)
                {
                    var distance = PearsonCorrelation(cA.Blog, cB.Blog);
                    if (distance < closest && cA != cB)
                    {
                        //Set new closest
                        closest = distance;
                        A = cA;
                        B = cB;
                    }
                }

            //Merge merge closest
            Console.WriteLine($"M {A.Blog.BlogTitle} and {B.Blog.BlogTitle}");
            var nC = Merge(A, B, closest);
            //Add merged cluster
            clusters.Add(nC);
            //Remove old singular clusters
            clusters.Remove(A);
            clusters.Remove(B);
        }

        private Cluster Merge(Cluster A, Cluster B, double distance)
        {
            var P = new Cluster();
            P.Left = A;
            A.Parent = P;
            P.Right = B;
            B.Parent = P;
            //Averaging word counts for each word
            var nB = new Blog();

            var n = 0;
            foreach (var item in A.Blog.Words)
            {
                var wA = A.Blog.Words.ElementAt(n);
                var wB = B.Blog.Words.ElementAt(n);
                var cnt = (wA.Amount + wB.Amount) / 2;
                var newWord = new Word();
                nB.Words.Add(new Word
                {
                    WordTitle = wA.WordTitle,
                    Amount = cnt
                });
                n++;
            }

            //Set blog to new cluster
            P.Blog = nB;
            //Set distance
            P.Distance = distance;
            //Return new parent cluster
            return P;
        }

        private double PearsonCorrelation(Blog A, Blog B)
        {
            double sumA = 0, sumB = 0, sumAsq = 0, sumBsq = 0, pSum = 0;
            int n = 0;  //Counter for amount of total words

            foreach (var word in A.Words)
            {
                var wA = A.Words.ElementAt(n); // Order repositories in a way that enables me to use ElementAt O(1) instead of potentually looping all words in O(n).
                var wB = B.Words.ElementAt(n); // Order repositories in a way that enables me to use ElementAt O(1) instead of potentually looping all words in O(n).
                sumA += wA.Amount;
                sumB += wB.Amount;
                sumAsq += wA.Amount * wA.Amount;
                sumBsq += wB.Amount * wB.Amount;
                pSum += wA.Amount * wB.Amount;
                n++;
            }
            //Calculate
            var num = pSum - ((sumA * sumB) / n);
            var den = Math.Sqrt((sumAsq - Math.Pow(sumA, 2.0) / n) * (sumBsq - Math.Pow(sumB, 2.0) / n));
            return (1.0 - num / den);
        }
    }
}