using System;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;

namespace Neu.MOP.FactorsCalculator
{

    /// <summary>
    /// PluginEntryPoint plug-in.
    /// This is a generic entry point for a plug-in class. Use the Plug-in Registration tool found in the CRM SDK to register this class, import the assembly into CRM, and then create step associations.
    /// A given plug-in can have any number of steps associated with it. 
    /// </summary>    
    public class PluginEntryPoint : PluginBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginEntryPoint"/> class.
        /// </summary>
        /// <param name="unsecure">Contains public (unsecured) configuration information.</param>
        /// <param name="secure">Contains non-public (secured) configuration information. 
        /// When using Microsoft Dynamics CRM for Outlook with Offline Access, 
        /// the secure string is not passed to a plug-in that executes while the client is offline.</param>
        public PluginEntryPoint(string unsecure, string secure)
            : base(typeof(PluginEntryPoint))
        {

            // TODO: Implement your custom configuration handling.
        }


        /// <summary>
        /// Main entry point for he business logic that the plug-in is to execute.
        /// </summary>
        /// <param name="localContext">The <see cref="LocalPluginContext"/> which contains the
        /// <see cref="IPluginExecutionContext"/>,
        /// <see cref="IOrganizationService"/>
        /// and <see cref="ITracingService"/>
        /// </param>
        /// <remarks>
        /// For improved performance, Microsoft Dynamics CRM caches plug-in instances.
        /// The plug-in's Execute method should be written to be stateless as the constructor
        /// is not called for every invocation of the plug-in. Also, multiple system threads
        /// could execute the plug-in at the same time. All per invocation state information
        /// is stored in the context. This means that you should not use global variables in plug-ins.
        /// </remarks>
        protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }
            //check if plugin register on opportunity profile entity
            if(localContext.PluginExecutionContext.PrimaryEntityName == Constant.EntityNameOpportunityProfile)
            {
                Entity entity = null; Guid entityId = Guid.Empty;
                if(localContext.PluginExecutionContext.MessageName.ToLower().Equals(Constant.CreateMessage))
                {
                    entity = localContext.PluginExecutionContext.InputParameters[Constant.Target] as Entity;                    
                }
                if(localContext.PluginExecutionContext.MessageName.ToLower().Equals(Constant.UpdateMessage))
                {
                    entity = localContext.PluginExecutionContext.PostEntityImages[Constant.PostImage];
                }
                string[] opportunityFactors = Constant.OpportunityFactors.Split(',');
                string[] riskFactors =Constant.RiskFactors.Split(',');
                int opportunitScore = 0; int riskScore = 0;
                foreach (var attribute in entity.Attributes)
                {
                    if(Array.IndexOf(opportunityFactors,attribute.Key)>-1)
                    {
                        opportunitScore += ((OptionSetValue)attribute.Value).Value;
                    }
                    if(Array.IndexOf(riskFactors,attribute.Key) > -1)
                    {
                        riskScore += ((OptionSetValue)attribute.Value).Value;
                    }
                }

                //update opportunity factors and risk factors
                Entity opportunityProfile = new Entity(Constant.EntityNameOpportunityProfile);
                opportunityProfile.Attributes.Add(Constant.PrimaryKeyOpportunityProfile, entity.Id);
                opportunityProfile.Attributes.Add(Constant.AttributeOpportunityFactors, opportunitScore);
                opportunityProfile.Attributes.Add(Constant.AttributeRiskFactors, riskScore);
                localContext.OrganizationService.Update(opportunityProfile);                
            }
            // TODO: Implement your custom plug-in business logic.

        }
    }
}
