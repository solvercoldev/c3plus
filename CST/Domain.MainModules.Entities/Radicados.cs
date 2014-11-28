//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

using Domain.Core.Entities;

namespace Domain.MainModules.Entities
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Contratos))]
    [KnownType(typeof(DocumentosRadicado))]
    [KnownType(typeof(TBL_Admin_Usuarios))]
    
    public partial class Radicados: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdContrato
        {
            get { return _idContrato; }
            set
            {
                if (_idContrato != value)
                {
                    ChangeTracker.RecordOriginalValue("IdContrato", _idContrato);
                    if (!IsDeserializing)
                    {
                        if (Contratos != null && Contratos.IdContrato != value)
                        {
                            Contratos = null;
                        }
                    }
                    _idContrato = value;
                    OnPropertyChanged("IdContrato");
                }
            }
        }
        private int _idContrato;
    
        [DataMember]
        public long IdRadicado
        {
            get { return _idRadicado; }
            set
            {
                if (_idRadicado != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdRadicado' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idRadicado = value;
                    OnPropertyChanged("IdRadicado");
                }
            }
        }
        private long _idRadicado;
    
        [DataMember]
        public byte TipoRadicado
        {
            get { return _tipoRadicado; }
            set
            {
                if (_tipoRadicado != value)
                {
                    _tipoRadicado = value;
                    OnPropertyChanged("TipoRadicado");
                }
            }
        }
        private byte _tipoRadicado;
    
        [DataMember]
        public string Numero
        {
            get { return _numero; }
            set
            {
                if (_numero != value)
                {
                    _numero = value;
                    OnPropertyChanged("Numero");
                }
            }
        }
        private string _numero;
    
        [DataMember]
        public System.DateTime FechaReciboSalida
        {
            get { return _fechaReciboSalida; }
            set
            {
                if (_fechaReciboSalida != value)
                {
                    _fechaReciboSalida = value;
                    OnPropertyChanged("FechaReciboSalida");
                }
            }
        }
        private System.DateTime _fechaReciboSalida;
    
        [DataMember]
        public string Asunto
        {
            get { return _asunto; }
            set
            {
                if (_asunto != value)
                {
                    _asunto = value;
                    OnPropertyChanged("Asunto");
                }
            }
        }
        private string _asunto;
    
        [DataMember]
        public Nullable<int> IdFrom
        {
            get { return _idFrom; }
            set
            {
                if (_idFrom != value)
                {
                    ChangeTracker.RecordOriginalValue("IdFrom", _idFrom);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios2 != null && TBL_Admin_Usuarios2.IdUser != value)
                        {
                            TBL_Admin_Usuarios2 = null;
                        }
                    }
                    _idFrom = value;
                    OnPropertyChanged("IdFrom");
                }
            }
        }
        private Nullable<int> _idFrom;
    
        [DataMember]
        public string IdFromExterno
        {
            get { return _idFromExterno; }
            set
            {
                if (_idFromExterno != value)
                {
                    _idFromExterno = value;
                    OnPropertyChanged("IdFromExterno");
                }
            }
        }
        private string _idFromExterno;
    
        [DataMember]
        public Nullable<int> IdTo
        {
            get { return _idTo; }
            set
            {
                if (_idTo != value)
                {
                    ChangeTracker.RecordOriginalValue("IdTo", _idTo);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios3 != null && TBL_Admin_Usuarios3.IdUser != value)
                        {
                            TBL_Admin_Usuarios3 = null;
                        }
                    }
                    _idTo = value;
                    OnPropertyChanged("IdTo");
                }
            }
        }
        private Nullable<int> _idTo;
    
        [DataMember]
        public string IdToExterno
        {
            get { return _idToExterno; }
            set
            {
                if (_idToExterno != value)
                {
                    _idToExterno = value;
                    OnPropertyChanged("IdToExterno");
                }
            }
        }
        private string _idToExterno;
    
        [DataMember]
        public string Resumen
        {
            get { return _resumen; }
            set
            {
                if (_resumen != value)
                {
                    _resumen = value;
                    OnPropertyChanged("Resumen");
                }
            }
        }
        private string _resumen;
    
        [DataMember]
        public bool RespuestaPendiente
        {
            get { return _respuestaPendiente; }
            set
            {
                if (_respuestaPendiente != value)
                {
                    _respuestaPendiente = value;
                    OnPropertyChanged("RespuestaPendiente");
                }
            }
        }
        private bool _respuestaPendiente;
    
        [DataMember]
        public byte EstadoRadicado
        {
            get { return _estadoRadicado; }
            set
            {
                if (_estadoRadicado != value)
                {
                    _estadoRadicado = value;
                    OnPropertyChanged("EstadoRadicado");
                }
            }
        }
        private byte _estadoRadicado;
    
        [DataMember]
        public Nullable<System.DateTime> FechaRespuesta
        {
            get { return _fechaRespuesta; }
            set
            {
                if (_fechaRespuesta != value)
                {
                    _fechaRespuesta = value;
                    OnPropertyChanged("FechaRespuesta");
                }
            }
        }
        private Nullable<System.DateTime> _fechaRespuesta;
    
        [DataMember]
        public long IdRadicadoEntrada
        {
            get { return _idRadicadoEntrada; }
            set
            {
                if (_idRadicadoEntrada != value)
                {
                    _idRadicadoEntrada = value;
                    OnPropertyChanged("IdRadicadoEntrada");
                }
            }
        }
        private long _idRadicadoEntrada;
    
        [DataMember]
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged("IsActive");
                }
            }
        }
        private bool _isActive;
    
        [DataMember]
        public int CreateBy
        {
            get { return _createBy; }
            set
            {
                if (_createBy != value)
                {
                    ChangeTracker.RecordOriginalValue("CreateBy", _createBy);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios != null && TBL_Admin_Usuarios.IdUser != value)
                        {
                            TBL_Admin_Usuarios = null;
                        }
                    }
                    _createBy = value;
                    OnPropertyChanged("CreateBy");
                }
            }
        }
        private int _createBy;
    
        [DataMember]
        public System.DateTime CreateOn
        {
            get { return _createOn; }
            set
            {
                if (_createOn != value)
                {
                    _createOn = value;
                    OnPropertyChanged("CreateOn");
                }
            }
        }
        private System.DateTime _createOn;
    
        [DataMember]
        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set
            {
                if (_modifiedBy != value)
                {
                    ChangeTracker.RecordOriginalValue("ModifiedBy", _modifiedBy);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios1 != null && TBL_Admin_Usuarios1.IdUser != value)
                        {
                            TBL_Admin_Usuarios1 = null;
                        }
                    }
                    _modifiedBy = value;
                    OnPropertyChanged("ModifiedBy");
                }
            }
        }
        private int _modifiedBy;
    
        [DataMember]
        public System.DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set
            {
                if (_modifiedOn != value)
                {
                    _modifiedOn = value;
                    OnPropertyChanged("ModifiedOn");
                }
            }
        }
        private System.DateTime _modifiedOn;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Contratos Contratos
        {
            get { return _contratos; }
            set
            {
                if (!ReferenceEquals(_contratos, value))
                {
                    var previousValue = _contratos;
                    _contratos = value;
                    FixupContratos(previousValue);
                    OnNavigationPropertyChanged("Contratos");
                }
            }
        }
        private Contratos _contratos;
    
        [DataMember]
        public TrackableCollection<DocumentosRadicado> DocumentosRadicado
        {
            get
            {
                if (_documentosRadicado == null)
                {
                    _documentosRadicado = new TrackableCollection<DocumentosRadicado>();
                    _documentosRadicado.CollectionChanged += FixupDocumentosRadicado;
                }
                return _documentosRadicado;
            }
            set
            {
                if (!ReferenceEquals(_documentosRadicado, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_documentosRadicado != null)
                    {
                        _documentosRadicado.CollectionChanged -= FixupDocumentosRadicado;
                    }
                    _documentosRadicado = value;
                    if (_documentosRadicado != null)
                    {
                        _documentosRadicado.CollectionChanged += FixupDocumentosRadicado;
                    }
                    OnNavigationPropertyChanged("DocumentosRadicado");
                }
            }
        }
        private TrackableCollection<DocumentosRadicado> _documentosRadicado;
    
        [DataMember]
        public TBL_Admin_Usuarios TBL_Admin_Usuarios
        {
            get { return _tBL_Admin_Usuarios; }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios, value))
                {
                    var previousValue = _tBL_Admin_Usuarios;
                    _tBL_Admin_Usuarios = value;
                    FixupTBL_Admin_Usuarios(previousValue);
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios");
                }
            }
        }
        private TBL_Admin_Usuarios _tBL_Admin_Usuarios;
    
        [DataMember]
        public TBL_Admin_Usuarios TBL_Admin_Usuarios1
        {
            get { return _tBL_Admin_Usuarios1; }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios1, value))
                {
                    var previousValue = _tBL_Admin_Usuarios1;
                    _tBL_Admin_Usuarios1 = value;
                    FixupTBL_Admin_Usuarios1(previousValue);
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios1");
                }
            }
        }
        private TBL_Admin_Usuarios _tBL_Admin_Usuarios1;
    
        [DataMember]
        public TBL_Admin_Usuarios TBL_Admin_Usuarios2
        {
            get { return _tBL_Admin_Usuarios2; }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios2, value))
                {
                    var previousValue = _tBL_Admin_Usuarios2;
                    _tBL_Admin_Usuarios2 = value;
                    FixupTBL_Admin_Usuarios2(previousValue);
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios2");
                }
            }
        }
        private TBL_Admin_Usuarios _tBL_Admin_Usuarios2;
    
        [DataMember]
        public TBL_Admin_Usuarios TBL_Admin_Usuarios3
        {
            get { return _tBL_Admin_Usuarios3; }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios3, value))
                {
                    var previousValue = _tBL_Admin_Usuarios3;
                    _tBL_Admin_Usuarios3 = value;
                    FixupTBL_Admin_Usuarios3(previousValue);
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios3");
                }
            }
        }
        private TBL_Admin_Usuarios _tBL_Admin_Usuarios3;
    
        [DataMember]
        public TrackableCollection<TBL_Admin_Usuarios> TBL_Admin_Usuarios4
        {
            get
            {
                if (_tBL_Admin_Usuarios4 == null)
                {
                    _tBL_Admin_Usuarios4 = new TrackableCollection<TBL_Admin_Usuarios>();
                    _tBL_Admin_Usuarios4.CollectionChanged += FixupTBL_Admin_Usuarios4;
                }
                return _tBL_Admin_Usuarios4;
            }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios4, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_Admin_Usuarios4 != null)
                    {
                        _tBL_Admin_Usuarios4.CollectionChanged -= FixupTBL_Admin_Usuarios4;
                    }
                    _tBL_Admin_Usuarios4 = value;
                    if (_tBL_Admin_Usuarios4 != null)
                    {
                        _tBL_Admin_Usuarios4.CollectionChanged += FixupTBL_Admin_Usuarios4;
                    }
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios4");
                }
            }
        }
        private TrackableCollection<TBL_Admin_Usuarios> _tBL_Admin_Usuarios4;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            Contratos = null;
            DocumentosRadicado.Clear();
            TBL_Admin_Usuarios = null;
            TBL_Admin_Usuarios1 = null;
            TBL_Admin_Usuarios2 = null;
            TBL_Admin_Usuarios3 = null;
            TBL_Admin_Usuarios4.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupContratos(Contratos previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Radicados.Contains(this))
            {
                previousValue.Radicados.Remove(this);
            }
    
            if (Contratos != null)
            {
                if (!Contratos.Radicados.Contains(this))
                {
                    Contratos.Radicados.Add(this);
                }
    
                IdContrato = Contratos.IdContrato;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Contratos")
                    && (ChangeTracker.OriginalValues["Contratos"] == Contratos))
                {
                    ChangeTracker.OriginalValues.Remove("Contratos");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Contratos", previousValue);
                }
                if (Contratos != null && !Contratos.ChangeTracker.ChangeTrackingEnabled)
                {
                    Contratos.StartTracking();
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Radicados.Contains(this))
            {
                previousValue.Radicados.Remove(this);
            }
    
            if (TBL_Admin_Usuarios != null)
            {
                if (!TBL_Admin_Usuarios.Radicados.Contains(this))
                {
                    TBL_Admin_Usuarios.Radicados.Add(this);
                }
    
                CreateBy = TBL_Admin_Usuarios.IdUser;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_Admin_Usuarios")
                    && (ChangeTracker.OriginalValues["TBL_Admin_Usuarios"] == TBL_Admin_Usuarios))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_Admin_Usuarios");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_Admin_Usuarios", previousValue);
                }
                if (TBL_Admin_Usuarios != null && !TBL_Admin_Usuarios.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_Admin_Usuarios.StartTracking();
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios1(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Radicados1.Contains(this))
            {
                previousValue.Radicados1.Remove(this);
            }
    
            if (TBL_Admin_Usuarios1 != null)
            {
                if (!TBL_Admin_Usuarios1.Radicados1.Contains(this))
                {
                    TBL_Admin_Usuarios1.Radicados1.Add(this);
                }
    
                ModifiedBy = TBL_Admin_Usuarios1.IdUser;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_Admin_Usuarios1")
                    && (ChangeTracker.OriginalValues["TBL_Admin_Usuarios1"] == TBL_Admin_Usuarios1))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_Admin_Usuarios1");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_Admin_Usuarios1", previousValue);
                }
                if (TBL_Admin_Usuarios1 != null && !TBL_Admin_Usuarios1.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_Admin_Usuarios1.StartTracking();
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios2(TBL_Admin_Usuarios previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Radicados2.Contains(this))
            {
                previousValue.Radicados2.Remove(this);
            }
    
            if (TBL_Admin_Usuarios2 != null)
            {
                if (!TBL_Admin_Usuarios2.Radicados2.Contains(this))
                {
                    TBL_Admin_Usuarios2.Radicados2.Add(this);
                }
    
                IdFrom = TBL_Admin_Usuarios2.IdUser;
            }
            else if (!skipKeys)
            {
                IdFrom = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_Admin_Usuarios2")
                    && (ChangeTracker.OriginalValues["TBL_Admin_Usuarios2"] == TBL_Admin_Usuarios2))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_Admin_Usuarios2");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_Admin_Usuarios2", previousValue);
                }
                if (TBL_Admin_Usuarios2 != null && !TBL_Admin_Usuarios2.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_Admin_Usuarios2.StartTracking();
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios3(TBL_Admin_Usuarios previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Radicados3.Contains(this))
            {
                previousValue.Radicados3.Remove(this);
            }
    
            if (TBL_Admin_Usuarios3 != null)
            {
                if (!TBL_Admin_Usuarios3.Radicados3.Contains(this))
                {
                    TBL_Admin_Usuarios3.Radicados3.Add(this);
                }
    
                IdTo = TBL_Admin_Usuarios3.IdUser;
            }
            else if (!skipKeys)
            {
                IdTo = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_Admin_Usuarios3")
                    && (ChangeTracker.OriginalValues["TBL_Admin_Usuarios3"] == TBL_Admin_Usuarios3))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_Admin_Usuarios3");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_Admin_Usuarios3", previousValue);
                }
                if (TBL_Admin_Usuarios3 != null && !TBL_Admin_Usuarios3.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_Admin_Usuarios3.StartTracking();
                }
            }
        }
    
        private void FixupDocumentosRadicado(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (DocumentosRadicado item in e.NewItems)
                {
                    item.Radicados = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("DocumentosRadicado", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (DocumentosRadicado item in e.OldItems)
                {
                    if (ReferenceEquals(item.Radicados, this))
                    {
                        item.Radicados = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("DocumentosRadicado", item);
                    }
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios4(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_Admin_Usuarios item in e.NewItems)
                {
                    if (!item.Radicados4.Contains(this))
                    {
                        item.Radicados4.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_Admin_Usuarios4", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_Admin_Usuarios item in e.OldItems)
                {
                    if (item.Radicados4.Contains(this))
                    {
                        item.Radicados4.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_Admin_Usuarios4", item);
                    }
                }
            }
        }

        #endregion
    }
}
