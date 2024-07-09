namespace LinqSamples
{
    
    internal class Program
    {
        
        static readonly City[] cities = [
    new City("Tokyo", 37_833_000),
    new City("Delhi", 30_290_000),
    new City("Shanghai", 27_110_000),
    new City("São Paulo", 22_043_000),
    new City("Mumbai", 20_412_000),
    new City("Beijing", 20_384_000),
    new City("Cairo", 18_772_000),
    new City("Dhaka", 17_598_000),
    new City("Osaka", 19_281_000),
    new City("New York-Newark", 18_604_000),
    new City("Karachi", 16_094_000),
    new City("Chongqing", 15_872_000),
    new City("Istanbul", 15_029_000),
    new City("Buenos Aires", 15_024_000),
    new City("Kolkata", 14_850_000),
    new City("Lagos", 14_368_000),
    new City("Kinshasa", 14_342_000),
    new City("Manila", 13_923_000),
    new City("Rio de Janeiro", 13_374_000),
    new City("Tianjin", 13_215_000)
];

        static readonly Country[] countries = [
            new Country ("Vatican City", 0.44, 526, [new City("Vatican City", 826)]),
    new Country ("Monaco", 2.02, 38_000, [new City("Monte Carlo", 38_000)]),
    new Country ("Nauru", 21, 10_900, [new City("Yaren", 1_100)]),
    new Country ("Tuvalu", 26, 11_600, [new City("Funafuti", 6_200)]),
    new Country ("San Marino", 61, 33_900, [new City("San Marino", 4_500)]),
    new Country ("Liechtenstein", 160, 38_000, [new City("Vaduz", 5_200)]),
    new Country ("Marshall Islands", 181, 58_000, [new City("Majuro", 28_000)]),
    new Country ("Saint Kitts & Nevis", 261, 53_000, [new City("Basseterre", 13_000)])
        ];
        static void Main(string[] args)
        {
            //Sample1();

            // The Three Parts of a LINQ Query:
            //Sample2();

            //Sample3();

            //Sample4();

            //SampleWithMethod();

            //LinqExtensionMethodsSample();

            int[] list1 = [1, 2, 3, 4, 5];
            int[] list2 = [4, 5, 6, 7, 8];

            // Union
            var unionList = list1.Union(list2);
            var intersectList = list1.Intersect(list2);

            // IQueryable vs IEnumerable
            IQueryable<City> queryableCities = cities.AsQueryable();
            var query = queryableCities.Where(c => c.Population > 100000);
            query = query.Where(c => c.Name.Contains("Tokyo"));

            query.Count();
            query.ToList();

        }

        private static void LinqExtensionMethodsSample()
        {
            // Select one item from country
            var firstCountry = countries[0];
            countries.First(); // Will throw exetion if no item found
            countries.FirstOrDefault(); // Will return null if no item found
            countries.FirstOrDefault(c => c.Name == "Vatican City");
            countries.Last();
            countries.LastOrDefault();

            countries.Single(c => c.Name == "Vatican City");
            countries.SingleOrDefault(c => c.Name == "Vatican City");

            // Select multiple items from country
            var selectedCountries = countries.Where(c =>
            {
                return c.Population > 1000
                   && c.Population <= 2000;
            });

            // Check for data
            bool anyItem = countries.Any();
            bool validation1 = countries.Any(c => c.Population > 1000);
            bool validation2 = countries.All(c => c.Population > 1000);

            // Order for data
            var orderdList = countries.OrderBy(c => c.Name);
            var orderdList2 = countries.OrderByDescending(c => c.Name);

            //
            var skipList = countries.Skip(2);
            var takeList = countries.Take(2);
            var skipTakeList = countries
                .Where(c => c.Population > 1000)
                .OrderBy(c => c.Name).Skip(2).Take(2);


            // Transformation
            var selectList = countries.Select(c => new { c.Name, c.Population });

            // grouping
            var groupedList = cities.GroupBy(c => c.Name[0]);
            foreach (var group in groupedList)
            {
                Console.WriteLine(group.Key);
                foreach (var city in group)
                {
                    Console.WriteLine(city);
                }
            }

            // Counting
            var count = countries.Count();
            var count2 = countries.Count(c => c.Population > 1000);

            // Sum
            var sum = countries.Sum(c => c.Population);

            // Average
            var average = countries.Average(c => c.Population);

            // Max
            var max = countries.Max(c => c.Population);

            // Min
            var min = countries.Min(c => c.Population);
        }

        private static void SampleWithMethod()
        {
            //Query syntax
            IEnumerable<City> queryMajorCities =
                from city in cities
                where city.Population > 100000
                select city;

            // Execute the query to produce the results
            foreach (City city in queryMajorCities)
            {
                Console.WriteLine(city);
            }

            // Output:
            // City { Population = 120000 }
            // City { Population = 112000 }
            // City { Population = 150340 }

            // Method-based syntax
            var queryMajorCities2 = cities
                .Where(c => c.Population > 100000);

            foreach (var city in queryMajorCities2)
            {
                Console.WriteLine(city);
            }
        }

        private static void Sample4()
        {
            // Data source.
            int[] scores = [90, 71, 82, 93, 75, 82];

            // Query Expression.
            IEnumerable<int> scoreQuery = //query variable
                from score in scores //required
                where score > 80 // optional
                orderby score descending // optional
                select score; //must end with select or group

            // Execute the query to produce the results
            foreach (var testScore in scoreQuery)
            {
                Console.WriteLine(testScore);
            }
        }

        private static void Sample3()
        {
            List<int> numbers = [1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20];

            IEnumerable<int> queryFactorsOfFour =
                from num in numbers
                where num % 4 == 0
                select num;

            // Store the results in a new variable
            // without executing a foreach loop.
            var factorsofFourList = queryFactorsOfFour.ToList();

            // Read and write from the newly created list to demonstrate that it holds data.
            Console.WriteLine(factorsofFourList[2]);
            factorsofFourList[2] = 0;
            Console.WriteLine(factorsofFourList[2]);
        }

        private static void Sample2()
        {
            // 1. Data source.
            int[] numbers = [0, 1, 2, 3, 4, 5, 6];

            // 2. Query creation.
            // numQuery is an IEnumerable<int>
            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            // 3. Query execution.
            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }

        private static void Sample1()
        {
            // Specify the data source.
            int[] scores = [97, 92, 81, 60];

            // For loop to find the scores greater than 80.
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] > 80)
                {
                    Console.Write(scores[i] + " ");
                }
            }

            // Define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            // Execute the query.
            foreach (var i in scoreQuery)
            {
                Console.Write(i + " ");
            }
        }
    }

    record City(string Name, long Population);
    record Country(string Name, double Area, long Population, List<City> Cities);
    record Product(string Name, string Category);
}
