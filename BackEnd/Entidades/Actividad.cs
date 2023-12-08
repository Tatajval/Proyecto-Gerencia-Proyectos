using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackEnd.Entidades
{
    public class Actividad
    {
        #region Atributos
        [Display(Name = "ID Objeto")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ObjID { get; set; }

        [Display(Name = "Acción")]
        [BsonElement("accion")]
        public string Accion { get; set; }
        [Display(Name = "Tipo")]
        [BsonElement("tipo_objeto")]
        public string Tipo { get; set; }
        [Display(Name = "Objeto")]
        [BsonElement("objeto")]
        public string Objeto { get; set; }
        [Display(Name = "Usuario")]
        [BsonElement("usuario")]
        public string Usuario { get; set; }
        [Display(Name = "Completada?")]
        [BsonElement("completada")]
        public bool Completada { get; set; }
        [Display(Name = "Fecha y hora")]
        [BsonElement("fecha_hora")]
        public DateTime FechaHora { get; set; }
        #endregion

        #region Constructor
        public Actividad()
        {
            Accion = string.Empty;
            Tipo = string.Empty;
            Objeto = string.Empty;
            Usuario = string.Empty;
            Completada = false;
            FechaHora = DateTime.MinValue;
        }
        #endregion
    }
}
