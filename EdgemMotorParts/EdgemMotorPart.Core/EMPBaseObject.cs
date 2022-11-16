
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System;
using System.ComponentModel;

namespace EdgemMotorPart.Core
{
    [OptimisticLocking(false)]
    [NonPersistent]
    public abstract class EMPBaseObject : XPCustomObject
    {
        #region CONTRUCTORS
        public EMPBaseObject(Session session) : base(session) { }
        #endregion        

        #region LOCAL FIELDS - Static        
        protected bool isSystemObject;
        protected bool isAdminObject;
        public static bool IsXpoProfiling = false;
        #endregion

        #region LOCAL FIELDS
        private XPMemberInfo defaultPropertyMemberInfo;
        private bool isDefaultPropertyAttributeInit = false;
        string modifiedBy;
        DateTime? modifiedOn;
        string createdBy;
        DateTime? createdOn;
        bool isActive;

        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]

        [XafDisplayName("New")]
        public bool IsNewRecord
        {
            get { return Session.IsNewObject(this); }
        }
        #endregion

        #region P R O P E R T I E S - Quick Audits
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "G")]
        public DateTime? CreatedOn
        {
            get => createdOn;
            set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
        }
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False")]
        [Size(64)]
        public string CreatedBy
        {
            get => createdBy;
            set => SetPropertyValue(nameof(CreatedBy), ref createdBy, value);
        }
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "G")]
        public DateTime? ModifiedOn
        {
            get => modifiedOn;
            set => SetPropertyValue(nameof(ModifiedOn), ref modifiedOn, value);
        }
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False")]
        [Size(64)]
        public string ModifiedBy
        {
            get => modifiedBy;
            set => SetPropertyValue(nameof(ModifiedBy), ref modifiedBy, value);
        }
        #endregion

        #region P R O P E R T I E S
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public bool IsActive
        {
            get => isActive;
            set => SetPropertyValue(nameof(IsActive), ref isActive, value);
        }
        /// <summary>
        /// Only Administrators can modify this object.
        /// </summary>
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False")]
        public bool IsAdminObject
        {
            get => isAdminObject;
            set => SetPropertyValue(nameof(IsAdminObject), ref isAdminObject, value);
        }
        /// <summary>
        /// This object is internally used by the system.
        /// This preserves data integrity.
        /// This implements domain-wide standards.
        /// </summary>
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False")]
        public bool IsSystemObject
        {
            get => isSystemObject;
            set => SetPropertyValue(nameof(IsSystemObject), ref isSystemObject, value);
        }
        #endregion

        #region O V E R R I D E S
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
            createdBy = SecuritySystem.CurrentUserName;
            createdOn = DateTime.Now;
        }

        public override string ToString()
        {
            if (!IsXpoProfiling)
            {
                if (!isDefaultPropertyAttributeInit)
                {
                    string defaultPropertyName = string.Empty;
                    XafDefaultPropertyAttribute xafDefaultPropertyAttribute = XafTypesInfo.Instance
                                                                                          .FindTypeInfo(GetType())
                                                                                          .FindAttribute<XafDefaultPropertyAttribute>();
                    if (xafDefaultPropertyAttribute != null)
                    {
                        defaultPropertyName = xafDefaultPropertyAttribute.Name;
                    }
                    else
                    {
                        DefaultPropertyAttribute defaultPropertyAttribute = XafTypesInfo.Instance
                                                                                        .FindTypeInfo(GetType())
                                                                                        .FindAttribute<DefaultPropertyAttribute>();
                        if (defaultPropertyAttribute != null)
                        {
                            defaultPropertyName = defaultPropertyAttribute.Name;
                        }
                    }
                    if (!string.IsNullOrEmpty(defaultPropertyName))
                    {
                        defaultPropertyMemberInfo = ClassInfo.FindMember(defaultPropertyName);
                    }
                    isDefaultPropertyAttributeInit = true;
                }
                if (defaultPropertyMemberInfo != null)
                {
                    try
                    {
                        object obj = defaultPropertyMemberInfo.GetValue(this);
                        if (obj != null)
                        {
                            return obj.ToString();
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return base.ToString();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (Session.IsNewObject(this))
            {
                createdOn = DateTime.Now;
                createdBy = SecuritySystem.CurrentUserName;
            }
            else
            {
                if (Session.IsObjectMarkedDeleted(this))
                    return;

                modifiedOn = DateTime.Now;
                modifiedBy = SecuritySystem.CurrentUserName;
            }
        }
        protected override void OnDeleting()
        {
            base.OnDeleting();
            if (IsSystemObject)
            {
                throw new UserFriendlyException("Unable to delete a SYSTEM object. Please contact your Administrator.");
            }
        }
        #endregion
    }
}
