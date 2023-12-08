using BackEnd.Entidades;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace BackEnd.Datos
{
    public class RegistroActividades : IRegistroActividades
    {
        #region Atributos
        private readonly string Conexion = "mongodb+srv://eliza:123456*eli@cluster0.cfbailq.mongodb.net/?retryWrites=true&w=majority";
        private MongoClient ClienteDB;
        private IMongoDatabase BaseDatos;

        private const string NombreBaseDatos = "ControlRutas";
        #endregion

        #region Constructor
        public RegistroActividades()
        {
            try
            {
                BaseDatosEnLinea();
            }
            /*
            catch (MongoException exMDB)
            {
                throw exMDB;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // */
            finally
            {
                if (ClienteDB != null)
                    ClienteDB = null;

                if (BaseDatos != null)
                    BaseDatos = null;
            }
        }
        #endregion

        #region Métodos

        #region Privados
        /// <summary>
        /// Realiza la conexión a la base de datos utilizando los parámetros ya establecidos.
        /// </summary>
        /// <returns>Verdadero si la conexión es posible, falso si la conexión no es posible.</returns>
        private bool BaseDatosEnLinea()
        {
            bool _baseDatosEnLinea = false;

            try
            {
                ClienteDB = new MongoClient(Conexion);

                BaseDatos = ClienteDB.GetDatabase(NombreBaseDatos);
                _baseDatosEnLinea = BaseDatos.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            }
            catch (MongoException exMDB)
            {
                throw exMDB;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _baseDatosEnLinea;
        }
        #endregion

        #region Públicos
        /// <summary>
        /// Agrega una nueva entrada en la colección de actividades.
        /// </summary>
        /// <param name="_actividad">Evento de actividad realizada.</param>
        /// <returns>Cierto si el registro se pudo almacenar, falso si hubo algún error.</returns>
        public void Agregar(Actividad _actividad)
        {
            if (BaseDatosEnLinea())
            {
                try
                {
                    var _registroActividades = BaseDatos.GetCollection<Actividad>("RegistroActividades");
                    _registroActividades.InsertOne(_actividad);
                }
                catch (MongoException exMDB)
                {
                    throw exMDB;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ClienteDB = null;
                    BaseDatos = null;
                }
            }
        }

        /// <summary>
        /// Devuelve la lista completa de actividades realizadas en el sistema para un listado completo.
        /// </summary>
        /// <returns>Lista de entidades de tipo RegistroActividad</returns>
        public List<Actividad> ListarTodo()
        {
            List<Actividad> _actividades = new List<Actividad>();

            if (BaseDatosEnLinea())
            {
                try
                {
                    var _registroActividades = BaseDatos.GetCollection<Actividad>("RegistroActividades");
                    _actividades = _registroActividades.Find(d => true).ToList();
                }
                catch (MongoException exMDB)
                {
                    throw exMDB;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ClienteDB = null;
                    BaseDatos = null;
                }
            }

            return _actividades;
        }
        #endregion

        #endregion
    }
}

