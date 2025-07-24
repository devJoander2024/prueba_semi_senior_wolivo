namespace prueba_tecnica_semi_senior_wolivo.Dtos.Report
{
    public class ReporteActividadDto
    {
        public string Usuario { get; set; }
        public List<ProyectoActividadesDto> Proyectos { get; set; }
    }

    public class ProyectoActividadesDto
    {
        public string Nombre { get; set; }
        public List<ActividadReporteDto> Actividades { get; set; }
    }

    public class ActividadReporteDto
    {
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }
        public int HorasReales { get; set; }
    }

    public class ActividadProyectoJoin
    {
        public Guid ProyectoId { get; set; }
        public string ProyectoNombre { get; set; }
        public DateTime ActividadFecha { get; set; }
        public string ActividadTitulo { get; set; }
        public int ActividadHorasReales { get; set; }
    }

}
