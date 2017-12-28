﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="myTherapist")]
public partial class OilsUsedDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertOilsUsed(OilsUsed instance);
  partial void UpdateOilsUsed(OilsUsed instance);
  partial void DeleteOilsUsed(OilsUsed instance);
  #endregion
	
	public OilsUsedDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["myTherapistConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public OilsUsedDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public OilsUsedDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public OilsUsedDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public OilsUsedDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<OilsUsed> OilsUseds
	{
		get
		{
			return this.GetTable<OilsUsed>();
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.OilsUsed")]
public partial class OilsUsed : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private System.DateTime _ApptDate;
	
	private string _OilsUsed1;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnApptDateChanging(System.DateTime value);
    partial void OnApptDateChanged();
    partial void OnOilsUsed1Changing(string value);
    partial void OnOilsUsed1Changed();
    #endregion
	
	public OilsUsed()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ApptDate", DbType="DateTime NOT NULL", IsPrimaryKey=true)]
	public System.DateTime ApptDate
	{
		get
		{
			return this._ApptDate;
		}
		set
		{
			if ((this._ApptDate != value))
			{
				this.OnApptDateChanging(value);
				this.SendPropertyChanging();
				this._ApptDate = value;
				this.SendPropertyChanged("ApptDate");
				this.OnApptDateChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Name="OilsUsed", Storage="_OilsUsed1", DbType="NVarChar(MAX)")]
	public string OilsUsed1
	{
		get
		{
			return this._OilsUsed1;
		}
		set
		{
			if ((this._OilsUsed1 != value))
			{
				this.OnOilsUsed1Changing(value);
				this.SendPropertyChanging();
				this._OilsUsed1 = value;
				this.SendPropertyChanged("OilsUsed1");
				this.OnOilsUsed1Changed();
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
#pragma warning restore 1591