namespace Challenge.Models
{
    public class Province
    {
        public class ProvinceResult:Interfaces.IResult
        {
            public int cantidad { get; set; }
            public int inicio { get; set; }
            public Parametros parametros { get; set; }
            public List<Provincias> provincias { get; set; }
            public int total { get; set; }
            public int IdResponse { get; set; }
            public string Response { get; set; }
        }
        public class Parametros
        {
            public string nombre { get; set; }
        }

        public class Provincias
        {
            public Coordenadas centroide { get; set; }
            public int id { get; set; }
            public string nombre { get; set; }
        }
        public class Coordenadas
        {
            public float lat { get; set; }
            public float lon { get; set; }
        }
    }
}
