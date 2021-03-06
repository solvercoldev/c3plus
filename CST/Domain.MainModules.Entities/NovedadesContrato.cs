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
    [KnownType(typeof(TBL_Admin_Usuarios))]
    
    public partial class NovedadesContrato: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdNovedad
        {
            get { return _idNovedad; }
            set
            {
                if (_idNovedad != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdNovedad' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idNovedad = value;
                    OnPropertyChanged("IdNovedad");
                }
            }
        }
        private int _idNovedad;
    
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
        public string TipoNovedad
        {
            get { return _tipoNovedad; }
            set
            {
                if (_tipoNovedad != value)
                {
                    _tipoNovedad = value;
                    OnPropertyChanged("TipoNovedad");
                }
            }
        }
        private string _tipoNovedad;
    
        [DataMember]
        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                if (_descripcion != value)
                {
                    _descripcion = value;
                    OnPropertyChanged("Descripcion");
                }
            }
        }
        private string _descripcion;
    
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
        public System.DateTime FechaFin
        {
            get { return _fechaFin; }
            set
            {
                if (_fechaFin != value)
                {
                    _fechaFin = value;
                    OnPropertyChanged("FechaFin");
                }
            }
        }
        private System.DateTime _fechaFin;
    
        [DataMember]
        public int DiasDesplazamiento
        {
            get { return _diasDesplazamiento; }
            set
            {
                if (_diasDesplazamiento != value)
                {
                    _diasDesplazamiento = value;
                    OnPropertyChanged("DiasDesplazamiento");
                }
            }
        }
        private int _diasDesplazamiento;
    
        [DataMember]
        public int IdResponsable
        {
            get { return _idResponsable; }
            set
            {
                if (_idResponsable != value)
                {
                    ChangeTracker.RecordOriginalValue("IdResponsable", _idResponsable);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Usuarios2 != null && TBL_Admin_Usuarios2.IdUser != value)
                        {
                            TBL_Admin_Usuarios2 = null;
                        }
                    }
                    _idResponsable = value;
                    OnPropertyChanged("IdResponsable");
                }
            }
        }
        private int _idResponsable;
    
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
            TBL_Admin_Usuarios = null;
            TBL_Admin_Usuarios1 = null;
            TBL_Admin_Usuarios2 = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupContratos(Contratos previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.NovedadesContrato.Contains(this))
            {
                previousValue.NovedadesContrato.Remove(this);
            }
    
            if (Contratos != null)
            {
                if (!Contratos.NovedadesContrato.Contains(this))
                {
                    Contratos.NovedadesContrato.Add(this);
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
    
            if (previousValue != null && previousValue.NovedadesContrato.Contains(this))
            {
                previousValue.NovedadesContrato.Remove(this);
            }
    
            if (TBL_Admin_Usuarios != null)
            {
                if (!TBL_Admin_Usuarios.NovedadesContrato.Contains(this))
                {
                    TBL_Admin_Usuarios.NovedadesContrato.Add(this);
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
    
            if (previousValue != null && previousValue.NovedadesContrato1.Contains(this))
            {
                previousValue.NovedadesContrato1.Remove(this);
            }
    
            if (TBL_Admin_Usuarios1 != null)
            {
                if (!TBL_Admin_Usuarios1.NovedadesContrato1.Contains(this))
                {
                    TBL_Admin_Usuarios1.NovedadesContrato1.Add(this);
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
    
        private void FixupTBL_Admin_Usuarios2(TBL_Admin_Usuarios previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.NovedadesContrato2.Contains(this))
            {
                previousValue.NovedadesContrato2.Remove(this);
            }
    
            if (TBL_Admin_Usuarios2 != null)
            {
                if (!TBL_Admin_Usuarios2.NovedadesContrato2.Contains(this))
                {
                    TBL_Admin_Usuarios2.NovedadesContrato2.Add(this);
                }
    
                IdResponsable = TBL_Admin_Usuarios2.IdUser;
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

        #endregion
    }
}
