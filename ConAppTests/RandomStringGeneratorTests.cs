using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConApp.Services;
using NUnit.Framework;

namespace ConAppTests
{
    public class RandomStringGeneratorTests
    {
        private RandomStringGenerator _stringGenerator;

       
        [SetUp]
        public void Setup()
        {
            _stringGenerator = new RandomStringGenerator(1_000_000_000);
        }

        [Test]
        public void InitialTest()
        {
            Assert.Pass();
        }

        [Test]
        public void Generate_test()
        {
            var generateStr = _stringGenerator.Generate();

            Assert.IsNotNull(generateStr);
        }

        [Test]
        public void GenerateParallel_test()
        {
            var timer = Stopwatch.StartNew();

            var generatedStr = _stringGenerator.GenerateParallel();
            timer.Stop();

            Console.WriteLine($"\"{nameof(_stringGenerator.GenerateParallel)}\" " +
                              $"elapsed = {timer.Elapsed.TotalMilliseconds} ms, {timer.Elapsed.TotalSeconds} s");

            Assert.IsNotNull(generatedStr);
        }

        [Test]
        public void Benchmark()
        {
            var elapsedInfoTimes = new Dictionary<string, TimeSpan>();

            var timer = Stopwatch.StartNew();

            var inlineGeneratedStr = _stringGenerator.Generate();

            timer.Stop();
            elapsedInfoTimes.Add(nameof(_stringGenerator.Generate), timer.Elapsed);

            timer.Restart();
            var parallelGeneratedStr = _stringGenerator.GenerateParallel();

            timer.Stop();
            elapsedInfoTimes.Add(nameof(_stringGenerator.GenerateParallel), timer.Elapsed);

            foreach (var infoTime in elapsedInfoTimes)
            {
                Console.WriteLine($"\"{infoTime.Key}\" elapsed = {infoTime.Value.TotalMilliseconds} ms, {infoTime.Value.TotalSeconds} s");
            }

            Assert.IsNotNull(inlineGeneratedStr);
            Assert.IsNotNull(parallelGeneratedStr);
            Assert.AreNotEqual(inlineGeneratedStr, parallelGeneratedStr);
        }

        [Test]
        [TestCase(10_000)]
        [TestCase(100_000)]
        [TestCase(1_000_000)]
        [TestCase(10_000_000)]
        [TestCase(100_000_000)]
        public void Benchmark_static_generation(int stringLen)
        {
            var elapsedInfoTimes = new Dictionary<string, TimeSpan>();

            var timer = Stopwatch.StartNew();

            var inlineGeneratedStr = RandomStringGenerator.Generate(stringLen);

            timer.Stop();
            elapsedInfoTimes.Add(nameof(RandomStringGenerator.Generate), timer.Elapsed);

            timer.Restart();
            var parallelGeneratedStr = RandomStringGenerator.GenerateParallel(stringLen);

            timer.Stop();
            elapsedInfoTimes.Add(nameof(RandomStringGenerator.GenerateParallel), timer.Elapsed);

            foreach (var infoTime in elapsedInfoTimes)
            {
                Console.WriteLine($"\"{infoTime.Key}\" elapsed = {infoTime.Value.TotalMilliseconds} ms, {infoTime.Value.TotalSeconds} s");
            }

            Assert.IsNotNull(inlineGeneratedStr);
            Assert.IsNotNull(parallelGeneratedStr);
            Assert.AreNotEqual(inlineGeneratedStr, parallelGeneratedStr);
        }

        [Test]
        [TestCase(10_000)]
        [TestCase(100_000)]
        [TestCase(1_000_000)]
        [TestCase(10_000_000)]
        [TestCase(100_000_000)]
        public void Benchmark_static_generation_reverse(int stringLen)
        {
            var elapsedInfoTimes = new Dictionary<string, TimeSpan>();

            var timer = Stopwatch.StartNew();

            var parallelGeneratedStr = RandomStringGenerator.GenerateParallel(stringLen);

            timer.Stop();
            elapsedInfoTimes.Add(nameof(RandomStringGenerator.GenerateParallel), timer.Elapsed);

            timer.Restart();
            var inlineGeneratedStr = RandomStringGenerator.Generate(stringLen);

            timer.Stop();
            elapsedInfoTimes.Add(nameof(RandomStringGenerator.Generate), timer.Elapsed);

            foreach (var infoTime in elapsedInfoTimes)
            {
                Console.WriteLine($"\"{infoTime.Key}\" elapsed = {infoTime.Value.TotalMilliseconds} ms, {infoTime.Value.TotalSeconds} s");
            }

            Assert.IsNotNull(inlineGeneratedStr);
            Assert.IsNotNull(parallelGeneratedStr);
            Assert.AreNotEqual(inlineGeneratedStr, parallelGeneratedStr);
        }
    }
}