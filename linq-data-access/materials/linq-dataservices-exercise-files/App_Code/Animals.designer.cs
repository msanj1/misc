﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



public partial class AnimalsDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertAnimal(Animal instance);
  partial void UpdateAnimal(Animal instance);
  partial void DeleteAnimal(Animal instance);
  #endregion
	
	public AnimalsDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public AnimalsDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public AnimalsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public AnimalsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<Animal> Animals
	{
		get
		{
			return this.GetTable<Animal>();
		}
	}
}

[Table(Name="")]
[InheritanceMapping(Code="D", Type=typeof(Dog))]
[InheritanceMapping(Code="C", Type=typeof(Cat), IsDefault=true)]
public abstract partial class Animal : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private string _Name;
	
	private int _ID;
	
	private string _Discriminator;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnDiscriminatorChanging(string value);
    partial void OnDiscriminatorChanged();
    #endregion
	
	public Animal()
	{
		OnCreated();
	}
	
	[Column(Storage="_Name", CanBeNull=false)]
	public string Name
	{
		get
		{
			return this._Name;
		}
		set
		{
			if ((this._Name != value))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._Name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	[Column(Storage="_ID", AutoSync=AutoSync.OnInsert, IsPrimaryKey=true, IsDbGenerated=true)]
	public int ID
	{
		get
		{
			return this._ID;
		}
		set
		{
			if ((this._ID != value))
			{
				this.OnIDChanging(value);
				this.SendPropertyChanging();
				this._ID = value;
				this.SendPropertyChanged("ID");
				this.OnIDChanged();
			}
		}
	}
	
	[Column(Storage="_Discriminator", CanBeNull=false, IsDiscriminator=true)]
	protected string Discriminator
	{
		get
		{
			return this._Discriminator;
		}
		set
		{
			if ((this._Discriminator != value))
			{
				this.OnDiscriminatorChanging(value);
				this.SendPropertyChanging();
				this._Discriminator = value;
				this.SendPropertyChanged("Discriminator");
				this.OnDiscriminatorChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

public partial class Dog : Animal
{
	
	private System.Nullable<bool> _KennelClubMember;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnKennelClubMemberChanging(System.Nullable<bool> value);
    partial void OnKennelClubMemberChanged();
    #endregion
	
	public Dog()
	{
		OnCreated();
	}
	
	[Column(Storage="_KennelClubMember")]
	public System.Nullable<bool> KennelClubMember
	{
		get
		{
			return this._KennelClubMember;
		}
		set
		{
			if ((this._KennelClubMember != value))
			{
				this.OnKennelClubMemberChanging(value);
				this.SendPropertyChanging();
				this._KennelClubMember = value;
				this.SendPropertyChanged("KennelClubMember");
				this.OnKennelClubMemberChanged();
			}
		}
	}
}

public partial class Cat : Animal
{
	
	private System.Nullable<bool> _FelineDistemperShot;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnFelineDistemperShotChanging(System.Nullable<bool> value);
    partial void OnFelineDistemperShotChanged();
    #endregion
	
	public Cat()
	{
		OnCreated();
	}
	
	[Column(Storage="_FelineDistemperShot")]
	public System.Nullable<bool> FelineDistemperShot
	{
		get
		{
			return this._FelineDistemperShot;
		}
		set
		{
			if ((this._FelineDistemperShot != value))
			{
				this.OnFelineDistemperShotChanging(value);
				this.SendPropertyChanging();
				this._FelineDistemperShot = value;
				this.SendPropertyChanged("FelineDistemperShot");
				this.OnFelineDistemperShotChanged();
			}
		}
	}
}
#pragma warning restore 1591
