// ------------------------------------------------------------------------------
// <auto-generated>
//     This file is generated by tool.
//     Runtime Version : 1.0.0
//  
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Kae.StateMachine;
using Kae.DomainModel.Csharp.Framework;
using Kae.DomainModel.Csharp.Framework.Adaptor.ExternalStorage;

namespace ADTTestModel
{
    partial class DomainClassLDStateMachine
    {
        protected void ActionWaitForMeasure()
        {
            // Action Description on Model as a reference.



        }

        protected void ActionMeasured()
        {
            // Action Description on Model as a reference.

            //  1 : SELF.MeasureEnvironment();
            //  2 : GENERATE LD2:Measured TO SELF;

            // Line : 1
            target.MeasureEnvironment();
            // Line : 2
            DomainClassLDStateMachine.LD2_Measured.Create(receiver:target, sendNow:true);


        }

    }
}
