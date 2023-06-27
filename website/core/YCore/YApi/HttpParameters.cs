namespace YApi
{
    public class HttpParameters
    {
        public class Parameter
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public Parameter(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public Parameter(string name, object value)
            {
                Name = name;
                Value = value.ToString() ?? throw new ArgumentNullException(nameof(value));
            }
        }

        private readonly List<Parameter> _parameters = new();

        public List<Parameter> Parameters => _parameters;

        public string this[string name] => Parameters.Find(p => p.Name == name)?.Value 
            ?? throw new NullReferenceException($"Parameter \"{name}\" not found.");

        public static implicit operator Dictionary<string, object>(HttpParameters httpParameters) => 
            httpParameters.Parameters.ToDictionary(p => p.Name, p => (object)p.Value);

        public void Add(string name, string value)
        {
            if (Contains(name))
                throw new InvalidOperationException($"Parameters already contains \"{name}\".");
            Parameters.Add(new(name, value));
        }

        public void Add(string name, int value)
        {
            if (Contains(name))
                throw new InvalidOperationException($"Parameters already contains \"{name}\".");
            Parameters.Add(new(name, Convert.ToString(value)));
        }

        public void Add(string name, object value)
        {
            if (Contains(name))
                throw new InvalidOperationException($"Parameters already contains \"{name}\".");
            Parameters.Add(new(name, value));
        }

        public void Set(string name, string value)
        {
            var parameter = Parameters.Find(p => p.Name == name) 
                ?? throw new NullReferenceException($"Parameter \"{name}\" not found.");
            parameter.Value = value;
        }

        public void Set(string name, int value)
        {
            var parameter = Parameters.Find(p => p.Name == name)
                ?? throw new NullReferenceException($"Parameter \"{name}\" not found.");
            parameter.Value = Convert.ToString(value);
        }

        public void Set(string name, object value)
        {
            var parameter = Parameters.Find(p => p.Name == name)
                ?? throw new NullReferenceException($"Parameter \"{name}\" not found.");
            parameter.Value = value.ToString() ?? throw new ArgumentNullException(nameof(value));
        }

        public void Delete(string name)
        {
            Parameters.RemoveAll(p => p.Name == name);
        }

        public bool Contains(string name) => Parameters.Any(p => p.Name == name);

        public int Count() => Parameters.Count;

        public override string? ToString() => 
            _parameters.Select(p => $"{p.Name}={p.Value}")
                .Aggregate((p1, p2) => $"{p1}&{p2}");
    }
}