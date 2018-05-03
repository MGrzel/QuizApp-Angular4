using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizAppApi.Models
{
    public class GeoJson<T>
    {
        public string Type { get; set; }
        public Crs Crs { get; set; }
        public List<Feature<T>> Features { get; set; }
    }

    public class Properties
    {
        public string Name { get; set; }
    }

    public class Crs
    {
        public string Type { get; set; }
        public Properties Properties { get; set; }
    }

    public class Feature<T>
    {
        public string Type { get; set; }
        public T Properties { get; set; }
        public object Geometry { get; set; }
    }
}
