namespace centro.recursos.net.Models.Entity_Framework
{
    public class ClasePersonalizada
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public InfoRegistro Auditoria { get; set; }

        public string ArticuloId { get; set; }
        public Articulo Articulo { get; set; }
    }
}