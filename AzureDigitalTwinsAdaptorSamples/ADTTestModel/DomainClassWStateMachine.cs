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

namespace ADTTestModel
{
    public partial class DomainClassWStateMachine : StateMachineBase, ITransition
    {
        public enum Events
        {
            W_1 = 0,     // Create
            W_2 = 1,     // Start
            W_3 = 2    // Done
        }

        public enum States
        {
            _NoState_ = 0,
            Created = 1,
            Working = 2,
            Completed = 3
        }

        private interface IEventArgsTargetIdDef
        {
            public string targetId { get; set; }
        }
        public class W_1_Create : EventData, IEventArgsTargetIdDef
        {
            DomainClassW reciever;

            public W_1_Create(DomainClassW reciever) : base("W_1_Create", (int)Events.W_1)
            {
                this.reciever = reciever;
            }

            public override void Send()
            {
                reciever.TakeEvent(this);
            }

            public string targetId { get; set; }
            public static W_1_Create Create(DomainClassW receiver, string targetId, bool isSelfEvent, bool sendNow, InstanceRepository instanceRepository, Logger logger)
            {
                var newEvent = new W_1_Create(receiver) { targetId = targetId };
                if (receiver == null && instanceRepository != null)
                {
                    receiver = DomainClassWBase.CreateInstance(instanceRepository, logger);
                }
                if (sendNow)
                {
                    receiver.TakeEvent(newEvent);
                }

                return newEvent;
            }

            public override IDictionary<string, object> GetSupplementalData()
            {
                var supplementalData = new Dictionary<string, object>();

                supplementalData.Add("targetId", targetId);

                return supplementalData;
            }
        }

        public class W_2_Start : EventData
        {
            DomainClassW reciever;

            public W_2_Start(DomainClassW reciever) : base("W_2_Start", (int)Events.W_2)
            {
                this.reciever = reciever;
            }

            public override void Send()
            {
                reciever.TakeEvent(this);
            }

            public static W_2_Start Create(DomainClassW receiver, bool isSelfEvent, bool sendNow)
            {
                var newEvent = new W_2_Start(receiver);
                if (receiver != null)
                {
                    if (sendNow)
                    {
                        receiver.TakeEvent(newEvent, isSelfEvent);
                    }
                }
                else
                {
                    if (sendNow)
                    {
                        newEvent = null;
                    }
                }

                return newEvent;
            }

            public override IDictionary<string, object> GetSupplementalData()
            {
                var supplementalData = new Dictionary<string, object>();


                return supplementalData;
            }
        }

        public class W_3_Done : EventData
        {
            DomainClassW reciever;

            public W_3_Done(DomainClassW reciever) : base("W_3_Done", (int)Events.W_3)
            {
                this.reciever = reciever;
            }

            public override void Send()
            {
                reciever.TakeEvent(this);
            }

            public static W_3_Done Create(DomainClassW receiver, bool isSelfEvent, bool sendNow)
            {
                var newEvent = new W_3_Done(receiver);
                if (receiver != null)
                {
                    if (sendNow)
                    {
                        receiver.TakeEvent(newEvent, isSelfEvent);
                    }
                }
                else
                {
                    if (sendNow)
                    {
                        newEvent = null;
                    }
                }

                return newEvent;
            }

            public override IDictionary<string, object> GetSupplementalData()
            {
                var supplementalData = new Dictionary<string, object>();


                return supplementalData;
            }
        }

        protected DomainClassW target;

        protected InstanceRepository instanceRepository;

        protected string DomainName { get { return target.DomainName; } }

        // Constructor
        public DomainClassWStateMachine(DomainClassW target, bool synchronousMode, InstanceRepository instanceRepository, Logger logger) : base(0, synchronousMode, logger)
        {
            this.target = target;
            this.stateTransition = this;
            this.logger = logger;
            this.instanceRepository = instanceRepository;
        }

        protected int[,] stateTransitionTable = new int[3, 3]
            {
                { (int)ITransition.Transition.CantHappen, (int)States.Working, (int)ITransition.Transition.CantHappen }, 
                { (int)ITransition.Transition.CantHappen, (int)ITransition.Transition.CantHappen, (int)States.Completed }, 
                { (int)ITransition.Transition.CantHappen, (int)ITransition.Transition.CantHappen, (int)ITransition.Transition.CantHappen }
            };

        public int GetNextState(int currentState, int eventNumber)
        {
            return stateTransitionTable[currentState, eventNumber];
        }

        private List<ChangedState> changedStates;

        protected override void RunEntryAction(int nextState, EventData eventData)
        {
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:W(WId={target.Attr_WId}):entering[current={CurrentState},event={eventData.EventNumber}");


            changedStates = new List<ChangedState>();

            switch (nextState)
            {
            case (int)States.Created:
                ActionCreated(((IEventArgsTargetIdDef)eventData).targetId);
                break;
            case (int)States.Working:
                ActionWorking();
                break;
            case (int)States.Completed:
                ActionCompleted();
                break;
            }
            if (logger != null) logger.LogInfo($"@{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}:W(WId={target.Attr_WId}):entered[current={CurrentState},event={eventData.EventNumber}");


            instanceRepository.SyncChangedStates(changedStates);
        }
    }
}
