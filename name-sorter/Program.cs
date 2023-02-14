


using System.IO.IsolatedStorage;

namespace name_sorter
{
    internal class Program
    {
        // Assumptions 
        // - Each person can only have one last name and multiple given names.
        // - Each person has at least one name that's given to them.
        // - The applications only takes one parameter and needs a parameter otherwise it won't work.

        static void Main(string[] args)
        {
            try
            {
                // Get the name of the file.
                string arguement = args[0];
                string fileName = arguement.Substring(2, arguement.Length - 2);

                // Get the file information in the same directory and then read all the lines.
                string path = Environment.CurrentDirectory + $"\\{fileName}";
                string[] allNames = File.ReadAllLines(path);

                List<Person> personList = ConvertStringsToClass(allNames);
                List<Person> sortedList = personList.OrderBy(n => n.LastName).ThenBy(n => n.GivenNames).ToList();

                foreach (Person person in sortedList)
                {
                    Console.WriteLine($"{person.GivenNames}{person.LastName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Uh Oh! Something went wrong. Sorry!");
                Console.WriteLine(ex);
            }

        }

        /// <summary>
        /// This class represents the person entirely separating their names
        /// so i't's easier to classify and manipulate their information.
        /// </summary>
        public class Person
        {
            public Person(string givenNames, string lastName)
            {
                GivenNames = givenNames;
                LastName = lastName;
            }

            public string GivenNames { get; set; }
            public string LastName { get; set;}

           
        }

        /// <summary>
        /// Converts the string array into a person object, encompassing another class
        /// to help with the process to provide the given names to the person 
        /// based off an intitial assumption.
        /// </summary>
        /// <param name="namesList"></param>
        /// <returns></returns>
        public static List<Person> ConvertStringsToClass(string[] namesList)
        {
            List<Person> personsList = new List<Person>();

            foreach (var fullName in namesList)
            {
                string[] nameArray = fullName.Split(' ');
                
                Person completedPerson = new Person(
                    givenNames: AssignGivenNames(nameArray),
                    lastName: nameArray[nameArray.Length - 1]
                    );

                personsList.Add(completedPerson);
            }

            return personsList;
        }

        /// <summary>
        /// Gets the array of given names and collates them into a single string and
        /// returns it, so it provides the information of given names to the application.
        /// </summary>
        /// <param name="givenNames"></param>
        /// <returns></returns>
        public static string AssignGivenNames(string[] givenNames)
        {
            string completedGivenNames = string.Empty;

            for (int i = 0; i < givenNames.Length - 1; i++)
            {
                completedGivenNames += givenNames[i] + ' ';
            }

            return completedGivenNames;
        }

    }
}