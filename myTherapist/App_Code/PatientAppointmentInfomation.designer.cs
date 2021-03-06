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



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MyTherapist")]
public partial class PatientAppointmentInfomationDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertPatientAppointmentInformation(PatientAppointmentInformation instance);
  partial void UpdatePatientAppointmentInformation(PatientAppointmentInformation instance);
  partial void DeletePatientAppointmentInformation(PatientAppointmentInformation instance);
  #endregion
	
	public PatientAppointmentInfomationDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["MyTherapistConnectionString2"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public PatientAppointmentInfomationDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public PatientAppointmentInfomationDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public PatientAppointmentInfomationDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public PatientAppointmentInfomationDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<PatientAppointmentInformation> PatientAppointmentInformations
	{
		get
		{
			return this.GetTable<PatientAppointmentInformation>();
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PatientAppointmentInformation")]
public partial class PatientAppointmentInformation : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private System.DateTime _ApptDate;
	
	private long _PatientId;
	
	private string _RLU;
	
	private string _SP;
	
	private string _KD1;
	
	private string _LHT;
	
	private string _LV;
	
	private string _KD2;
	
	private string _TherapyPerformed;
	
	private string _OilsUsed;
	
	private string _SessionGoals;
	
	private string _ImageBeforeTherapy;
	
	private string _ImageAfterTherapy;
	
	private System.Guid _ApptId;
	
	private System.Guid _TherapistId;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnApptDateChanging(System.DateTime value);
    partial void OnApptDateChanged();
    partial void OnPatientIdChanging(long value);
    partial void OnPatientIdChanged();
    partial void OnRLUChanging(string value);
    partial void OnRLUChanged();
    partial void OnSPChanging(string value);
    partial void OnSPChanged();
    partial void OnKD1Changing(string value);
    partial void OnKD1Changed();
    partial void OnLHTChanging(string value);
    partial void OnLHTChanged();
    partial void OnLVChanging(string value);
    partial void OnLVChanged();
    partial void OnKD2Changing(string value);
    partial void OnKD2Changed();
    partial void OnTherapyPerformedChanging(string value);
    partial void OnTherapyPerformedChanged();
    partial void OnOilsUsedChanging(string value);
    partial void OnOilsUsedChanged();
    partial void OnSessionGoalsChanging(string value);
    partial void OnSessionGoalsChanged();
    partial void OnImageBeforeTherapyChanging(string value);
    partial void OnImageBeforeTherapyChanged();
    partial void OnImageAfterTherapyChanging(string value);
    partial void OnImageAfterTherapyChanged();
    partial void OnApptIdChanging(System.Guid value);
    partial void OnApptIdChanged();
    partial void OnTherapistIdChanging(System.Guid value);
    partial void OnTherapistIdChanged();
    #endregion
	
	public PatientAppointmentInformation()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ApptDate", DbType="DateTime NOT NULL")]
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
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PatientId", DbType="BigInt NOT NULL")]
	public long PatientId
	{
		get
		{
			return this._PatientId;
		}
		set
		{
			if ((this._PatientId != value))
			{
				this.OnPatientIdChanging(value);
				this.SendPropertyChanging();
				this._PatientId = value;
				this.SendPropertyChanged("PatientId");
				this.OnPatientIdChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RLU", DbType="NVarChar(50)")]
	public string RLU
	{
		get
		{
			return this._RLU;
		}
		set
		{
			if ((this._RLU != value))
			{
				this.OnRLUChanging(value);
				this.SendPropertyChanging();
				this._RLU = value;
				this.SendPropertyChanged("RLU");
				this.OnRLUChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SP", DbType="NVarChar(50)")]
	public string SP
	{
		get
		{
			return this._SP;
		}
		set
		{
			if ((this._SP != value))
			{
				this.OnSPChanging(value);
				this.SendPropertyChanging();
				this._SP = value;
				this.SendPropertyChanged("SP");
				this.OnSPChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_KD1", DbType="NVarChar(50)")]
	public string KD1
	{
		get
		{
			return this._KD1;
		}
		set
		{
			if ((this._KD1 != value))
			{
				this.OnKD1Changing(value);
				this.SendPropertyChanging();
				this._KD1 = value;
				this.SendPropertyChanged("KD1");
				this.OnKD1Changed();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LHT", DbType="NVarChar(50)")]
	public string LHT
	{
		get
		{
			return this._LHT;
		}
		set
		{
			if ((this._LHT != value))
			{
				this.OnLHTChanging(value);
				this.SendPropertyChanging();
				this._LHT = value;
				this.SendPropertyChanged("LHT");
				this.OnLHTChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LV", DbType="NVarChar(50)")]
	public string LV
	{
		get
		{
			return this._LV;
		}
		set
		{
			if ((this._LV != value))
			{
				this.OnLVChanging(value);
				this.SendPropertyChanging();
				this._LV = value;
				this.SendPropertyChanged("LV");
				this.OnLVChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_KD2", DbType="NVarChar(50)")]
	public string KD2
	{
		get
		{
			return this._KD2;
		}
		set
		{
			if ((this._KD2 != value))
			{
				this.OnKD2Changing(value);
				this.SendPropertyChanging();
				this._KD2 = value;
				this.SendPropertyChanged("KD2");
				this.OnKD2Changed();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TherapyPerformed", DbType="NVarChar(MAX)")]
	public string TherapyPerformed
	{
		get
		{
			return this._TherapyPerformed;
		}
		set
		{
			if ((this._TherapyPerformed != value))
			{
				this.OnTherapyPerformedChanging(value);
				this.SendPropertyChanging();
				this._TherapyPerformed = value;
				this.SendPropertyChanged("TherapyPerformed");
				this.OnTherapyPerformedChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OilsUsed", DbType="NVarChar(MAX)")]
	public string OilsUsed
	{
		get
		{
			return this._OilsUsed;
		}
		set
		{
			if ((this._OilsUsed != value))
			{
				this.OnOilsUsedChanging(value);
				this.SendPropertyChanging();
				this._OilsUsed = value;
				this.SendPropertyChanged("OilsUsed");
				this.OnOilsUsedChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SessionGoals", DbType="NVarChar(MAX)")]
	public string SessionGoals
	{
		get
		{
			return this._SessionGoals;
		}
		set
		{
			if ((this._SessionGoals != value))
			{
				this.OnSessionGoalsChanging(value);
				this.SendPropertyChanging();
				this._SessionGoals = value;
				this.SendPropertyChanged("SessionGoals");
				this.OnSessionGoalsChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ImageBeforeTherapy", DbType="NVarChar(MAX)")]
	public string ImageBeforeTherapy
	{
		get
		{
			return this._ImageBeforeTherapy;
		}
		set
		{
			if ((this._ImageBeforeTherapy != value))
			{
				this.OnImageBeforeTherapyChanging(value);
				this.SendPropertyChanging();
				this._ImageBeforeTherapy = value;
				this.SendPropertyChanged("ImageBeforeTherapy");
				this.OnImageBeforeTherapyChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ImageAfterTherapy", DbType="NVarChar(MAX)")]
	public string ImageAfterTherapy
	{
		get
		{
			return this._ImageAfterTherapy;
		}
		set
		{
			if ((this._ImageAfterTherapy != value))
			{
				this.OnImageAfterTherapyChanging(value);
				this.SendPropertyChanging();
				this._ImageAfterTherapy = value;
				this.SendPropertyChanged("ImageAfterTherapy");
				this.OnImageAfterTherapyChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ApptId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
	public System.Guid ApptId
	{
		get
		{
			return this._ApptId;
		}
		set
		{
			if ((this._ApptId != value))
			{
				this.OnApptIdChanging(value);
				this.SendPropertyChanging();
				this._ApptId = value;
				this.SendPropertyChanged("ApptId");
				this.OnApptIdChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TherapistId", DbType="UniqueIdentifier NOT NULL")]
	public System.Guid TherapistId
	{
		get
		{
			return this._TherapistId;
		}
		set
		{
			if ((this._TherapistId != value))
			{
				this.OnTherapistIdChanging(value);
				this.SendPropertyChanging();
				this._TherapistId = value;
				this.SendPropertyChanged("TherapistId");
				this.OnTherapistIdChanged();
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
