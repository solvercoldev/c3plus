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
    [KnownType(typeof(Compromisos))]
    [KnownType(typeof(Contratos))]
    [KnownType(typeof(Fases))]
    [KnownType(typeof(TBL_Admin_Usuarios))]
    [KnownType(typeof(NovedadesFase))]
    
    public partial class Fases: IObjectWithChangeTracker, INotifyPropertyChanged
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
        public long IdFase
        {
            get { return _idFase; }
            set
            {
                if (_idFase != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdFase' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    if (!IsDeserializing)
                    {
                        if (Fases2 != null && Fases2.IdFase != value)
                        {
                            Fases2 = null;
                        }
                    }
                    _idFase = value;
                    OnPropertyChanged("IdFase");
                }
            }
        }
        private long _idFase;
    
        [DataMember]
        public string Periodo
        {
            get { return _periodo; }
            set
            {
                if (_periodo != value)
                {
                    _periodo = value;
                    OnPropertyChanged("Periodo");
                }
            }
        }
        private string _periodo;
    
        [DataMember]
        public int NumeroFase
        {
            get { return _numeroFase; }
            set
            {
                if (_numeroFase != value)
                {
                    _numeroFase = value;
                    OnPropertyChanged("NumeroFase");
                }
            }
        }
        private int _numeroFase;
    
        [DataMember]
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged("Nombre");
                }
            }
        }
        private string _nombre;
    
        [DataMember]
        public System.DateTime FechaInicio
        {
            get { return _fechaInicio; }
            set
            {
                if (_fechaInicio != value)
                {
                    _fechaInicio = value;
                    OnPropertyChanged("FechaInicio");
                }
            }
        }
        private System.DateTime _fechaInicio;
    
        [DataMember]
        public int DuracionMeses
        {
            get { return _duracionMeses; }
            set
            {
                if (_duracionMeses != value)
                {
                    _duracionMeses = value;
                    OnPropertyChanged("DuracionMeses");
                }
            }
        }
        private int _duracionMeses;
    
        [DataMember]
        public System.DateTime FechaFinalizacion
        {
            get { return _fechaFinalizacion; }
            set
            {
                if (_fechaFinalizacion != value)
                {
                    _fechaFinalizacion = value;
                    OnPropertyChanged("FechaFinalizacion");
                }
            }
        }
        private System.DateTime _fechaFinalizacion;
    
        [DataMember]
        public byte Estado
        {
            get { return _estado; }
            set
            {
                if (_estado != value)
                {
                    _estado = value;
                    OnPropertyChanged("Estado");
                }
            }
        }
        private byte _estado;
    
        [DataMember]
        public bool Aplica
        {
            get { return _aplica; }
            set
            {
                if (_aplica != value)
                {
                    _aplica = value;
                    OnPropertyChanged("Aplica");
                }
            }
        }
        private bool _aplica;
    
        [DataMember]
        public Nullable<byte> TipoEvalFase
        {
            get { return _tipoEvalFase; }
            set
            {
                if (_tipoEvalFase != value)
                {
                    _tipoEvalFase = value;
                    OnPropertyChanged("TipoEvalFase");
                }
            }
        }
        private Nullable<byte> _tipoEvalFase;
    
        [DataMember]
        public Nullable<byte> NumeroFaseAnt
        {
            get { return _numeroFaseAnt; }
            set
            {
                if (_numeroFaseAnt != value)
                {
                    _numeroFaseAnt = value;
                    OnPropertyChanged("NumeroFaseAnt");
                }
            }
        }
        private Nullable<byte> _numeroFaseAnt;
    
        [DataMember]
        public string IdCampo
        {
            get { return _idCampo; }
            set
            {
                if (_idCampo != value)
                {
                    _idCampo = value;
                    OnPropertyChanged("IdCampo");
                }
            }
        }
        private string _idCampo;
    
        [DataMember]
        public string IdPozo
        {
            get { return _idPozo; }
            set
            {
                if (_idPozo != value)
                {
                    _idPozo = value;
                    OnPropertyChanged("IdPozo");
                }
            }
        }
        private string _idPozo;
    
        [DataMember]
        public Nullable<byte> UltimaNovedad
        {
            get { return _ultimaNovedad; }
            set
            {
                if (_ultimaNovedad != value)
                {
                    _ultimaNovedad = value;
                    OnPropertyChanged("UltimaNovedad");
                }
            }
        }
        private Nullable<byte> _ultimaNovedad;
    
        [DataMember]
        public Nullable<int> NumFaseUnificada
        {
            get { return _numFaseUnificada; }
            set
            {
                if (_numFaseUnificada != value)
                {
                    _numFaseUnificada = value;
                    OnPropertyChanged("NumFaseUnificada");
                }
            }
        }
        private Nullable<int> _numFaseUnificada;
    
        [DataMember]
        public int Grupo
        {
            get { return _grupo; }
            set
            {
                if (_grupo != value)
                {
                    _grupo = value;
                    OnPropertyChanged("Grupo");
                }
            }
        }
        private int _grupo;
    
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
        public TrackableCollection<Compromisos> Compromisos
        {
            get
            {
                if (_compromisos == null)
                {
                    _compromisos = new TrackableCollection<Compromisos>();
                    _compromisos.CollectionChanged += FixupCompromisos;
                }
                return _compromisos;
            }
            set
            {
                if (!ReferenceEquals(_compromisos, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_compromisos != null)
                    {
                        _compromisos.CollectionChanged -= FixupCompromisos;
                    }
                    _compromisos = value;
                    if (_compromisos != null)
                    {
                        _compromisos.CollectionChanged += FixupCompromisos;
                    }
                    OnNavigationPropertyChanged("Compromisos");
                }
            }
        }
        private TrackableCollection<Compromisos> _compromisos;
    
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
        public Fases Fases1
        {
            get { return _fases1; }
            set
            {
                if (!ReferenceEquals(_fases1, value))
                {
                    var previousValue = _fases1;
                    _fases1 = value;
                    FixupFases1(previousValue);
                    OnNavigationPropertyChanged("Fases1");
                }
            }
        }
        private Fases _fases1;
    
        [DataMember]
        public Fases Fases2
        {
            get { return _fases2; }
            set
            {
                if (!ReferenceEquals(_fases2, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added && value != null)
                    {
                        // This the dependent end of an identifying relationship, so the principal end cannot be changed if it is already set,
                        // otherwise it can only be set to an entity with a primary key that is the same value as the dependent's foreign key.
                        if (IdFase != value.IdFase)
                        {
                            throw new InvalidOperationException("The principal end of an identifying relationship can only be changed when the dependent end is in the Added state.");
                        }
                    }
                    var previousValue = _fases2;
                    _fases2 = value;
                    FixupFases2(previousValue);
                    OnNavigationPropertyChanged("Fases2");
                }
            }
        }
        private Fases _fases2;
    
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
        public TrackableCollection<NovedadesFase> NovedadesFase
        {
            get
            {
                if (_novedadesFase == null)
                {
                    _novedadesFase = new TrackableCollection<NovedadesFase>();
                    _novedadesFase.CollectionChanged += FixupNovedadesFase;
                }
                return _novedadesFase;
            }
            set
            {
                if (!ReferenceEquals(_novedadesFase, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_novedadesFase != null)
                    {
                        _novedadesFase.CollectionChanged -= FixupNovedadesFase;
                    }
                    _novedadesFase = value;
                    if (_novedadesFase != null)
                    {
                        _novedadesFase.CollectionChanged += FixupNovedadesFase;
                    }
                    OnNavigationPropertyChanged("NovedadesFase");
                }
            }
        }
        private TrackableCollection<NovedadesFase> _novedadesFase;

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
    
        // This entity type is the dependent end in at least one association that performs cascade deletes.
        // This event handler will process notifications that occur when the principal end is deleted.
        internal void HandleCascadeDelete(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                this.MarkAsDeleted();
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
            Compromisos.Clear();
            Contratos = null;
            Fases1 = null;
            Fases2 = null;
            TBL_Admin_Usuarios = null;
            TBL_Admin_Usuarios1 = null;
            NovedadesFase.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupContratos(Contratos previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Fases.Contains(this))
            {
                previousValue.Fases.Remove(this);
            }
    
            if (Contratos != null)
            {
                if (!Contratos.Fases.Contains(this))
                {
                    Contratos.Fases.Add(this);
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
    
        private void FixupFases1(Fases previousValue)
        {
            // This is the principal end in an association that performs cascade deletes.
            // Update the event listener to refer to the new dependent.
            if (previousValue != null)
            {
                ChangeTracker.ObjectStateChanging -= previousValue.HandleCascadeDelete;
            }
    
            if (Fases1 != null)
            {
                ChangeTracker.ObjectStateChanging += Fases1.HandleCascadeDelete;
            }
    
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && ReferenceEquals(previousValue.Fases2, this))
            {
                previousValue.Fases2 = null;
            }
    
            if (Fases1 != null)
            {
                Fases1.Fases2 = this;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Fases1")
                    && (ChangeTracker.OriginalValues["Fases1"] == Fases1))
                {
                    ChangeTracker.OriginalValues.Remove("Fases1");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Fases1", previousValue);
                    // This is the principal end of an identifying association, so the dependent must be deleted when the relationship is removed.
                    // If the current state of the dependent is Added, the relationship can be changed without causing the dependent to be deleted.
                    if (previousValue != null && previousValue.ChangeTracker.State != ObjectState.Added)
                    {
                        previousValue.MarkAsDeleted();
                    }
                }
                if (Fases1 != null && !Fases1.ChangeTracker.ChangeTrackingEnabled)
                {
                    Fases1.StartTracking();
                }
            }
        }
    
        private void FixupFases2(Fases previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && ReferenceEquals(previousValue.Fases1, this))
            {
                previousValue.Fases1 = null;
            }
    
            if (Fases2 != null)
            {
                Fases2.Fases1 = this;
                IdFase = Fases2.IdFase;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Fases2")
                    && (ChangeTracker.OriginalValues["Fases2"] == Fases2))
                {
                    ChangeTracker.OriginalValues.Remove("Fases2");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Fases2", previousValue);
                }
                if (Fases2 != null && !Fases2.ChangeTracker.ChangeTrackingEnabled)
                {
                    Fases2.StartTracking();
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Fases.Contains(this))
            {
                previousValue.Fases.Remove(this);
            }
    
            if (TBL_Admin_Usuarios != null)
            {
                if (!TBL_Admin_Usuarios.Fases.Contains(this))
                {
                    TBL_Admin_Usuarios.Fases.Add(this);
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
    
            if (previousValue != null && previousValue.Fases1.Contains(this))
            {
                previousValue.Fases1.Remove(this);
            }
    
            if (TBL_Admin_Usuarios1 != null)
            {
                if (!TBL_Admin_Usuarios1.Fases1.Contains(this))
                {
                    TBL_Admin_Usuarios1.Fases1.Add(this);
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
    
        private void FixupCompromisos(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Compromisos item in e.NewItems)
                {
                    item.Fases = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Compromisos", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Compromisos item in e.OldItems)
                {
                    if (ReferenceEquals(item.Fases, this))
                    {
                        item.Fases = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Compromisos", item);
                    }
                }
            }
        }
    
        private void FixupNovedadesFase(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (NovedadesFase item in e.NewItems)
                {
                    item.Fases = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("NovedadesFase", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (NovedadesFase item in e.OldItems)
                {
                    if (ReferenceEquals(item.Fases, this))
                    {
                        item.Fases = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("NovedadesFase", item);
                    }
                }
            }
        }

        #endregion
    }
}