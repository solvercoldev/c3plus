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
    [KnownType(typeof(PagosObligaciones))]
    
    public partial class TiposPagoObligacion: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public string IdTipoPagoObligacion
        {
            get { return _idTipoPagoObligacion; }
            set
            {
                if (_idTipoPagoObligacion != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdTipoPagoObligacion' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idTipoPagoObligacion = value;
                    OnPropertyChanged("IdTipoPagoObligacion");
                }
            }
        }
        private string _idTipoPagoObligacion;
    
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

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<PagosObligaciones> PagosObligaciones
        {
            get
            {
                if (_pagosObligaciones == null)
                {
                    _pagosObligaciones = new TrackableCollection<PagosObligaciones>();
                    _pagosObligaciones.CollectionChanged += FixupPagosObligaciones;
                }
                return _pagosObligaciones;
            }
            set
            {
                if (!ReferenceEquals(_pagosObligaciones, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_pagosObligaciones != null)
                    {
                        _pagosObligaciones.CollectionChanged -= FixupPagosObligaciones;
                    }
                    _pagosObligaciones = value;
                    if (_pagosObligaciones != null)
                    {
                        _pagosObligaciones.CollectionChanged += FixupPagosObligaciones;
                    }
                    OnNavigationPropertyChanged("PagosObligaciones");
                }
            }
        }
        private TrackableCollection<PagosObligaciones> _pagosObligaciones;

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
            PagosObligaciones.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupPagosObligaciones(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (PagosObligaciones item in e.NewItems)
                {
                    item.TiposPagoObligacion = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("PagosObligaciones", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (PagosObligaciones item in e.OldItems)
                {
                    if (ReferenceEquals(item.TiposPagoObligacion, this))
                    {
                        item.TiposPagoObligacion = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("PagosObligaciones", item);
                    }
                }
            }
        }

        #endregion
    }
}
