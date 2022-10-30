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
    public partial class DomainClassMEBase : DomainClassME
    {
        protected static readonly string className = "ME";

        public string DomainName { get { return CIMADTTestModelLib.DomainName; }}
        public string ClassName { get { return className; } }

        InstanceRepository instanceRepository;
        protected Logger logger;


        public string GetIdForExternalStorage() {  return attr_MiddleEntityId; }

        public static DomainClassMEBase CreateInstance(InstanceRepository instanceRepository, Logger logger=null, IList<ChangedState> changedStates=null)
        {
            var newInstance = new DomainClassMEBase(instanceRepository, logger);
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:ME(MiddleEntityId={newInstance.Attr_MiddleEntityId}):create");

            instanceRepository.Add(newInstance);

            if (changedStates !=null) changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Create, Target = newInstance, ChangedProperties = null });

            return newInstance;
        }

        public DomainClassMEBase(InstanceRepository instanceRepository, Logger logger)
        {
            this.instanceRepository = instanceRepository;
            this.logger = logger;
            attr_MiddleEntityId = Guid.NewGuid().ToString();
        }
        protected string attr_MiddleEntityId;
        protected bool stateof_MiddleEntityId = false;

        protected string attr_TopEntityId;
        protected bool stateof_TopEntityId = false;

        protected bool attr_Comfortable;
        protected bool stateof_Comfortable = false;

        protected int attr_PreferredTemperature;
        protected bool stateof_PreferredTemperature = false;

        protected int attr_PreferredHumidity;
        protected bool stateof_PreferredHumidity = false;

        public string Attr_MiddleEntityId { get { return attr_MiddleEntityId; } set { attr_MiddleEntityId = value; stateof_MiddleEntityId = true; } }
        public string Attr_TopEntityId { get { return attr_TopEntityId; } }
        public bool Attr_Comfortable { get { return attr_Comfortable; } set { attr_Comfortable = value; stateof_Comfortable = true; } }
        public int Attr_PreferredTemperature { get { return attr_PreferredTemperature; } set { attr_PreferredTemperature = value; stateof_PreferredTemperature = true; } }
        public int Attr_PreferredHumidity { get { return attr_PreferredHumidity; } set { attr_PreferredHumidity = value; stateof_PreferredHumidity = true; } }


        // This method can be used as compare predicattion when calling InstanceRepository's SelectInstances method. 
        public static bool Compare(DomainClassME instance, IDictionary<string, object> conditionPropertyValues)
        {
            bool result = true;
            foreach (var propertyName in conditionPropertyValues.Keys)
            {
                switch (propertyName)
                {
                    case "MiddleEntityId":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_MiddleEntityId)
                        {
                            result = false;
                        }
                        break;
                    case "TopEntityId":
                        if ((string)conditionPropertyValues[propertyName] != instance.Attr_TopEntityId)
                        {
                            result = false;
                        }
                        break;
                    case "Comfortable":
                        if ((bool)conditionPropertyValues[propertyName] != instance.Attr_Comfortable)
                        {
                            result = false;
                        }
                        break;
                    case "PreferredTemperature":
                        if ((int)conditionPropertyValues[propertyName] != instance.Attr_PreferredTemperature)
                        {
                            result = false;
                        }
                        break;
                    case "PreferredHumidity":
                        if ((int)conditionPropertyValues[propertyName] != instance.Attr_PreferredHumidity)
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
        protected LinkedInstance relR1TE;
        public DomainClassTE LinkedR1()
        {
            if (relR1TE == null)
            {
                var candidates = instanceRepository.GetDomainInstances("TE").Where(inst=>(this.Attr_TopEntityId==((DomainClassTE)inst).Attr_TopEntityId));
                if (candidates.Count() == 0)
                {
                   if (instanceRepository.ExternalStorageAdaptor != null) candidates = instanceRepository.ExternalStorageAdaptor.CheckTraverseStatus(DomainName, this, "TE", "R1", candidates, () => { return DomainClassTEBase.CreateInstance(instanceRepository, logger); }, "any").Result;
                }
                relR1TE = new LinkedInstance() { Source = this, Destination = candidates.FirstOrDefault(), RelationshipID = "R1", Phrase = "" };

            }
            return relR1TE.GetDestination<DomainClassTE>();
        }

        public bool LinkR1(DomainClassTE instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR1TE == null)
            {
                this.attr_TopEntityId = instance.Attr_TopEntityId;
                this.stateof_TopEntityId = true;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:ME(MiddleEntityId={this.Attr_MiddleEntityId}):link[TE(TopEntityId={instance.Attr_TopEntityId})]");

                result = (LinkedR1()!=null);
                if (result)
                {
                    if(changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Create, Target = relR1TE });
                }
            }
            return result;
        }

        public bool UnlinkR1(DomainClassTE instance, IList<ChangedState> changedStates=null)
        {
            bool result = false;
            if (relR1TE != null && ( this.Attr_TopEntityId==instance.Attr_TopEntityId ))
            {
                if (changedStates != null) changedStates.Add(new CLinkChangedState() { OP = ChangedState.Operation.Delete, Target = relR1TE });
        
                this.attr_TopEntityId = null;
                this.stateof_TopEntityId = true;
                relR1TE = null;

                if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:ME(MiddleEntityId={this.Attr_MiddleEntityId}):unlink[TE(TopEntityId={instance.Attr_TopEntityId})]");


                result = true;
            }
            return result;
        }

        public IEnumerable<DomainClassLD> LinkedR2Measurement()
        {
            var result = new List<DomainClassLD>();
            var candidates = instanceRepository.GetDomainInstances("LD").Where(inst=>(this.Attr_MiddleEntityId==((DomainClassLD)inst).Attr_MiddleEntityId));
            if (instanceRepository.ExternalStorageAdaptor != null) candidates = instanceRepository.ExternalStorageAdaptor.CheckTraverseStatus(DomainName, this, "LD", "R2", candidates, () => { return DomainClassLDBase.CreateInstance(instanceRepository, logger); }, "many").Result;
            foreach (var c in candidates)
            {
                ((DomainClassLD)c).LinkedR2();
                result.Add((DomainClassLD)c);
            }
            return result;
        }

        public DomainClassMML LinkedR5OtherLief()
        {
            var candidates = instanceRepository.GetDomainInstances("MML").Where(inst=>(this.Attr_MiddleEntityId==((DomainClassMML)inst).Attr_MiddleEntityId));
            if (candidates.Count() == 0)
            {
                if (instanceRepository.ExternalStorageAdaptor != null) candidates = instanceRepository.ExternalStorageAdaptor.CheckTraverseStatus(DomainName, this, "MML", "R5_Lief", candidates, () => { return DomainClassMMLBase.CreateInstance(instanceRepository, logger); }, "any").Result;
                if (candidates.Count() > 0) ((DomainClassMML)candidates.FirstOrDefault()).LinkedR5OneMiddle();
            }
            return (DomainClassMML)candidates.FirstOrDefault();
        }



        
        public bool Validate()
        {
            bool isValid = true;
            if (relR1TE == null)
            {
                isValid = false;
            }
            return isValid;
        }

        public void DeleteInstance(IList<ChangedState> changedStates=null)
        {
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:ME(MiddleEntityId={this.Attr_MiddleEntityId}):delete");

            changedStates.Add(new CInstanceChagedState() { OP = ChangedState.Operation.Delete, Target = this, ChangedProperties = null });

            instanceRepository.Delete(this);
        }

        // methods for storage
        public void Restore(IDictionary<string, object> propertyValues)
        {
            if (propertyValues.ContainsKey("MiddleEntityId"))
            {
                attr_MiddleEntityId = (string)propertyValues["MiddleEntityId"];
            }
            stateof_MiddleEntityId = false;
            if (propertyValues.ContainsKey("TopEntityId"))
            {
                attr_TopEntityId = (string)propertyValues["TopEntityId"];
            }
            stateof_TopEntityId = false;
            if (propertyValues.ContainsKey("Comfortable"))
            {
                attr_Comfortable = (bool)propertyValues["Comfortable"];
            }
            stateof_Comfortable = false;
            if (propertyValues.ContainsKey("PreferredTemperature"))
            {
                attr_PreferredTemperature = (int)propertyValues["PreferredTemperature"];
            }
            stateof_PreferredTemperature = false;
            if (propertyValues.ContainsKey("PreferredHumidity"))
            {
                attr_PreferredHumidity = (int)propertyValues["PreferredHumidity"];
            }
            stateof_PreferredHumidity = false;
        }
        
        public IDictionary<string, object> ChangedProperties()
        {
            var results = new Dictionary<string, object>();
            if (stateof_MiddleEntityId)
            {
                results.Add("MiddleEntityId", attr_MiddleEntityId);
                stateof_MiddleEntityId = false;
            }
            if (stateof_TopEntityId)
            {
                results.Add("TopEntityId", attr_TopEntityId);
                stateof_TopEntityId = false;
            }
            if (stateof_Comfortable)
            {
                results.Add("Comfortable", attr_Comfortable);
                stateof_Comfortable = false;
            }
            if (stateof_PreferredTemperature)
            {
                results.Add("PreferredTemperature", attr_PreferredTemperature);
                stateof_PreferredTemperature = false;
            }
            if (stateof_PreferredHumidity)
            {
                results.Add("PreferredHumidity", attr_PreferredHumidity);
                stateof_PreferredHumidity = false;
            }

            return results;
        }

        public string GetIdentities()
        {
            string identities = $"MiddleEntityId={this.Attr_MiddleEntityId}";

            return identities;
        }
        
        public IDictionary<string, object> GetProperties(bool onlyIdentity)
        {
            var results = new Dictionary<string, object>();

            results.Add("MiddleEntityId", attr_MiddleEntityId);
            if (!onlyIdentity) results.Add("TopEntityId", attr_TopEntityId);
            if (!onlyIdentity) results.Add("Comfortable", attr_Comfortable);
            if (!onlyIdentity) results.Add("PreferredTemperature", attr_PreferredTemperature);
            if (!onlyIdentity) results.Add("PreferredHumidity", attr_PreferredHumidity);

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
