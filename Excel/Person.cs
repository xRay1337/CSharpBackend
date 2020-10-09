namespace Excel
{
    class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public Person(int id, string name, int age, string phone)
        {
            Id = id;
            Name = name;
            Age = age;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Age: {Age}, Phone: {Phone}";
        }
    }
}