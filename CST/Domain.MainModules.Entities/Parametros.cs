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
    [KnownType(typeof(FormatosIndice))]
    
    public partial class Parametros: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public byte IdParametro
        {
            get { return _idParametro; }
            set
            {
                if (_idParametro != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IdParametro' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _idParametro = value;
                    OnPropertyChanged("IdParametro");
                }
            }
        }
        private byte _idParametro;
    
        [DataMember]
        public string ServidorCorreo
        {
            get { return _servidorCorreo; }
            set
            {
                if (_servidorCorreo != value)
                {
                    _servidorCorreo = value;
                    OnPropertyChanged("ServidorCorreo");
                }
            }
        }
        private string _servidorCorreo;
    
        [DataMember]
        public Nullable<int> Puerto
        {
            get { return _puerto; }
            set
            {
                if (_puerto != value)
                {
                    _puerto = value;
                    OnPropertyChanged("Puerto");
                }
            }
        }
        private Nullable<int> _puerto;
    
        [DataMember]
        public Nullable<bool> HabilitarSSL
        {
            get { return _habilitarSSL; }
            set
            {
                if (_habilitarSSL != value)
                {
                    _habilitarSSL = value;
                    OnPropertyChanged("HabilitarSSL");
                }
            }
        }
        private Nullable<bool> _habilitarSSL;
    
        [DataMember]
        public string CuentaEmisora
        {
            get { return _cuentaEmisora; }
            set
            {
                if (_cuentaEmisora != value)
                {
                    _cuentaEmisora = value;
                    OnPropertyChanged("CuentaEmisora");
                }
            }
        }
        private string _cuentaEmisora;
    
        [DataMember]
        public string PasswordCorreo
        {
            get { return _passwordCorreo; }
            set
            {
                if (_passwordCorreo != value)
                {
                    _passwordCorreo = value;
                    OnPropertyChanged("PasswordCorreo");
                }
            }
        }
        private string _passwordCorreo;
    
        [DataMember]
        public string Dominio
        {
            get { return _dominio; }
            set
            {
                if (_dominio != value)
                {
                    _dominio = value;
                    OnPropertyChanged("Dominio");
                }
            }
        }
        private string _dominio;
    
        [DataMember]
        public string RecursoCompartido
        {
            get { return _recursoCompartido; }
            set
            {
                if (_recursoCompartido != value)
                {
                    _recursoCompartido = value;
                    OnPropertyChanged("RecursoCompartido");
                }
            }
        }
        private string _recursoCompartido;
    
        [DataMember]
        public string DirEmpresas
        {
            get { return _dirEmpresas; }
            set
            {
                if (_dirEmpresas != value)
                {
                    _dirEmpresas = value;
                    OnPropertyChanged("DirEmpresas");
                }
            }
        }
        private string _dirEmpresas;
    
        [DataMember]
        public string DirContratos
        {
            get { return _dirContratos; }
            set
            {
                if (_dirContratos != value)
                {
                    _dirContratos = value;
                    OnPropertyChanged("DirContratos");
                }
            }
        }
        private string _dirContratos;
    
        [DataMember]
        public string SubDirDocumentos
        {
            get { return _subDirDocumentos; }
            set
            {
                if (_subDirDocumentos != value)
                {
                    _subDirDocumentos = value;
                    OnPropertyChanged("SubDirDocumentos");
                }
            }
        }
        private string _subDirDocumentos;
    
        [DataMember]
        public string SubDirRadicadosEntrada
        {
            get { return _subDirRadicadosEntrada; }
            set
            {
                if (_subDirRadicadosEntrada != value)
                {
                    _subDirRadicadosEntrada = value;
                    OnPropertyChanged("SubDirRadicadosEntrada");
                }
            }
        }
        private string _subDirRadicadosEntrada;
    
        [DataMember]
        public string SubDirRadicadosSalida
        {
            get { return _subDirRadicadosSalida; }
            set
            {
                if (_subDirRadicadosSalida != value)
                {
                    _subDirRadicadosSalida = value;
                    OnPropertyChanged("SubDirRadicadosSalida");
                }
            }
        }
        private string _subDirRadicadosSalida;
    
        [DataMember]
        public Nullable<short> DiasAntAlarmVencimientoContrato
        {
            get { return _diasAntAlarmVencimientoContrato; }
            set
            {
                if (_diasAntAlarmVencimientoContrato != value)
                {
                    _diasAntAlarmVencimientoContrato = value;
                    OnPropertyChanged("DiasAntAlarmVencimientoContrato");
                }
            }
        }
        private Nullable<short> _diasAntAlarmVencimientoContrato;
    
        [DataMember]
        public Nullable<short> DiasAntAlarmVencimientoFase
        {
            get { return _diasAntAlarmVencimientoFase; }
            set
            {
                if (_diasAntAlarmVencimientoFase != value)
                {
                    _diasAntAlarmVencimientoFase = value;
                    OnPropertyChanged("DiasAntAlarmVencimientoFase");
                }
            }
        }
        private Nullable<short> _diasAntAlarmVencimientoFase;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<FormatosIndice> FormatosIndice
        {
            get
            {
                if (_formatosIndice == null)
                {
                    _formatosIndice = new TrackableCollection<FormatosIndice>();
                    _formatosIndice.CollectionChanged += FixupFormatosIndice;
                }
                return _formatosIndice;
            }
            set
            {
                if (!ReferenceEquals(_formatosIndice, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_formatosIndice != null)
                    {
                        _formatosIndice.CollectionChanged -= FixupFormatosIndice;
                        // This is the principal end in an association that performs cascade deletes.
                        // Remove the cascade delete event handler for any entities in the current collection.
                        foreach (FormatosIndice item in _formatosIndice)
                        {
                            ChangeTracker.ObjectStateChanging -= item.HandleCascadeDelete;
                        }
                    }
                    _formatosIndice = value;
                    if (_formatosIndice != null)
                    {
                        _formatosIndice.CollectionChanged += FixupFormatosIndice;
                        // This is the principal end in an association that performs cascade deletes.
                        // Add the cascade delete event handler for any entities that are already in the new collection.
                        foreach (FormatosIndice item in _formatosIndice)
                        {
                            ChangeTracker.ObjectStateChanging += item.HandleCascadeDelete;
                        }
                    }
                    OnNavigationPropertyChanged("FormatosIndice");
                }
            }
        }
        private TrackableCollection<FormatosIndice> _formatosIndice;

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
            FormatosIndice.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupFormatosIndice(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (FormatosIndice item in e.NewItems)
                {
                    item.Parametros = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("FormatosIndice", item);
                    }
                    // This is the principal end in an association that performs cascade deletes.
                    // Update the event listener to refer to the new dependent.
                    ChangeTracker.ObjectStateChanging += item.HandleCascadeDelete;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (FormatosIndice item in e.OldItems)
                {
                    if (ReferenceEquals(item.Parametros, this))
                    {
                        item.Parametros = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("FormatosIndice", item);
                        // Delete the dependent end of this identifying association. If the current state is Added,
                        // allow the relationship to be changed without causing the dependent to be deleted.
                        if (item.ChangeTracker.State != ObjectState.Added)
                        {
                            item.MarkAsDeleted();
                        }
                    }
                    // This is the principal end in an association that performs cascade deletes.
                    // Remove the previous dependent from the event listener.
                    ChangeTracker.ObjectStateChanging -= item.HandleCascadeDelete;
                }
            }
        }

        #endregion
    }
}
