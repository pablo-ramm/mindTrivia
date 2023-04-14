namespace MindTrivia.Models
{
    public class PreguntasModel
    {
        public int IdPregunta { get; set; }
        public string Categoria { get; set; }
        public string Material { get; set; }
        public string[] Preguntas { get; set; }
        public string[] RespuestasCorrectas { get; set; }
        public string[] RespuestasIncorrectas { get; set; }
        public string[] RespuestasIncorrectas2 { get; set; }

    }
}
