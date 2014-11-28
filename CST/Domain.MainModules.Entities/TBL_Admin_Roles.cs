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
    [KnownType(typeof(TBL_Admin_OpcionesMenu))]
    [KnownType(typeof(TBL_Admin_Usuarios))]
    [KnownType(typeof(TBL_ModuloDocumentosAnexos_Carpetas))]
    
    public partial class TBL_Admin_Roles: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IdRol
        {
            get { return _idRol; }
            set
            {
                if (_idRol != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdRol' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idRol = value;
                    OnPropertyChanged("IdRol");
                }
            }
        }
        private int _idRol;
    
        [DataMember]
        public string NombreRol
        {
            get { return _nombreRol; }
            set
            {
                if (_nombreRol != value)
                {
                    _nombreRol = value;
                    OnPropertyChanged("NombreRol");
                }
            }
        }
        private string _nombreRol;
    
        [DataMember]
        public bool Activo
        {
            get { return _activo; }
            set
            {
                if (_activo != value)
                {
                    _activo = value;
                    OnPropertyChanged("Activo");
                }
            }
        }
        private bool _activo;
    
        [DataMember]
        public Nullable<bool> IsGroup
        {
            get { return _isGroup; }
            set
            {
                if (_isGroup != value)
                {
                    _isGroup = value;
                    OnPropertyChanged("IsGroup");
                }
            }
        }
        private Nullable<bool> _isGroup;
    
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
        public TrackableCollection<TBL_Admin_OpcionesMenu> TBL_Admin_OpcionesMenu
        {
            get
            {
                if (_tBL_Admin_OpcionesMenu == null)
                {
                    _tBL_Admin_OpcionesMenu = new TrackableCollection<TBL_Admin_OpcionesMenu>();
                    _tBL_Admin_OpcionesMenu.CollectionChanged += FixupTBL_Admin_OpcionesMenu;
                }
                return _tBL_Admin_OpcionesMenu;
            }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_OpcionesMenu, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_Admin_OpcionesMenu != null)
                    {
                        _tBL_Admin_OpcionesMenu.CollectionChanged -= FixupTBL_Admin_OpcionesMenu;
                    }
                    _tBL_Admin_OpcionesMenu = value;
                    if (_tBL_Admin_OpcionesMenu != null)
                    {
                        _tBL_Admin_OpcionesMenu.CollectionChanged += FixupTBL_Admin_OpcionesMenu;
                    }
                    OnNavigationPropertyChanged("TBL_Admin_OpcionesMenu");
                }
            }
        }
        private TrackableCollection<TBL_Admin_OpcionesMenu> _tBL_Admin_OpcionesMenu;
    
        [DataMember]
        public TrackableCollection<TBL_Admin_Usuarios> TBL_Admin_Usuarios
        {
            get
            {
                if (_tBL_Admin_Usuarios == null)
                {
                    _tBL_Admin_Usuarios = new TrackableCollection<TBL_Admin_Usuarios>();
                    _tBL_Admin_Usuarios.CollectionChanged += FixupTBL_Admin_Usuarios;
                }
                return _tBL_Admin_Usuarios;
            }
            set
            {
                if (!ReferenceEquals(_tBL_Admin_Usuarios, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_Admin_Usuarios != null)
                    {
                        _tBL_Admin_Usuarios.CollectionChanged -= FixupTBL_Admin_Usuarios;
                    }
                    _tBL_Admin_Usuarios = value;
                    if (_tBL_Admin_Usuarios != null)
                    {
                        _tBL_Admin_Usuarios.CollectionChanged += FixupTBL_Admin_Usuarios;
                    }
                    OnNavigationPropertyChanged("TBL_Admin_Usuarios");
                }
            }
        }
        private TrackableCollection<TBL_Admin_Usuarios> _tBL_Admin_Usuarios;
    
        [DataMember]
        public TrackableCollection<TBL_ModuloDocumentosAnexos_Carpetas> TBL_ModuloDocumentosAnexos_Carpetas
        {
            get
            {
                if (_tBL_ModuloDocumentosAnexos_Carpetas == null)
                {
                    _tBL_ModuloDocumentosAnexos_Carpetas = new TrackableCollection<TBL_ModuloDocumentosAnexos_Carpetas>();
                    _tBL_ModuloDocumentosAnexos_Carpetas.CollectionChanged += FixupTBL_ModuloDocumentosAnexos_Carpetas;
                }
                return _tBL_ModuloDocumentosAnexos_Carpetas;
            }
            set
            {
                if (!ReferenceEquals(_tBL_ModuloDocumentosAnexos_Carpetas, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tBL_ModuloDocumentosAnexos_Carpetas != null)
                    {
                        _tBL_ModuloDocumentosAnexos_Carpetas.CollectionChanged -= FixupTBL_ModuloDocumentosAnexos_Carpetas;
                    }
                    _tBL_ModuloDocumentosAnexos_Carpetas = value;
                    if (_tBL_ModuloDocumentosAnexos_Carpetas != null)
                    {
                        _tBL_ModuloDocumentosAnexos_Carpetas.CollectionChanged += FixupTBL_ModuloDocumentosAnexos_Carpetas;
                    }
                    OnNavigationPropertyChanged("TBL_ModuloDocumentosAnexos_Carpetas");
                }
            }
        }
        private TrackableCollection<TBL_ModuloDocumentosAnexos_Carpetas> _tBL_ModuloDocumentosAnexos_Carpetas;

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
            TBL_Admin_OpcionesMenu.Clear();
            TBL_Admin_Usuarios.Clear();
            TBL_ModuloDocumentosAnexos_Carpetas.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTBL_Admin_OpcionesMenu(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_Admin_OpcionesMenu item in e.NewItems)
                {
                    if (!item.TBL_Admin_Roles.Contains(this))
                    {
                        item.TBL_Admin_Roles.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_Admin_OpcionesMenu", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_Admin_OpcionesMenu item in e.OldItems)
                {
                    if (item.TBL_Admin_Roles.Contains(this))
                    {
                        item.TBL_Admin_Roles.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_Admin_OpcionesMenu", item);
                    }
                }
            }
        }
    
        private void FixupTBL_Admin_Usuarios(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_Admin_Usuarios item in e.NewItems)
                {
                    if (!item.TBL_Admin_Roles.Contains(this))
                    {
                        item.TBL_Admin_Roles.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_Admin_Usuarios", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_Admin_Usuarios item in e.OldItems)
                {
                    if (item.TBL_Admin_Roles.Contains(this))
                    {
                        item.TBL_Admin_Roles.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_Admin_Usuarios", item);
                    }
                }
            }
        }
    
        private void FixupTBL_ModuloDocumentosAnexos_Carpetas(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (TBL_ModuloDocumentosAnexos_Carpetas item in e.NewItems)
                {
                    if (!item.TBL_Admin_Roles.Contains(this))
                    {
                        item.TBL_Admin_Roles.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("TBL_ModuloDocumentosAnexos_Carpetas", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TBL_ModuloDocumentosAnexos_Carpetas item in e.OldItems)
                {
                    if (item.TBL_Admin_Roles.Contains(this))
                    {
                        item.TBL_Admin_Roles.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("TBL_ModuloDocumentosAnexos_Carpetas", item);
                    }
                }
            }
        }

        #endregion
    }
}
