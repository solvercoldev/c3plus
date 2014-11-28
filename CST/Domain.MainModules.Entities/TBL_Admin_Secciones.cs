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
    [KnownType(typeof(TBL_Admin_Modulos))]
    
    public partial class TBL_Admin_Secciones: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdSeccion
        {
            get { return _idSeccion; }
            set
            {
                if (_idSeccion != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdSeccion' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idSeccion = value;
                    OnPropertyChanged("IdSeccion");
                }
            }
        }
        private int _idSeccion;
    
        [DataMember]
        public int IdModule
        {
            get { return _idModule; }
            set
            {
                if (_idModule != value)
                {
                    ChangeTracker.RecordOriginalValue("IdModule", _idModule);
                    if (!IsDeserializing)
                    {
                        if (TBL_Admin_Modulos != null && TBL_Admin_Modulos.IdModulo != value)
                        {
                            TBL_Admin_Modulos = null;
                        }
                    }
                    _idModule = value;
                    OnPropertyChanged("IdModule");
                }
            }
        }
        private int _idModule;
    
        [DataMember]
        public int Posicion
        {
            get { return _posicion; }
            set
            {
                if (_posicion != value)
                {
                    _posicion = value;
                    OnPropertyChanged("Posicion");
                }
            }
        }
        private int _posicion;
    
        [DataMember]
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                if (_titulo != value)
                {
                    _titulo = value;
                    OnPropertyChanged("Titulo");
                }
            }
        }
        private string _titulo;
    
        [DataMember]
        public string TituloEdit
        {
            get { return _tituloEdit; }
            set
            {
                if (_tituloEdit != value)
                {
                    _tituloEdit = value;
                    OnPropertyChanged("TituloEdit");
                }
            }
        }
        private string _tituloEdit;
    
        [DataMember]
        public string PathPreview
        {
            get { return _pathPreview; }
            set
            {
                if (_pathPreview != value)
                {
                    _pathPreview = value;
                    OnPropertyChanged("PathPreview");
                }
            }
        }
        private string _pathPreview;
    
        [DataMember]
        public string PathEdit
        {
            get { return _pathEdit; }
            set
            {
                if (_pathEdit != value)
                {
                    _pathEdit = value;
                    OnPropertyChanged("PathEdit");
                }
            }
        }
        private string _pathEdit;
    
        [DataMember]
        public Nullable<bool> ShowPreview
        {
            get { return _showPreview; }
            set
            {
                if (_showPreview != value)
                {
                    _showPreview = value;
                    OnPropertyChanged("ShowPreview");
                }
            }
        }
        private Nullable<bool> _showPreview;
    
        [DataMember]
        public Nullable<bool> ShowInEdit
        {
            get { return _showInEdit; }
            set
            {
                if (_showInEdit != value)
                {
                    _showInEdit = value;
                    OnPropertyChanged("ShowInEdit");
                }
            }
        }
        private Nullable<bool> _showInEdit;
    
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
        public string CreateBy
        {
            get { return _createBy; }
            set
            {
                if (_createBy != value)
                {
                    _createBy = value;
                    OnPropertyChanged("CreateBy");
                }
            }
        }
        private string _createBy;
    
        [DataMember]
        public Nullable<System.DateTime> CreateOn
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
        private Nullable<System.DateTime> _createOn;
    
        [DataMember]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set
            {
                if (_modifiedBy != value)
                {
                    _modifiedBy = value;
                    OnPropertyChanged("ModifiedBy");
                }
            }
        }
        private string _modifiedBy;
    
        [DataMember]
        public Nullable<System.DateTime> ModifiedOn
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
        private Nullable<System.DateTime> _modifiedOn;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TBL_Admin_Modulos TBL_Admin_Modulos
        {
            get { return _tBL_Admin_Modulos; }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Modulos, value))
                {
                    var previousValue = _tBL_Admin_Modulos;
                    _tBL_Admin_Modulos = value;
                    FixupTBL_Admin_Modulos(previousValue);
                    OnNavigationPropertyChanged("TBL_Admin_Modulos");
                }
            }
        }
        private TBL_Admin_Modulos _tBL_Admin_Modulos;

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
            TBL_Admin_Modulos = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_Admin_Modulos(TBL_Admin_Modulos previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TBL_Admin_Secciones.Contains(this))
            {
                previousValue.TBL_Admin_Secciones.Remove(this);
            }
    
            if (TBL_Admin_Modulos != null)
            {
                if (!TBL_Admin_Modulos.TBL_Admin_Secciones.Contains(this))
                {
                    TBL_Admin_Modulos.TBL_Admin_Secciones.Add(this);
                }
    
                IdModule = TBL_Admin_Modulos.IdModulo;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("TBL_Admin_Modulos")
                    && (ChangeTracker.OriginalValues["TBL_Admin_Modulos"] == TBL_Admin_Modulos))
                {
                    ChangeTracker.OriginalValues.Remove("TBL_Admin_Modulos");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("TBL_Admin_Modulos", previousValue);
                }
                if (TBL_Admin_Modulos != null && !TBL_Admin_Modulos.ChangeTracker.ChangeTrackingEnabled)
                {
                    TBL_Admin_Modulos.StartTracking();
                }
            }
        }

        #endregion
    }
}
