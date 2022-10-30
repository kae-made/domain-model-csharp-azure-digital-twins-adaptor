// ------------------------------------------------------------------------------
// <auto-generated>
//     This file is generated by tool.
//     Runtime Version : 1.0.0
//  
//     Updates this file cause incorrect behavior 
//     and will be lost when the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Kae.StateMachine;
using Kae.Utility.Logging;
using Kae.DomainModel.Csharp.Framework;
using Kae.DomainModel.Csharp.Framework.Adaptor.ExternalStorage;

namespace ADTTestModel
{
    public partial class DomainClassOODBase : DomainClassOOD
    {
        protected static readonly string className = "OOD";

        public string DomainName { get { return CIMADTTestModelLib.DomainName; }}
        public string ClassName { get { return className; } }

        InstanceRepository instanceRepository;
        protected Logger logger;


        public string GetIdForExternalStorage() {  return attr_OodId; }

        public static DomainClassOODBase CreateInstance(InstanceRepository instanceRepository, Logger logger=null, IList<ChangedState> changedStates=null)
        {
            var newInstance = new DomainClassOODBase(instanceRepository, logger);
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:OOD(OodId={newInstance.Attr_OodId}):create");

            instanceRepository.Add(newInstance);

            if (changedStates !=null) changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Create, Target = newInstance, ChangedProperties = null });

            return newInstance;
        }

        public DomainClassOODBase(InstanceRepository instanceRepository, Logger logger)
        {
            this.instanceRepository = instanceRepository;
            this.logger = logger;
            attr_OodId = Guid.NewGuid().ToString();
        }
        protected string attr_OodId;
        protected bool stateof_OodId = false;

        protected string attr_left_LiefDeviceId;
        protected bool stateof_left_LiefDeviceId = false;

        protected string attr_right_LiefDeviceId;
        protected bool stateof_right_LiefDeviceId = false;

        public string Attr_OodId { get { return attr_OodId; } set { attr_OodId = value; stateof_OodId = true; } }
        public string Attr_left_LiefDeviceId { get { return attr_left_LiefDeviceId; } }
        public string Attr_right_LiefDeviceId { get { return attr_right_LiefDeviceId; } }


        // This method can be used as compare predicattion when calling InstanceRepository's SelectInstances method. 
        public static bool Compare(DomainClassOOD instance, IDictionary<string, object> conditionPropertyValues)
        {
            bool result = true;
            foreach (var propertyName in conditionPropertyValues.Keys)
            {
                switch (propertyName)
                {
                    case "OodId":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_OodId)
                        {
                            result = false;
                        }
                        break;
                    case "left_LiefDeviceId":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_left_LiefDeviceId)
                        {
                            result = false;
                        }
                        break;
                    case "right_LiefDeviceId":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_right_LiefDeviceId)
                        {
                            result = false;
                        }
                        break;
                }
                if (result== false)
                {
                    break;
                }
            }
            return result;
        }
        protected LinkedInstance relR3LDRight;
        // private DomainClassLD relR3LDRight;
        protected LinkedInstance relR3LDLeft;
        // private DomainClassLD relR3LDLeft;
        public bool LinkR3(DomainClassLD oneInstanceRight, DomainClassLD otherInstanceLeft, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR3LDRight == null && relR3LDLeft == null)
            {
                this.attr_left_LiefDeviceId = oneInstanceRight.Attr_LiefDeviceId;
                this.stateof_left_LiefDeviceId = true;
                this.attr_right_LiefDeviceId = otherInstanceLeft.Attr_LiefDeviceId;
                this.stateof_right_LiefDeviceId = true;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:OOD(OodId={this.Attr_OodId}):link[One(LD(LiefDeviceId={oneInstanceRight.Attr_LiefDeviceId})),Other(LD(LiefDeviceId={otherInstanceLeft.Attr_LiefDeviceId}))]");

                result = (LinkedR3OneRight()!=null) && (LinkedR3OtherLeft()!=null);
                if (result)
                {
                    if (changedStates != null)
                    {
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR3LDRight });
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR3LDLeft });
                    }
                }
            }
            return result;
        }
        
        public bool UnlinkR3(DomainClassLD oneInstanceRight, DomainClassLD otherInstanceLeft, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR3LDRight != null && relR3LDLeft != null)
            {
                if ((this.Attr_left_LiefDeviceId==oneInstanceRight.Attr_LiefDeviceId) && (this.Attr_right_LiefDeviceId==otherInstanceLeft.Attr_LiefDeviceId))
                {
                    if (changedStates != null)
                    {
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR3LDRight });
                        changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR3LDLeft });
                    }
        
                    this.attr_left_LiefDeviceId = null;
                    this.stateof_left_LiefDeviceId = true;
                    this.attr_right_LiefDeviceId = null;
                    this.stateof_right_LiefDeviceId = true;
                    relR3LDRight = null;
                    relR3LDLeft = null;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:OOD(OodId={this.Attr_OodId}):unlink[LD(LiefDeviceId={oneInstanceRight.Attr_LiefDeviceId})]");

                    result = true;
                }
            }
            return result;
        }
        
        public DomainClassLD LinkedR3OneRight()
        {
            if (relR3LDRight == null)
            {
                var candidates = instanceRepository.GetDomainInstances("LD").Where(inst=>(this.Attr_left_LiefDeviceId==((DomainClassLD)inst).Attr_LiefDeviceId));
                if (candidates.Count() == 0)
                {
                   if (instanceRepository.ExternalStorageAdaptor != null) candidates = instanceRepository.ExternalStorageAdaptor.CheckTraverseStatus(DomainName, this, "LD", "R3_Right", candidates, () => { return DomainClassLDBase.CreateInstance(instanceRepository, logger); }, "any").Result;
                }
                relR3LDRight = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R3", Phrase = "Right" };
                // (DomainClassLD)candidates.FirstOrDefault();
            }
            return relR3LDRight.GetDestination<DomainClassLD>();
        }
        
        public DomainClassLD LinkedR3OtherLeft()
        {
            if (relR3LDLeft == null)
            {
                var candidates = instanceRepository.GetDomainInstances("LD").Where(inst=>(this.Attr_right_LiefDeviceId==((DomainClassLD)inst).Attr_LiefDeviceId));
                if (candidates.Count() == 0)
                {
                   if (instanceRepository.ExternalStorageAdaptor != null) candidates = instanceRepository.ExternalStorageAdaptor.CheckTraverseStatus(DomainName, this, "LD", "R3_Left", candidates, () => { return DomainClassLDBase.CreateInstance(instanceRepository, logger); }, "any").Result;
                }
                relR3LDLeft = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R3", Phrase = "Left" };
                // (DomainClassLD)candidates.FirstOrDefault();
            }
            return relR3LDLeft.GetDestination<DomainClassLD>();
        }



        
        public bool Validate()
        {
            bool isValid = true;
            if (relR3LDRight == null)
            {
                isValid = false;
            }
            if (relR3LDLeft == null)
            {
                isValid = false;
            }
            return isValid;
        }

        public void DeleteInstance(IList<ChangedState> changedStates=null)
        {
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:OOD(OodId={this.Attr_OodId}):delete");

            changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Delete, Target = this, ChangedProperties = null });

            instanceRepository.Delete(this);
        }

        // methods for storage
        public void Restore(IDictionary<string, object> propertyValues)
        {
            if (propertyValues.ContainsKey("OodId"))
            {
                attr_OodId = (string)propertyValues["OodId"];
            }
            stateof_OodId = false;
            if (propertyValues.ContainsKey("left_LiefDeviceId"))
            {
                attr_left_LiefDeviceId = (string)propertyValues["left_LiefDeviceId"];
            }
            stateof_left_LiefDeviceId = false;
            if (propertyValues.ContainsKey("right_LiefDeviceId"))
            {
                attr_right_LiefDeviceId = (string)propertyValues["right_LiefDeviceId"];
            }
            stateof_right_LiefDeviceId = false;
        }
        
        public IDictionary<string, object> ChangedProperties()
        {
            var results = new Dictionary<string, object>();
            if (stateof_OodId)
            {
                results.Add("OodId", attr_OodId);
                stateof_OodId = false;
            }
            if (stateof_left_LiefDeviceId)
            {
                results.Add("left_LiefDeviceId", attr_left_LiefDeviceId);
                stateof_left_LiefDeviceId = false;
            }
            if (stateof_right_LiefDeviceId)
            {
                results.Add("right_LiefDeviceId", attr_right_LiefDeviceId);
                stateof_right_LiefDeviceId = false;
            }

            return results;
        }

        public string GetIdentities()
        {
            string identities = $"OodId={this.Attr_OodId}";

            return identities;
        }
        
        public IDictionary<string, object> GetProperties(bool onlyIdentity)
        {
            var results = new Dictionary<string, object>();

            results.Add("OodId", attr_OodId);
            if (!onlyIdentity) results.Add("left_LiefDeviceId", attr_left_LiefDeviceId);
            if (!onlyIdentity) results.Add("right_LiefDeviceId", attr_right_LiefDeviceId);

            return results;
        }

#if false
        List<ChangedState> changedStates = new List<ChangedState>();

        public IList<ChangedState> ChangedStates()
        {
            List<ChangedState> results = new List<ChangedState>();
            results.AddRange(changedStates);
            results.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Update, Target = this, ChangedProperties = ChangedProperties() });
            changedStates.Clear();

            return results;
        }
#endif
    }
}
